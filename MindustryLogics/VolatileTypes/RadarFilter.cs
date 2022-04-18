namespace MindustryLogics
{
    /// <summary>
    /// <para>
    /// Filter for radar methods.<br/>
    /// Use <see cref="Filter.Radar()"/> to create a filter than add constraints into it. (maximum three constraints)
    /// </para>
    /// <para>
    /// To filter enemy ground unit: <c>Filter.Radar().Enemy.Ground</c><br/>
    /// To filter anything: <c>Filter.Radar()</c> or <c>null</c>
    /// </para>
    /// </summary>
    public abstract class RadarFilter
    {
        /// <inheritdoc cref="RadarFilter"/>
        public RadarFilter Enemy => default;
        /// <inheritdoc cref="RadarFilter"/>
        public RadarFilter Ally => default;
        /// <inheritdoc cref="RadarFilter"/>
        public RadarFilter Player => default;
        /// <inheritdoc cref="RadarFilter"/>
        public RadarFilter Attacker => default;
        /// <inheritdoc cref="RadarFilter"/>
        public RadarFilter Flying => default;
        /// <inheritdoc cref="RadarFilter"/>
        public RadarFilter Boss => default;
        /// <inheritdoc cref="RadarFilter"/>
        public RadarFilter Ground => default;
    }
}
