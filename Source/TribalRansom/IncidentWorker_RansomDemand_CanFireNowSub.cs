using HarmonyLib;
using RimWorld;

namespace TribalRansom;

[HarmonyPatch(typeof(IncidentWorker_RansomDemand), "CanFireNowSub", typeof(IncidentParms))]
public static class IncidentWorker_RansomDemand_CanFireNowSub
{
    public static bool IsCheckingForRansom;

    public static void Prefix()
    {
        IsCheckingForRansom = true;
    }

    public static void Postfix()
    {
        IsCheckingForRansom = false;
    }
}