using System.Collections.Concurrent;
using System.Reflection;
using System.Windows.Media.Animation;
namespace Easy.Toolkit
{
    /// <summary>
    /// AnimationBuildBase
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    /// <typeparam name="TAnimation"></typeparam>
    /// <typeparam name="TType"></typeparam>
    public abstract class AnimationBuildBase<TOwner, TAnimation, TType> : AnimationBuildBase<TOwner, TAnimation>
       where TOwner : AnimationBuildBase<TOwner, TAnimation, TType>, new()
       where TAnimation : Timeline, new()
    {
        private const string @FromString = "From";
        private const string @ToStrings = "To";
        private const string @ByString = "By";
        private const string @EasingFunctionString = "EasingFunction";
        private readonly ConcurrentDictionary<string, PropertyInfo> PropertyMapper = new();

        /// <summary>
        /// FromTo
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public TOwner FromTo(TType from, TType to, int milliseconds)
        {
            target.Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(milliseconds));
            PropertyInfo fromProperty = PropertyMapper.GetOrAdd(@FromString, i => target.GetType().GetRuntimeProperty(@FromString));
            PropertyInfo toProperty = PropertyMapper.GetOrAdd(@ToStrings, i => target.GetType().GetRuntimeProperty(@ToStrings));

            fromProperty?.SetValue(target, from);
            toProperty?.SetValue(target, to);
            return (TOwner)this;
        }

        /// <summary>
        /// To
        /// </summary>
        /// <param name="to"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public TOwner To(TType to, int milliseconds)
        {
            target.Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(milliseconds));
            PropertyInfo toProperty = PropertyMapper.GetOrAdd(@ToStrings, i => target.GetType().GetRuntimeProperty(@ToStrings));
            toProperty?.SetValue(target, to);
            return (TOwner)this;
        }
        /// <summary>
        /// By
        /// </summary>
        /// <param name="by"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public TOwner By(TType by, int milliseconds)
        {
            target.Duration = new System.Windows.Duration(TimeSpan.FromMilliseconds(milliseconds));
            PropertyInfo byProperty = PropertyMapper.GetOrAdd(@ByString, i => target.GetType().GetRuntimeProperty(@ByString));
            byProperty?.SetValue(target, by);
            return (TOwner)this;
        }

        /// <summary>
        /// EasingFunction
        /// </summary>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public virtual TOwner EasingFunction(IEasingFunction easingFunction)
        {
            PropertyInfo easingFunctionProperty = PropertyMapper.GetOrAdd(@EasingFunctionString, i => target.GetType().GetRuntimeProperty(@EasingFunctionString));
            easingFunctionProperty?.SetValue(target, easingFunction);
            return (TOwner)this;
        }
    }

    /// <summary>
    ///  KeyFrameAnimationBuildBase
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    /// <typeparam name="TAnimation"></typeparam>
    public abstract class KeyFrameAnimationBuildBase<TOwner, TAnimation> : AnimationBuildBase<TOwner, TAnimation>
        where TOwner : KeyFrameAnimationBuildBase<TOwner, TAnimation>, new()
        where TAnimation : Timeline, IKeyFrameAnimation, new()
    {

        /// <summary>
        /// AddKeyFrame
        /// </summary>
        /// <param name="fram"></param>
        /// <returns></returns>
        public virtual TOwner AddKeyFrame(IKeyFrame fram)
        {
            if (fram != null)
            {
                target.KeyFrames.Add(fram);
            }


            return (TOwner)this;
        }
    }
}
