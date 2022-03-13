using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Logger = QModManager.Utility.Logger;

namespace KnifeDamageMod_SN
{
    [HarmonyPatch(typeof(Knife))]
    [HarmonyPatch("OnToolUseAnim")]
    internal class PatchOnToolUseAnim
    {
        /*
            // [41 11 - 41 70]
            IL_008e: ldloc.3      // ancestor
            IL_008f: ldarg.0      // this
            IL_0090: ldfld        float32 Knife::damage
            IL_0095: ldloc.0      // position
            IL_0096: ldarg.0      // this
            IL_0097: ldfld        valuetype DamageType Knife::damageType
            IL_009c: ldnull
            IL_009d: callvirt     instance bool LiveMixin::TakeDamage(float32, valuetype [UnityEngine.CoreModule]UnityEngine.Vector3, valuetype DamageType, class [UnityEngine.CoreModule]UnityEngine.GameObject)
            IL_00a2: pop
        */

        static List<CodeInstruction> GetNewCodes()
        {
            /*
                ldarg.0      // __instance
                isinst       ['Assembly-CSharp']Knife
                stloc.0      // knife

                ldloc.0      // knife
                ldnull
                call         bool [UnityEngine.CoreModule]UnityEngine.Object::op_Inequality(class [UnityEngine.CoreModule]UnityEngine.Object, class [UnityEngine.CoreModule]UnityEngine.Object)
                stloc.1      // V_1

                ldloc.1      // V_1
                brfalse.s    ret
           */
            List<CodeInstruction> newCodes = new List<CodeInstruction>();

            // ldarg.0      ancestor      
            CodeInstruction newCode = new CodeInstruction(OpCodes.Ldloc_3);
            newCodes.Add(newCode);

            // isinst       ['Assembly-CSharp']Knife
            newCode = new CodeInstruction(OpCodes.Isinst, typeof(Knife));
            newCodes.Add(newCode);
            
            // ldnull
            newCode = new CodeInstruction(OpCodes.Ldnull);
            newCodes.Add(newCode);

            // call bool    Object::op_Inequality
            newCode = new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(UnityEngine.Object), "op_Inequality"));

            

            return newCodes;
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
            MethodInfo takeDamage = typeof(LiveMixin).GetMethod(nameof(LiveMixin.TakeDamage));

            List<CodeInstruction> newCodes = GetNewCodes();


            for (int i = 0; i < codes.Count; i++)
            {
                
                CodeInstruction code = codes[i];
                if (code.Calls(takeDamage))
                {






                    CodeInstruction minusSeven = codes[i - 7];
                    Logger.Log(Logger.Level.Info, $"hotdog: {minusSeven.ToString()}");
                    LiveMixin liveMixin = codes[i-7].operand as LiveMixin;
                    if (liveMixin != null)
                    {
                        Logger.Log(Logger.Level.Info, "hotdog");
                    }
                }
            }

            foreach (var instruction in instructions)
            {
                Logger.Log(Logger.Level.Info, $"{instruction.opcode.ToString()}	  {(instruction.operand != null ? instruction.operand.ToString() : "")}");
            }

            return codes.AsEnumerable();
        }
    }
}

