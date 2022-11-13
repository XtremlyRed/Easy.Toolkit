using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Easy.Toolkit
{
    /// <summary>
    /// Collection Extensions
    /// </summary>
    public static class EnumerableExtensions
    {

        /// <summary>
        ///  array is  null  or empty
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<Target>(this Target[] array)
        {
            return array is null || array.Length == 0;
        }

        /// <summary>
        ///  collection is  null  or empty
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<Target>(this ICollection<Target> collection)
        {
            return collection is null || collection.Count == 0;
        }


        /// <summary>
        ///  collection is  null  or empty
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <param name="firstInvoke"></param>
        /// <returns></returns>
        public static TSource FindFirst<TSource>(this IEnumerable<TSource> collection, Func<TSource, bool> predicate, Action<TSource> firstInvoke)
        {
            if (collection is null)
            {
                return default;
            }

            TSource first = predicate is null ? collection.FirstOrDefault() : collection.FirstOrDefault(predicate);

            firstInvoke?.Invoke(first);

            return first;
        }
  
        /// <summary>
        /// forEach   collection 
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="collection">collection</param>
        /// <param name="forEachBody">forEachBody</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ForEach<Target>(this IEnumerable<Target> collection, Action<Target> forEachBody)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (forEachBody is null)
            {
                throw new ArgumentNullException(nameof(forEachBody));
            }

            foreach (Target item in collection)
            {
                forEachBody(item);
            }
        }

        /// <summary>
        /// loop collection{<typeparamref name="Target"/>} by forEachBody and element Index
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="collection">collection</param>
        /// <param name="forEachBody">forEachBody</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ForEach<Target>(this IEnumerable<Target> collection, Action<Target, int> forEachBody)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (forEachBody is null)
            {
                throw new ArgumentNullException(nameof(forEachBody));
            }

            int index = 0;

            foreach (Target item in collection)
            {
                forEachBody(item, index);
                index++;
            }
        }

        /// <summary>
        /// to read only list
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IReadOnlyList<Target> ToReadOnlyList<Target>(this IEnumerable<Target> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            ReadOnlyList<Target> collection2 = new(collection);

            IReadOnlyList<Target> c2 = collection2;

            return c2;
        }

        /// <summary>
        /// to read dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            ReadOnlyDictionary<TKey, TValue> list = new(collection);
            IReadOnlyDictionary<TKey, TValue> newList = list;

            return newList;
        }

        /// <summary>
        /// to read dictionary
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="collection"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TObject, TKey, TValue>(this IEnumerable<TObject> collection, Func<TObject, TKey> keySelector, Func<TObject, TValue> valueSelector)

        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            if (valueSelector is null)
            {
                throw new ArgumentNullException(nameof(valueSelector));
            }

            ReadOnlyDictionary<TKey, TValue> dict = new();

            foreach (TObject item in collection)
            {
                TKey key = keySelector(item);

                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (dict.TryGetValue(key, out TValue _))
                {
                    throw new Exception($"{nameof(key)}:{key} already exists");
                }

                TValue value = valueSelector(item);

                dict[key] = value;
            }

            return dict;
        }


        /// <summary>
        /// collection to  <see cref="ObservableCollection{Target}"/>
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ObservableCollection<Target> ToObservableCollection<Target>(this IEnumerable<Target> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            ObservableCollection<Target> observableCollection = new(collection);

            return observableCollection;
        }

        /// <summary>
        /// add items into collection
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="collection">collection</param>
        /// <param name="items">items</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ICollection<Target> AddItems<Target>(this ICollection<Target> collection, IEnumerable<Target> items)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (items is null)
            {
                return collection;
            }

            foreach (Target item in items)
            {
                collection.Add(item);
            }

            return collection;
        }

        /// <summary>
        /// add items into collection
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="collection">collection</param>
        /// <param name="items">items array</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ICollection<Target> AddItems<Target>(this ICollection<Target> collection, params Target[] items)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (items is null || items.Length == 0)
            {
                return collection;
            }

            foreach (Target item in items)
            {
                collection.Add(item);
            }

            return collection;
        }

        /// <summary>
        /// Max items OF collection
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <typeparam name="TComparable"></typeparam>
        /// <param name="collection">collection</param>
        /// <param name="maxComparer"></param> 
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Target MaxBy<Target, TComparable>(this IEnumerable<Target> collection, Func<Target, TComparable> maxComparer)
            where TComparable : IComparable<TComparable>
            where Target : notnull
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Any() == false)
            {
                throw new ArgumentException($"{nameof(collection)} is Empty");
            }

            if (maxComparer == null)
            {
                throw new ArgumentNullException(nameof(maxComparer));
            }

            Target first = collection.First();
            TComparable firstCompa = maxComparer(first);
            foreach (Target item in collection)
            {
                TComparable current = maxComparer(item);

                int com = current.CompareTo(firstCompa);
                if (com > 0)
                {
                    firstCompa = current;
                    first = item;
                }
            }

            return first;
        }



        /// <summary>
        /// Min items OF collection
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <typeparam name="TComparable"></typeparam>
        /// <param name="collection">collection</param>
        /// <param name="minComparer"></param> 
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Target MinBy<Target, TComparable>(this IEnumerable<Target> collection, Func<Target, TComparable> minComparer)
            where TComparable : IComparable<TComparable>
            where Target : notnull
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Any() == false)
            {
                throw new ArgumentException($"{nameof(collection)} is Empty");
            }

            if (minComparer == null)
            {
                throw new ArgumentNullException(nameof(minComparer));
            }

            Target first = collection.First();
            TComparable firstCompa = minComparer(first);
            foreach (Target item in collection)
            {
                TComparable current = minComparer(item);

                int com = current.CompareTo(firstCompa);
                if (com < 0)
                {
                    firstCompa = current;
                    first = item;
                }
            }

            return first;
        }


