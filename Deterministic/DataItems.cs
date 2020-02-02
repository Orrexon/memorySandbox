using System;
using System.Collections.Generic;
using System.Text;

namespace Deterministic
{
    public class DataItems
    {
    }
    public struct Data<T> where T : unmanaged
    {
        public T test;
    }
}
