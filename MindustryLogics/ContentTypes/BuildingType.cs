namespace MindustryLogics
{
    /// <summary>
    /// The type of a building.
    /// </summary>
    public abstract class BuildingType : KebabCaseType
    {
        /** */ public const BuildingType None = default;

        /// <summary>
        /// Solid non-buildings.<br/>Example: Environment walls.
        /// </summary>
        public static BuildingType Solid => default;

        /** */ public static BuildingType Graphite_Press => default;
        /** */ public static BuildingType Multi_Press => default;
        /** */ public static BuildingType Silicon_Smelter => default;
        /** */ public static BuildingType Silicon_Crucible => default;
        /** */ public static BuildingType Kiln => default;
        /** */ public static BuildingType Plastanium_Compressor => default;
        /** */ public static BuildingType Phase_Weaver => default;
        /** */ public static BuildingType Alloy_Smelter => default;
        /** */ public static BuildingType Cryofluid_Mixer => default;
        /** */ public static BuildingType Pyratite_Mixer => default;
        /** */ public static BuildingType Blast_Mixer => default;
        /** */ public static BuildingType Melter => default;
        /** */ public static BuildingType Separator => default;
        /** */ public static BuildingType Disassembler => default;
        /** */ public static BuildingType Spore_Press => default;
        /** */ public static BuildingType Pulverizer => default;
        /** */ public static BuildingType Coal_Centrifuge => default;
        /** */ public static BuildingType Incinerator => default;
        /** */ public static BuildingType Copper_Wall => default;
        /** */ public static BuildingType Copper_Wall_Large => default;
        /** */ public static BuildingType Titanium_Wall => default;
        /** */ public static BuildingType Titanium_Wall_Large => default;
        /** */ public static BuildingType Plastanium_Wall => default;
        /** */ public static BuildingType Plastanium_Wall_Large => default;
        /** */ public static BuildingType Thorium_Wall => default;
        /** */ public static BuildingType Thorium_Wall_Large => default;
        /** */ public static BuildingType Phase_Wall => default;
        /** */ public static BuildingType Phase_Wall_Large => default;
        /** */ public static BuildingType Surge_Wall => default;
        /** */ public static BuildingType Surge_Wall_Large => default;
        /** */ public static BuildingType Door => default;
        /** */ public static BuildingType Door_Large => default;
        /** */ public static BuildingType Scrap_Wall => default;
        /** */ public static BuildingType Scrap_Wall_Large => default;
        /** */ public static BuildingType Scrap_Wall_Huge => default;
        /** */ public static BuildingType Scrap_Wall_Gigantic => default;
        /** */ public static BuildingType Mender => default;
        /** */ public static BuildingType Mend_Projector => default;
        /** */ public static BuildingType Overdrive_Projector => default;
        /** */ public static BuildingType Overdrive_Dome => default;
        /** */ public static BuildingType Force_Projector => default;
        /** */ public static BuildingType Shock_Mine => default;
        /** */ public static BuildingType Conveyor => default;
        /** */ public static BuildingType Titanium_Conveyor => default;
        /** */ public static BuildingType Plastanium_Conveyor => default;
        /** */ public static BuildingType Armored_Conveyor => default;
        /** */ public static BuildingType Junction => default;
        /** */ public static BuildingType Bridge_Conveyor => default;
        /** */ public static BuildingType Phase_Conveyor => default;
        /** */ public static BuildingType Sorter => default;
        /** */ public static BuildingType Inverted_Sorter => default;
        /** */ public static BuildingType Router => default;
        /** */ public static BuildingType Distributor => default;
        /** */ public static BuildingType Overflow_Gate => default;
        /** */ public static BuildingType Underflow_Gate => default;
        /** */ public static BuildingType Mass_Driver => default;
        /** */ public static BuildingType Duct => default;
        /** */ public static BuildingType Duct_Router => default;
        /** */ public static BuildingType Duct_Bridge => default;
        /** */ public static BuildingType Mechanical_Pump => default;
        /** */ public static BuildingType Rotary_Pump => default;
        /** */ public static BuildingType Thermal_Pump => default;
        /** */ public static BuildingType Conduit => default;
        /** */ public static BuildingType Pulse_Conduit => default;
        /** */ public static BuildingType Plated_Conduit => default;
        /** */ public static BuildingType Liquid_Router => default;
        /** */ public static BuildingType Liquid_Tank => default;
        /** */ public static BuildingType Liquid_Junction => default;
        /** */ public static BuildingType Bridge_Conduit => default;
        /** */ public static BuildingType Phase_Conduit => default;
        /** */ public static BuildingType Power_Node => default;
        /** */ public static BuildingType Power_Node_Large => default;
        /** */ public static BuildingType Surge_Tower => default;
        /** */ public static BuildingType Diode => default;
        /** */ public static BuildingType Battery => default;
        /** */ public static BuildingType Battery_Large => default;
        /** */ public static BuildingType Combustion_Generator => default;
        /** */ public static BuildingType Thermal_Generator => default;
        /** */ public static BuildingType Steam_Generator => default;
        /** */ public static BuildingType Differential_Generator => default;
        /** */ public static BuildingType RTG_Generator => default;
        /** */ public static BuildingType Solar_Panel => default;
        /** */ public static BuildingType Solar_Panel_Large => default;
        /** */ public static BuildingType Thorium_Reactor => default;
        /** */ public static BuildingType Impact_Reactor => default;
        /** */ public static BuildingType Mechanical_Drill => default;
        /** */ public static BuildingType Pneumatic_Drill => default;
        /** */ public static BuildingType Laser_Drill => default;
        /** */ public static BuildingType Blast_Drill => default;
        /** */ public static BuildingType Water_Extractor => default;
        /** */ public static BuildingType Cultivator => default;
        /** */ public static BuildingType Oil_Extractor => default;
        /** */ public static BuildingType Core_Shard => default;
        /** */ public static BuildingType Core_Foundation => default;
        /** */ public static BuildingType Core_Nucleus => default;
        /** */ public static BuildingType Vault => default;
        /** */ public static BuildingType Container => default;
        /** */ public static BuildingType Unloader => default;
        /** */ public static BuildingType Duo => default;
        /** */ public static BuildingType Scatter => default;
        /** */ public static BuildingType Scorch => default;
        /** */ public static BuildingType Hail => default;
        /** */ public static BuildingType Wave => default;
        /** */ public static BuildingType Lancer => default;
        /** */ public static BuildingType Arc => default;
        /** */ public static BuildingType Parallax => default;
        /** */ public static BuildingType Swarmer => default;
        /** */ public static BuildingType Salvo => default;
        /** */ public static BuildingType Segment => default;
        /** */ public static BuildingType Tsunami => default;
        /** */ public static BuildingType Fuse => default;
        /** */ public static BuildingType Ripple => default;
        /** */ public static BuildingType Cyclone => default;
        /** */ public static BuildingType Foreshadow => default;
        /** */ public static BuildingType Spectre => default;
        /** */ public static BuildingType Meltdown => default;
        /** */ public static BuildingType Command_Center => default;
        /** */ public static BuildingType Ground_Factory => default;
        /** */ public static BuildingType Air_Factory => default;
        /** */ public static BuildingType Naval_Factory => default;
        /** */ public static BuildingType Additive_Reconstructor => default;
        /** */ public static BuildingType Multiplicative_Reconstructor => default;
        /** */ public static BuildingType Exponential_Reconstructor => default;
        /** */ public static BuildingType Tetrative_Reconstructor => default;
        /** */ public static BuildingType Repair_Point => default;
        /** */ public static BuildingType Repair_Turret => default;
        /** */ public static BuildingType Payload_Conveyor => default;
        /** */ public static BuildingType Payload_Router => default;
        /** */ public static BuildingType Payload_Propulsion_Tower => default;
        /** */ public static BuildingType Power_Source => default;
        /** */ public static BuildingType Power_Void => default;
        /** */ public static BuildingType Item_Source => default;
        /** */ public static BuildingType Item_Void => default;
        /** */ public static BuildingType Liquid_Source => default;
        /** */ public static BuildingType Liquid_Void => default;
        /** */ public static BuildingType Payload_Void => default;
        /** */ public static BuildingType Payload_Source => default;
        /** */ public static BuildingType Illuminator => default;
        /** */ public static BuildingType Launch_Pad => default;
        /** */ public static BuildingType Interplanetary_Accelerator => default;
        /** */ public static BuildingType Message => default;
        /** */ public static BuildingType Switch => default;
        /** */ public static BuildingType Micro_Processor => default;
        /** */ public static BuildingType Logic_Processor => default;
        /** */ public static BuildingType Hyper_Processor => default;
        /** */ public static BuildingType Memory_Cell => default;
        /** */ public static BuildingType Memory_Bank => default;
        /** */ public static BuildingType Logic_Display => default;
        /** */ public static BuildingType Large_Logic_Display => default;
        /** */ public static BuildingType Liquid_Container => default;
        /** */ public static BuildingType Deconstructor => default;
        /** */ public static BuildingType Constructor => default;
        /** */ public static BuildingType Thruster => default;
        /** */ public static BuildingType Large_Constructor => default;
        /** */ public static BuildingType Payload_Loader => default;
        /** */ public static BuildingType Payload_Unloader => default;

        /// <remarks>Only available on V6.<br/>
        /// V7 used storage buildings to resupply units.</remarks>
        public static BuildingType Resupply_Point => default;
    }
}