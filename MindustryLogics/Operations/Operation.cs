namespace MindustryLogics
{
    /// <summary>
    /// This class contains operations for two number or objects.
    /// </summary>
    public static class Operation
    {
        /// <summary>
        /// Add two numbers together.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Add(double a, double b) => default;
        /// <summary>
        /// Subtract number <paramref name="b"/> from number <paramref name="a"/>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Sub(double a, double b) => default;
        /// <summary>
        /// Multiply number <paramref name="a"/> by number <paramref name="b"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Mul(double a, double b) => default;
        /// <summary>
        /// Divide number <paramref name="a"/> by number <paramref name="b"/>.<br/>
        /// Returns <see langword="null"/> on divide by zero.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Div(double a, double b) => default;
        /// <summary>
        /// Divide number <paramref name="a"/> by number <paramref name="b"/>.<br/>
        /// The result will be rounded down to the closest integer.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long Idiv(double a, double b) => default;
        /// <summary>
        /// Mod number <paramref name="a"/> by number <paramref name="b"/>.<br/>
        /// Get the remainder of number <paramref name="a"/> divided by number <paramref name="b"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Mod(double a, double b) => default;
        /// <summary>
        /// The number <paramref name="a"/> raised to the power <paramref name="b"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Pow(double a, double b) => default;
        /// <summary>
        /// Does object <paramref name="a"/> equals to object <paramref name="b"/>?<br/>
        /// objects become 1, null become 0 when comparing with numbers.<br/>
        /// Coerces types.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Equal(object a, object b) => default;
        /// <summary>
        /// Does object <paramref name="a"/> not equals to object <paramref name="b"/>?<br/>
        /// objects become 1, null become 0 when comparing with numbers.<br/>
        /// Coerces types.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool NotEqual(object a, object b) => default;
        /// <summary>
        /// Does number <paramref name="a"/> less than number <paramref name="b"/>?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool LessThan(double a, double b) => default;
        /// <summary>
        /// Does number <paramref name="a"/> less than or equals to number <paramref name="b"/>?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool LessThanEq(double a, double b) => default;
        /// <summary>
        /// Does number <paramref name="a"/> greater than number <paramref name="b"/>?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool GreaterThan(double a, double b) => default;
        /// <summary>
        /// Does number <paramref name="a"/> greater than or equals to number <paramref name="b"/>?
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool GreaterThanEq(double a, double b) => default;
        /// <summary>
        /// Does object <paramref name="a"/> equals to object <paramref name="b"/>?<br/>
        /// Objects compared with numbers always return <see langword="false"/>.<br/>
        /// Does not coerces types.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool StrictEqual(object a, object b) => default;
        /// <summary>
        /// Shift all the bits of <paramref name="v"/> to the left <paramref name="s"/> times.<br/>
        /// Equivalent code: <c>v &lt;&lt; s</c>
        /// </summary>
        /// <param name="v"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ulong Shl(ulong v, int s) => default;
        /// <summary>
        /// Shift all the bits of <paramref name="v"/> to the right <paramref name="s"/> times.<br/>
        /// Equivalent code: <c>v &gt;&gt; s</c>
        /// </summary>
        /// <param name="v"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ulong Shr(ulong v, int s) => default;
        /// <summary>
        /// Bitwise or of <paramref name="a"/>, <paramref name="b"/>.<br/>
        /// Equivalent code: <c>v | s</c>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ulong Or(ulong a, ulong b) => default;
        /// <summary>
        /// Bitwise and of <paramref name="a"/>, <paramref name="b"/>.<br/>
        /// Equivalent code: <c>v &amp; s</c>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ulong And(ulong a, ulong b) => default;
        /// <summary>
        /// Bitwise xor of <paramref name="a"/>, <paramref name="b"/>.<br/>
        /// Equivalent code: <c>v ^ s</c>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ulong Xor(ulong a, ulong b) => default;
        /// <summary>
        /// Flip all the bits of <paramref name="v"/> (1 to 0, 0 to 1).<br/>
        /// Equivalent code: <c>~v</c>
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static ulong Not(ulong v) => default;
        /// <summary>
        /// Returns the maximum number of <paramref name="a"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Max(double a, double b) => default;
        /// <summary>
        /// Returns the minimum number of <paramref name="a"/> and <paramref name="b"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Min(double a, double b) => default;
        /// <summary>
        /// Returns the angle of a vector that starts at (0, 0), ends at (<paramref name="a"/>, <paramref name="b"/>).<br/>
        /// The angle is in degrees.
        /// <list type="bullet">
        /// <listheader>It is a real number in range [0, 360).</listheader>
        /// <item><term><b>0</b> </term> <description>faces right</description></item>
        /// <item><term><b>90</b> </term> <description>faces up</description></item>
        /// <item><term><b>180</b> </term> <description>faces left</description></item>
        /// <item><term><b>270</b> </term> <description>faces down</description></item>
        /// </list>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Angle(double a, double b) => default;
        /// <summary>
        /// Returns the length of a vector that starts at (0, 0), ends at (<paramref name="a"/>, <paramref name="b"/>).
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Len(double a, double b) => default;
        /// <summary>
        /// Returns the 2D simplex noise at position (<paramref name="a"/>, <paramref name="b"/>).
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Noise(double a, double b) => default;
        /// <summary>
        /// Absolute value of <paramref name="v"/>.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double Abs(double v) => default;
        /// <summary>
        /// Returns the natural logarithm (ln) of <paramref name="v"/>.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double Log(double v) => default;
        /// <summary>
        /// Returns the base10 logarithm of <paramref name="v"/>.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double Log10(double v) => default;
        /// <summary>
        /// Round down <paramref name="v"/> to the closest integer.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double Floor(double v) => default;
        /// <summary>
        /// Round up <paramref name="v"/> to the closest integer.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double Ceil(double v) => default;
        /// <summary>
        /// Returns the square root of <paramref name="v"/>.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double Sqrt(double v) => default;
        /// <summary>
        /// Returns a random integer in range [0, <paramref name="v"/>).
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static long Rand(long v) => default;
        /// <summary>
        /// Returns the sine of <paramref name="v"/> in degrees.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double Sin(double v) => default;
        /// <summary>
        /// Returns the cosine of <paramref name="v"/> in degrees.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double Cos(double v) => default;
        /// <summary>
        /// Returns the tangent of <paramref name="v"/> in degrees.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double Tan(double v) => default;
        /// <summary>
        /// Returns the arc sine of <paramref name="v"/> in degrees.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        /// <remarks>Not available in V6.</remarks>
        public static double Asin(double v) => default;
        /// <summary>
        /// Returns the arc cosine of <paramref name="v"/> in degrees.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        /// <remarks>Not available in V6.</remarks>
        public static double Acos(double v) => default;
        /// <summary>
        /// Returns the arc tangent of <paramref name="v"/> in degrees.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        /// <remarks>Not available in V6.</remarks>
        public static double Atan(double v) => default;
    }
}
