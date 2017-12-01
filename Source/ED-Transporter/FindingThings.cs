using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;

namespace Enhanced_Development.Utilities
{
    public class Utilities
    {
        public static IEnumerable<Pawn> findClosePawnsInColony(IntVec3 position, Map map, float radius)
        {
            //IEnumerable<Pawn> pawns = Find.ListerPawns.ColonistsAndPrisoners;
            //IEnumerable<Pawn> pawns = Find.ListerPawns.FreeColonists;
            //IEnumerable<Pawn> pawns = Find.ListerPawns.AllPawns.Where(item => item.IsColonistPlayerControlled || item.IsColonistPlayerControlled);

            IEnumerable<Pawn> pawns = map.mapPawns.PawnsInFaction(Faction.OfPlayer);
           

            IEnumerable<Pawn> closePawns;

            if (pawns != null)
            {
                closePawns = pawns.Where<Pawn>(t => t.Position.InHorDistOf(position, radius));
                return closePawns;
            }
            return null;
        }

        static public Thing FindItemThingNearBuilding(Map map, Thing centerBuilding, int radius)
        {
            IEnumerable<Thing> closeThings = GenRadial.RadialDistinctThingsAround(centerBuilding.Position, map, radius, true);

            foreach (Thing tempThing in closeThings)
            {
                if (tempThing.def.category == ThingCategory.Item)
                {
                    return tempThing;
                }

            }

            return (Thing)null;
        }

        static public IEnumerable<Thing> FindItemThingsNearBuilding(Map map, Thing centerBuilding, int radius)
        {
            return GenRadial.RadialDistinctThingsAround(centerBuilding.Position, map, radius, true).Where(x => x.def.category == ThingCategory.Item);
        }

    }
}

