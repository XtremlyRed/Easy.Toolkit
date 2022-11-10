using System;
using System.Collections;
using System.Collections.Generic;

namespace Easy.Toolkit
{
    /// <summary>
    /// random extensions
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// random one in scope
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="random"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Target InScope<Target>(this Random random, IList<Target> collection)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ICollection.Count));
            }
            int index = random.Next(0, collection.Count);
            return collection[index];
        }

        /// <summary>
        /// random one in scope
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="random"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Target InScope<Target>(this Random random, params Target[] collection)
        {
            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ICollection.Count));
            }
            int index = random.Next(0, collection.Length);
            return collection[index];
        }
    }
}