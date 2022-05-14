namespace MindustryLogics
{
    /// <summary>
    /// <para>BuildingGroup for <see cref="Mindustry.LocateBuilding(BuildingGroup, bool, out double, out double, out Building)"/>.</para>
    /// <para>To locate cores: <c>BuildingGroup.Core</c></para>
    /// </summary>
    /// <remarks>
    /// Created building groups can only be passed as arguments to methods.<br/>
    /// It <b><i>cannot</i></b> be stored in a variable or passed into user defined methods.
    /// </remarks>
    public abstract class BuildingGroup
    {
        /// <summary>Any kind of core.</summary>
        public static BuildingGroup Core => default;
        /// <summary>Storage building. (<see cref="BuildingType.Vault"/> and <see cref="BuildingType.Container"/>)</summary>
        public static BuildingGroup Storage => default;
        /// <summary>Buildings that generate power.</summary>
        public static BuildingGroup Generator => default;
        /// <summary>Any kind of turret.</summary>
        public static BuildingGroup Turret => default;
        /// <summary>Buildings that transform resources.</summary>
        public static BuildingGroup Factory => default;
        /// <summary>Repair points. (<see cref="BuildingType.Repair_Point"/> and <see cref="BuildingType.Repair_Turret"/>)</summary>
        public static BuildingGroup Repair => default;
        /// <summary><see cref="BuildingType.Command_Center"/>.</summary>
        public static BuildingGroup Rally => default;
        /// <summary><see cref="BuildingType.Battery"/> and <see cref="BuildingType.Battery_Large"/>.</summary>
        public static BuildingGroup Battery => default;
        /// <summary><see cref="BuildingType.Resupply_Point"/></summary>
        /// <remarks>Only available on V6.<br/>
        /// V7 used storage buildings to resupply units.</remarks>
        public static BuildingGroup Resupply => default;
        /// <summary><see cref="BuildingType.Thorium_Reactor"/> and <see cref="BuildingType.Impact_Reactor"/>.</summary>
        public static BuildingGroup Reactor => default;
    }
}
