using System;
using System.Collections.Generic;

namespace LewordVocab.Utils
{
    public static class CollectionExtensions
    {
        public static int IndexOf<T>(this T[] inItems, Predicate<T> inCondition)
        {
            for (int i = 0; i < inItems.Length; ++i) {
                if (inCondition(inItems[i])) {
                    return i;
                }
            }
            return -1;
        }

        public static int IndexOf<T>(this T[] inItems, T inItem, IEqualityComparer<T> inComparer)
            => inItems.IndexOf(item => inComparer.Equals(item, inItem));

        public static int IndexOf<T>(this T[] inItems, T inItem)
            => inItems.IndexOf(inItem, EqualityComparer<T>.Default);
    }
}
