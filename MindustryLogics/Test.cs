using MindustryLogics;
using static MindustryLogics.Mindustry;
using static MindustryLogics.UnitControl;
using static MindustryLogics.Operation;
using static MindustryLogics.Drawing;

[Credit("By SmolIndieGame aka hi*3")]
class Test
{
    //[LinkedTo(BuildingType.Duo, 27)]
    //LinkedBuilding mm;
    //[LinkedTo(BuildingType.Message, 1)]
    object yo;
    object ret;
    LinkedBuilding mm = GetLink(BuildingType.Arc, 2);

    /*public Test()
    {
        gr();
    }

    private void rr()
    {
        Print("yo");
    }

    void gr() => gr();*/

    void Main()
    {
        // The get icon thingy.
        /*Print("hi\n");
        Print(GetIcon(ItemType.Copper));
        Print(GetIcon(ItemType.Lead));
        Print(GetIcon(ItemType.Metaglass));
        Print(GetIcon(ItemType.Graphite));
        Print(GetIcon(ItemType.Sand));
        Print(GetIcon(ItemType.Coal));
        Print(GetIcon(ItemType.Titanium));
        Print(GetIcon(ItemType.Thorium));
        Print(GetIcon(ItemType.Scrap));
        Print(GetIcon(ItemType.Silicon));
        Print(GetIcon(ItemType.Plastanium));
        Print(GetIcon(ItemType.Phase_Fabric));
        Print(GetIcon(ItemType.Spore_Pod));
        Print(GetIcon(ItemType.Blast_Compound));
        Print(GetIcon(ItemType.Pyratite));
        Print("\n");
        Print(GetIcon(LiquidType.Water));
        Print(GetIcon(LiquidType.Slag));
        Print(GetIcon(LiquidType.Oil));
        Print(GetIcon(LiquidType.CryoFluid));
        Print("\n");
        Print(GetIcon(UnitType.Dagger));
        Print(GetIcon(UnitType.Mace));
        Print(GetIcon(UnitType.Fortress));
        Print(GetIcon(UnitType.Scepter));
        Print(GetIcon(UnitType.Reign));
        Print(GetIcon(UnitType.Nova));
        Print(GetIcon(UnitType.Pulsar));
        Print(GetIcon(UnitType.Quasar));
        Print(GetIcon(UnitType.Vela));
        Print(GetIcon(UnitType.Corvus));
        Print(GetIcon(UnitType.Crawler));
        Print(GetIcon(UnitType.Atrax));
        Print(GetIcon(UnitType.Spiroct));
        Print(GetIcon(UnitType.Arkyid));
        Print(GetIcon(UnitType.Toxopid));
        Print(GetIcon(UnitType.Flare));
        Print(GetIcon(UnitType.Horizon));
        Print(GetIcon(UnitType.Zenith));
        Print(GetIcon(UnitType.Antumbra));
        Print(GetIcon(UnitType.Eclipse));
        Print(GetIcon(UnitType.Mono));
        Print(GetIcon(UnitType.Poly));
        Print(GetIcon(UnitType.Mega));
        Print(GetIcon(UnitType.Quad));
        Print(GetIcon(UnitType.Oct));
        Print(GetIcon(UnitType.Risso));
        Print(GetIcon(UnitType.Minke));
        Print(GetIcon(UnitType.Bryde));
        Print(GetIcon(UnitType.Sei));
        Print(GetIcon(UnitType.Omura));
        Print(GetIcon(UnitType.Retusa));
        Print(GetIcon(UnitType.Oxynoe));
        Print(GetIcon(UnitType.Cyerce));
        Print(GetIcon(UnitType.Aegires));
        Print(GetIcon(UnitType.Navanax));
        Print(GetIcon(UnitType.Alpha));
        Print(GetIcon(UnitType.Beta));
        Print(GetIcon(UnitType.Gamma));
        Print("\n");
        Print(GetIcon(BuildingType.Graphite_Press));
        Print(GetIcon(BuildingType.Multi_Press));
        Print(GetIcon(BuildingType.Silicon_Smelter));
        Print(GetIcon(BuildingType.Silicon_Crucible));
        Print(GetIcon(BuildingType.Kiln));
        Print(GetIcon(BuildingType.Plastanium_Compressor));
        Print(GetIcon(BuildingType.Phase_Weaver));
        Print(GetIcon(BuildingType.Alloy_Smelter));
        Print(GetIcon(BuildingType.Cryofluid_Mixer));
        Print(GetIcon(BuildingType.Pyratite_Mixer));
        Print(GetIcon(BuildingType.Blast_Mixer));
        Print(GetIcon(BuildingType.Melter));
        Print(GetIcon(BuildingType.Separator));
        Print(GetIcon(BuildingType.Disassembler));
        Print(GetIcon(BuildingType.Spore_Press));
        Print(GetIcon(BuildingType.Pulverizer));
        Print(GetIcon(BuildingType.Coal_Centrifuge));
        Print(GetIcon(BuildingType.Incinerator));
        Print(GetIcon(BuildingType.Copper_Wall));
        Print(GetIcon(BuildingType.Copper_Wall_Large));
        Print(GetIcon(BuildingType.Titanium_Wall));
        Print(GetIcon(BuildingType.Titanium_Wall_Large));
        Print(GetIcon(BuildingType.Plastanium_Wall));
        Print(GetIcon(BuildingType.Plastanium_Wall_Large));
        Print(GetIcon(BuildingType.Thorium_Wall));
        Print(GetIcon(BuildingType.Thorium_Wall_Large));
        Print(GetIcon(BuildingType.Phase_Wall));
        Print(GetIcon(BuildingType.Phase_Wall_Large));
        Print(GetIcon(BuildingType.Surge_Wall));
        Print(GetIcon(BuildingType.Surge_Wall_Large));
        Print(GetIcon(BuildingType.Door));
        Print(GetIcon(BuildingType.Door_Large));
        Print(GetIcon(BuildingType.Scrap_Wall));
        Print(GetIcon(BuildingType.Scrap_Wall_Large));
        Print(GetIcon(BuildingType.Scrap_Wall_Huge));
        Print(GetIcon(BuildingType.Scrap_Wall_Gigantic));
        Print(GetIcon(BuildingType.Mender));
        Print(GetIcon(BuildingType.Mend_Projector));
        Print(GetIcon(BuildingType.Overdrive_Projector));
        Print(GetIcon(BuildingType.Overdrive_Dome));
        Print(GetIcon(BuildingType.Force_Projector));
        Print(GetIcon(BuildingType.Shock_Mine));
        Print(GetIcon(BuildingType.Conveyor));
        Print(GetIcon(BuildingType.Titanium_Conveyor));
        Print(GetIcon(BuildingType.Plastanium_Conveyor));
        Print(GetIcon(BuildingType.Armored_Conveyor));
        Print(GetIcon(BuildingType.Junction));
        Print(GetIcon(BuildingType.Bridge_Conveyor));
        Print(GetIcon(BuildingType.Phase_Conveyor));
        Print(GetIcon(BuildingType.Sorter));
        Print(GetIcon(BuildingType.Inverted_Sorter));
        Print(GetIcon(BuildingType.Router));
        Print(GetIcon(BuildingType.Distributor));
        Print(GetIcon(BuildingType.Overflow_Gate));
        Print(GetIcon(BuildingType.Underflow_Gate));
        Print(GetIcon(BuildingType.Mass_Driver));
        Print(GetIcon(BuildingType.Duct));
        Print(GetIcon(BuildingType.Duct_Router));
        Print(GetIcon(BuildingType.Duct_Bridge));
        Print(GetIcon(BuildingType.Mechanical_Pump));
        Print(GetIcon(BuildingType.Rotary_Pump));
        Print(GetIcon(BuildingType.Thermal_Pump));
        Print(GetIcon(BuildingType.Conduit));
        Print(GetIcon(BuildingType.Pulse_Conduit));
        Print(GetIcon(BuildingType.Plated_Conduit));
        Print(GetIcon(BuildingType.Liquid_Router));
        Print(GetIcon(BuildingType.Liquid_Tank));
        Print(GetIcon(BuildingType.Liquid_Junction));
        Print(GetIcon(BuildingType.Bridge_Conduit));
        Print(GetIcon(BuildingType.Phase_Conduit));
        Print(GetIcon(BuildingType.Power_Node));
        Print(GetIcon(BuildingType.Power_Node_Large));
        Print(GetIcon(BuildingType.Surge_Tower));
        Print(GetIcon(BuildingType.Diode));
        Print(GetIcon(BuildingType.Battery));
        Print(GetIcon(BuildingType.Battery_Large));
        Print(GetIcon(BuildingType.Combustion_Generator));
        Print(GetIcon(BuildingType.Thermal_Generator));
        Print(GetIcon(BuildingType.Steam_Generator));
        Print(GetIcon(BuildingType.Differential_Generator));
        Print(GetIcon(BuildingType.RTG_Generator));
        Print(GetIcon(BuildingType.Solar_Panel));
        Print(GetIcon(BuildingType.Solar_Panel_Large));
        Print(GetIcon(BuildingType.Thorium_Reactor));
        Print(GetIcon(BuildingType.Impact_Reactor));
        Print(GetIcon(BuildingType.Mechanical_Drill));
        Print(GetIcon(BuildingType.Pneumatic_Drill));
        Print(GetIcon(BuildingType.Laser_Drill));
        Print(GetIcon(BuildingType.Blast_Drill));
        Print(GetIcon(BuildingType.Water_Extractor));
        Print(GetIcon(BuildingType.Cultivator));
        Print(GetIcon(BuildingType.Oil_Extractor));
        Print(GetIcon(BuildingType.Core_Shard));
        Print(GetIcon(BuildingType.Core_Foundation));
        Print(GetIcon(BuildingType.Core_Nucleus));
        Print(GetIcon(BuildingType.Vault));
        Print(GetIcon(BuildingType.Container));
        Print(GetIcon(BuildingType.Unloader));
        Print(GetIcon(BuildingType.Duo));
        Print(GetIcon(BuildingType.Scatter));
        Print(GetIcon(BuildingType.Scorch));
        Print(GetIcon(BuildingType.Hail));
        Print(GetIcon(BuildingType.Wave));
        Print(GetIcon(BuildingType.Lancer));
        Print(GetIcon(BuildingType.Arc));
        Print(GetIcon(BuildingType.Parallax));
        Print(GetIcon(BuildingType.Swarmer));
        Print(GetIcon(BuildingType.Salvo));
        Print(GetIcon(BuildingType.Segment));
        Print(GetIcon(BuildingType.Tsunami));
        Print(GetIcon(BuildingType.Fuse));
        Print(GetIcon(BuildingType.Ripple));
        Print(GetIcon(BuildingType.Cyclone));
        Print(GetIcon(BuildingType.Foreshadow));
        Print(GetIcon(BuildingType.Spectre));
        Print(GetIcon(BuildingType.Meltdown));
        Print(GetIcon(BuildingType.Command_Center));
        Print(GetIcon(BuildingType.Ground_Factory));
        Print(GetIcon(BuildingType.Air_Factory));
        Print(GetIcon(BuildingType.Naval_Factory));
        Print(GetIcon(BuildingType.Additive_Reconstructor));
        Print(GetIcon(BuildingType.Multiplicative_Reconstructor));
        Print(GetIcon(BuildingType.Exponential_Reconstructor));
        Print(GetIcon(BuildingType.Tetrative_Reconstructor));
        Print(GetIcon(BuildingType.Repair_Point));
        Print(GetIcon(BuildingType.Repair_Turret));
        Print(GetIcon(BuildingType.Payload_Conveyor));
        Print(GetIcon(BuildingType.Payload_Router));
        Print(GetIcon(BuildingType.Payload_Propulsion_Tower));
        Print(GetIcon(BuildingType.Power_Source));
        Print(GetIcon(BuildingType.Power_Void));
        Print(GetIcon(BuildingType.Item_Source));
        Print(GetIcon(BuildingType.Item_Void));
        Print(GetIcon(BuildingType.Liquid_Source));
        Print(GetIcon(BuildingType.Liquid_Void));
        Print(GetIcon(BuildingType.Payload_Void));
        Print(GetIcon(BuildingType.Payload_Source));
        Print(GetIcon(BuildingType.Illuminator));
        Print(GetIcon(BuildingType.Launch_Pad));
        Print(GetIcon(BuildingType.Interplanetary_Accelerator));
        Print(GetIcon(BuildingType.Message));
        Print(GetIcon(BuildingType.Switch));
        Print(GetIcon(BuildingType.Micro_Processor));
        Print(GetIcon(BuildingType.Logic_Processor));
        Print(GetIcon(BuildingType.Hyper_Processor));
        Print(GetIcon(BuildingType.Memory_Cell));
        Print(GetIcon(BuildingType.Memory_Bank));
        Print(GetIcon(BuildingType.Logic_Display));
        Print(GetIcon(BuildingType.Large_Logic_Display));
        Print(GetIcon(BuildingType.Liquid_Container));
        Print(GetIcon(BuildingType.Deconstructor));
        Print(GetIcon(BuildingType.Constructor));
        Print(GetIcon(BuildingType.Thruster));
        Print(GetIcon(BuildingType.Large_Constructor));
        Print(GetIcon(BuildingType.Payload_Loader));
        Print(GetIcon(BuildingType.Payload_Unloader));
        Print("\n");
        Print(GetIcon(StatusType.Burning));
        Print(GetIcon(StatusType.Freezing));
        Print(GetIcon(StatusType.Unmoving));
        Print(GetIcon(StatusType.Wet));
        Print(GetIcon(StatusType.Melting));
        Print(GetIcon(StatusType.Sapped));
        Print(GetIcon(StatusType.Electrified));
        Print(GetIcon(StatusType.Sprore_Slowed));
        Print(GetIcon(StatusType.Tarred));
        Print(GetIcon(StatusType.Overdrive));
        Print(GetIcon(StatusType.Overclock));
        Print(GetIcon(StatusType.Guardian));
        Print(GetIcon(StatusType.Shocked));
        Print(GetIcon(StatusType.Blasted));
        PrintFlush(GetLink(BuildingType.Message, 1));*/

        // loop test
        /*for (int i = 0, j = 0; i < 10 && j < 10; i++, j++)
        {
            var g = Mindustry.GetLink(i);
            if (g.Dead) break;
            Mindustry.Print(g.Type);
            while (i < 5)
            {
                if (i == 2) break;
                Mindustry.PrintFlush(Mindustry.GetLink(BuildingType.Message, 1));
            }
            do
            {
                _ = 1 + (int)yo;
            } while (false);
        }*/

        // const test
        /*var a = LinksCount;
        var b = This;
        var c = ThisX + ThisY;
        var d = MapHeight - MapWidth;
        var e = (Time + Ticks) / IPT;
        var f = BindingUnit;*/

        // ???
        /*UnitOp.Bind(UnitType.Dagger);
        UnitOp.Radar(Filter.Radar().Player.Ally, SortMethod.Distance, true);*/

        // recursion test
        /*F();
        F();
        F();*/

        /*UnitOp.Bind(UnitType.Poly);
        
        bool f = UnitOp.LocateBuilding(BuildingGroup.Core, building: out Building b, y: out double x, x: out double y, isEnemy: false);
        Print("Building: ");
        if (f)
        {
            Print(x);
            Print(" ");
            Print(y);
            Print(" ");
            Print(b.Type);
        }
        else
            Print("Not found");
        Print("\n");

        f = UnitOp.LocateOre(ItemType.Copper, out x, out y);
        Print("Ore: ");
        if (f)
        {
            Print(x);
            Print(" ");
            Print(y);
        }
        else
            Print("Not found");
        Print("\n");

        f = UnitOp.LocateEnemySpawn(y: out double y2, x: out double x2, building: out Building b2);
        Print("Enemy Spawn: ");
        if (f)
        {
            Print(x2);
            Print(" ");
            Print(y2);
            Print(" ");
            Print(b2);
        }
        else
            Print("Not found");
        Print("\n");

        f = UnitOp.LocateDamagedAllyBuilding(y: out double y3, x: out double x3, building: out Building b3);
        Print("Damaged: ");
        if (f)
        {
            Print(x3);
            Print(" ");
            Print(y3);
            Print(" ");
            Print(b3.Type);
        }
        else
            Print("Not found");
        Print("\n");

        PrintFlush(GetLink(BuildingType.Message, 1));*/

        /*Bind(UnitType.Mega);
        Idle();
        Stop();
        Move(2, 5);
        Approach(3, 4, 5);
        Boost(true);
        Pathfind();
        Target(2, 3, false);
        Targetp(GetLink(0), true);
        ItemDrop(GetLink(0), 20);
        ItemTake(GetLink(BuildingType.Hail, 1), 1020);
        PayDrop();
        PayTake(false);
        PayEnter();
        Mine(99, 999);
        Flag(293841975);
        Build(-1, -4, BuildingType.Payload_Unloader, 3, null);
        GetBlock(6, 3, out BuildingType type, out Building b);
        _ = IsWithin(ThisX, ThisY, 5);*/

        /*_ = Add(1, 2);
        _ = Sub(2, 3);
        _ = Mul(3, 4);
        _ = Div(4, 5);
        _ = Idiv(5, 6);
        _ = Mod(6, 7);
        _ = Pow(7, 8);
        _ = Equal(8, 9);
        _ = NotEqual(9, 10);
        _ = LessThan(10, 11);
        _ = LessThanEq(11, 12);
        _ = GreaterThan(12, 13);
        _ = GreaterThanEq(13, 14);
        _ = StrictEqual(14, 15);
        _ = Shl(467, 12);
        _ = Shr(467, 12);
        _ = Or(467, 12);
        _ = And(467, 12);
        _ = Xor(467, 12);
        _ = Not(46);
        _ = Max(4.32, 327.48);
        _ = Min(4.32, 327.48);
        _ = Angle(4.32, 327.48);
        _ = Len(4.32, 327.48);
        _ = Noise(4.32, 327.48);
        _ = Abs(4.6);
        _ = Log(4.6);
        _ = Log10(4.6);
        _ = Floor(4.6);
        _ = Ceil(4.6);
        _ = Sqrt(4.6);
        _ = Rand(4);
        _ = Sin(4.6);
        _ = Cos(4.6);
        _ = Tan(4.6);
        _ = Asin(4.6);
        _ = Acos(4.6);
        _ = Atan(4.6);*/

        //draw test
        /*Clear(10, 255, 30);
        SetColor(255, 0, 0, 100);
        SetStroke(5);
        DrawLine(0, 20, 88, 20);
        DrawRect(30, 30, 30, 30);
        DrawLineRect(60, 30, 30, 30);
        DrawPoly(88, 88, 5, 20, 90);
        DrawLinePoly(88, 130, 7, 30, 0);
        DrawTriangle(0, 0, 176, 0, 0, 176);
        DrawImage(88, 40, ItemType.Graphite, 40, 45);
        DrawFlush(GetLink(342798));
        DrawFlush(32);*/

        /*int a = 1;
        //bool tmp57768564674 = a == 0 || a == 1 || a == 2 && a == 3;
        if (a == 0 || a == 1 || a == 2 || a == 3)
        {
            Print("Execute");
        }
        else
        {
            Print("Not");
        }
        var b = GetLink(0);*/

        // linked building test
        /*int a = 1;
        mm.Config = UnitType.Dagger;
        Print(mm.Ammo);
        mm.Radar(null, SortMethod.MaxHealth, true);
        _ = mm.Name;*/


        //F("adsuf9q4f");
        //Print("\U001000494324");

        //content type change test
        /*Print(GetLink(BuildingType.Message, 1));
        var b = GetLink(0);
        Print(Sensor(b, InfoType.Ammo));
        var info = ItemType.Lead;
        Print(Sensor(b, info));

        UnitBind((UnitType)null);
        LocateOre(ItemType.Copper, out double x, out double y);

        var ut = UnitType.Retusa;
        UnitBind(ut);
        var item = ItemType.None;
        LocateOre(item, out x, out y);

        ItemTake(b, item, 99);
        ItemTake(b, ItemType.Titanium, 99);
        Build(1, 2, BuildingType.Conveyor, 2, b);
        var bt = BuildingType.Duo;
        Build(10, 20, bt, 0, null);
        GetBlock(10, 20, out BuildingType aa, out b);
        GetBlock(1, 2, out aa, out var bb);*/
        Building b = GetLink(10);
        mm.SetColor(10, 20, 30);
    }

    private object F(string v)
    {
        return 89273;
    }

    int g(ref int a)
    {
        a = 0;
        return a;
    }


    /*void F()
    {
        G();
    }

    void G()
    {
        int d = 0;

        Print(H(d));
    }
    double H(int i) { return 0; }*/
}