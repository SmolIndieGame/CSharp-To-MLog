namespace MindustryLogics
{
    /// <summary>
    /// <para>This class contains all drawing operations.</para>
    /// <para>
    /// <b>Coordinate inside displays:</b>
    /// <list type="bullet">
    /// <item><description>The size of a Logic Display is 80x80.</description></item>
    /// <item><description>(x = 0, y = 0) is the bottom left corner.</description></item>
    /// <item><description>(x = 80, y = 80) is the upper right corner.</description></item>
    /// <item><description>The size of a Large Logic Display is 176x176.</description></item>
    /// <item><description>(x = 0, y = 0) is the bottom left corner.</description></item>
    /// <item><description>(x = 176, y = 176) is the upper right corner.</description></item>
    /// </list>
    /// </para>
    /// </summary>
    /// <remarks>All operations do nothing until <see cref="Mindustry.DrawFlush(Building)"/> or <see cref="Mindustry.DrawFlush(int)"/> is used.</remarks>
    public static class Drawing
    {
        /// <summary>
        /// Fill the display with color (<paramref name="r"/>, <paramref name="g"/>, <paramref name="b"/>).
        /// </summary>
        /// <param name="r">The amount of red in the color, 0 is no red, and 255 is full red.</param>
        /// <param name="g">The amount of green in the color, 0 is no green, and 255 is full green.</param>
        /// <param name="b">The amount of blue in the color, 0 is no blue, and 255 is full blue.</param>
        public static void Clear(byte r, byte g, byte b) { }

        /// <summary>
        /// Set the color of the pen.<br/>
        /// The pen is used for all drawing operations.
        /// </summary>
        /// <param name="r">The amount of red in the color, 0 is no red, and 255 is full red.</param>
        /// <param name="g">The amount of green in the color, 0 is no green, and 255 is full green.</param>
        /// <param name="b">The amount of blue in the color, 0 is no blue, and 255 is full blue.</param>
        /// <param name="a">The opacity of the color, 0 is completely transparent, and 255 is completely opaque.</param>
        public static void SetColor(byte r, byte g, byte b, byte a) { }

        /// <summary>
        /// Set the width of the pen.<br/>
        /// The pen is used for all drawing operations.<br/>
        /// However, the pen's width has no effect on <see cref="DrawPoly(double, double, long, double, double)"/>, <see cref="DrawRect(double, double, double, double)"/> and <see cref="DrawTriangle(double, double, double, double, double, double)"/>.
        /// </summary>
        /// <param name="width"></param>
        public static void SetStroke(double width) { }

        /// <summary>
        /// Draw a line that starts at (<paramref name="startX"/>, <paramref name="startY"/>) and ends at (<paramref name="endX"/>, <paramref name="endY"/>).
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <remarks>This method uses the pen's width and its color.<br/>Does not do anything until <see cref="Mindustry.DrawFlush(Building)"/> or <see cref="Mindustry.DrawFlush(int)"/> is used.</remarks>
        public static void DrawLine(double startX, double startY, double endX, double endY) { }

        /// <summary>
        /// <para>Draw a rectangle that starts at (<paramref name="startX"/>, <paramref name="startY"/>), with width <paramref name="width"/> and height <paramref name="height"/>.<br/>
        /// The rectangle drawn using this method is solid.</para>
        /// <para>(<paramref name="startX"/>, <paramref name="startY"/>) is the bottom left corner of the rectangle,<br/>
        /// (<paramref name="startX"/> + <paramref name="width"/>, <paramref name="startY"/> + <paramref name="height"/>) is the upper right corner of the rectangle.</para>
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <remarks>This method uses the pen's color.<br/>Does not do anything until <see cref="Mindustry.DrawFlush(Building)"/> or <see cref="Mindustry.DrawFlush(int)"/> is used.</remarks>
        public static void DrawRect(double startX, double startY, double width, double height) { }

        /// <summary>
        /// <para>Draw a rectangle that starts at (<paramref name="startX"/>, <paramref name="startY"/>), with width <paramref name="width"/> and height <paramref name="height"/>.<br/>
        /// The rectangle drawn using this method is hollow.</para>
        /// <para>(<paramref name="startX"/>, <paramref name="startY"/>) is the bottom left corner of the rectangle,<br/>
        /// (<paramref name="startX"/> + <paramref name="width"/>, <paramref name="startY"/> + <paramref name="height"/>) is the upper right corner of the rectangle,<br/>
        /// regardless of the width of the pen.</para>
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <inheritdoc cref="DrawLine(double, double, double, double)" path="/remarks"/>
        public static void DrawLineRect(double startX, double startY, double width, double height) { }

        /// <summary>
        /// Draw a <paramref name="sides"/>-sided polygon. Centered at (<paramref name="centerX"/>, <paramref name="centerY"/>), with radius <paramref name="radius"/>.<br/>
        /// The polygon will be rotated counter-clockwise <paramref name="rotation"/> degrees.<br/>
        /// The polygon drawn using this method is solid.
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="sides"></param>
        /// <param name="radius"></param>
        /// <param name="rotation"></param>
        /// <inheritdoc cref="DrawRect(double, double, double, double)" path="/remarks"/>
        public static void DrawPoly(double centerX, double centerY, long sides, double radius, double rotation) { }

        /// <summary>
        /// Draw a <paramref name="sides"/>-sided polygon. Centered at (<paramref name="centerX"/>, <paramref name="centerY"/>), with radius <paramref name="radius"/>.<br/>
        /// The polygon will be rotated counter-clockwise <paramref name="rotation"/> degrees.<br/>
        /// The width of the pen will effect the size of the polygon.<br/>
        /// The polygon drawn using this method is hollow.
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="sides"></param>
        /// <param name="radius"></param>
        /// <param name="rotation"></param>
        /// <inheritdoc cref="DrawLine(double, double, double, double)" path="/remarks"/>
        public static void DrawLinePoly(double centerX, double centerY, long sides, double radius, double rotation) { }

        /// <summary>
        /// Draw a solid triangle, with vertices at (<paramref name="x1"/>, <paramref name="y1"/>), (<paramref name="x2"/>, <paramref name="y2"/>), (<paramref name="x3"/>, <paramref name="y3"/>).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <inheritdoc cref="DrawRect(double, double, double, double)" path="/remarks"/>
        public static void DrawTriangle(double x1, double y1, double x2, double y2, double x3, double y3) { }

        /// <summary>
        /// Draw an icon of a content of type <paramref name="image"/> centered at (<paramref name="centerX"/>, <paramref name="centerY"/>) with size <paramref name="size"/>.<br/>
        /// The image will be rotated counter-clockwise <paramref name="rotation"/> degrees.<br/>
        /// <paramref name="image"/> can only be either a <see cref="BuildingType"/>, <see cref="UnitType"/>, <see cref="ItemType"/>, <see cref="LiquidType"/>.<br/>
        /// Other types will cause this method to draw a white square with red text "ohno" on it.<br/>
        /// The size of the icon is the same as the diameter of the icon.
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="image"></param>
        /// <param name="size"></param>
        /// <param name="rotation"></param>
        /// <inheritdoc cref="DrawRect(double, double, double, double)" path="/remarks"/>
        public static void DrawImage(double centerX, double centerY, object image, double size, double rotation) { }
    }
}
