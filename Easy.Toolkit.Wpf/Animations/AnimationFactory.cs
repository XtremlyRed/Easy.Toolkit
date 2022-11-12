using System.Windows.Media.Animation;
namespace Easy.Toolkit
{
    /// <summary>
    /// animation factory
    /// </summary>
    public static class AnimationFactory
    {
        /// <summary>
        /// ObjectKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static ObjectKeyFrameAnimationBuilder ObjectKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new ObjectKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// BoolKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static BoolKeyFrameAnimationBuilder BoolKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new BoolKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// ByteKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static ByteKeyFrameAnimationBuilder ByteKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new ByteKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// ColorKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static ColorKeyFrameAnimationBuilder ColorKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new ColorKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// DoubleKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static DoubleKeyFrameAnimationBuilder DoubleKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new DoubleKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// DecimalKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static DecimalKeyFrameAnimationBuilder DecimalKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new DecimalKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// Int16KeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static Int16KeyFrameAnimationBuilder Int16KeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new Int16KeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// Int32KeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static Int32KeyFrameAnimationBuilder Int32KeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new Int32KeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// Int64KeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static Int64KeyFrameAnimationBuilder Int64KeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new Int64KeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// MatrixKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static MatrixKeyFrameAnimationBuilder MatrixKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new MatrixKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// PointKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static PointKeyFrameAnimationBuilder PointKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new PointKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// QuaternionKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static QuaternionKeyFrameAnimationBuilder QuaternionKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new QuaternionKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// RectKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static RectKeyFrameAnimationBuilder RectKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new RectKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// Rotation3DKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static Rotation3DKeyFrameAnimationBuilder Rotation3DKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new Rotation3DKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// SingleKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static SingleKeyFrameAnimationBuilder SingleKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new SingleKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// StringKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static StringKeyFrameAnimationBuilder StringKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new StringKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// SizeKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static SizeKeyFrameAnimationBuilder SizeKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new SizeKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// ThicknessKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static ThicknessKeyFrameAnimationBuilder ThicknessKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new ThicknessKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// Vector3DKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static Vector3DKeyFrameAnimationBuilder Vector3DKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new Vector3DKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// VectorKeyFrameAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static VectorKeyFrameAnimationBuilder VectorKeyFrameAnimationBuilder(this Storyboard storyboard)
        {
            return new VectorKeyFrameAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// ObjectKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static ObjectKeyFrameAnimationBuilder ObjectKeyFrameAnimationBuilder()
        {
            return new ObjectKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// BoolKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static BoolKeyFrameAnimationBuilder BoolKeyFrameAnimationBuilder()
        {
            return new BoolKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// ByteKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static ByteKeyFrameAnimationBuilder ByteKeyFrameAnimationBuilder()
        {
            return new ByteKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// ColorKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static ColorKeyFrameAnimationBuilder ColorKeyFrameAnimationBuilder()
        {
            return new ColorKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// DoubleKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static DoubleKeyFrameAnimationBuilder DoubleKeyFrameAnimationBuilder()
        {
            return new DoubleKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// DecimalKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static DecimalKeyFrameAnimationBuilder DecimalKeyFrameAnimationBuilder()
        {
            return new DecimalKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// Int16KeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static Int16KeyFrameAnimationBuilder Int16KeyFrameAnimationBuilder()
        {
            return new Int16KeyFrameAnimationBuilder();
        }
        /// <summary>
        /// Int32KeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static Int32KeyFrameAnimationBuilder Int32KeyFrameAnimationBuilder()
        {
            return new Int32KeyFrameAnimationBuilder();
        }
        /// <summary>
        /// Int64KeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static Int64KeyFrameAnimationBuilder Int64KeyFrameAnimationBuilder()
        {
            return new Int64KeyFrameAnimationBuilder();
        }
        /// <summary>
        /// MatrixKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static MatrixKeyFrameAnimationBuilder MatrixKeyFrameAnimationBuilder()
        {
            return new MatrixKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// PointKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static PointKeyFrameAnimationBuilder PointKeyFrameAnimationBuilder()
        {
            return new PointKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// QuaternionKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static QuaternionKeyFrameAnimationBuilder QuaternionKeyFrameAnimationBuilder()
        {
            return new QuaternionKeyFrameAnimationBuilder();
        }

        /// <summary>
        /// RectKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static RectKeyFrameAnimationBuilder RectKeyFrameAnimationBuilder()
        {
            return new RectKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// Rotation3DKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static Rotation3DKeyFrameAnimationBuilder Rotation3DKeyFrameAnimationBuilder()
        {
            return new Rotation3DKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// SingleKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static SingleKeyFrameAnimationBuilder SingleKeyFrameAnimationBuilder()
        {
            return new SingleKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// StringKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static StringKeyFrameAnimationBuilder StringKeyFrameAnimationBuilder()
        {
            return new StringKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// SizeKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static SizeKeyFrameAnimationBuilder SizeKeyFrameAnimationBuilder()
        {
            return new SizeKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// ThicknessKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static ThicknessKeyFrameAnimationBuilder ThicknessKeyFrameAnimationBuilder()
        {
            return new ThicknessKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// Vector3DKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static Vector3DKeyFrameAnimationBuilder Vector3DKeyFrameAnimationBuilder()
        {
            return new Vector3DKeyFrameAnimationBuilder();
        }
        /// <summary>
        /// VectorKeyFrameAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static VectorKeyFrameAnimationBuilder VectorKeyFrameAnimationBuilder()
        {
            return new VectorKeyFrameAnimationBuilder();
        }













        //public static ObjectAnimationBuilder ObjectBuilder(this Storyboard storyboard)
        //{
        //    return new ObjectAnimationBuilder().SetOwner(storyboard);

        //}
        //public static BoolAnimationBuilder BoolBuilder(this Storyboard storyboard)
        //{
        //    return new BoolAnimationBuilder().SetOwner(storyboard);
        //}
        /// <summary>
        /// ByteAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static ByteAnimationBuilder ByteAnimationBuilder(this Storyboard storyboard)
        {
            return new ByteAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// ColorAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static ColorAnimationBuilder ColorAnimationBuilder(this Storyboard storyboard)
        {
            return new ColorAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// DoubleAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static DoubleAnimationBuilder DoubleAnimationBuilder(this Storyboard storyboard)
        {
            return new DoubleAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// DecimalAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static DecimalAnimationBuilder DecimalAnimationBuilder(this Storyboard storyboard)
        {
            return new DecimalAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// Int16AnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static Int16AnimationBuilder Int16AnimationBuilder(this Storyboard storyboard)
        {
            return new Int16AnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// Int32AnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static Int32AnimationBuilder Int32AnimationBuilder(this Storyboard storyboard)
        {
            return new Int32AnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// Int64AnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static Int64AnimationBuilder Int64AnimationBuilder(this Storyboard storyboard)
        {
            return new Int64AnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// PointAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static PointAnimationBuilder PointAnimationBuilder(this Storyboard storyboard)
        {
            return new PointAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// QuaternionAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static QuaternionAnimationBuilder QuaternionAnimationBuilder(this Storyboard storyboard)
        {
            return new QuaternionAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// RectAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static RectAnimationBuilder RectAnimationBuilder(this Storyboard storyboard)
        {
            return new RectAnimationBuilder().SetOwner(storyboard);
        }

        /// <summary>
        /// Rotation3DAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static Rotation3DAnimationBuilder Rotation3DAnimationBuilder(this Storyboard storyboard)
        {
            return new Rotation3DAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// SingleAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static SingleAnimationBuilder SingleAnimationBuilder(this Storyboard storyboard)
        {
            return new SingleAnimationBuilder().SetOwner(storyboard);
        }
        //public static StringAnimationBuilder StringBuilder(this Storyboard storyboard)
        //{
        //    return new StringAnimationBuilder().SetOwner(storyboard);
        //}

        /// <summary>
        /// SizeAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static SizeAnimationBuilder SizeAnimationBuilder(this Storyboard storyboard)
        {
            return new SizeAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// ThicknessAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static ThicknessAnimationBuilder ThicknessAnimationBuilder(this Storyboard storyboard)
        {
            return new ThicknessAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// Vector3DAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static Vector3DAnimationBuilder Vector3DAnimationBuilder(this Storyboard storyboard)
        {
            return new Vector3DAnimationBuilder().SetOwner(storyboard);
        }
        /// <summary>
        /// VectorAnimationBuilder
        /// </summary>
        /// <param name="storyboard"></param>
        /// <returns></returns>
        public static VectorAnimationBuilder VectorAnimationBuilder(this Storyboard storyboard)
        {
            return new VectorAnimationBuilder().SetOwner(storyboard);
        }



        //public static ObjectAnimationBuilder ObjectBuilder()
        //{
        //    return new ObjectAnimationBuilder();

        //}
        //public static BoolAnimationBuilder BoolBuilder()
        //{
        //    return new BoolAnimationBuilder();
        //}
        /// <summary>
        /// ByteAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static ByteAnimationBuilder ByteAnimationBuilder()
        {
            return new ByteAnimationBuilder();
        }
        /// <summary>
        /// ColorAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static ColorAnimationBuilder ColorAnimationBuilder()
        {
            return new ColorAnimationBuilder();
        }
        /// <summary>
        /// DoubleAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static DoubleAnimationBuilder DoubleAnimationBuilder()
        {
            return new DoubleAnimationBuilder();
        }
        /// <summary>
        /// DecimalAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static DecimalAnimationBuilder DecimalAnimationBuilder()
        {
            return new DecimalAnimationBuilder();
        }
        /// <summary>
        /// Int16AnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static Int16AnimationBuilder Int16AnimationBuilder()
        {
            return new Int16AnimationBuilder();
        }
        /// <summary>
        /// Int32AnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static Int32AnimationBuilder Int32AnimationBuilder()
        {
            return new Int32AnimationBuilder();
        }
        /// <summary>
        /// Int64AnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static Int64AnimationBuilder Int64AnimationBuilder()
        {
            return new Int64AnimationBuilder();
        }
        //public static MatrixAnimationBuilder MatrixBuilder()
        //{
        //    return new MatrixAnimationBuilder();
        //}
        /// <summary>
        /// PointAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static PointAnimationBuilder PointAnimationBuilder()
        {
            return new PointAnimationBuilder();
        }
        /// <summary>
        /// QuaternionAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static QuaternionAnimationBuilder QuaternionAnimationBuilder()
        {
            return new QuaternionAnimationBuilder();
        }
        /// <summary>
        /// RectAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static RectAnimationBuilder RectAnimationBuilder()
        {
            return new RectAnimationBuilder();
        }
        /// <summary>
        /// Rotation3DAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static Rotation3DAnimationBuilder Rotation3DAnimationBuilder()
        {
            return new Rotation3DAnimationBuilder();
        }
        /// <summary>
        /// SingleAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static SingleAnimationBuilder SingleAnimationBuilder()
        {
            return new SingleAnimationBuilder();
        }
        //public static StringAnimationBuilder StringBuilder()
        //{
        //    return new StringAnimationBuilder();
        //}
        /// <summary>
        /// SizeAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static SizeAnimationBuilder SizeAnimationBuilder()
        {
            return new SizeAnimationBuilder();
        }
        /// <summary>
        /// ThicknessAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static ThicknessAnimationBuilder ThicknessAnimationBuilder()
        {
            return new ThicknessAnimationBuilder();
        }
        /// <summary>
        /// Vector3DAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static Vector3DAnimationBuilder Vector3DAnimationBuilder()
        {
            return new Vector3DAnimationBuilder();
        }
        /// <summary>
        /// VectorAnimationBuilder
        /// </summary>
        /// <returns></returns>
        public static VectorAnimationBuilder VectorAnimationBuilder()
        {
            return new VectorAnimationBuilder();
        }
    }
}
