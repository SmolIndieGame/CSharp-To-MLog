namespace MindustryLogics
{
    /// <summary>
    /// Create filters for methods that need filters.
    /// </summary>
    /// <remarks>
    /// Created filters can only be passed as arguments to methods.<br/>
    /// It <b><i>cannot</i></b> be stored in a variable or passed into user defined methods.
    /// </remarks>
    public static class Filter
    {
        /// <summary>
        /// Create a filter that accepts anything for <see cref="Building.Radar(RadarFilter, SortMethod, bool)"/> and UnitControl.Radar.
        /// </summary>
        /// <returns></returns>
        public static RadarFilter Radar() => default;
    }
}
