namespace MindustryLogics
{
    /// <summary>
    /// The type of a unit.
    /// </summary>
    public abstract class UnitType : KebabCaseType
    {
        /// <summary>
        /// The default value.
        /// </summary>
        public const UnitType None = default;
        /// <summary>
        /// Tier-1 offensive ground unit.
        /// </summary>
        public static UnitType Dagger => default;
        /// <summary>
        /// Tier-2 offensive ground unit.
        /// </summary>
        public static UnitType Mace => default;
        /// <summary>
        /// Tier-3 offensive ground unit.
        /// </summary>
        public static UnitType Fortress => default;
        /// <summary>
        /// Tier-4 offensive ground unit.
        /// </summary>
        public static UnitType Scepter => default;
        /// <summary>
        /// Tier-5 offensive ground unit.
        /// </summary>
        public static UnitType Reign => default;
        /// <summary>
        /// Tier-1 support ground unit.
        /// </summary>
        public static UnitType Nova => default;
        /// <summary>
        /// Tier-2 support ground unit.
        /// </summary>
        public static UnitType Pulsar => default;
        /// <summary>
        /// Tier-3 support ground unit.
        /// </summary>
        public static UnitType Quasar => default;
        /// <summary>
        /// Tier-4 support ground unit.
        /// </summary>
        public static UnitType Vela => default;
        /// <summary>
        /// Tier-5 support ground unit.
        /// </summary>
        public static UnitType Corvus => default;
        /// <summary>
        /// Tier-1 insectoid ground unit.
        /// </summary>
        public static UnitType Crawler => default;
        /// <summary>
        /// Tier-2 insectoid ground unit.
        /// </summary>
        public static UnitType Atrax => default;
        /// <summary>
        /// Tier-3 insectoid ground unit.
        /// </summary>
        public static UnitType Spiroct => default;
        /// <summary>
        /// Tier-4 insectoid ground unit.
        /// </summary>
        public static UnitType Arkyid => default;
        /// <summary>
        /// Tier-5 insectoid ground unit.
        /// </summary>
        public static UnitType Toxopid => default;
        /// <summary>
        /// Tier-1 offensive air unit.
        /// </summary>
        public static UnitType Flare => default;
        /// <summary>
        /// Tier-2 offensive air unit.
        /// </summary>
        public static UnitType Horizon => default;
        /// <summary>
        /// Tier-3 offensive air unit.
        /// </summary>
        public static UnitType Zenith => default;
        /// <summary>
        /// Tier-4 offensive air unit.
        /// </summary>
        public static UnitType Antumbra => default;
        /// <summary>
        /// Tier-5 offensive air unit.
        /// </summary>
        public static UnitType Eclipse => default;
        /// <summary>
        /// Tier-1 support air unit.
        /// </summary>
        public static UnitType Mono => default;
        /// <summary>
        /// Tier-2 support air unit.
        /// </summary>
        public static UnitType Poly => default;
        /// <summary>
        /// Tier-3 support air unit.
        /// </summary>
        public static UnitType Mega => default;
        /// <summary>
        /// Tier-4 support air unit.
        /// </summary>
        public static UnitType Quad => default;
        /// <summary>
        /// Tier-5 support air unit.
        /// </summary>
        public static UnitType Oct => default;
        /// <summary>
        /// Tier-1 offensive naval unit.
        /// </summary>
        public static UnitType Risso => default;
        /// <summary>
        /// Tier-2 offensive naval unit.
        /// </summary>
        public static UnitType Minke => default;
        /// <summary>
        /// Tier-3 offensive naval unit.
        /// </summary>
        public static UnitType Bryde => default;
        /// <summary>
        /// Tier-4 offensive naval unit.
        /// </summary>
        public static UnitType Sei => default;
        /// <summary>
        /// Tier-5 offensive naval unit.
        /// </summary>
        public static UnitType Omura => default;
        /// <summary>
        /// Tier-1 support naval unit.<br/>
        /// Not available on V6.
        /// </summary>
        public static UnitType Retusa => default;
        /// <summary>
        /// Tier-2 support naval unit.<br/>
        /// Not available on V6.
        /// </summary>
        public static UnitType Oxynoe => default;
        /// <summary>
        /// Tier-3 support naval unit.<br/>
        /// Not available on V6.
        /// </summary>
        public static UnitType Cyerce => default;
        /// <summary>
        /// Tier-4 support naval unit.<br/>
        /// Not available on V6.
        /// </summary>
        public static UnitType Aegires => default;
        /// <summary>
        /// Tier-5 support naval unit.<br/>
        /// Not available on V6.
        /// </summary>
        public static UnitType Navanax => default;
        /// <summary>
        /// Tier-1 player unit.
        /// </summary>
        public static UnitType Alpha => default;
        /// <summary>
        /// Tier-2 player unit.
        /// </summary>
        public static UnitType Beta => default;
        /// <summary>
        /// Tier-3 player unit.
        /// </summary>
        public static UnitType Gamma => default;
    }
}