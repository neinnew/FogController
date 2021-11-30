using ICities;
using UnityEngine;

namespace FogController
{
    public class ModThreading : ThreadingExtensionBase
    {
        private bool RPnull = true;
        RenderProperties renderProperties = null;
        public void OnEnabled()
        {
            FCSettings.LoadSettings();
            
        }
        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            
            if (FogController.enabledInscol)
            {
                
                if (RPnull)
                {
                    renderProperties = UnityEngine.Object.FindObjectOfType<RenderProperties>();
                    RPnull = false;
                }
                renderProperties.m_inscatteringColor = DayNightProperties.instance.currentLightColor;
            }
                   
        }
    }
}