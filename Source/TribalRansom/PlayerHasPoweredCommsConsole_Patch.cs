using HarmonyLib;
using RimWorld;
using Verse;

namespace TribalRansom;

[HarmonyPatch(typeof(CommsConsoleUtility), "PlayerHasPoweredCommsConsole", typeof(Map))]
public static class PlayerHasPoweredCommsConsole_Patch
{
    public static void Postfix(Map map, ref bool __result)
    {
        if (!__result)
        {
            __result = TribalRansom.PlayerHasPoweredCommsConsole(map, out _);
        }
    }
}