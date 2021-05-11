using System;
using ICities;
using UnityEngine;


namespace FogController
{
    public class FogController : LoadingExtensionBase, IUserMod
    {
        public string Name => "Fog Controller";
        public string Description => "adjust the fog";

        private static readonly string[] InscolLabels =
        {
            "Vanilla",
            "match to sun color",
            "Custom",
        };

        /// <summary>
        /// Called by the game when the mod is enabled.
        /// </summary>
        public void OnEnabled()
        {
            // Load the settings file.
            FCSettings.LoadSettings();
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group = helper.AddGroup("DayNight Fog");
            group.AddCheckbox("Daynight Fog", FCSettings.daynightfog, sel =>
            {

                var dnfog = UnityEngine.Object.FindObjectOfType<DayNightFogEffect>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (dnfog != null)
                {
                    dnfog.enabled = sel;
                }

                // Update and save settings.
                FCSettings.daynightfog = sel;
                FCSettings.SaveSettings();

            });

            group.AddSlider("Color Decay(0.05 ~ 1)", 0.05f, 1, 0.01f, FCSettings.colordecay, sel =>
            {
                var coldecay = UnityEngine.Object.FindObjectOfType<FogProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (coldecay != null)
                {
                    coldecay.m_ColorDecay = sel;
                }

                // Update and save settings.
                FCSettings.colordecay = sel;
                FCSettings.SaveSettings();
            });

            group.AddTextfield("Color Decay", FCSettings.colordecay.ToString(), sel =>
            {
                var coldecay = UnityEngine.Object.FindObjectOfType<FogProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (coldecay != null)
                {
                    coldecay.m_ColorDecay = float.Parse(sel);
                }

                // Update and save settings.
                FCSettings.colordecay = float.Parse(sel);
                FCSettings.SaveSettings();
            });

            group.AddSlider("Fog Density", 0, 0.00223f, 0.0001f, FCSettings.fogdensity, sel =>
            {
                var coldecay = UnityEngine.Object.FindObjectOfType<FogProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (coldecay != null)
                {
                    coldecay.m_FogDensity = sel;
                }

                // Update and save settings.
                FCSettings.fogdensity = sel;
                FCSettings.SaveSettings();

            });

            group.AddSlider("Noise Contribution", 0.1f, 1.4f, 0.01f, FCSettings.noisecontribution, sel =>
            {
                var nocontri = UnityEngine.Object.FindObjectOfType<FogProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (nocontri != null)
                {
                    nocontri.m_NoiseContribution = sel;
                }

                // Update and save settings.
                FCSettings.noisecontribution = sel;
                FCSettings.SaveSettings();

            });

            group.AddTextfield("Fog Height", FCSettings.fogheight.ToString(), sel =>
            {
                var fogheight = UnityEngine.Object.FindObjectOfType<FogProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (fogheight != null)
                {
                    fogheight.m_FogHeight = int.Parse(sel);
                }

                // Update and save settings.
                FCSettings.fogheight = int.Parse(sel);
                FCSettings.SaveSettings();
            });

            group.AddTextfield("Horizon Height", FCSettings.horizonheight.ToString(), sel =>
            {
                var horizonh = UnityEngine.Object.FindObjectOfType<FogProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (horizonh != null)
                {
                    horizonh.m_HorizonHeight = int.Parse(sel);
                }

                // Update and save settings.
                FCSettings.horizonheight = int.Parse(sel);
                FCSettings.SaveSettings();
            });

            group.AddSlider("Fog Visibility", 0, 8000, 1, FCSettings.fogstart, sel =>
            {
                var fogstart = UnityEngine.Object.FindObjectOfType<FogProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (fogstart != null)
                {
                    fogstart.m_FogStart = sel;
                }

                // Update and save settings.
                FCSettings.fogstart = (int)sel;
                FCSettings.SaveSettings();

            });

            group.AddSlider("Wind Speed", 0, 0.01f, 0.0001f, FCSettings.windspeed, sel =>
            {
                var windspeed = UnityEngine.Object.FindObjectOfType<FogProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (windspeed != null)
                {
                    windspeed.m_WindSpeed = sel;
                }

                // Update and save settings.
                FCSettings.windspeed = (int)sel;
                FCSettings.SaveSettings();

            });

            group.AddCheckbox("Edge Fog", FCSettings.daynightedge, sel =>
            {

                var dnedge = UnityEngine.Object.FindObjectOfType<FogProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (dnedge != null)
                {
                    dnedge.m_edgeFog = sel;
                }

                // Update and save settings.
                FCSettings.daynightedge = sel;
                FCSettings.SaveSettings();

            });

            UIHelperBase group2 = helper.AddGroup("Classic Fog");
            group2.AddCheckbox("Classic Fog (Enable Cubemap)", FCSettings.classicfog, sel =>
            {
                var cfog = UnityEngine.Object.FindObjectOfType<FogEffect>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (cfog != null)
                {
                    cfog.enabled = sel;
                }

                // Update and save settings.
                FCSettings.classicfog = sel;
                FCSettings.SaveSettings();

            });

            group2.AddCheckbox("Volume Fog", FCSettings.volumefog, sel =>
            {

                var vfog = UnityEngine.Object.FindObjectOfType<RenderProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (vfog != null)
                {
                    vfog.m_useVolumeFog = sel;
                }

                // Update and save settings.
                FCSettings.volumefog = sel;
                FCSettings.SaveSettings();

            });


            group2.AddSlider("Inscattering Size", -10, -1, 0.1f, FCSettings.insEx, sel =>
            {
                var insex = UnityEngine.Object.FindObjectOfType<RenderProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (insex != null)
                {
                    insex.m_inscatteringExponent = -(float)Math.Pow(sel, 5);
                }

                FCSettings.insEx = sel;
                FCSettings.SaveSettings();
            });

            group2.AddTextfield("Inscattering Intensity", FCSettings.insTs.ToString(), sel =>
            {
                var insts = UnityEngine.Object.FindObjectOfType<RenderProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (insts != null)
                {
                    insts.m_inscatteringIntensity = float.Parse(sel);
                }

                // Update and save settings.
                FCSettings.insTs = float.Parse(sel);
                FCSettings.SaveSettings();
            });

            group2.AddDropdown("Inscattering Color", InscolLabels, FCSettings.inscatteringcolor, sel =>
            {
                FCSettings.inscatteringcolor = sel;
                FCSettings.SaveSettings();

                var inscol = UnityEngine.Object.FindObjectOfType<RenderProperties>();

                if (FCSettings.inscatteringcolor == 2)
                {
                    inscol.m_inscatteringColor = new Color(FCSettings.ins_r, FCSettings.ins_g, FCSettings.ins_b, 1f);
                }

                else if (FCSettings.inscatteringcolor == 0)
                {
                    inscol.m_inscatteringColor = new Color(0.5647059f, 0.9254902f, 1f, 1f);
                }
            });

            group2.AddSlider("R", 0, 1, 0.001f, FCSettings.ins_r, sel =>
            {
                var inscol = UnityEngine.Object.FindObjectOfType<RenderProperties>();

                if (FCSettings.inscatteringcolor == 2)
                {
                    inscol.m_inscatteringColor = new Color(FCSettings.ins_r, FCSettings.ins_g, FCSettings.ins_b, 1f);
                }

                FCSettings.ins_r = sel;
                FCSettings.SaveSettings();
            });

            group2.AddSlider("G", 0, 1, 0.001f, FCSettings.ins_g, sel =>
            {
                var inscol = UnityEngine.Object.FindObjectOfType<RenderProperties>();

                if (FCSettings.inscatteringcolor == 2)
                {
                    inscol.m_inscatteringColor = new Color(FCSettings.ins_r, FCSettings.ins_g, FCSettings.ins_b, 1f);
                }

                FCSettings.ins_g = sel;
                FCSettings.SaveSettings();
            });

            group2.AddSlider("B", 0, 1, 0.001f, FCSettings.ins_b, sel =>
            {
                var inscol = UnityEngine.Object.FindObjectOfType<RenderProperties>();

                if (FCSettings.inscatteringcolor == 2)
                {
                    inscol.m_inscatteringColor = new Color(FCSettings.ins_r, FCSettings.ins_g, FCSettings.ins_b, 1f);
                }

                FCSettings.ins_b = sel;
                FCSettings.SaveSettings();
            });


            group2.AddCheckbox("Custom Volume Fog Color", FCSettings.volcustom, sel =>
            {
                FCSettings.volcustom = sel;
                FCSettings.SaveSettings();

                var inscol = UnityEngine.Object.FindObjectOfType<RenderProperties>();

                if (FCSettings.volcustom == false)
                {
                    inscol.m_volumeFogColor = new Color(0.6509804f, 0.8862745f, 1f, 1f);
                }
                else
                {
                    inscol.m_volumeFogColor = new Color(FCSettings.vol_r, FCSettings.vol_g, FCSettings.vol_b, 1f);
                }
            });

            group2.AddSlider("R", 0, 1, 0.001f, FCSettings.vol_r, sel =>
            {
                var inscol = UnityEngine.Object.FindObjectOfType<RenderProperties>();

                if (FCSettings.volcustom)
                {
                    inscol.m_volumeFogColor = new Color(FCSettings.vol_r, FCSettings.vol_g, FCSettings.vol_b, 1f);
                }

                FCSettings.vol_r = sel;
                FCSettings.SaveSettings();
            }); ;

            group2.AddSlider("G", 0, 1, 0.001f, FCSettings.vol_g, sel =>
            {
                var inscol = UnityEngine.Object.FindObjectOfType<RenderProperties>();

                if (FCSettings.volcustom)
                {
                    inscol.m_volumeFogColor = new Color(FCSettings.vol_r, FCSettings.vol_g, FCSettings.vol_b, 1f);
                }

                FCSettings.vol_g = sel;
                FCSettings.SaveSettings();
            });

            group2.AddSlider("B", 0, 1, 0.001f, FCSettings.vol_b, sel =>
            {
                var inscol = UnityEngine.Object.FindObjectOfType<RenderProperties>();

                if (FCSettings.volcustom)
                {
                    inscol.m_volumeFogColor = new Color(FCSettings.vol_r, FCSettings.vol_g, FCSettings.vol_b, 1f);
                }

                FCSettings.vol_b = sel;
                FCSettings.SaveSettings();
            });

            group2.AddCheckbox("Edge Fog", FCSettings.classicedge, sel =>
            {

                var cedge = UnityEngine.Object.FindObjectOfType<FogEffect>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (cedge != null)
                {
                    cedge.m_edgeFog = sel;
                }

                // Update and save settings.
                FCSettings.classicedge = sel;
                FCSettings.SaveSettings();

                var vfstart = UnityEngine.Object.FindObjectOfType<RenderProperties>();

                if (FCSettings.classicedge)
                {
                    vfstart.m_volumeFogStart = 1711;
                }
                else
                {
                    vfstart.m_volumeFogStart = 0;
                }

            });

            UIHelperBase groupReset = helper.AddGroup("Reset");
            groupReset.AddButton("Reset to Default", () => 
            {
                FCSettings.colordecay = 0.2f;
                FCSettings.fogdensity = 0.00223f;
                FCSettings.noisecontribution = 1f;
                FCSettings.windspeed = 0.001f;
                FCSettings.fogheight = 1000;
                FCSettings.horizonheight = 800;
                FCSettings.fogstart = 194;

                FCSettings.classicfog = false;
                FCSettings.daynightfog = true;
                FCSettings.daynightedge = true;
                FCSettings.classicedge = true;
                FCSettings.volumefog = true;
                FCSettings.volcustom = true;

                FCSettings.inscatteringcolor = 0;

                FCSettings.ins_r = 0.5647059f;
                FCSettings.ins_g = 0.9254902f;
                FCSettings.ins_b = 1f;

                FCSettings.insEx = 1f;
                FCSettings.insTs = 1.72f;

                FCSettings.vol_r = 0.6509804f;
                FCSettings.vol_g = 0.8862745f;
                FCSettings.vol_b = 1f;

                FCSettings.SaveSettings();
                FCSettings.LoadSettings();
            });
        }
        public override void OnLevelLoaded(LoadMode mode)
        {    
            var fc = UnityEngine.Object.FindObjectOfType<FogProperties>();
            fc.m_ColorDecay = FCSettings.colordecay;
            fc.m_FogDensity = FCSettings.fogdensity;
            fc.m_NoiseContribution = FCSettings.noisecontribution;
            fc.m_edgeFog = FCSettings.daynightedge;
            fc.m_FogHeight = FCSettings.fogheight;
            fc.m_HorizonHeight = FCSettings.horizonheight;
            fc.m_FogStart = FCSettings.fogstart;
            fc.m_WindSpeed = FCSettings.windspeed;

            var fc2 = UnityEngine.Object.FindObjectOfType<FogEffect>();
            fc2.enabled = FCSettings.classicfog;
            fc2.m_edgeFog = FCSettings.classicedge;

            var fc3 = UnityEngine.Object.FindObjectOfType<DayNightFogEffect>();
            fc3.enabled = FCSettings.daynightfog;

            var fc4 = UnityEngine.Object.FindObjectOfType<RenderProperties>();
            fc4.m_useVolumeFog = FCSettings.volumefog;
            fc4.m_inscatteringExponent = (float)-Math.Pow(FCSettings.insEx, 5);
            fc4.m_inscatteringIntensity = FCSettings.insTs;

            if (FCSettings.inscatteringcolor == 2)
            {
                fc4.m_inscatteringColor = new Color(FCSettings.ins_r, FCSettings.ins_g, FCSettings.ins_b, 1f);
            }
            else if (FCSettings.inscatteringcolor == 0)
            {
                fc4.m_inscatteringColor = new Color(0.5647059f, 0.9254902f, 1f, 1f);
            }

            if (FCSettings.volcustom == false)
            {
                fc4.m_volumeFogColor = new Color(0.6509804f, 0.8862745f, 1f, 1f);
            }
            else
            {
                fc4.m_volumeFogColor = new Color(FCSettings.vol_r, FCSettings.vol_g, FCSettings.vol_b, 1f);
            }

            if (FCSettings.classicedge)
            {
                fc4.m_volumeFogStart = 1711;
            }
            else
            {
                fc4.m_volumeFogStart = 0;
            }
        }
    }
}
