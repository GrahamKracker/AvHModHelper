using System;
using System.Globalization;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace AvHModHelper.UI.Menus;

static class SettingsMenu
{
    public static void GenerateMenu(GameObject parent)
    {
        GameManagerScript.instance.mainMenu.transform.Find("Dropdown")?.SetParent(parent.transform);
        GameManagerScript.instance.mainMenu.transform.Find("Fullscreen Toggle")?.SetParent(parent.transform);
        GenerateSensitivitySlider(parent);
        GenerateVolumeSlider(parent);
    }
    static void GenerateSensitivitySlider(GameObject parent)
    {
        var stepAmount = 0.2f;

        var sensitivitySlider = uObject.Instantiate(Main.baseSlider, parent.transform);
        sensitivitySlider.name = "Sensitivity Slider";
        var slider =  sensitivitySlider.GetComponent<Slider>();
        
        
        slider.maxValue = 10f;
        slider.minValue = 0.1f;
        slider.direction = Slider.Direction.LeftToRight;
        slider.wholeNumbers = false;

        var text = new GameObject("Slider Text");
        text.transform.SetParent(slider.handleRect.transform);
        text.AddComponent<RectTransform>();
        text.AddComponent<TextMeshProUGUI>();
        text.transform.localPosition = new Vector3(90, 25, 0);


        slider.value = Settings.MouseSensitivity;
        text.GetComponent<TextMeshProUGUI>().text = Settings.MouseSensitivity.ToString(CultureInfo.InvariantCulture);

        var numberOfSteps = (int)slider.maxValue / stepAmount;

        slider.onValueChanged.AddListener(value =>
        {
            var range =
                (slider.value /
                 slider.maxValue) *
                numberOfSteps;
            var ceil = Mathf.CeilToInt(range);
            slider.value = ceil * stepAmount;

            text.GetComponent<TextMeshProUGUI>().text = slider.value
                .ToString(CultureInfo.InvariantCulture);
            Settings.MouseSensitivity = slider.value;

            //MelonLogger.Msg("Mouse Sensitivity: " + Settings.MouseSensitivity);
        });
    }

    public static void GenerateVolumeSlider(GameObject parent)
    {
        var stepAmount = 1f;

        var volumeSlider = uObject.Instantiate(Main.baseSlider, parent.transform);
        volumeSlider.name = "Volume Slider";
        var slider =  volumeSlider.GetComponent<Slider>();
        
        
        slider.maxValue = 100;
        slider.minValue = 0f;
        slider.direction = Slider.Direction.LeftToRight;
        slider.wholeNumbers = false;

        var text = new GameObject("Slider Text");
        text.transform.SetParent(slider.handleRect.transform);
        text.AddComponent<RectTransform>();
        text.AddComponent<TextMeshProUGUI>();
        text.transform.localPosition = new Vector3(90, 25, 0);


        slider.value = Settings.Volume;
        text.GetComponent<TextMeshProUGUI>().text = Settings.Volume.ToString(CultureInfo.InvariantCulture);

        var numberOfSteps = (int) slider.maxValue / stepAmount;

        slider.onValueChanged.AddListener(value =>
        {
            var range =
                (slider.value /
                 slider.maxValue) *
                numberOfSteps;
            var ceil = Mathf.CeilToInt(range);
            slider.value = ceil * stepAmount;

            text.GetComponent<TextMeshProUGUI>().text = slider.value
                .ToString(CultureInfo.InvariantCulture);
            PlayerPrefs.SetFloat("AudioVol", (slider.value/100));
            Settings.Volume = slider.value;
            
            //MelonLogger.Msg("AudioVolume: " + PlayerPrefs.GetFloat("AudioVol"));
        });
    }
    
}
