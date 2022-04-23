using MindustryLogics;
using static MindustryLogics.Mindustry;
using static MindustryLogics.UnitControl;
using static MindustryLogics.Operation;
using static MindustryLogics.Drawing;

[Credit("By SmolIndieGame aka hi*3")]
class Test2
{
    Unit player;
    string playerName;

    Test2()
    {
        playerName = "hi<3";
    }

    void Main()
    {
        if (!GetLink(BuildingType.Switch, 1).Enabled) return;

        UnitBind(UnitType.Poly);

        if (player == null)
        {
            var tmpP = UnitRadar(Filter.Radar().Ally.Player, SortMethod.Distance, true);
            if (tmpP.Name == playerName)
                player = tmpP;
            if (player == null)
            {
                LocateBuilding(BuildingGroup.Core, false, out double coreX, out double coreY, out _);
                Approach(coreX, coreY, 10);
                return;
            }
        }

        var x = player.X;
        var y = player.Y;
        if (!IsWithin(x, y, 15) || IsWithin(x, y, 7))
            Approach(player.X, player.Y, 12);
        else
            Idle();
    }
}