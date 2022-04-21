namespace MindustryLogics
{
    /// <summary>
    /// How an entity is controlled, returns of <see cref="Entity.Controlled"/>.
    /// </summary>
    public enum ControlType
    {
        /// <summary>
        /// The entity is not being controlled.
        /// </summary>
        CtrlNone,
        /// <summary>
        /// The entity is controlled by a processor.
        /// </summary>
        CtrlProcessor,
        /// <summary>
        /// The entity is controlled by a player.
        /// </summary>
        CtrlPlayer,
        /// <summary>
        /// The entity is in a formation that is controlled by a player.
        /// </summary>
        CtrlFormation
    }
}
