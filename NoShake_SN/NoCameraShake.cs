using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Logger = QModManager.Utility.Logger;

namespace NoCameraShake_SN
{
    [HarmonyPatch(typeof(MainCameraControl))]
    [HarmonyPatch("ShakeCamera")]
    internal class Patch_UpdateCamShake
    {
        /*
            // [99 5 - 99 56]
            IL_0000: ldarg.0      // this
            IL_0001: ldarg.1      // intensity
            IL_0002: ldc.r4       0.0
            IL_0007: ldc.r4       5 <- We will change this to 0.0
            IL_000c: call         float32 [UnityEngine.CoreModule]UnityEngine.Mathf::Clamp(float32, float32, float32)
            IL_0011: stfld        float32 MainCameraControl::shakeAmount
        */

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldc_R4 && instruction.operand.ToString() == "5")
                {
                    instruction.operand = 0.0f;
                    Logger.Log(Logger.Level.Info, "Max shake is now: 0");
                }
                yield return instruction;
            }
        }
    }
}

