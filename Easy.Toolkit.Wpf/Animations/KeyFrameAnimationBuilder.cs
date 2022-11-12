using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
namespace Easy.Toolkit
{
    /// <summary>
    /// ObjectKeyFrameAnimationBuilder
    /// </summary>
    public sealed class ObjectKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<ObjectKeyFrameAnimationBuilder, ObjectAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public ObjectKeyFrameAnimationBuilder AddKeyFrame(object value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteObjectKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// BoolKeyFrameAnimationBuilder
    /// </summary>
    public sealed class BoolKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<BoolKeyFrameAnimationBuilder, BooleanAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public BoolKeyFrameAnimationBuilder AddKeyFrame(bool value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteBooleanKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// ByteKeyFrameAnimationBuilder
    /// </summary>
    public sealed class ByteKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<ByteKeyFrameAnimationBuilder, ByteAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public ByteKeyFrameAnimationBuilder AddEasingKeyFrame(byte value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingByteKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }
        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public ByteKeyFrameAnimationBuilder AddDiscreteKeyFrame(byte value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteByteKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public ByteKeyFrameAnimationBuilder AddLinearKeyFrame(byte value, int milliseconds)
        {
            return base.AddKeyFrame(new LinearByteKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public ByteKeyFrameAnimationBuilder AddSplineKeyFrame(byte value, int milliseconds)
        {
            return base.AddKeyFrame(new SplineByteKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// ColorKeyFrameAnimationBuilder
    /// </summary>
    public sealed class ColorKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<ColorKeyFrameAnimationBuilder, ColorAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public ColorKeyFrameAnimationBuilder AddEasingKeyFrame(Color value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingColorKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }
        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public ColorKeyFrameAnimationBuilder AddDiscreteKeyFrame(Color value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteColorKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public ColorKeyFrameAnimationBuilder AddLinearKeyFrame(Color value, int milliseconds)
        {
            return base.AddKeyFrame(new LinearColorKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public ColorKeyFrameAnimationBuilder AddSplineKeyFrame(Color value, int milliseconds)
        {
            return base.AddKeyFrame(new SplineColorKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// DoubleKeyFrameAnimationBuilder
    /// </summary>
    public sealed class DoubleKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<DoubleKeyFrameAnimationBuilder, DoubleAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public DoubleKeyFrameAnimationBuilder AddEasingKeyFrame(double value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingDoubleKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }
        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public DoubleKeyFrameAnimationBuilder AddDiscreteKeyFrame(double value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteDoubleKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public DoubleKeyFrameAnimationBuilder AddLinearKeyFrame(double value, int milliseconds)
        {
            return base.AddKeyFrame(new LinearDoubleKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public DoubleKeyFrameAnimationBuilder AddSplineKeyFrame(double value, int milliseconds)
        {
            return base.AddKeyFrame(new SplineDoubleKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// DecimalKeyFrameAnimationBuilder
    /// </summary>
    public sealed class DecimalKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<DecimalKeyFrameAnimationBuilder, DecimalAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public DecimalKeyFrameAnimationBuilder AddEasingKeyFrame(decimal value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingDecimalKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }

        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public DecimalKeyFrameAnimationBuilder AddDiscreteKeyFrame(decimal value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteDecimalKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public DecimalKeyFrameAnimationBuilder AddLinearKeyFrame(decimal value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteDecimalKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public DecimalKeyFrameAnimationBuilder AddSplineKeyFrame(decimal value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteDecimalKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// Int16KeyFrameAnimationBuilder
    /// </summary>
    public sealed class Int16KeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<Int16KeyFrameAnimationBuilder, Int16AnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public Int16KeyFrameAnimationBuilder AddEasingKeyFrame(short value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingInt16KeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }

        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Int16KeyFrameAnimationBuilder AddDiscreteKeyFrame(short value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteInt16KeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Int16KeyFrameAnimationBuilder AddLinearKeyFrame(short value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteInt16KeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Int16KeyFrameAnimationBuilder AddSplineKeyFrame(short value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteInt16KeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }

    /// <summary>
    /// Int32KeyFrameAnimationBuilder
    /// </summary>
    public sealed class Int32KeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<Int32KeyFrameAnimationBuilder, Int32AnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public Int32KeyFrameAnimationBuilder AddEasingKeyFrame(int value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingInt32KeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }
        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Int32KeyFrameAnimationBuilder AddDiscreteKeyFrame(int value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteInt32KeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Int32KeyFrameAnimationBuilder AddLinearKeyFrame(int value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteInt32KeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Int32KeyFrameAnimationBuilder AddSplineKeyFrame(int value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteInt32KeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }

    /// <summary>
    /// Int64KeyFrameAnimationBuilder
    /// </summary>
    public sealed class Int64KeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<Int64KeyFrameAnimationBuilder, Int64AnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public Int64KeyFrameAnimationBuilder AddEasingKeyFrame(long value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingInt64KeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }
        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Int64KeyFrameAnimationBuilder AddDiscreteKeyFrame(long value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteInt64KeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Int64KeyFrameAnimationBuilder AddLinearKeyFrame(long value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteInt64KeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Int64KeyFrameAnimationBuilder AddSplineKeyFrame(long value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteInt64KeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// MatrixKeyFrameAnimationBuilder
    /// </summary>
    public sealed class MatrixKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<MatrixKeyFrameAnimationBuilder, MatrixAnimationUsingKeyFrames>
    {

        /// <summary>
        /// AddKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public MatrixKeyFrameAnimationBuilder AddKeyFrame(Matrix value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteMatrixKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// PointKeyFrameAnimationBuilder
    /// </summary>
    public sealed class PointKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<PointKeyFrameAnimationBuilder, PointAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public PointKeyFrameAnimationBuilder AddEasingKeyFrame(Point value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingPointKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }

        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public PointKeyFrameAnimationBuilder AddDiscreteKeyFrame(Point value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscretePointKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public PointKeyFrameAnimationBuilder AddLinearKeyFrame(Point value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscretePointKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public PointKeyFrameAnimationBuilder AddSplineKeyFrame(Point value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscretePointKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// QuaternionKeyFrameAnimationBuilder
    /// </summary>
    public sealed class QuaternionKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<QuaternionKeyFrameAnimationBuilder, QuaternionAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public QuaternionKeyFrameAnimationBuilder AddEasingKeyFrame(Quaternion value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingQuaternionKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }

        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public QuaternionKeyFrameAnimationBuilder AddDiscreteKeyFrame(Quaternion value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteQuaternionKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public QuaternionKeyFrameAnimationBuilder AddLinearKeyFrame(Quaternion value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteQuaternionKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public QuaternionKeyFrameAnimationBuilder AddSplineKeyFrame(Quaternion value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteQuaternionKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// RectKeyFrameAnimationBuilder
    /// </summary>
    public sealed class RectKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<RectKeyFrameAnimationBuilder, RectAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public RectKeyFrameAnimationBuilder AddEasingKeyFrame(Rect value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingRectKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }

        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public RectKeyFrameAnimationBuilder AddDiscreteKeyFrame(Rect value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteRectKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public RectKeyFrameAnimationBuilder AddLinearKeyFrame(Rect value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteRectKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public RectKeyFrameAnimationBuilder AddSplineKeyFrame(Rect value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteRectKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// Rotation3DKeyFrameAnimationBuilder
    /// </summary>
    public sealed class Rotation3DKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<Rotation3DKeyFrameAnimationBuilder, Rotation3DAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public Rotation3DKeyFrameAnimationBuilder AddEasingKeyFrame(Rotation3D value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingRotation3DKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }

        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Rotation3DKeyFrameAnimationBuilder AddDiscreteKeyFrame(Rotation3D value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteRotation3DKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Rotation3DKeyFrameAnimationBuilder AddLinearKeyFrame(Rotation3D value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteRotation3DKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Rotation3DKeyFrameAnimationBuilder AddSplineKeyFrame(Rotation3D value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteRotation3DKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// SingleKeyFrameAnimationBuilder
    /// </summary>
    public sealed class SingleKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<SingleKeyFrameAnimationBuilder, SingleAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public SingleKeyFrameAnimationBuilder AddEasingKeyFrame(float value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingSingleKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }

        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public SingleKeyFrameAnimationBuilder AddDiscreteKeyFrame(float value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteSingleKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public SingleKeyFrameAnimationBuilder AddLinearKeyFrame(float value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteSingleKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public SingleKeyFrameAnimationBuilder AddSplineKeyFrame(float value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteSingleKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// StringKeyFrameAnimationBuilder
    /// </summary>
    public sealed class StringKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<StringKeyFrameAnimationBuilder, StringAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public StringKeyFrameAnimationBuilder AddKeyFrame(string value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteStringKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// SizeKeyFrameAnimationBuilder
    /// </summary>
    public sealed class SizeKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<SizeKeyFrameAnimationBuilder, SizeAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public SizeKeyFrameAnimationBuilder AddEasingKeyFrame(Size value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingSizeKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }

        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public SizeKeyFrameAnimationBuilder AddDiscreteKeyFrame(Size value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteSizeKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public SizeKeyFrameAnimationBuilder AddLinearKeyFrame(Size value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteSizeKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public SizeKeyFrameAnimationBuilder AddSplineKeyFrame(Size value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteSizeKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// ThicknessKeyFrameAnimationBuilder
    /// </summary>
    public sealed class ThicknessKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<ThicknessKeyFrameAnimationBuilder, ThicknessAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public ThicknessKeyFrameAnimationBuilder AddEasingKeyFrame(Thickness value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingThicknessKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }

        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public ThicknessKeyFrameAnimationBuilder AddDiscreteKeyFrame(Thickness value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteThicknessKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public ThicknessKeyFrameAnimationBuilder AddLinearKeyFrame(Thickness value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteThicknessKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public ThicknessKeyFrameAnimationBuilder AddSplineKeyFrame(Thickness value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteThicknessKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// Vector3DKeyFrameAnimationBuilder
    /// </summary>
    public sealed class Vector3DKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<Vector3DKeyFrameAnimationBuilder, Vector3DAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public Vector3DKeyFrameAnimationBuilder AddEasingKeyFrame(Vector3D value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingVector3DKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }

        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Vector3DKeyFrameAnimationBuilder AddDiscreteKeyFrame(Vector3D value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteVector3DKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Vector3DKeyFrameAnimationBuilder AddLinearKeyFrame(Vector3D value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteVector3DKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Vector3DKeyFrameAnimationBuilder AddSplineKeyFrame(Vector3D value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteVector3DKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
    /// <summary>
    /// VectorKeyFrameAnimationBuilder
    /// </summary>
    public sealed class VectorKeyFrameAnimationBuilder : KeyFrameAnimationBuildBase<VectorKeyFrameAnimationBuilder, VectorAnimationUsingKeyFrames>
    {
        /// <summary>
        /// AddEasingKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <param name="easingFunction"></param>
        /// <returns></returns>
        public VectorKeyFrameAnimationBuilder AddEasingKeyFrame(Vector value, int milliseconds, IEasingFunction easingFunction)
        {
            return base.AddKeyFrame(new EasingVectorKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds), easingFunction));
        }

        /// <summary>
        /// AddDiscreteKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public VectorKeyFrameAnimationBuilder AddDiscreteKeyFrame(Vector value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteVectorKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
        /// <summary>
        /// AddLinearKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public VectorKeyFrameAnimationBuilder AddLinearKeyFrame(Vector value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteVectorKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }

        /// <summary>
        /// AddSplineKeyFrame
        /// </summary>
        /// <param name="value"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public VectorKeyFrameAnimationBuilder AddSplineKeyFrame(Vector value, int milliseconds)
        {
            return base.AddKeyFrame(new DiscreteVectorKeyFrame(value, TimeSpan.FromMilliseconds(milliseconds)));
        }
    }
}
