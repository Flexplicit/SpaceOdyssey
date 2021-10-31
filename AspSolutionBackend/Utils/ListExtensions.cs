using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class ListExtensions
    {
        public static void RemoveRangeWithoutSize<T>(this List<T> source, int start, int? size = null)
        {
            if (start == -1)
            {
                source.RemoveAt(source.Count - 1);
                return;
            }

            var amountToDelete = size ?? source.Count - start;
            source.RemoveRange(start, amountToDelete);
        }

        //https://stackoverflow.com/questions/3683105/calculate-difference-from-previous-item-with-linq/3683217#3683217
        public static IEnumerable<TResult> SelectWithPrevious<TSource, TResult>
        (this IEnumerable<TSource> source,
            Func<TSource, TSource, TResult> projection)
        {
            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    yield break;
                }

                TSource previous = iterator.Current;
                while (iterator.MoveNext())
                {
                    yield return projection(previous, iterator.Current);
                    previous = iterator.Current;
                }
            }
        }
    }
}