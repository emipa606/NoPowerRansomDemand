using System;
using RimWorld;
using HarmonyLib;
using Verse;

namespace TribalRansom
{

    public static class HarmonyPatches
    {

        public static bool IsCheckingForRansom = false;

        [HarmonyPatch(typeof(IncidentWorker_RansomDemand), "CanFireNowSub", new Type[] { typeof(IncidentParms) })]
        public static class CanFireNowSub_Patch
        {
            public static void Prefix()
            {
                IsCheckingForRansom = true;
            }

            public static void Postfix()
            {
                IsCheckingForRansom = false;
            }
        }

        [HarmonyPatch(typeof(CommsConsoleUtility), "PlayerHasPoweredCommsConsole", new Type[] { typeof(Map) })]
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

        [HarmonyPatch(typeof(LetterStack), "ReceiveLetter", new Type[] { typeof(Letter), typeof(string) })]
        public static class ReceiveLetter_Patch
        {
            public static void Prefix(ref Letter let)
            {
                if (!(let is ChoiceLetter_RansomDemand))
                {
                    return;
                }
                var letter = (ChoiceLetter_RansomDemand)let;
                if (!TribalRansom.PlayerHasPoweredCommsConsole(letter.map, out ThingDef type) || type == null)
                {
                    return;
                }
                letter.title = TranslatorFormattedStringExtensions.Translate("TribalDemandTitle", letter.map.Parent.Label);
                letter.radioMode = false;
                if (type.defName == "BirdPostMessageTable")
                {
                    letter.text = GenText.AdjustedFor(TranslatorFormattedStringExtensions.Translate("TribalDemandPigeon", letter.kidnapped.LabelShort, letter.faction.Name, letter.fee, letter.kidnapped.Named("PAWN")), letter.kidnapped, "PAWN");
                }
                if (type.defName == "SignalFire")
                {
                    letter.text = GenText.AdjustedFor(TranslatorFormattedStringExtensions.Translate("TribalDemandSignalfire", letter.kidnapped.LabelShort, letter.faction.Name, letter.fee, letter.kidnapped.Named("PAWN")), letter.kidnapped, "PAWN");
                }
                let = letter;
            }
        }
    }

}
