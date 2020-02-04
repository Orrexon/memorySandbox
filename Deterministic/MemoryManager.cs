using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Deterministic
{
    public unsafe class MemoryManager
    {
        private const int HEAP_ZERO_MEMORY = 0x00000008;
        public IntPtr heapPointer;
        void*[] pointers = new void*[1];
        int listSize = 0;

        public void* Heap(int size)
        {
            void* result = HeapAlloc(heapPointer, HEAP_ZERO_MEMORY, (UIntPtr)size);
            if (result == null) throw new OutOfMemoryException();
            pointers[listSize++] = result;
            return result;
        }
        public int FreeHeap(void* pointer)
        {
            return FreeHeap((int)pointer);
        }
        public int FreeHeap(int memoryAddr)
        {
            int index = GetListIndexFromMemAddr(memoryAddr);
            if (!(index > -1))
            {
                throw new Exception("Index not found, memory address does not exist");
            }
            if (HeapFree(heapPointer, 0, pointers[index]))
            {
                pointers[index] = null;
                listSize--;
                return memoryAddr;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public int GetListIndexFromMemAddr(int mem)
        {
            for (int i = 0; i < listSize; i++)
            {
                if (mem == (int)pointers[i])
                {
                    return i;
                }
            }
            return -1;
        }
        // Heap API functions
        [DllImport("kernel32")]
        private static extern IntPtr GetProcessHeap();

        [DllImport("kernel32")]
        private static extern void* HeapAlloc(IntPtr hHeap, int flags, UIntPtr size);

        [DllImport("kernel32")]
        private static extern bool HeapFree(IntPtr hHeap, int flags, void* block);

        [DllImport("kernel32")]
        private static extern void* HeapReAlloc(IntPtr hHeap, int flags, void* block, UIntPtr size);

        [DllImport("kernel32")]
        private static extern UIntPtr HeapSize(IntPtr hHeap, int flags, void* block);
    }
}
