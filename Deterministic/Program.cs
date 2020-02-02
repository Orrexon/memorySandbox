using System;

namespace Deterministic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            unsafe
            {
                DeterministicBase b = new DeterministicBase();
                int mem = b.Heap(32);
                b.DataPtr->test = 55;
                Console.WriteLine("alloc: " + (UInt32)mem);
                int mem2 = b.FreeHeap(b.MemoryAddress);
                Console.WriteLine("free mem: " + (UInt32)mem2);
                b.AccessMemory(b.MemoryAddress);
                b.Dispose();
            }
        }
    }
}
