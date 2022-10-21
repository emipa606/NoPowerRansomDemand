using HarmonyLib;
using RimWorld;
using Verse;

namespace TribalRansom;

[HarmonyPatch(typeof(LetterStack), "ReceiveLetter", typeof(Letter), typeof(string))]
public static class ReceiveLetter_Patch
{
    public static void Prefix(ref Letter let)
    {
        if (!(let is ChoiceLetter_RansomDemand letter))
        {
            return;
        }

        if (!TribalRansom.PlayerHasPoweredCommsConsole(letter.map, out var type) || type == null)
        {
            return;
        }

        letter.title = "TribalDemandTitle".Translate(letter.map.Parent.Label);
        letter.radioMode = false;
        switch (type.defName)
        {
            case "BirdPostMessageTable" or "DankPyon_ScribeTable":
                letter.Text =
                    GenText.AdjustedFor(
                        "TribalDemandPigeon".Translate(letter.kidnapped.LabelShort, letter.faction.Name, letter.fee,
                            letter.kidnapped.Named("PAWN")), letter.kidnapped);
                break;
            case "SignalFire":
                letter.Text =
                    GenText.AdjustedFor(
                        "TribalDemandSignalfire".Translate(letter.kidnapped.LabelShort, letter.faction.Name,
                            letter.fee, letter.kidnapped.Named("PAWN")), letter.kidnapped);
                break;
        }

        let = letter;
    }
}