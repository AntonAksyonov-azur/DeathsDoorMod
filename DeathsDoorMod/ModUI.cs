using System.Reflection;
using UnityEngine;

namespace DeathsDoorMod
{
    public static class ModUI
    {
        private static GUIStyle _labelStyle;
        
        public static void DrawGUI(ModSettings settings)
        {
            if (_labelStyle == null)
            {
                _labelStyle = new GUIStyle(GUI.skin.GetStyle("Label"))
                {
                    alignment = TextAnchor.MiddleCenter
                };
            }

            settings.AutoSize = GUILayout.Toggle(settings.AutoSize, "Is Auto Size");
            
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Font Size");
                if (GUILayout.Button("<<"))
                {
                    settings.FontSize = Mathf.Clamp( settings.FontSize - 10, 1, int.MaxValue);
                }
                
                if (GUILayout.Button("<"))
                {
                    settings.FontSize = Mathf.Clamp( settings.FontSize - 1, 1, int.MaxValue);
                }
                
                GUILayout.Label( settings.FontSize.ToString(), _labelStyle);
                
                if (GUILayout.Button(">"))
                {
                    settings.FontSize += 1;
                }
                
                if (GUILayout.Button(">>"))
                {
                    settings.FontSize += 10;
                }
            }
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Text");
                settings.Text = GUILayout.TextField(settings.Text);
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Set 1 HP"))
            {
                var fieldInfo = typeof(PlayerGlobal).GetField("dmg", BindingFlags.Instance | BindingFlags.NonPublic);
                var methodInfo = typeof(Damageable).GetMethod("applyHealthChange", BindingFlags.Instance | BindingFlags.NonPublic);
                
                if (fieldInfo != null && methodInfo != null)
                {
                    var dmg = fieldInfo.GetValue(PlayerGlobal.instance) as Damageable;
                    if (dmg != null)
                    {
                        dmg.SetHealth(1);
                        methodInfo.Invoke(dmg, new object[] { 0 });
                    }
                }
            }

            if (GUILayout.Button("Set Full HP"))
            {
                var fieldInfo = typeof(PlayerGlobal).GetField("dmg", BindingFlags.Instance | BindingFlags.NonPublic);
                var methodInfo = typeof(Damageable).GetMethod("applyHealthChange", BindingFlags.Instance | BindingFlags.NonPublic);
                
                if (fieldInfo != null && methodInfo != null)
                {
                    var dmg = fieldInfo.GetValue(PlayerGlobal.instance) as Damageable;
                    if (dmg != null)
                    {
                        dmg.SetHealth(dmg.maxHealth);
                        methodInfo.Invoke(dmg, new object[] { 0 });
                    }
                }
            }
            
            if (GUILayout.Button("Kill"))
            {
                var fieldInfo = typeof(PlayerGlobal).GetField("dmg", BindingFlags.Instance | BindingFlags.NonPublic);
                var methodInfo = typeof(Damageable).GetMethod("applyHealthChange", BindingFlags.Instance | BindingFlags.NonPublic);
                
                if (fieldInfo != null && methodInfo != null)
                {
                    var dmg = fieldInfo.GetValue(PlayerGlobal.instance) as Damageable;
                    if (dmg != null)
                    {
                        dmg.SetHealth(0);
                        methodInfo.Invoke(dmg, new object[] { 0 });
                    }
                }
            }
        }
    }
}