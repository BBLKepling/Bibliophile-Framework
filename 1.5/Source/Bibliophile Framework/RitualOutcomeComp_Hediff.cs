using System;
using RimWorld;
using Verse;

namespace Bibliophile_Framework
{
    public class RitualOutcomeComp_Hediff : RitualOutcomeComp_Quality
    {
        [NoTranslate]
        public string roleId;

        public HediffDef hediffDef;
        public override float Count(LordJob_Ritual ritual, RitualOutcomeComp_Data data)
        {
            Pawn pawn = ritual.PawnWithRole(roleId);
            if (pawn == null || !pawn.health.hediffSet.TryGetHediff(hediffDef, out Hediff hediff))
            {
                return 0f;
            }
            return hediff.Severity;
        }
        public override string GetDesc(LordJob_Ritual ritual = null, RitualOutcomeComp_Data data = null)
        {
            if (ritual == null)
            {
                return labelAbstract;
            }
            Pawn pawn = ritual.PawnWithRole(roleId);
            if (pawn?.health == null || !pawn.health.hediffSet.TryGetHediff(hediffDef, out Hediff hediff))
            {
                return null;
            }
            float x = hediff.Severity;
            float num = curve.Evaluate(x);
            string text = ((num < 0f) ? "" : "+");
            return label.CapitalizeFirst().Formatted(pawn.Named("PAWN")) + ": " + "OutcomeBonusDesc_QualitySingleOffset".Translate(text + num.ToStringPercent()) + ".";
        }
        public override QualityFactor GetQualityFactor(Precept_Ritual ritual, TargetInfo ritualTarget, RitualObligation obligation, RitualRoleAssignments assignments, RitualOutcomeComp_Data data)
        {
            Pawn pawn = assignments.FirstAssignedPawn(roleId);
            if (pawn == null || !pawn.health.hediffSet.TryGetHediff(hediffDef, out Hediff hediff))
            {
                return null;
            }
            float x = hediff.Severity;
            float num = curve.Evaluate(x);
            return new QualityFactor
            {
                label = label.Formatted(pawn.Named("PAWN")),
                count = x + " / " + base.MaxValue,
                qualityChange = ((Math.Abs(num) > float.Epsilon) ? "OutcomeBonusDesc_QualitySingleOffset".Translate(num.ToStringWithSign("0.#%")).Resolve() : " - "),
                positive = (num >= 0f),
                quality = num,
                priority = 0f
            };
        }
    }
}
