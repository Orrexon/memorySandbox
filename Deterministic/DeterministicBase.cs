using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Deterministic
{
    
    public unsafe class DeterministicBase : IDisposable
    {
        public int MemoryAddress { get; private set; }
        public MemoryManager memMgr;
        public Data<int>* DataPtr;

        public DeterministicBase()
        {
            MemoryAddress = 0;
            memMgr = new MemoryManager();
        }

        bool isDisposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed) // only dispose once!
            {
                if (disposing)
                {
                    Console.WriteLine("Not in destructor, OK to reference other objects");
                }
                // perform cleanup for this object
                Console.WriteLine("Disposing...");
            }
            isDisposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~DeterministicBase()
        {
            Dispose(false);
        }
    }
}
