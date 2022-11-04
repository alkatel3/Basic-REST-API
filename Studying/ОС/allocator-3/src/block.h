#include <stdbool.h>
#include <stdlib.h>

#include "allocator_impl.h"

// This will work if ALIGN > 2
#define BLOCK_FLAG_BUSY (size_t)0x1
#define BLOCK_FLAG_FIRST (size_t)0x2
#define BLOCK_FLAG_LAST (size_t)0x1
#define BLOCK_SIZE_CURR_MASK ~(BLOCK_FLAG_BUSY|BLOCK_FLAG_FIRST)
#define BLOCK_SIZE_PREV_MASK ~(BLOCK_FLAG_LAST)

struct block {
    size_t size_curr; // current size, flag busy, flag first
    size_t size_prev; // previous size, flag last
};

#define BLOCK_STRUCT_SIZE ROUND_BYTES(sizeof(struct block))

void block_split(struct block *, size_t);
void block_merge(struct block *, struct block *);

static inline void *
block_to_payload(const struct block *block)
{
    return (char *)block + BLOCK_STRUCT_SIZE;
}

static inline struct block *
payload_to_block(const void *ptr)
{
    return (struct block *)((char *)ptr - BLOCK_STRUCT_SIZE);
}

static inline size_t
block_get_size_curr(const struct block *block)
{
    return block->size_curr & BLOCK_SIZE_CURR_MASK;
}

static inline void
block_set_size_curr(struct block *block, size_t size)
{
    block->size_curr = size | (block->size_curr & ~BLOCK_SIZE_CURR_MASK);
}

static inline size_t
block_get_size_prev(const struct block *block)
{
    return block->size_prev & BLOCK_SIZE_PREV_MASK;
}

static inline void
block_set_size_prev(struct block *block, size_t size)
{
    block->size_prev = size | (block->size_prev & ~BLOCK_SIZE_PREV_MASK);
}

static inline bool
block_get_flag_busy(const struct block *block)
{
    return block->size_curr & BLOCK_FLAG_BUSY;
}

static inline void
block_clr_flag_busy(struct block *block)
{
    block->size_curr &= ~BLOCK_FLAG_BUSY;
}

static inline void
block_set_flag_busy(struct block *block)
{
    block->size_curr |= BLOCK_FLAG_BUSY;
}

static inline bool
block_get_flag_first(const struct block *block)
{
    return block->size_curr & BLOCK_FLAG_FIRST;
}

static inline void
block_set_flag_first(struct block *block)
{
    block->size_curr |= BLOCK_FLAG_FIRST;
}

static inline bool
block_get_flag_last(const struct block *block)
{
    return block->size_prev & BLOCK_FLAG_LAST;
}

static inline void
block_set_flag_last(struct block *block)
{
    block->size_prev |= BLOCK_FLAG_LAST;
}

static inline void
block_clr_flag_last(struct block *block)
{
    block->size_prev &= ~BLOCK_FLAG_LAST;
}

static inline struct block *
block_next(const struct block *block)
{
    return (struct block *)
        ((char *)block + BLOCK_STRUCT_SIZE + block_get_size_curr(block));
}

static inline struct block *
block_prev(const struct block *block)
{
    return (struct block *)
        ((char *)block - BLOCK_STRUCT_SIZE - block_get_size_prev(block));
}

static inline void
block_init(struct block *block)
{
    block->size_curr = 0;
    block->size_prev = 0;
}

static inline void
arena_init(struct block *block, size_t size)
{
    block->size_curr = size;
    block->size_prev = 0;
    block_set_flag_first(block);
    block_set_flag_last(block);
}
















