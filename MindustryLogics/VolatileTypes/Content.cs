namespace MindustryLogics
{
    /// <summary>
    /// <para>Content type (item/liquid/unit/building) for <see cref="Mindustry.Lookup(Content, int)"/>.</para>
    /// <para>To lookup items: <c>Content.Item</c></para>
    /// </summary>
    /// <remarks>
    /// Created content type can only be passed as arguments to methods.<br/>
    /// It <b><i>cannot</i></b> be stored in a variable or passed into user defined methods.
    /// </remarks>
    public abstract class Content
    {
        /// <summary></summary>
        public static Content Item => default;
        /// <summary></summary>
        public static Content Liquid => default;
        /// <summary></summary>
        public static Content Block => default;
        /// <summary>
        /// Alias of <see cref="Block"/>.
        /// </summary>
        public static Content Building => default;
        /// <summary></summary>
        public static Content Unit => default;
    }
}
