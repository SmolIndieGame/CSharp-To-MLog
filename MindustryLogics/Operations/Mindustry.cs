namespace MindustryLogics
{
    /// <summary>
    /// Static class that contains most Mindustry instructions.
    /// </summary>
    public static class Mindustry
    {
        /// <summary>
        /// A <see cref="Building"/> representing the processor that is executing this code.
        /// </summary>
        public static Building This { get; }
        /// <summary>
        /// The x coordinate of this processor.
        /// </summary>
        public static double ThisX { get; }
        /// <summary>
        /// The y coordinate of the processor.
        /// </summary>
        public static double ThisY { get; }
        /// <summary>
        /// The number of instructions executed per tick (60 ticks/second).
        /// <list type="bullet">
        /// <item><term><b>Micro Processor</b> </term> <description>2</description></item>
        /// <item><term><b>Logic Processor</b> </term> <description>8</description></item>
        /// <item><term><b>Hyper Processor</b> </term> <description>25</description></item>
        /// </list>
        /// </summary>
        public static double IPT { get; }
        /// <summary>
        /// The number of buildings linked to this processor.
        /// </summary>
        public static int LinksCount { get; }
        /// <summary>
        /// <para>
        /// The current bound <see cref="Unit"/>. It changes when <see cref="UnitBind(UnitType)"/> is called,<br/>
        /// or set to null when <see cref="UnitControl.Unbind"/> is called.
        /// </para>
        /// <para>Logics can only manipulate the current bound unit (control, radar...).<br/>
        /// However, to sensor (get information about) a unit,<br/>
        /// you can reference the property inside that <see cref="Unit"/>.<br/>
        /// Example: <c>u.Flag</c> will returns the flag of the unit u,<br/>
        /// but <c>u.Move(...)</c> is not allowed.</para>
        /// <para>If you want to move the current bound unit, use <see cref="UnitControl.Move(double, double)"/>.</para>
        /// </summary>
        public static Unit BindingUnit { get; }
        /// <summary>
        /// The current UNIX timestamp in milliseconds.
        /// </summary>
        public static long Time { get; }
        /// <summary>
        /// The amount of ticks (60 ticks/second) since the start of the map.
        /// </summary>
        public static double Ticks { get; }
        /// <summary>
        /// Width of the map, in tiles.
        /// </summary>
        public static double MapWidth { get; }
        /// <summary>
        /// Height of the map, in tiles.
        /// </summary>
        public static double MapHeight { get; }
        /// <summary>
        /// Get the total count of <see cref="UnitType"/>.<br/>
        /// Useful for loops through all the <see cref="UnitType"/> with <see cref="Lookup(Content, int)"/>.
        /// </summary>
        /// <remarks>Not available on V6.</remarks>
        public static int UnitCount { get; }
        /// <summary>
        /// Get the total count of <see cref="ItemType"/>.<br/>
        /// Useful for loops through all the <see cref="ItemType"/> with <see cref="Lookup(Content, int)"/>.
        /// </summary>
        /// <remarks>Not available on V6.</remarks>
        public static int ItemCount { get; }
        /// <summary>
        /// Get the total count of <see cref="LiquidType"/>.<br/>
        /// Useful for loops through all the <see cref="LiquidType"/> with <see cref="Lookup(Content, int)"/>.
        /// </summary>
        /// <remarks>Not available on V6.</remarks>
        public static int LiquidCount { get; }
        /// <summary>
        /// Get the total count of <see cref="BuildingType"/>.<br/>
        /// Useful for loops through all the <see cref="BuildingType"/> with <see cref="Lookup(Content, int)"/>.
        /// </summary>
        /// <remarks>Not available on V6.</remarks>
        public static int BlockCount { get; }
        /// <inheritdoc cref="BlockCount"/>
        public static int BuildingCount { get; }

        /// <summary>
        /// Read a number from a linked memory cell or bank.
        /// </summary>
        /// <param name="memoryCell">The memory cell to read from.</param>
        /// <param name="index">The index of the number to read from.</param>
        /// <returns></returns>
        /// <remarks>Memory cells or banks can only store numeric values.</remarks>
        public static double Read(Building memoryCell, int index) => default;

        /// <summary>
        /// Write a number to a linked memory cell or bank.<br/>
        /// Note that this method accepts <paramref name="value"/> as objects just to make it easier to work with booleans.<br/>
        /// Any object passed to this method will be converted to the numeric representation of that object.
        /// </summary>
        /// <param name="memoryCell">The memory cell to write to.</param>
        /// <param name="index">The index of the number to write to.</param>
        /// <param name="value">The number to write to the memory cell.</param>
        /// <inheritdoc cref="Read(Building, int)" path="/remarks"/>
        public static void Write(object value, Building memoryCell, int index) { }

        /// <summary>
        /// Use methods inside <see cref="Drawing"/> instead.
        /// </summary>
        /// <param name="args"></param>
        [System.Obsolete("See the summary of this method for more detail.", true)]
        public static void Draw(params object[] args) { }

        /// <summary>
        /// Flush queued <see cref="Draw(object[])"/> operations to a display with suffix <paramref name="index"/>.<br/>
        /// Example: <c>DrawFlush(2)</c> will print to the display linked to this processor named display2.
        /// </summary>
        /// <param name="index">The index of the linked display. (starts at 1)<br/>Only constant <see cref="int"/> is accepted.</param>
        /// <remarks>Only constant values are allowed to pass into this function.</remarks>
        public static void DrawFlush(int index) { }

        /// <summary>
        /// Flush queued <see cref="Draw(object[])"/> operations to a display.
        /// </summary>
        /// <param name="block">The display block to draw to.</param>
        public static void DrawFlush(Building block) { }

        /// <summary>
        /// Add texts to the print buffer.<br/>
        /// To print multiple values at once, use comma to separate them.<br/>
        /// Example: <c>Print(value1, value2, value3)</c>
        /// </summary>
        /// <param name="values"></param>
        /// <remarks>Does not do anything until <see cref="PrintFlush(Building)"/> or <see cref="PrintFlush(int)"/> is used.</remarks>
        public static void Print(params object[] values) { }

        /// <summary>
        /// Flush queued <see cref="Print(object[])"/> operations to a message block with suffix <paramref name="index"/>.<br/>
        /// Example: <c>PrintFlush(2)</c> will print to the message block linked to this processor named message2.
        /// </summary>
        /// <param name="index">The index of the linked message block. (starts at 1)<br/>Only constant <see cref="int"/> is accepted.</param>
        /// <remarks>Only constant values are allowed to pass into this function.</remarks>
        public static void PrintFlush(int index) { }

        /// <summary>
        /// Flush queued <see cref="Print(object[])"/> operations to a message block.
        /// </summary>
        /// <param name="block">The message block to print to.</param>
        public static void PrintFlush(Building block) { }

        /// <summary>
        /// Get a linked building by its index. (starts at 0)
        /// </summary>
        /// <param name="index">The index of the building.</param>
        /// <returns></returns>
        public static Building GetLink(int index) => default;

        /// <summary>
        /// <para>Get a linked building by its name.</para>
        /// <para><c><see cref="Mindustry"/>.GetLink(<see cref="BuildingType.Force_Projector"/>, 2)</c> will return projector2.</para>
        /// </summary>
        /// <param name="buildingType">The type of the building.<br/>User defined variable is not accepted here.</param>
        /// <param name="index">The index of the linked building. (starts at 1)<br/>Only constant <see cref="int"/> is accepted.</param>
        /// <returns></returns>
        /// <remarks>Only constant values are allowed to pass into this function.</remarks>
        public static LinkedBuilding GetLink(BuildingType buildingType, int index) => default;

        /// <summary>
        /// <para>
        /// Create a custom type from <paramref name="typeName"/>.<br/>
        /// Usually for types that are in newer versions or in mods.<br/>
        /// Cast the result to either <see cref="BuildingType"/>, <see cref="UnitType"/>, <see cref="ItemType"/> or <see cref="LiquidType"/>.
        /// </para>
        /// <para>
        /// <c><see cref="Mindustry"/>.CustomType("Exam_ple")</c> will return @Exam_ple
        /// </para>
        /// </summary>
        /// <param name="typeName">The name of the type.<br/>Only constant <see cref="string"/> is accepted.</param>
        /// <returns>The type created from <paramref name="typeName"/>.</returns>
        /// <remarks>The return type automatically include the @ sign.<br/>Only constant values are allowed to pass into this function.</remarks>
        public static object CustomType(string typeName) => default;

        /*
        /// <summary>
        /// Get the commonly used icon of type <paramref name="type"/>.<br/>
        /// For more icons use the string literal "\uXXXX" where XXXX is a hex number representing a Unicode character.<br/>
        /// For example, "\uF800" is the icon of the unit Dagger.<br/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <remarks>Only constant values are allowed to pass into this function.</remarks>
        public static string GetIcon(ItemType type) => default;

        /// <inheritdoc cref="GetIcon(ItemType)"/>
        public static string GetIcon(LiquidType type) => default;

        /// <inheritdoc cref="GetIcon(ItemType)"/>
        public static string GetIcon(UnitType type) => default;

        /// <inheritdoc cref="GetIcon(ItemType)"/>
        public static string GetIcon(BuildingType type) => default;
        /// <inheritdoc cref="GetIcon(ItemType)"/>
        public static string GetIcon(StatusType type) => default;
        */

        /// <summary>
        /// Get the information of type <paramref name="info"/> about an <see cref="Entity"/>.<br/>
        /// Alias of <see cref="Entity.GetInfo(InfoType)"/>
        /// </summary>
        /// <param name="entity">The eneity to get information form.</param>
        /// <param name="info">The type of information to get.</param>
        /// <returns></returns>
        /// <remarks>Since the return type of this method is an object, it is strongly recommended to access the properties of the entity instead.</remarks>
        public static object Sensor(Entity entity, InfoType info) => default;

        /// <summary>
        /// Get the number of items of type <paramref name="item"/> in an <see cref="Entity"/>.<br/>
        /// Alias of <see cref="Entity.GetQuantityOf(ItemType)"/>
        /// </summary>
        /// <param name="entity">The eneity to get information form.</param>
        /// <param name="item">The type of the item.</param>
        /// <returns></returns>
        public static int Sensor(Entity entity, ItemType item) => default;

        /// <summary>
        /// Get the amount of liquid of type <paramref name="liquid"/> in an <see cref="Entity"/>.<br/>
        /// Alias of <see cref="Entity.GetQuantityOf(LiquidType)"/>
        /// </summary>
        /// <param name="entity">The eneity to get information form.</param>
        /// <param name="liquid">The type of the liquid.</param>
        /// <returns></returns>
        public static double Sensor(Entity entity, LiquidType liquid) => default;

        /// <summary>
        /// <para>
        /// <b>To control buildings:</b><br/>
        /// <list type="bullet">
        /// <item><term><b>enabled</b> </term> <description><see cref="Building.Enabled"/></description></item>
        /// <item><term><b>shoot</b> </term> <description><see cref="Building.Shoot(double, double, bool)"/></description></item>
        /// <item><term><b>shootp</b> </term> <description><see cref="Building.ShootTarget(Entity, bool)"/></description></item>
        /// <item><term><b>config</b> </term> <description><see cref="Building.Config"/></description></item>
        /// <item><term><b>configure</b> </term> <description><see cref="Building.Configure"/></description></item>
        /// <item><term><b>color</b> </term> <description>unsupported</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// <b>To control units:</b><br/>
        /// <list type="bullet">
        /// <item><description>Use methods inside <see cref="MindustryLogics.UnitControl"/>.</description></item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="args"></param>
        [System.Obsolete("See the summary of this method for more detail.", true)]
        public static void Control(Entity entity, params object[] args) { }

        /// <inheritdoc cref="Control(Entity, object[])"/>
        [System.Obsolete("See the summary of this method for more detail.", true)]
        public static void UnitControl(Entity entity, params object[] args) { }

        /// <summary>
        /// Set <see cref="BindingUnit"/> to a unit of type <paramref name="unitType"/>.<br/>
        /// If the type of the current bound unit is same as <paramref name="unitType"/>, than<br/>
        /// <see cref="BindingUnit"/> will be set to another unit with the same type.
        /// </summary>
        /// <param name="unitType"></param>
        /// <remarks>Repeatedly binds unit with the same <paramref name="unitType"/> will cycle through all units of the same type.</remarks>
        public static void UnitBind(UnitType unitType) { }

        /// <summary>
        /// Set <see cref="BindingUnit"/> to the unit <paramref name="unit"/>.<br/>
        /// So that <paramref name="unit"/> can be manipulated by Logics.<br/>
        /// Useful if you want to manipulate two or more units at the same time.
        /// </summary>
        /// <param name="unit">The ally unit to bind to.</param>
        /// <remarks>This method only works if <paramref name="unit"/> is not null and is an ally.</remarks>
        public static void UnitBind(Unit unit) { }

        /// <summary>
        /// <para>
        /// Locate units around the current bound unit.<br/>
        /// Unable to detect units outside the <see cref="Entity.Range"/> of the current bound unit.
        /// </para>
        /// <para>
        /// For locating units using buildings, use <see cref="Building.Radar(RadarFilter, SortMethod, bool)"/> instead.
        /// </para>
        /// <para>
        /// Example:<br/><c>Mindustry.UnitRadar(Filter.Radar().Enemy, SortMethod.Distance, true)</c><br/>
        /// This will return a enemy that is in range and is closest to the current bound unit (<see cref="BindingUnit"/>).
        /// </para>
        /// </summary>
        /// <param name="target">The target filter.<br/>Use <see cref="Filter.Radar()"/> to create the filter than add constraints into it. (maximum three constraints)<para>Ex:<br/>To target enemy ground unit: <c>Filter.Radar().Enemy.Ground</c><br/>To target anything: <c>Filter.Radar()</c> or <c>null</c></para></param>
        /// <param name="sortMethod">How to sort the results if there are multiple units that are in range and pass the filter.<para>Ex: Sort by health: <c>SortMethod.Health</c></para></param>
        /// <param name="firstOrLast">Returns the first unit in the results sorted by <paramref name="sortMethod"/> if <see langword="true"/>, returns the last unit otherwise.</param>
        /// <returns></returns>
        /// <remarks>This method uses the current bound unit. Bind a unit with <see cref="UnitBind(UnitType)"/> and make sure that <see cref="Mindustry.BindingUnit"/> is not null.</remarks>
        public static Unit UnitRadar(RadarFilter target, SortMethod sortMethod, bool firstOrLast) => default;

        /// <summary>
        /// <para>Find an ore tile of type <paramref name="ore"/> using the current bound unit.<br/>
        /// The return value of this method indicate that such ore tile is found or not.</para>
        /// <para>Example:<br/>
        /// <c>Mindustry.LocateOre(<see cref="ItemType.Copper"/>, out x, out y)</c><br/>
        /// This will locate any copper ore and put its coordinate into variable x and y.<br/>
        /// (You need to define x and y prior to calling this method)</para>
        /// </summary>
        /// <param name="ore">The type of ore to find.</param>
        /// <param name="x">The x coordinate of the ore tile if the ore is found.</param>
        /// <param name="y">The y coordinate of the ore tile if the ore is found.</param>
        /// <returns><see langword="true"/> if the current bound unit found an ore tile of type <paramref name="ore"/>, <see langword="false"/> otherwise.</returns>
        /// <inheritdoc cref="UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static bool LocateOre(ItemType ore, out double x, out double y) => (x = y = default) == default;

        /// <summary>
        /// <para>Find an ally building that is damaged using the current bound unit.<br/>
        /// The return value of this method indicate that such building is found or not.<br/>
        /// If there are multiple damaged buildings, the one with the lowest health will be selected.</para>
        /// </summary>
        /// <param name="x">The x coordinate of the building if it is found.</param>
        /// <param name="y">The x coordinate of the building if it is found.</param>
        /// <param name="building">An ally building that is damaged.</param>
        /// <returns><see langword="true"/> if the current bound unit found an damaged ally building, <see langword="false"/> otherwise.</returns>
        /// <inheritdoc cref="UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static bool LocateDamagedAllyBuilding(out double x, out double y, out Building building) => (x = y = default).Equals(building = default);

        /// <summary>
        /// Find an enemy spawn point using the current bound unit.<br/>
        /// The return value of this method indicate that such point is found or not.<br/>
        /// An enemy spawn point may be a location or enemy core, so even if that point is found, the <paramref name="building"/> may be <see langword="null"/>.
        /// </summary>
        /// <param name="x">The x coordinate of the spawn point if it is found.</param>
        /// <param name="y">The y coordinate of the spawn point if it is found.</param>
        /// <param name="building">The enemy core that spawns enemies if there is one.</param>
        /// <returns><see langword="true"/> if the current bound unit found an enemy spawn point, <see langword="false"/> otherwise.</returns>
        /// <inheritdoc cref="UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static bool LocateEnemySpawn(out double x, out double y, out Building building) => (x = y = default).Equals(building = default);

        /// <summary>
        /// <para>Find a building that belongs to <paramref name="buildingGroup"/> using the current bound unit.<br/>
        /// The return value of this method indicate that such building is found or not.</para>
        /// <para>Example:<br/>
        /// <c>Mindustry.LocateBuilding(<see cref="BuildingGroup.Core"/>, false, out x, out y, out b)</c>
        /// This will locate any ally core and put its coordinate into variable x and y, the actually core to b.<br/>
        /// (You need to define x, y and b prior to calling this method)</para>
        /// </summary>
        /// <param name="buildingGroup">The required buildings group (a way to categorize buildings) to find.<para>Example:<br/>To find turrets: <c><see cref="BuildingGroup.Turret"/></c></para></param>
        /// <param name="isEnemy">Find a building that belongs to the enemy if <see langword="true"/>, find a building that belongs to the ally if <see langword="false"/>.</param>
        /// <param name="x">The x coordinate of the building if it is found.</param>
        /// <param name="y">The y coordinate of the building if it is found.</param>
        /// <param name="building">A building that belongs to <paramref name="buildingGroup"/>.</param>
        /// <returns><see langword="true"/> if the current bound unit found the building, <see langword="false"/> otherwise.</returns>
        /// /// <inheritdoc cref="UnitRadar(RadarFilter, SortMethod, bool)" path="/remarks"/>
        public static bool LocateBuilding(BuildingGroup buildingGroup, bool isEnemy, out double x, out double y, out Building building) => (x = y = default).Equals(building = default);

        /// <summary>
        /// Wait a certain number of seconds.<br/>
        /// When the processor enter this line, it will stop (the @counter stopped moving) until <paramref name="second"/> seconds is passed.<br/>
        /// Then it will continue to execute the rest of the program.
        /// </summary>
        /// <param name="second"></param>
        /// <remarks>Not available in V6.</remarks>
        public static void Wait(double second) { }

        /// <summary>
        /// <para>
        /// Get the type of a content by its index. (include modded contents)<br/>
        /// Cast the result to either <see cref="BuildingType"/>, <see cref="UnitType"/>, <see cref="ItemType"/> or <see cref="LiquidType"/>.
        /// </para>
        /// <para>
        /// Example:<br/><c>Lookup(Content.Unit, 0)</c><br/>
        /// This will return <see cref="UnitType.Dagger"/>.
        /// </para>
        /// </summary>
        /// <param name="content">What type of content to lookup.<para>Example:<br/>To lookup items: <c>Content.Item</c></para></param>
        /// <param name="index">The index of the content. (starts at 0)</param>
        /// <returns></returns>
        /// <remarks>Not available in V6.</remarks>
        public static object Lookup(Content content, int index) => default;

        /// <summary>
        /// Put <paramref name="script"/> directly into the output result.<br/>
        /// Use for unsupported features or modded content.
        /// </summary>
        /// <param name="script">The custom code.<br/>Only constant <see cref="string"/> is accepted.</param>
        /// <remarks>Only constant values are allowed to pass into this function.</remarks>
        public static void CustomScript(string script) { }

        /// <summary>
        /// Noop stands for no operation. It does nothing but delay the processor by one instruction.<br/>
        /// Maybe you can use it to slow down the processor.
        /// </summary>
        public static void Noop() { }
    }
}