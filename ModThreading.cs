using ICities;
using UnityEngine;

namespace FogController
{
    public class ModThreading : ThreadingExtensionBase
    {
        public void OnEnabled()
        {
            FCSettings.LoadSettings();
        }
        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            var inscol = UnityEngine.Object.FindObjectOfType<RenderProperties>();

            if (FCSettings.inscatteringcolor == 1)
            {
                inscol.m_inscatteringColor = DayNightProperties.instance.currentLightColor;
            }
                   
        }
    }
}