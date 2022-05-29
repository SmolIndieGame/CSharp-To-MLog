using System.Diagnostics.SymbolStore;

namespace MindustryLogics
{
    /// <summary>
    /// This class contains all unit control operations. They all use <see cref="Mindustry.BindingUnit"/>.<br/>
    /// </summary>
    public static class UnitControl
    {
        /// <summary>
        /// Stop the current bound unit from moving, but keep building or mining. The default state.
        /// </summary>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void Idle() { }

        /// <summary>
        /// Stop the current bound unit completely.
        /// </summary>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void Stop() { }
        
        /// <summary>
        /// Move the current bound unit to the location (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <param name="x">The x coordinate of the location to move to.</param>
        /// <param name="y">The y coordinate of the location to move to.</param>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void Move(double x, double y) { }
        
        /// <summary>
        /// Move the current bound unit to the location (<paramref name="x"/>, <paramref name="y"/>).<br/>
        /// But keep a distance <paramref name="radius"/> from the location.
        /// </summary>
        /// <param name="x">The x coordinate of the location to move to.</param>
        /// <param name="y">The y coordinate of the location to move to.</param>
        /// <param name="radius">The distance to keep away from the location.</param>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void Approach(double x, double y, double radius) { }
        
        /// <summary>
        /// Enable or disable boosting of the current bound unit if it is able to.
        /// </summary>
        /// <param name="enable"><see langword="true"/> to enable boosting, <see langword="false"/> to disable it.</param>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void Boost(bool enable) { }
        
        /// <summary>
        /// Order the current bound unit pathfind to the enemy spawn.
        /// </summary>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void Pathfind() { }
        
        /// <summary>
        /// Order the current bound unit to shoot or aim at location (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <param name="x">The x coordinate to shoot or aim at.</param>
        /// <param name="y">The y coordinate to shoot or aim at.</param>
        /// <param name="shootOrAim">Shoot if <see langword="true"/>, only aim at target otherwise.</param>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void Target(double x, double y, bool shootOrAim) { }
        
        /// <summary>
        /// Order the current bound unit to shoot or aim at <paramref name="target"/>.<br/>
        /// It will try to predict the <paramref name="target"/> movement.
        /// </summary>
        /// <param name="target">The target entity (building or unit) to shoot or aim at.</param>
        /// <param name="shootOrAim">Shoot if <see langword="true"/>, only aim at target otherwise.</param>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void Targetp(Entity target, bool shootOrAim) { }

        /// <summary>
        /// Order the current bound unit to drop its items to building <paramref name="to"/>.<br/>
        /// The current bound unit must be near the building for this to work.
        /// </summary>
        /// <param name="to">The building to drop items to.</param>
        /// <param name="amount">The amount of items to drop.</param>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void ItemDrop(Building to, int amount) { }
        
        /// <summary>
        /// Order the current bound unit to take items from building <paramref name="from"/>.<br/>
        /// The current bound unit must be near the building for this to work.
        /// </summary>
        /// <param name="from">The building to take items from.</param>
        /// <param name="type">The type of item to take.</param>
        /// <param name="amount">The amount of items to take.</param>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void ItemTake(Building from, ItemType type, int amount) { }
        
        /// <summary>
        /// Order the current bound unit to drop the current payload (building or unit the current bound unit is carrying) at its current location.<br/>
        /// </summary>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void PayDrop() { }
        
        /// <summary>
        /// Order the current bound unit to carry a building or unit that is under its current location.<br/>
        /// </summary>
        /// <param name="takeUnits">Take only units if <see langword="true"/>, take only buildings otherwise.</param>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void PayTake(bool takeUnits) { }

        /// <summary>
        /// <para>Order the current bound unit to enter a building that is under its current location if that building accept units as input.</para>
        /// <para>Not available in V6.</para>
        /// </summary>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void PayEnter() { }

        /// <summary>
        /// Order the current bound unit to mine at the location (<paramref name="x"/>, <paramref name="y"/>) if it is a exposed ore tile that the unit can mine.
        /// </summary>
        /// <param name="x">The x coordinate to mine at.</param>
        /// <param name="y">The y coordinate to mine at.</param>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void Mine(double x, double y) { }

