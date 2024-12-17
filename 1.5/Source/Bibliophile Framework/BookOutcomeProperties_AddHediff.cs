using RimWorld;
using System;
using Verse;

namespace Bibliophile_Framework
{
    public class BookOutcomeProperties_AddHediff : BookOutcomeProperties
    {
        public HediffDef hediffDef;
        public int severityTicks = 60;
        public string benefitsString;
        public SimpleCurve qualityCurve;
        public override Type DoerClass => typeof(ReadingOutcomeDoerAddHediff);
    }
}
