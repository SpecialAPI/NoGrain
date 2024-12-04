using BepInEx;
using HarmonyLib;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace NoGrain
{
    [BepInPlugin(GUID, "No Grain", "1.0.0")]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "SpecialAPI.NoGrain";

        public void Awake()
        {
            new Harmony(GUID).PatchAll();
        }

        [HarmonyPatch(typeof(PostProcessVolume), "OnEnable")]
        [HarmonyPostfix]
        public static void KillGrain(PostProcessVolume __instance)
        {
            if(__instance.profile != null)
            {
                var grain = __instance.profile.settings.OfType<Grain>();

                foreach(var g in grain)
                {
                    g.enabled.value = false;
                    g.active = false;
                    g.intensity.value = 0f;
                }
            }
        }
    }
}
