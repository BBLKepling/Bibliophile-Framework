using RimWorld;
using System.Linq;
using Verse.AI;
using Verse;
using System.Collections.Generic;

namespace Bibliophile_Framework
{
    public class BibliophileUtility
    {
        private static readonly List<Thing> TmpCandidates = new List<Thing>();
        public static bool TryGetSpecificBookToRead(Pawn pawn, ThingDef thingDef, out Book book)
        {
            book = null;
            TmpCandidates.Clear();
            TmpCandidates.AddRange(from thing in pawn.Map.listerThings.ThingsInGroup(ThingRequestGroup.Book)
                                   where IsValidBook(thing)
                                   select thing);
            TmpCandidates.AddRange(from thing in pawn.Map.listerThings.GetThingsOfType<Building_Bookcase>().SelectMany((Building_Bookcase x) => x.HeldBooks)
                                   where IsValidBook(thing)
                                   select thing);
            if (TmpCandidates.NullOrEmpty()) return false;
            book = (Book)TmpCandidates.RandomElement();
            TmpCandidates.Clear();
            return true;
            bool IsValidBook(Thing t)
            {
                return t is Book && !t.IsForbiddenHeld(pawn) && t.def == thingDef && pawn.CanReserveAndReach(t, PathEndMode.Touch, Danger.None) && t.IsPoliticallyProper(pawn);
            }
        }
    }
}
