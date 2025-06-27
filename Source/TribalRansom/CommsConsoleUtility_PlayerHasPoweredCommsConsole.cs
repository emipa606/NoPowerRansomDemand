using HarmonyLib;
using RimWorld;
using Verse;

namespace TribalRansom;

[HarmonyPatch(typeof(CommsConsoleUtility), nameof(CommsConsoleUtility.PlayerHasPoweredCommsConsole), typeof(Map))]
public static class CommsConsoleUtility_PlayerHasPoweredCommsConsole
{
    public static void Postfix(Map map, ref bool __result)
    {
        if (!__result)
        {
            __result = TribalRansom.PlayerHasPoweredCommsConsole(map, out _);
        }
    }
}