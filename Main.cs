using System;
using System.Linq;
using AvHModHelper.Extensions.Stream;
using AvHModHelper.Extensions.Types;
using AvHModHelper.MonoBehaviors;
using AvHModHelper.UI.Menus;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityStandardAssets.Characters.FirstPerson;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Main = AvHModHelper.Main;
using Slider = UnityEngine.UI.Slider;

[assembly: MelonInfo(typeof(Main), "AvHModHelper", "1.0.0", "GrahamKracker")]
[assembly: MelonGame("Sayan", "Apes vs Helium")]
[assembly: MelonPriority(-1000)]
[assembly: MelonColor(ConsoleColor.DarkBlue)]
[assembly: MelonAuthorColor(ConsoleColor.DarkRed)]


namespace AvHModHelper;

internal class Main : AvHMod
{
    bool mapLoaded = false;
    public static GameObject baseSlider;
    public static GameObject baseToggle;
    public static GameObject baseDropdown;
    public static GameObject baseInputField;
    public override void OnApplicationStart()
    {
        var ourFirstCategory = MelonPreferences.CreateCategory("AvHModHelper");
        var ourFirstEntry =
            ourFirstCategory.CreateEntry<string>("TargetsFilePath", "YourAvHFolderWITHOUTTRAILINGSLASH");
        MelonPreferences.Save();
        TargetsMaker.CreateTargetsFile(ourFirstEntry.Value);

        var menuBundle =
            AssetBundle.LoadFromStream(
                MelonAssembly.Assembly.GetManifestResourceStream("AvHModHelper.AssetBundles.menu.bundle"));

        foreach (var asset in menuBundle.GetAllAssetNames())
        {
            MelonLogger.Msg(asset);
        }
        baseSlider = menuBundle.LoadAsset<GameObject>("assets/slider.prefab");
        baseToggle = menuBundle.LoadAsset<GameObject>("assets/toggle.prefab");
        baseDropdown = menuBundle.LoadAsset<GameObject>("assets/dropdown.prefab");
        baseInputField = menuBundle.LoadAsset<GameObject>("assets/inputfield.prefab");
        
        MelonLogger.Msg("AvHModHelper Loaded");
    }

    public override void OnMainMenuScene()
    {
        base.OnMainMenuScene();
        if (GameManagerScript.instance.mainMenu.transform.Find("Settings Text") is not null)
            return;
        var settingsText = uObject.Instantiate(GameManagerScript.instance.mainMenu.transform.Find("Version Text"),
            GameManagerScript.instance.mainMenu.transform);
        settingsText.name = "Settings Text";
        var tmp = settingsText.GetComponent<TextMeshProUGUI>();
        tmp.text = "SETTINGS";
        tmp.fontSize = 90;
        tmp.autoSizeTextContainer = true;
        tmp.characterSpacing = -5;
        settingsText.localPosition = new Vector3(-750, -290, 0);
        tmp.raycastTarget = true;


        var parent = GameManagerScript.instance.mainMenu.transform.parent;
        var settingsPanel = new GameObject("Settings Menu")
        {
            transform =
            {
                parent = parent,
                localPosition = new Vector3(0, 0, 0),
            }
        };
        settingsPanel.AddComponent<RectTransform>();
        settingsPanel.SetActive(false);

        var backButton = uObject.Instantiate(
            parent.Find("LevelSelect").Find("Levels").Find("BackButton").gameObject,
            settingsPanel.transform);
        
        backButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            settingsPanel.SetActive(false);
            GameManagerScript.instance.mainMenu.SetActive(true);
        });
        backButton.name = "BackButton";
        backButton.SetActive(true);

        
        SettingsMenu.GenerateMenu(settingsPanel);
        

        var textButton = settingsText.gameObject.AddComponent<Button>();
        textButton.onClick.AddListener(() =>
        {
            GameManagerScript.instance.mainMenu.SetActive(false);
            settingsPanel.SetActive(true);
        });
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
                
                var ml = (MouseLook) __instance.GetComponent<FirstPersonController>().GetPrivateValue("m_MouseLook");
                ml.YSensitivity *= Settings.MouseSensitivity;
                ml.XSensitivity *= Settings.MouseSensitivity;
                __instance.GetComponent<FirstPersonController>().SetPrivateValue("m_MouseLook", ml);
                
                var shadowMonkey =
                    uObject.Instantiate(root.Find(x => x.name == "Shop").transform.Find("Monkey Follower"),
                        __instance.transform);
                var monkeybasic = shadowMonkey.Find("Monkey Basic");
                var monkeybase = monkeybasic.Find("Monke Base");
                monkeybase.GetComponent<SkinnedMeshRenderer>().enabled = false;
                uObject.Destroy(shadowMonkey.gameObject.GetComponent<Animator>());
                uObject.Destroy(shadowMonkey.gameObject.GetComponent<BlinkingScript>());

                shadowMonkey.gameObject.AddComponent<ShadowMonkey>();
                monkeybase.GetComponent<SkinnedMeshRenderer>().shadowCastingMode =
                    ShadowCastingMode.ShadowsOnly;
                shadowMonkey.name = "Player Shadow";
                shadowMonkey.localPosition = new Vector3(0f, -0.9f, 0f);
                shadowMonkey.localRotation = Quaternion.Euler(0, 0, 0);
                shadowMonkey.localScale = new Vector3(1, 1.75f, 1);
                monkeybase.GetComponent<SkinnedMeshRenderer>().enabled = true;

                break;
            }
        }
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!mapLoaded) return;
        if (Input.GetKey(KeyCode.Space) && !(bool)EquipmentScript.instance.gameObject
                .GetComponent<FirstPersonController>().GetPrivateValue("m_Jumping"))
            EquipmentScript.instance.gameObject.GetComponent<FirstPersonController>().SetPrivateValue("m_Jump", true);
    }

    public override void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        base.OnSceneWasInitialized(buildIndex, sceneName);
        if (sceneName != "MainMenu")
        {
            mapLoaded = true;
        }
        else if (sceneName == "MainMenu")
            mapLoaded = false;

        foreach (var component in GameObject.Find("Post-process Volume").GetComponent<Volume>().profile.components)
        {
            if (component.name != "PaniniProjection(Clone)") continue;
            component.active = false;
        }
    }
}
