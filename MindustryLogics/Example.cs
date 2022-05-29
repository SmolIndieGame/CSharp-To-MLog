using MindustryLogics;
using static MindustryLogics.Mindustry;
using static MindustryLogics.UnitControl;
using static MindustryLogics.Operation;
using static MindustryLogics.Drawing;

// This code will order a flare to deliver copper to a duo,
// and the duo will prioritize enemies with the lowest health.
[Credit("By hi*3")]
class Example
{
    UnitType deliveryUnit;
    ItemType ammoType;
    LinkedBuilding turret = GetLink(BuildingType.Duo, 1);
    double flag;

    Unit unit;

    public Example()
    {
        deliveryUnit = UnitType.Flare;
        ammoType = ItemType.Copper;
        flag = Time;
    }

    void Main()
    {
        var enemy = turret.Radar(Filter.Radar().Enemy.Ground, SortMethod.Health, false);
        turret.ShootTarget(enemy, enemy != null);

        if (turret.Ammo == turret.AmmoCapacity)
        {
            if (unit.Flag == flag)
                Flag(0);
            return;
        }

        if (unit == null || unit.Dead || unit.Flag != flag)
        {
            UnitBind(deliveryUnit);
            if (BindingUnit.Flag != 0)
                return;
            unit = BindingUnit;
            Flag(flag);
        }

        if (unit.FirstItem != ammoType)
        {
            LocateBuilding(BuildingGroup.Core, false, out double x, out double y, out Building core);
            Move(x, y);
            ItemDrop(core, 999);
            ItemTake(core, ammoType, 999);
            return;
        }

        Approach(turret.X, turret.Y, 5);
        ItemDrop(turret, 999);
    }
}
