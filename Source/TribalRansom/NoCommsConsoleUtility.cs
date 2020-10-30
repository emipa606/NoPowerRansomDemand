using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace TribalRansom
{
    [StaticConstructorOnStartup]
    public static class TribalRansom
    {
        static TribalRansom()
        {
            var harmonyInstance = new Harmony("Mlie.TribalRansom");
            harmonyInstance.PatchAll();
        }

        public static bool PlayerHasPoweredCommsConsole(Map map, out ThingDef type)
        {
            type = null;
            if (ModLister.GetActiveModWithIdentifier("sulusdacor.meltup.nopowercommssimplified") != null)
            {
                var birdPostDef = DefDatabase<ThingDef>.GetNamedSilentFail("BirdPostMessageTable");
                var desks = (from Thing desk in map.listerThings.ThingsMatching(ThingRequest.ForDef(birdPostDef)) where desk.Faction != null && desk.Faction == Faction.OfPlayerSilentFail select desk).ToList();
                foreach (var desk in desks)
                {
                    if (desk.TryGetComp<CompPowerTrader>() != null && desk.TryGetComp<CompPowerTrader>().PowerOn)
                    {
                        type = birdPostDef;
                        return true;
                    }
                }
            }
            if (ModLister.GetActiveModWithIdentifier("mlie.tribalsignalfire") != null)
            {
                var signalFireDef = DefDatabase<ThingDef>.GetNamedSilentFail("SignalFire");
                var fires = (from Thing fire in map.listerThings.ThingsMatching(ThingRequest.ForDef(signalFireDef)) where fire.Faction != null && fire.Faction == Faction.OfPlayerSilentFail select fire).ToList();
                foreach (var fire in fires)
                {
                    if (fire.TryGetComp<CompRefuelable>() != null && fire.TryGetComp<CompRefuelable>().HasFuel)
                    {
                        type = signalFireDef;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
