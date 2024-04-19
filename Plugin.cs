using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace InstantRuneText
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class InstantRuneTextPlugin : BaseUnityPlugin
    {
        internal const string ModName = "InstantRuneText";
        internal const string ModVersion = "1.0.3";
        internal const string Author = "Azumatt";
        private const string ModGUID = Author + "." + ModName;
        private readonly Harmony _harmony = new(ModGUID);

        public void Awake()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            _harmony.PatchAll(assembly);
        }
    }

    [HarmonyPatch(typeof(TextViewer), nameof(TextViewer.ShowText))]
    static class TextViewerShowTextPatch
    {
        static void Prefix(TextViewer __instance, TextViewer.Style style, string topic, string textId, bool autoHide)
        {
            if (style != TextViewer.Style.Rune) return;
            if (Player.m_localPlayer == null) return;
            
            // Set this to a high value to make the text appear instantly. 15 seems too low..and I wasn't about to mess with it for 20 minutes
            // so I said fuck it and made it fast as shit.
            __instance.m_animator.speed = 500.0f;
        }
    }
}