        /// <summary>
        /// Flag the current bound unit with <paramref name="value"/>.<br/>
        /// The flag is only useful for logics.
        /// </summary>
        /// <param name="value">The numeric value to flag the unit with.</param>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void Flag(double value) { }

        /// <summary>
        /// Order the current bound unit to build a building of type <paramref name="buildingType"/> at location (<paramref name="x"/>, <paramref name="y"/>) if the unit can build.
        /// </summary>
        /// <param name="x">The x coordinate to build at.</param>
        /// <param name="y">The y coordinate to build at.</param>
        /// <param name="buildingType">The type of the building to build.</param>
        /// <param name="rotation">
        /// The roataion of the building to build.<br/>
        /// <b>For buildings that have directions:</b>
        /// <list type="bullet">
        /// <item><term><b>0</b> </term> <description>faces right</description></item>
        /// <item><term><b>1</b> </term> <description>faces up</description></item>
        /// <item><term><b>2</b> </term> <description>faces left</description></item>
        /// <item><term><b>3</b> </term> <description>faces down</description></item>
        /// </list>
        /// <b>For turrets and units:</b>
        /// <list type="bullet">
        /// <listheader>It is an angle start from 0 to 360.</listheader>
        /// <item><term><b>0</b> </term> <description>faces right</description></item>
        /// <item><term><b>90</b> </term> <description>faces up</description></item>
        /// <item><term><b>180</b> </term> <description>faces left</description></item>
        /// <item><term><b>270</b> </term> <description>faces down</description></item>
        /// </list>
        /// </param>
        /// <param name="config">
        /// The building to copy from.<br/>
        /// If the type of the building <paramref name="config"/> is same as <paramref name="buildingType"/>,<br/>
        /// then the building to build will have the same properties as <paramref name="config"/>.<br/>
        /// It is similar to a player pressing "f" on the building <paramref name="config"/> and placing it on (<paramref name="x"/>, <paramref name="y"/>).
        /// </param>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void Build(double x, double y, BuildingType buildingType, double rotation, Building config) { }

        /// <summary>
        /// <para>
        /// Get the block information at location (<paramref name="x"/>, <paramref name="y"/>).<br/>
        /// The current bound unit must be in range of the location.
        /// </para>
        /// <para>
        /// Example:<br/>
        /// <c>UnitControl.GetBlock(x, y, out t, out b)</c><br/>
        /// This will get the block information at (x, y) and store the type of the block in t, the building in b.<br/>
        /// (You need to define t and b prior to calling this method)
        /// </para>
        /// </summary>
        /// <param name="x">The x coordinate to get information from.</param>
        /// <param name="y">The y coordinate to get information from.</param>
        /// <param name="type">The type of the block at location (<paramref name="x"/>, <paramref name="y"/>).<br/>Solid non-buildings (environment walls) will have type <see cref="BuildingType.Solid"/>.</param>
        /// <param name="building">The building at location (<paramref name="x"/>, <paramref name="y"/>).<br/>It can be null if there is no block or the block is an environment wall.</param>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void GetBlock(double x, double y, out BuildingType type, out Building building) { type = default; building = default; }

        /// <summary>
        /// Is the current bound unit within the circle center at (<paramref name="x"/>, <paramref name="y"/>), radius <paramref name="r"/>?
        /// </summary>
        /// <param name="x">The x coordinate of the center of the circle.</param>
        /// <param name="y">The y coordinate of the center of the circle.</param>
        /// <param name="r">The radius of the circle.</param>
        /// <returns><see langword="true"/> if the unit is within the circle, <see langword="false"/> otherwise.</returns>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static bool IsWithin(double x, double y, double r) => default;

        /// <summary>
        /// Unbind the current bound unit, making this logic no longer able to manipulate this unit.<br/>
        /// Normally, an unit controlled by a processor will unbind after some time if the processor is no longer controlling it.<br/>
        /// Calling this function will make the unit switch back to using the default unit AI instantly.
        /// <para>Not available on V6.</para>
        /// </summary>
        /// <inheritdoc cref="Mindustry.UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static void Unbind() { }
    }
}
