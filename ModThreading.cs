using ICities;
using UnityEngine;

namespace FogController
{
    public class ModThreading : ThreadingExtensionBase
    {
        public static bool disableAtNight;
        RenderProperties renderProperties = null;
        FogEffect fogEffect = null;
        public void OnEnabled()
        {
            FCSettings.LoadSettings();
        }
        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            
            if (FogController.enabledInscol)
            {
                renderProperties ??= UnityEngine.Object.FindObjectOfType<RenderProperties>();

                renderProperties.m_inscatteringColor = DayNightProperties.instance.currentLightColor;
            }

            if (disableAtNight)
            {
                fogEffect ??= UnityEngine.Object.FindObjectOfType<FogEffect>();

                fogEffect.enabled = !SimulationManager.instance.m_isNightTime;
            }
                   
        }
    }
}