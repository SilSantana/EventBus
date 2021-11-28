using System;
using System.Collections.Generic;

namespace Demo.EntitiesDto.Resources.Helpers
{
    public class GenericComparerHelper<T> : IEqualityComparer<T>
    {

        public Func<T, T, bool> MethodEquals { get; }
        public Func<T, int> MethodHashCode { get; }


        public GenericComparerHelper(
            Func<T, T, bool> methodEquals,
            Func<T, int> methodHashCode
            )
        {
            this.MethodEquals = methodEquals;
            this.MethodHashCode = methodHashCode;
        }


        public static GenericComparerHelper<T> Create(
            Func<T, T, bool> methodEquals,
            Func<T, int> methodHashCode) => new GenericComparerHelper<T>(methodEquals, methodHashCode);      


        public bool Equals(T x, T y) => MethodEquals(x,y);

        public int GetHashCode(T obj) => MethodHashCode(obj);
       
    }
}
