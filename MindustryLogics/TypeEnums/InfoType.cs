namespace MindustryLogics
{
    /// <summary>
    /// The information type of information of buildings and units.<br/>
    /// It is used in <see cref="Mindustry.Sensor(Entity, InfoType)"/>.<br/>
    /// But you can also get the information by accessing the members of a entity.
    /// </summary>
    public enum InfoType
    {
        /// <summary>
        /// The default value, do not use.
        /// </summary>
        None,
        /// <summary>
        /// The total number of items inside the entity.
        /// </summary>
        TotalItems,
        /// <summary>
        /// The type of the first item inside the entity.
        /// </summary>
        FirstItem,
        /// <summary>
        /// The total amount of liquids inside the building.
        /// </summary>
        TotalLiquids,
        /// <summary>
        /// The amount of power of the building.<br/>
        /// This number indicates how full the power bar in the ingame UI is.<br/>
        /// 0 when the building has no power, 1 when the building has full power.
        /// </summary>
        TotalPower,
        /// <summary>
        /// The maximum item capacity of the entity.
        /// </summary>
        ItemCapacity,
        /// <summary>
        /// The maximum liquid capacity of the building.
        /// </summary>
        LiquidCapacity,
        /// <summary>
        /// The maximum power the building can store.<br/>
        /// This only works on batteries.
        /// </summary>
        PowerCapacity,
        /// <summary>
        /// The total power stored in all the batteries of the entire power network connected to the building.
        /// </summary>
        PowerNetStored,
        /// <summary>
        /// The maximum power capacity of all the batteries of the entire power network connected to the building.
        /// </summary>
        PowerNetCapacity,
        /// <summary>
        /// The power generated per second of all the power generators of the entire power network connected to the building.
        /// </summary>
        PowerNetIn,
        /// <summary>
        /// The power consumed per second of all the power consumers of the entire power network connected to the building.
        /// </summary>
        PowerNetOut,
        /// <summary>
        /// The number of ammo left inside the entity.
        /// </summary>
        Ammo,
        /// <summary>
        /// The maximum number of ammo the entity can hold.
        /// </summary>
        AmmoCapacity,
        /// <summary>
        /// The health of the entity (NOT in percentage of MaxHealth).
        /// </summary>
        Health,
        /// <summary>
        /// The maximum health of the entity.
        /// </summary>
        MaxHealth,
        /// <summary>
        /// The heat of the building. (How red is the thorium reactor)<br/>
        /// 0 if no heat, 1 if it is exploding.
        /// </summary>
        Heat,
        /// <summary>
        /// The efficiency of the building.<br/>
        /// water extractors, thermal generators... put on different tiles will have different efficiency.<br/>
        /// How full the power bar in the ingame UI is will affect the building's efficiency.
        /// 0 if it is 0% efficient, 1.5 if it is 150% efficient.
        /// </summary>
        Efficiency,
        /// <summary>
        /// Production, turret reload or contruction progress of the building.
        /// From 0 to 1.
        /// Not available on V6.
        /// </summary>
        Progress,
        /// <summary>
        /// The timescale of the building.<br/>
        /// Normally it is 1, but overdrive projector and overdrive dome can increase that number.
        /// </summary>
        Timescale,
        /// <summary>
        /// The roataion of the entity.<br/>
        /// <b>For buildings that have directions:</b>
        /// <list type="bullet">
        /// <item><term>0</term> <description>faces right</description></item>
        /// <item><term>1</term> <description>faces up</description></item>
        /// <item><term>2</term> <description>faces left</description></item>
        /// <item><term>3</term> <description>faces down</description></item>
        /// </list>
        /// <b>For turrets and units:</b>
        /// <list type="bullet">
        /// <listheader>It is an angle range from 0 to 360.</listheader>
        /// <item><term>0</term> <description>faces right</description></item>
        /// <item><term>90</term> <description>faces up</description></item>
        /// <item><term>180</term> <description>faces left</description></item>
        /// <item><term>270</term> <description>faces down</description></item>
        /// </list>
        /// </summary>
        Rotation,
        /// <summary>
        /// The x coordinate of the entity.
        /// </summary>
        X,
        /// <summary>
        /// The y coordinate of the entity.
        /// </summary>
        Y,
        /// <summary>
        /// The x coordinate of the aiming position of the entity.
        /// </summary>
        ShootX,
        /// <summary>
        /// The y coordinate of the aiming position of the entity.
        /// </summary>
        ShootY,
        /// <summary>
        /// The size of the entity.
        /// For units, this is the hitbox size.
        /// For buildings, this is the squared root of the total tiles this building occupied.
        /// </summary>
        Size,
        /// <summary>
        /// Whether the entity is dead or no longer valid.
        /// </summary>
        Dead,
        /// <summary>
        /// The radius of the detection circle of the entity.
        /// The entity can only detect the presence of other units that are within this circle.
        /// </summary>
        /// <remarks>This range is NOT the maximum attack range of the entity.</remarks>
        Range,
        /// <summary>
        /// Whether the entity is shooting or not.
        /// </summary>
        Shooting,
        /// <summary>
        /// Whether the entity is boosting or not.<br/>
        /// Boosting is only available for support ground units (except corvus).
        /// </summary>
        Boosting,
        /// <summary>
        /// The x coordinate of the mining position of the unit.
        /// </summary>
        MineX,
        /// <summary>
        /// The y coordinate of the mining position of the unit.
        /// </summary>
        MineY,
        /// <summary>
        /// Whether the unit is mining or not.
        /// </summary>
        Mining,
        /// <summary>
        /// The team of the entity.<br/>
        /// <list type="bullet">
        /// 
        /// </list>
        /// </summary>
        Team,
        /// <summary>
        /// The type of the entity.
        /// </summary>
        Type,
        /// <summary>
        /// The flag of the unit.<br/>
        /// This value is only used by logics.
        /// </summary>
        Flag,
        /// <summary>
        /// What is controlling the entity.<br/>
        /// See <see cref="ControlType"/> for more detail.
        /// </summary>
        Controlled,
        /// <summary>
        /// Who is controlling the entity.<br/>
        /// If the controller is a processor, returns that processor.<br/>
        /// If the controller is a player or the entity is in a formation controlled by that player, returns that player.<br/>
        /// Otherwise, returns the entity itself.
        /// </summary>
        Controller,
        /// <summary>
        /// The name of the player.<br/>
        /// This only works if the entity is a player.
        /// </summary>
        Name,
        /// <summary>
        /// How many payload (entities being carried) are in the entity.
        /// </summary>
        PayloadCount,
        /// <summary>
        /// The type of the first to drop payload (entities being carried) of the entity.<br/>
        /// Payload behave like a stack, the most recent acquired one will be dropped first.
        /// </summary>
        PayloadType,
        /// <summary>
        /// Whether the building is enabled or not.
        /// </summary>
        Enabled,
        /// <summary>
        /// <para>The configuration of the building.</para>
        /// <para>For <see cref="BuildingType.Sorter"/>, <see cref="BuildingType.Inverted_Sorter"/>, <see cref="BuildingType.Unloader"/>,<br/>
        /// this is an <see cref="ItemType"/> that indicate which item to sort or unload.</para>
        /// <para>For <see cref="BuildingType.Ground_Factory"/>, <see cref="BuildingType.Air_Factory"/>, <see cref="BuildingType.Naval_Factory"/>,<br/>
        /// this is an <see cref="UnitType"/> that indicate which unit to produce.</para>
        /// </summary>
        Config
    }
}
