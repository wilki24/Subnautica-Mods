using System.Reflection;
using HarmonyLib;
using BepInEx;
using System;

namespace NoCameraShake_SN
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        #region[Declarations]
        private const string
            MODNAME = "NoCameraShake",
            AUTHOR = "wilki24",
            GUID = "com.wilki24.NoCameraShake",
            VERSION = "1.0.0.0";
        #endregion

        public void Awake()
        {
            Console.WriteLine("NoCameraShake - Started patching v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3));

            var harmony = new Harmony(GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Console.WriteLine("NoCameraShake - Finished patching");
        }
    }
}
