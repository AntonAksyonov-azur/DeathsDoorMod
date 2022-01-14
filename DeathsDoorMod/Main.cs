using System.IO;
using System.Linq;
using TinyJson;
using TMPro;
using UnityEngine;
using UnityModManagerNet;

namespace DeathsDoorMod
{
    public class Main
    {
        private static ModSettings _modSettings;
        private static bool _executedFlag;
        
        public static void Load(UnityModManager.ModEntry modEntry)
        {
            modEntry.OnToggle = OnToggle;
            
            modEntry.OnGUI = OnGUI;
            modEntry.OnHideGUI = OnHideGUI;
            
            modEntry.OnUpdate = OnUpdate;
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool isOn)
        {
            modEntry.Logger.Log(isOn ? "DeathsDoorMod enabled" : "DeathsDoorMod disabled");
            if (isOn)
            {
                var pathToSettings = Path.Combine(modEntry.Path, "settings.json");
                if (File.Exists(pathToSettings))
                {
                    var file = File.ReadAllText(pathToSettings);
                    _modSettings = file.FromJson<ModSettings>();
                }
                else
                {
                    _modSettings = new ModSettings(false, 128, "НЕ СМЕРТЬ");
                }
            }
            
            return true;
        }
        
        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            ModUI.DrawGUI(_modSettings);
        }
        
        private static void OnHideGUI(UnityModManager.ModEntry modEntry)
        {
            var pathToSettings = Path.Combine(modEntry.Path, "settings.json");
            var json = _modSettings.ToJson();
            File.WriteAllText(pathToSettings, json);
        }
        
        private static void OnUpdate(UnityModManager.ModEntry modEntry, float deltaTime)
        {
            if (PlayerGlobal.instance == null)
            {
                return;
            }

            if (_executedFlag)
            {
                _executedFlag = !PlayerGlobal.instance.IsAlive();
                return;
            }

            if (!PlayerGlobal.instance.IsAlive())
            {
                var deathTextObject = Object.FindObjectOfType<DeathText>();
                if (deathTextObject != null)
                {
                    modEntry.Logger.Log("DeathsDoorMod catch death");
                    var languageObjectSwitch = deathTextObject.textObj
                        .GetComponentsInChildren<Object>()
                        .FirstOrDefault(a => a is LanguageObjectSwitch) as LanguageObjectSwitch;
                    if (languageObjectSwitch != null)
                    {
                        var russian = languageObjectSwitch.objNonEnglishSpecific[(int)DialogueManager.Language.Russian];
                        
                        var tmp = russian.GetComponent<TextMeshProUGUI>();
                        tmp.enableAutoSizing = _modSettings.AutoSize;
                        tmp.fontSize = _modSettings.FontSize;
                        tmp.text = _modSettings.Text;
                    }
                }

                _executedFlag = true;
            }
        }
    }
}