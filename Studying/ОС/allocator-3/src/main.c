#include <stdint.h>
#include <stdio.h>

#include "allocator.h"
#include "block.h"

struct T {
    void *ptr;
    size_t size;
    unsigned int checksum;
};

static void
buf_fill(unsigned char *c, size_t size)
{
    while (size--)
        *c++ = (unsigned char)rand();
}

static unsigned int
buf_checksum(unsigned char *c, size_t size)
{
    unsigned int checksum = 0;

    while (size--)
        checksum = (checksum << 5) ^ (checksum >> 3) ^ *c++;
    return checksum;
}

static void *
buf_alloc(size_t size)
{
    void *ptr;

    ptr = mem_alloc(size);
    if (ptr != NULL)
        buf_fill(ptr, size);
    return ptr;
}

static void
tester(void)
{
    const size_t SZ_MAX = 10000;
    const size_t SZ_MIN = 10;
    const unsigned long M = 1000;
    const size_t N = 10;
    struct T t[N];
    void *ptr;
    size_t idx, size, size_min;
    unsigned int checksum;

    for (idx = 0; idx < N; ++idx)
        t[idx].ptr = NULL;
    for (unsigned long i = 0; i < M; ++i) {
        mem_show("----------------------------------------------");
        for (idx = 0; idx < N; ++idx)
            if (t[idx].ptr != NULL) {
                if (t[idx].checksum != buf_checksum(t[idx].ptr, t[idx].size)) {
                    printf("1. Checksum check failed at [%p]\n", t[idx].ptr);
                    return;
                }
            }
        idx = (size_t)rand() % N;
        if (t[idx].ptr == NULL) {
            size = ((size_t)rand() % (SZ_MAX - SZ_MIN)) + SZ_MIN;
            ptr = buf_alloc(size);
            if (ptr != NULL) {
                t[idx].ptr = ptr;
                t[idx].size = size;
                t[idx].checksum = buf_checksum(ptr, size);
            }
        } else {
            if (rand() & 1) {
                size = ((size_t)rand() % (SZ_MAX - SZ_MIN)) + SZ_MIN;
                size_min = size < t[idx].size ? size : t[idx].size;
                checksum = buf_checksum(t[idx].ptr, size_min);
                ptr = mem_realloc(t[idx].ptr, size);
                if (ptr != NULL) {
                    if (checksum != buf_checksum(ptr, size_min)) {
                        printf("2. Checksum check failed at [%p]\n", ptr);
                        return;
                    }
                    t[idx].ptr = ptr;
                    t[idx].size = size;
                    t[idx].checksum = buf_checksum(ptr, size);
                }
            } else {
                mem_free(t[idx].ptr);
                t[idx].ptr = NULL;
            }
        }
    }
    for (size_t idx = 0; idx < N; ++idx) {
        if (t[idx].ptr != NULL) {
            mem_show("----------------------------------------------");
            if (t[idx].checksum != buf_checksum(t[idx].ptr, t[idx].size)) {
                printf("3. Checksum check failed at [%p]\n", t[idx].ptr);
                return;
            }
            mem_free(t[idx].ptr);
            t[idx].ptr = NULL;
        }
    }
    mem_show("----------------------------------------------");
}

int
main(void)
{
    tester();
}
