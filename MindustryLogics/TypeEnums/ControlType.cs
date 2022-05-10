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
        /// <remarks>Using any operations that require <see cref="Mindustry.BindingUnit"/> will mark the unit as controlled by a processor,<br/>
        /// if the unit is not controlled by a player.</remarks>
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
