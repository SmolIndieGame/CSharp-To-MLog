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
        /// <summary>The first unit of the results sorted by this method will be the closest one.<br/>
        /// The last unit of the results sorted by this method will be the furthest one.</summary>
        public static SortMethod Distance => default;
        /// <summary>The first unit of the results sorted by this method will be the one with the highest health.<br/>
        /// The last unit of the results sorted by this method will be the one with the lowest health.</summary>
        public static SortMethod Health => default;
        /// <summary>The first unit of the results sorted by this method will be the one with the highest shield.<br/>
        /// The last unit of the results sorted by this method will be the one with the lowest shield.</summary>
        public static SortMethod Shield => default;
        /// <summary>The first unit of the results sorted by this method will be the one with the highest armor.<br/>
        /// The last unit of the results sorted by this method will be the one with the lowest armor.</summary>
        public static SortMethod Armor => default;
        /// <summary>The first unit of the results sorted by this method will be the one with the highest max health.<br/>
        /// The last unit of the results sorted by this method will be the one with the lowest max health.</summary>
        public static SortMethod MaxHealth => default;
    }
}
