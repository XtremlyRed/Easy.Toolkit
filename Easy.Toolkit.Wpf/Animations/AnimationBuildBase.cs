
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Easy.Toolkit.Animations
{
    /// <summary>
    /// Animation Build Base
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    /// <typeparam name="TAnimation"></typeparam>
    public abstract class AnimationBuildBase<TOwner, TAnimation>
           where TOwner : AnimationBuildBase<TOwner, TAnimation>, new()
        where TAnimation : Timeline, new()
    {
        /// <summary>
        /// create a new buidler
        /// </summary>
        public static TOwner Share => new();

        /// <summary>
        /// 
        /// </summary>
        protected internal TAnimation target;

        /// <summary>
        /// 
        /// </summary>
        public AnimationBuildBase()
        {
            target = new TAnimation();
        }

        /// <summary>
        /// AutoReverse
        /// </summary>
        /// <param name="AutoReverse"></param>
        /// <returns></returns>
        public TOwner AutoReverse(bool AutoReverse)
        {
            target.AutoReverse = AutoReverse;
            return (TOwner)this;
        }
        /// <summary>
        /// SpeedRatio
        /// </summary>
        /// <param name="SpeedRatio"></param>
        /// <returns></returns>
        public TOwner SpeedRatio(double SpeedRatio)
        {
            target.SpeedRatio = SpeedRatio;
            return (TOwner)this;
        }
        /// <summary>
        /// RepeatBehavior
        /// </summary>
        /// <param name="RepeatBehavior"></param>
        /// <returns></returns>
        public TOwner RepeatBehavior(RepeatBehavior RepeatBehavior)
        {
            target.RepeatBehavior = RepeatBehavior;
            return (TOwner)this;
        }
        /// <summary>
        /// Name
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public TOwner Name(string Name)
        {
            target.Name = Name;
            return (TOwner)this;
        }
        /// <summary>
        /// FillBehavior
        /// </summary>
        /// <param name="FillBehavior"></param>
        /// <returns></returns>
        public TOwner FillBehavior(FillBehavior FillBehavior)
        {
            target.FillBehavior = FillBehavior;
            return (TOwner)this;
        }
        /// <summary>
        /// Duration
        /// </summary>
        /// <param name="Duration"></param>
        /// <returns></returns>
        public TOwner Duration(Duration Duration)
        {
            target.Duration = Duration;
            return (TOwner)this;
        }
        /// <summary>
        /// DecelerationRatio
        /// </summary>
        /// <param name="DecelerationRatio"></param>
        /// <returns></returns>
        public TOwner DecelerationRatio(double DecelerationRatio)
        {
            target.DecelerationRatio = DecelerationRatio;
            return (TOwner)this;
        }
        /// <summary>
        /// BeginTime
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public TOwner BeginTime(int milliseconds)
        {
            target.BeginTime = TimeSpan.FromMilliseconds(milliseconds);
            return (TOwner)this;
        }
        /// <summary>
        /// AccelerationRatio
        /// </summary>
        /// <param name="AccelerationRatio"></param>
        /// <returns></returns>
        public TOwner AccelerationRatio(double AccelerationRatio)
        {
            target.AccelerationRatio = AccelerationRatio;
            return (TOwner)this;
        }

        /// <summary>
        /// SetTarget
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TOwner SetTarget(DependencyObject dependencyObject)
        {
            if (dependencyObject is null)
            {
                throw new ArgumentNullException(nameof(dependencyObject));
            }

            Storyboard.SetTarget(target, dependencyObject);
            return (TOwner)this;
        }

        /// <summary>
        /// SetTargetProperty
        /// </summary>
        /// <param name="propertyPath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TOwner SetTargetProperty(PropertyPath propertyPath)
        {
            if (propertyPath is null)
            {
                throw new ArgumentNullException(nameof(propertyPath));
            }
            Storyboard.SetTargetProperty(target, propertyPath);
            return (TOwner)this;
        }

        /// <summary>
        /// SetTargetName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TOwner SetTargetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Storyboard.SetTargetName(target, name);
            return (TOwner)this;
        }

        /// <summary>
        /// SetOwner
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public TOwner SetOwner(Storyboard owner)
        {
            if (!owner.Children.Contains(target))
            {
                owner.Children.Add(target);
            }
            return (TOwner)this;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual TAnimation Build()
        {
            return target;
        }
    }
}
