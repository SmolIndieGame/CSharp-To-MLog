namespace MindustryLogics
{
    /// <summary>
    /// How an entity is controlled, returns of <see cref="Entity.Controlled"/>.
    /// </summary>
    public abstract class ControlType : CamelCaseType
    {
        /// <summary>
        /// The entity is not being controlled.
        /// </summary>
        public const ControlType CtrlNone = default;

        /// <summary>
        /// The entity is controlled by a processor.
        /// </summary>
        /// <remarks>Using any operations that require <see cref="Mindustry.BindingUnit"/> will mark the unit as controlled by a processor,<br/>
        /// if the unit is not controlled by a player.</remarks>
        public static ControlType CtrlProcessor => default;

        /// <summary>
        /// The entity is controlled by a player.
        /// </summary>
        public static ControlType CtrlPlayer => default;

        /// <summary>
        /// The entity is in a formation that is controlled by a player.
        /// </summary>
        public static ControlType CtrlFormation => default;
    }
}
