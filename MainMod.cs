using System;

[assembly: MelonInfo(typeof(MainMod), "AvHModHelper", "1.0.0", "GrahamKracker")]
[assembly: MelonGame("Sayan", "Apes vs Helium")]
[assembly: MelonPriority(-1000)]
[assembly: MelonColor(ConsoleColor.DarkBlue)]
[assembly: MelonAuthorColor(ConsoleColor.DarkRed)]


namespace AvHModHelper;

using System.Linq;
using Extensions.Types;
using UnityStandardAssets.Characters.FirstPerson;

internal class MainMod : AvHMod
{
    public override void OnApplicationStart()
    {
        MelonLogger.Msg("AvHModHelper Loaded");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKey(KeyCode.Space) && !(bool) EquipmentScript.instance.gameObject.GetComponent<FirstPersonController>().GetPrivateValue("m_Jumping")) EquipmentScript.instance.gameObject.GetComponent<FirstPersonController>().SetPrivateValue("m_Jump", true);
    }

    [HarmonyPatch(typeof(EquipmentScript), "Start")]
    internal static class EquipmentScriptStart
    {
        [HarmonyPostfix]
        public static void Postfix(ref EquipmentScript __instance)
        {
            var root = __instance.gameObject.scene.GetRootGameObjects().ToList();
            for (int i = 0; i < __instance.transform.childCount; i++)
            {
                if (__instance.transform.GetChild(i).name != "FirstPersonCharacter") continue;

                var person = __instance.transform.GetChild(i);
                var shadowMonkey = uObject.Instantiate(root.Find(x => x.name == "Shop").transform.Find("Monkey Follower"), __instance.transform);
                var monkeybasic = shadowMonkey.Find("Monkey Basic");
                var monkeybase = monkeybasic.Find("Monke Base");
                monkeybase.GetComponent<SkinnedMeshRenderer>().enabled = false;

                shadowMonkey.gameObject.AddComponent<ShadowMonkeyMono>();
                monkeybase.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                shadowMonkey.name = "Player Shadow";
                shadowMonkey.localPosition = new Vector3(0, - 0.9f, - 1.0002f);
                shadowMonkey.localRotation = Quaternion.Euler(0, 0, 0);
                shadowMonkey.localScale = new Vector3(1, 1.75f, 1);
                monkeybase.GetComponent<SkinnedMeshRenderer>().enabled = true;

                break;
            }
        }
    }
}
