using RimWorld;
using Verse;

namespace Bibliophile_Framework
{
    public class ReadingOutcomeDoerAddHediff : BookOutcomeDoer
    {
        public new BookOutcomeProperties_AddHediff Props => (BookOutcomeProperties_AddHediff)props;
        public override bool DoesProvidesOutcome(Pawn reader)
        {
            return true;
        }
        public override void OnReadingTick(Pawn reader, float factor)
        {
            if (Find.TickManager.TicksGame % Props.severityTicks != 0 || !(reader.health is Pawn_HealthTracker health)) return;
            if (!health.hediffSet.TryGetHediff(Props.hediffDef, out Hediff hediff))
            {
                hediff = health.AddHediff(Props.hediffDef);
                hediff.Severity = KnowledgeQuality(Quality);
            }
            else hediff.Severity += KnowledgeQuality(Quality);
        }
        public override string GetBenefitsString(Pawn reader = null)
        {
            return string.Format(" - {0}: +{1} {2} {3} {4}", Props.benefitsString.Translate(), KnowledgeQuality(Quality).ToStringPercent(), "BBLK_Knowledge_Per".Translate(), Props.severityTicks, "BBLK_Knowledge_Tick".Translate());
        }
        public float KnowledgeQuality(QualityCategory qc)
        {
            return Props.qualityCurve.Evaluate((int)qc);
        }
    }
}
