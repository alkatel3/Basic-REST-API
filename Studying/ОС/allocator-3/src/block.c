#include <assert.h>

#include "block.h"

void
block_split(struct block *block1, size_t size)
{
    assert(block_get_size_curr(block1) >= size);

    struct block *block2;
    size_t size_curr;

    block_set_flag_busy(block1);
    size_curr = block_get_size_curr(block1);
    if (size_curr >= size + BLOCK_STRUCT_SIZE) {
        block_set_size_curr(block1, size);
        size_curr -= size + BLOCK_STRUCT_SIZE;
        block2 = block_next(block1);
        block_init(block2);
        block_set_size_curr(block2, size_curr);
        block_set_size_prev(block2, size);
        if (block_get_flag_last(block1)) {
            block_clr_flag_last(block1);
            block_set_flag_last(block2);
        } else {
            block_set_size_prev(block_next(block2), size_curr);
        }
    }
}

void
block_merge(struct block *block1, struct block *block2)
{
    assert(block_next(block1) == block2);

    size_t size_curr;

    size_curr = block_get_size_curr(block1) + BLOCK_STRUCT_SIZE +
        block_get_size_curr(block2);
    block_set_size_curr(block1, size_curr);
    if (block_get_flag_last(block2))
        block_set_flag_last(block1);
    else
        block_set_size_prev(block_next(block1), size_curr);
}