#if !NET6_0_OR_GREATER

        /// <summary>
        /// segment a <typeparamref name="Target"/> collection
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="targets">collection</param>
        /// <param name="segmentCapacity">the capacity of segment</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<IEnumerable<Target>> Chunk<Target>(this IEnumerable<Target> targets, int segmentCapacity)
        {

            if (targets is null)
            {
                throw new ArgumentNullException(nameof(targets));
            }

            if (segmentCapacity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(segmentCapacity));
            }

            int totalCount = targets.Count();

            int segmentCount = totalCount / segmentCapacity;

            int segmentCounter = segmentCount * segmentCapacity;

            int remainingCount = totalCount - segmentCounter;

            for (int i = 0; i < segmentCount; i++)
            {
                yield return targets.Skip(i * segmentCapacity).Take(segmentCapacity);
            }

            if (remainingCount > 0)
            {
                yield return targets.Skip(segmentCounter).Take(remainingCount);
            }
        }

#endif

        /// <summary>
        /// Disrupt the order of a  collection
        /// </summary> 
        /// <param name="targets"></param>
        public static Target Disorder<Target>(this Target targets)
            where Target : System.Collections.IList
        {

            if (targets is null)
            {
                throw new System.ArgumentNullException(nameof(targets));
            }

            if (targets.Count == 0)
            {
                throw new InvalidOperationException("empty collection");
            }

            Random random = new(DateTime.Now.Millisecond);

            int currentIndex;
            object tempValue;
            int maxIndex = targets.Count - 1;
            for (int i = 0; i < targets.Count; i++)
            {
                int targetIndex = maxIndex - i;
                currentIndex = random.Next(0, targets.Count - i);
                tempValue = targets[currentIndex];
                targets[currentIndex] = targets[targetIndex];
                targets[targetIndex] = tempValue;
            }

            random = null;

            return targets;
        }
    }
}