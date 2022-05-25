namespace MindustryLogics
{
    /// <summary>
    /// The type of liquid.
    /// </summary>
    public abstract class LiquidType : KebabCaseType
    {
        /** */ public const LiquidType None = default;
        /** */ public static LiquidType Water => default;
        /** */ public static LiquidType Slag => default;
        /** */ public static LiquidType Oil => default;
        /** */ public static LiquidType CryoFluid => default;
    }
}
