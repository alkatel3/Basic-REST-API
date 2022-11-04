#include <assert.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "allocator.h"
#include "block.h"
#include "config.h"
#include "kernel.h"

#if ALLOCATOR_PAGE_SIZE > SIZE_MAX / ALLOCATOR_PAGE_SIZE
# undef ALLOCATOR_PAGE_SIZE
# define ALLOCATOR_PAGE_SIZE (SIZE_MAX / ALLOCATOR_PAGE_SIZE)
#endif
#define ARENA_SIZE ((size_t)ALLOCATOR_PAGE_SIZE * ALLOCATOR_ARENA_PAGES)

#define ARENA_BLOCK_SIZE_MAX (ARENA_SIZE - 2 * BLOCK_STRUCT_SIZE)

static struct block *arena = NULL;

static int
arena_alloc(void)
{
    arena = kernel_mem_alloc(ARENA_SIZE);
    if (arena == NULL)
        return -1;
    arena_init(arena, ARENA_SIZE - BLOCK_STRUCT_SIZE);
    return 0;
}

void *
mem_alloc(size_t size)
{
    struct block *block;

    if (arena == NULL)
        if (arena_alloc() < 0)
            return NULL;
    if (size > ARENA_BLOCK_SIZE_MAX)
        return NULL;
    size = ROUND_BYTES(size);
    for (block = arena;; block = block_next(block)) {
        if (!block_get_flag_busy(block) && block_get_size_curr(block) >= size) {
            block_split(block, size);
            return block_to_payload(block);
        }
        if (block_get_flag_last(block))
            break;
    }
    return NULL;
}

void *
mem_realloc(void *ptr, size_t size)
{
    assert(block_get_flag_busy(payload_to_block(ptr)));

    struct block *block, *block_r, *block_l;
    void *ptr_new;
    size_t size_curr, size_curr_l, size_curr_r;

    if (ptr == NULL)
        return mem_alloc(size);
    if (size > ARENA_BLOCK_SIZE_MAX)
        return NULL;
    size = ROUND_BYTES(size);
    block = payload_to_block(ptr);
    size_curr = block_get_size_curr(block);
    if (size_curr == size)
        return ptr;
    if (!block_get_flag_last(block)) {
        block_r = block_next(block);
        if (block_get_flag_busy(block_r))
            block_r = NULL;
    } else {
        block_r = NULL;
    }
    size_curr_r = block_r != NULL ?
        (block_get_size_curr(block_r) + BLOCK_STRUCT_SIZE) : 0;
    if (size < size_curr || size <= size_curr + size_curr_r) {
        // in-place reallocation.
        if (block_r != NULL)
            block_merge(block, block_r);
        block_split(block, size);
        return ptr;
    } else if (!block_get_flag_first(block)) {
        // try to use left and right blocks.
        block_l = block_prev(block);
        if (!block_get_flag_busy(block_l)) {
            size_curr_l = block_get_size_curr(block_l) + BLOCK_STRUCT_SIZE;
            if (size <= size_curr + size_curr_l + size_curr_r) {
                block_set_flag_busy(block_l);
                block_merge(block_l, block);
                if (block_r != NULL)
                    block_merge(block_l, block_r);
                ptr_new = block_to_payload(block_l);
                memmove(ptr_new, ptr, size_curr);
                block_split(block_l, size);
                return ptr_new;
            }
        }
    }
    // true reallocation.
    ptr_new = mem_alloc(size);
    if (ptr_new != NULL) {
        memcpy(ptr_new, ptr, size_curr);
        mem_free(ptr);
        return ptr_new;
    }
    return NULL;
}

void
mem_free(void *ptr)
{
    assert(block_get_flag_busy(payload_to_block(ptr)));

    struct block *block, *block_r, *block_l;

    if (ptr == NULL)
        return;
    block = payload_to_block(ptr);
    block_clr_flag_busy(block);
    if (!block_get_flag_last(block)) {
        block_r = block_next(block);
        if (!block_get_flag_busy(block_r))
            block_merge(block, block_r);
    }
    if (!block_get_flag_first(block)) {
        block_l = block_prev(block);
        if (!block_get_flag_busy(block_l))
            block_merge(block_l, block);
    }
}

void
mem_show(const char *msg)
{
    const struct block *block;

    printf("%s:\n", msg);
    if (arena == NULL) {
        printf("Arena was not initialized\n");
        return;
    }
    for (block = arena;; block = block_next(block)) {
        printf("[%p] %s %10zu %10zu%s%s\n",
            block_to_payload(block),
            block_get_flag_busy(block) ? "busy" : "free",
            block_get_size_curr(block), block_get_size_prev(block),
            block_get_flag_first(block) ? " first" : "",
            block_get_flag_last(block) ? " last" : "");
        if (block_get_flag_last(block))
            break;
    }
}
