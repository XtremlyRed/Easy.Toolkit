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
        /// default randomer
        /// </summary>
        internal static Random random1 = new Random();

        /// <summary>
        /// use <see cref="Random"/> to generate an index at random and return the corresponding value of the index
        /// <para>if the <c><paramref name="randomer"/></c> is null, use the built-in random</para>
        /// </summary>
        /// <typeparam name="Target"></typeparam> 
        /// <param name="collection"></param>
        /// <param name="randomer"></param>
        /// <returns></returns>
        /// <Exception cref="ArgumentNullException"></Exception>
        /// <Exception cref="ArgumentOutOfRangeException"></Exception>
        public static Target InRandom<Target>(this IList<Target> collection, Random randomer = null)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ICollection.Count));
            }

            int index = (randomer ?? random1).Next(0, collection.Count);

            return collection[index];
        }

        ///// <summary>
        ///// random one in scope
        ///// </summary>
        ///// <typeparam name="Target"></typeparam>
        ///// <param name="random"></param>
        ///// <param name="collection"></param>
        ///// <returns></returns>
        ///// <Exception cref="ArgumentNullException"></Exception>
        ///// <Exception cref="ArgumentOutOfRangeException"></Exception>
        //public static Target Random<Target>(this Target[] collection)
        //{
        //    if (random is null)
        //    {
        //        throw new ArgumentNullException(nameof(random));
        //    }

        //    if (collection is null)
        //    {
        //        throw new ArgumentNullException(nameof(collection));
        //    }

        //    if (collection.Length == 0)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(ICollection.Count));
        //    }
        //    int index = random.Next(0, collection.Length);
        //    return collection[index];
        //}
    }
}