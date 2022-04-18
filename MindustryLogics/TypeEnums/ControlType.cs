namespace MindustryLogics
{
    /// <summary>
    /// How a unit is controlled, returns of <see cref="Entity.Controlled"/>.
    /// </summary>
    public enum ControlType
    {
        /// <summary>
        /// The unit is not being controlled.
        /// </summary>
        CtrlNone,
        /// <summary>
        /// The unit is controlled by a processor.
        /// </summary>
        CtrlProcessor,
        /// <summary>
        /// The unit is controlled by a player.
        /// </summary>
        CtrlPlayer,
        /// <summary>
        /// The unit is in a formation that is controlled by a player.
        /// </summary>
        CtrlFormation
    }
}
