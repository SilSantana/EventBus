using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.EntitiesDto.Resources.Helpers
{
    public static class DistinctExtensions
    {
        public static IEnumerable<TSource> Distinct<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, TSource, bool> methodEquals,
            Func<TSource, int> methodmethodHashCode) => source.Distinct(
                GenericComparerHelper<TSource>.Create(methodEquals, methodmethodHashCode)                
                );

    }
}
