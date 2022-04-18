namespace MindustryLogics
{
    /// <summary>
    /// <para>SortMethod for radar methods.</para>
    /// <para>To sort by health: <c>SortMethod.Health</c></para>
    /// </summary>
    /// <remarks>
    /// Created sort methods can only be passed as arguments to methods.<br/>
    /// It <b><i>cannot</i></b> be stored in a variable or passed into user defined methods.
    /// </remarks>
    public abstract class SortMethod
    {
        /// <summary></summary>
        public static SortMethod Distance => default;
        /// <summary></summary>
        public static SortMethod Health => default;
        /// <summary></summary>
        public static SortMethod Shield => default;
        /// <summary></summary>
        public static SortMethod Armor => default;
        /// <summary></summary>
        public static SortMethod MaxHealth => default;
    }
}
