using System.Reflection;
using HarmonyLib;
using HarmonyLib.Tools;
using QModManager.API.ModLoading;
using Logger = QModManager.Utility.Logger;


namespace NoCameraShake_SN
{
    [QModCore]
    public class QMod
    {
        [QModPatch]
        public static void Patch()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var modName = ($"wilki24_{assembly.GetName().Name}");
            Logger.Log(Logger.Level.Info, $"Patching {modName}");
            Harmony harmony = new Harmony(modName);
            harmony.PatchAll(assembly);
            Logger.Log(Logger.Level.Info, "Patched successfully!");
        }

    }
}
