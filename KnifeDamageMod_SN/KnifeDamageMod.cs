using HarmonyLib;
using Logger = QModManager.Utility.Logger;

namespace KnifeDamageMod_SN
{
    [HarmonyPatch(typeof(PlayerTool))]
    [HarmonyPatch("Awake")]
    internal class PatchPlayerToolAwake
    {
        [HarmonyPostfix]
        public static void Postfix(PlayerTool __instance)
        {
            // Check to see if this is the knife
            Knife knife = __instance as Knife;
            if (knife != null)
            {
                // Double the knife damage
                float knifeDamage = knife.damage;
                float newKnifeDamage = knifeDamage * 10;
                knife.damage = newKnifeDamage;
                Logger.Log(Logger.Level.Debug, $"Knife damage was: {knifeDamage}," +
                    $" is now: {newKnifeDamage}");
            }
        }
    }
}

