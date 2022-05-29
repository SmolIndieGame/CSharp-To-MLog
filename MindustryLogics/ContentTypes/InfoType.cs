namespace MindustryLogics
{
    /// <summary>
    /// The information type of information of buildings and units.<br/>
    /// It is used in <see cref="Mindustry.Sensor(Entity, InfoType)"/>.<br/>
    /// But you can also get the information by accessing the members of a entity.
    /// </summary>
    public abstract class InfoType : CamelCaseType
    {
        /// <summary>
        /// The default value, do not use.
        /// </summary>
        public const InfoType None = default;
        /// <summary>
        /// The total number of items inside the entity.
        /// </summary>
        public static InfoType TotalItems => default;
        /// <summary>
        /// The type of the first item inside the entity.
        /// </summary>
        public static InfoType FirstItem => default;
        /// <summary>
        /// The total amount of liquids inside the building.
        /// </summary>
        public static InfoType TotalLiquids => default;
        /// <summary>
        /// The amount of power of the building.<br/>
        /// This number indicates how full the power bar in the ingame UI is.<br/>
        /// 0 when the building has no power, 1 when the building has full power.
        /// </summary>
        public static InfoType TotalPower => default;
        /// <summary>
        /// The maximum item capacity of the entity.
        /// </summary>
        public static InfoType ItemCapacity => default;
        /// <summary>
        /// The maximum liquid capacity of the building.
        /// </summary>
        public static InfoType LiquidCapacity => default;
        /// <summary>
        /// The maximum power the building can store.<br/>
        /// This only works on batteries.
        /// </summary>
        public static InfoType PowerCapacity => default;
        /// <summary>
        /// The total power stored in all the batteries of the entire power network connected to the building.
        /// </summary>
        public static InfoType PowerNetStored => default;
        /// <summary>
        /// The maximum power capacity of all the batteries of the entire power network connected to the building.
        /// </summary>
        public static InfoType PowerNetCapacity => default;
        /// <summary>
        /// The power generated per second of all the power generators of the entire power network connected to the building.
        /// </summary>
        public static InfoType PowerNetIn => default;
        /// <summary>
        /// The power consumed per second of all the power consumers of the entire power network connected to the building.
        /// </summary>
        public static InfoType PowerNetOut => default;
        /// <summary>
        /// The number of ammo left inside the entity.
        /// </summary>
        public static InfoType Ammo => default;
        /// <summary>
        /// The maximum number of ammo the entity can hold.
        /// </summary>
        public static InfoType AmmoCapacity => default;
        /// <summary>
        /// The health of the entity (NOT in percentage of MaxHealth).
        /// </summary>
        public static InfoType Health => default;
        /// <summary>
        /// The maximum health of the entity.
        /// </summary>
        public static InfoType MaxHealth => default;
        /// <summary>
        /// The heat of the building. (How red is the thorium reactor)<br/>
        /// 0 if no heat, 1 if it is exploding.
        /// </summary>
        public static InfoType Heat => default;
        /// <summary>
        /// The efficiency of the building.<br/>
        /// water extractors, thermal generators... put on different tiles will have different efficiency.<br/>
        /// How full the power bar in the ingame UI is will affect the building's efficiency.
        /// 0 if it is 0% efficient, 1.5 if it is 150% efficient.
        /// </summary>
        public static InfoType Efficiency => default;
        /// <summary>
        /// Production, turret reload or contruction progress of the building.
        /// From 0 to 1.
        /// Not available on V6.
        /// </summary>
        public static InfoType Progress => default;
        /// <summary>
        /// The timescale of the building.<br/>
        /// Normally it is 1, but overdrive projector and overdrive dome can increase that number.
        /// </summary>
        public static InfoType Timescale => default;
        /// <summary>
        /// The roataion of the entity.<br/>
        /// <b>For buildings that have directions:</b>
        /// <list type="bullet">
        /// <item><term><b>0</b> </term> <description>faces right</description></item>
        /// <item><term><b>1</b> </term> <description>faces up</description></item>
        /// <item><term><b>2</b> </term> <description>faces left</description></item>
        /// <item><term><b>3</b> </term> <description>faces down</description></item>
        /// </list>
        /// <b>For turrets and units:</b>
        /// <list type="bullet">
        /// <listheader>It is an angle range from 0 to 360.</listheader>
        /// <item><term><b>0</b> </term> <description>faces right</description></item>
        /// <item><term><b>90</b> </term> <description>faces up</description></item>
        /// <item><term><b>180</b> </term> <description>faces left</description></item>
        /// <item><term><b>270</b> </term> <description>faces down</description></item>
        /// </list>
        /// </summary>
        public static InfoType Rotation => default;
        /// <summary>
        /// The x coordinate of the entity.
        /// </summary>
        public static InfoType X => default;
        /// <summary>
        /// The y coordinate of the entity.
        /// </summary>
        public static InfoType Y => default;
        /// <summary>
        /// The x coordinate of the aiming position of the entity.
        /// </summary>
        public static InfoType ShootX => default;
        /// <summary>
        /// The y coordinate of the aiming position of the entity.
        /// </summary>
        public static InfoType ShootY => default;
        /// <summary>
        /// The size of the entity.
        /// For units, this is the hitbox size.
        /// For buildings, this is the squared root of the total tiles this building occupied.
        /// </summary>
        public static InfoType Size => default;
        /// <summary>
        /// Whether the entity is dead or no longer valid.
        /// </summary>
        public static InfoType Dead => default;
        /// <summary>
        /// The radius of the detection circle of the entity.
        /// The entity can only detect the presence of other units that are within this circle.
        /// </summary>
        /// <remarks>This range is NOT the maximum attack range of the entity.</remarks>
        public static InfoType Range => default;
        /// <summary>
        /// Whether the entity is shooting or not.
        /// </summary>
        public static InfoType Shooting => default;
        /// <summary>
        /// Whether the entity is boosting or not.<br/>
        /// Boosting is only available for support ground units (except corvus).
        /// </summary>
        public static InfoType Boosting => default;
        /// <summary>
        /// The x coordinate of the mining position of the unit.
        /// </summary>
        public static InfoType MineX => default;
        /// <summary>
        /// The y coordinate of the mining position of the unit.
        /// </summary>
        public static InfoType MineY => default;
        /// <summary>
        /// Whether the unit is mining or not.
        /// </summary>
        public static InfoType Mining => default;
        /// <summary>
        /// The team of the entity.<br/>
        /// <list type="bullet">
        /// 
        /// </list>
        /// </summary>
        public static InfoType Team => default;
        /// <summary>
        /// The type of the entity.
        /// </summary>
        public static InfoType Type => default;
        /// <summary>
        /// The flag of the unit.<br/>
        /// This value is only used by logics.
        /// </summary>
        public static InfoType Flag => default;
        /// <summary>
        /// What is controlling the entity.<br/>
        /// See <see cref="ControlType"/> for more detail.
        /// </summary>
        public static InfoType Controlled => default;
        /// <summary>
        /// Who is controlling the entity.<br/>
        /// If the controller is a processor, returns that processor.<br/>
        /// If the controller is a player or the entity is in a formation controlled by that player, returns that player.<br/>
        /// Otherwise, returns the entity itself.
        /// </summary>
        public static InfoType Controller => default;
        /// <summary>
        /// The name of the player.<br/>
        /// This only works if the entity is a player.
        /// </summary>
        public static InfoType Name => default;
        /// <summary>
        /// How many payload (entities being carried) are in the entity.
        /// </summary>
        public static InfoType PayloadCount => default;
        /// <summary>
        /// The type of the first to drop payload (entities being carried) of the entity.<br/>
        /// Payload behave like a stack, the most recent acquired one will be dropped first.
        /// </summary>
        public static InfoType PayloadType => default;
        /// <summary>
        /// Whether the building is enabled or not.
        /// </summary>
        public static InfoType Enabled => default;
        /// <summary>
        /// <para>The configuration of the building.</para>
        /// <para>For <see cref="BuildingType.Sorter"/>, <see cref="BuildingType.Inverted_Sorter"/>, <see cref="BuildingType.Unloader"/>,<br/>
        /// this is an <see cref="ItemType"/> that indicate which item to sort or unload.</para>
        /// <para>For <see cref="BuildingType.Ground_Factory"/>, <see cref="BuildingType.Air_Factory"/>, <see cref="BuildingType.Naval_Factory"/>,<br/>
        /// this is an <see cref="UnitType"/> that indicate which unit to produce.</para>
        /// </summary>
        public static InfoType Config => default;
    }
}
