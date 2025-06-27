using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace TribalRansom;

[StaticConstructorOnStartup]
public static class TribalRansom
{
    private static readonly bool nopowercommssimplified;

    private static readonly bool tribalsignalfire;

    private static readonly bool medievalOverhaul;

    private static readonly ThingDef birdPostDef;

    private static readonly ThingDef moBirdPostDef;

    private static readonly ThingDef signalFireDef;

    static TribalRansom()
    {
        var harmonyInstance = new Harmony("Mlie.TribalRansom");
        harmonyInstance.PatchAll();
        nopowercommssimplified =
            ModLister.GetActiveModWithIdentifier("sulusdacor.meltup.nopowercommssimplified", true) != null;
        if (nopowercommssimplified)
        {
            birdPostDef = DefDatabase<ThingDef>.GetNamedSilentFail("BirdPostMessageTable");
        }

        medievalOverhaul = ModLister.GetActiveModWithIdentifier("DankPyon.Medieval.Overhaul", true) != null ||
                           ModLister.GetActiveModWithIdentifier("Zaf.Medieval", true) != null;
        if (medievalOverhaul)
        {
            moBirdPostDef = DefDatabase<ThingDef>.GetNamedSilentFail("DankPyon_ScribeTable");
        }

        tribalsignalfire = ModLister.GetActiveModWithIdentifier("mlie.tribalsignalfire", true) != null;
        if (tribalsignalfire)
        {
            signalFireDef = DefDatabase<ThingDef>.GetNamedSilentFail("SignalFire");
        }
    }

    public static bool PlayerHasPoweredCommsConsole(Map map, out ThingDef type)
    {
        type = null;
        if (nopowercommssimplified)
        {
            var desks = (from Thing desk in map.listerThings.ThingsMatching(ThingRequest.ForDef(birdPostDef))
                where desk.Faction != null && desk.Faction == Faction.OfPlayerSilentFail
                select desk).ToList();
            foreach (var desk in desks)
            {
                if (desk.TryGetComp<CompPowerTrader>() == null || !desk.TryGetComp<CompPowerTrader>().PowerOn)
                {
                    continue;
                }

                type = birdPostDef;
                return true;
            }
        }

        if (medievalOverhaul)
        {
            var desks = (from Thing desk in map.listerThings.ThingsMatching(ThingRequest.ForDef(moBirdPostDef))
                where desk.Faction != null && desk.Faction == Faction.OfPlayerSilentFail
                select desk).ToList();
            foreach (var desk in desks)
            {
                if (desk.TryGetComp<CompPowerTrader>() == null || !desk.TryGetComp<CompPowerTrader>().PowerOn)
                {
                    continue;
                }

                type = moBirdPostDef;
                return true;
            }
        }

        if (!tribalsignalfire)
        {
            return false;
        }

        var fires = (from Thing fire in map.listerThings.ThingsMatching(ThingRequest.ForDef(signalFireDef))
            where fire.Faction != null && fire.Faction == Faction.OfPlayerSilentFail
            select fire).ToList();
        foreach (var fire in fires)
        {
            if (fire.TryGetComp<CompRefuelable>() == null || !fire.TryGetComp<CompRefuelable>().HasFuel)
            {
                continue;
            }

            type = signalFireDef;
            return true;
        }

        return false;
    }
}