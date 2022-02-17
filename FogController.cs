using System;
using ICities;
using UnityEngine;
using ColossalFramework;
using ColossalFramework.UI;


namespace FogController
{
    public class FogController : LoadingExtensionBase, IUserMod
    {
        public string Name => "Fog Controller";
        public string Description => "adjust the fog";

        public static bool enabledInscol = false;
        private static readonly string[] InscolLabels =
        {
            "Vanilla",
            "match to sun color",
            "Custom",
        };

        private UISlider sliderColorDecay;
        private UISlider sliderFogDensity;
        private UISlider sliderNoiseContribution;
        private UISlider sliderFogVisibility;
        private UISlider sliderWindSpeed;

        private UICheckBox checkBoxDisableAtNight;

        /// <summary>
        /// Called by the game when the mod is enabled.
        /// </summary>
        public void OnEnabled()
        {
            // Load the settings file.
            FCSettings.LoadSettings();
            // 
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

            sliderColorDecay = (UISlider)group.AddSlider("Color Decay", 0.05f, 1, 0.01f, FCSettings.colordecay, sel =>
            {
                var coldecay = UnityEngine.Object.FindObjectOfType<FogProperties>();
                if (coldecay != null) coldecay.m_ColorDecay = sel;

                // Update and save settings.
                UpdateLabelValue(sliderColorDecay, sel.ToString());
                FCSettings.colordecay = sel;
                FCSettings.SaveSettings();
            });
            SetSliderLabel(sliderColorDecay, FCSettings.colordecay.ToString());

            #region Legacy
            /*group.AddTextfield("Color Decay", FCSettings.colordecay.ToString(), sel =>
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
            });*/
            #endregion

            sliderFogDensity = (UISlider)group.AddSlider("Fog Density", 0, 0.00223f, 0.00001f, FCSettings.fogdensity, sel =>
            {
                var coldecay = UnityEngine.Object.FindObjectOfType<FogProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (coldecay != null)
                {
                    coldecay.m_FogDensity = sel;
                }

                // Update and save settings.
                UpdateLabelValue(sliderFogDensity, sel.ToString());
                FCSettings.fogdensity = sel;
                FCSettings.SaveSettings();

            });
            SetSliderLabel(sliderFogDensity, FCSettings.fogdensity.ToString());

            sliderNoiseContribution = (UISlider)group.AddSlider("Noise Contribution", 0.1f, 1.4f, 0.01f, FCSettings.noisecontribution, sel =>
            {
                var nocontri = UnityEngine.Object.FindObjectOfType<FogProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (nocontri != null)
                {
                    nocontri.m_NoiseContribution = sel;
                }

                // Update and save settings.
                UpdateLabelValue(sliderNoiseContribution, sel.ToString());
                FCSettings.noisecontribution = sel;
                FCSettings.SaveSettings();
            });
            SetSliderLabel(sliderNoiseContribution, FCSettings.noisecontribution.ToString());

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

            sliderFogVisibility = (UISlider)group.AddSlider("Fog Visibility", 0, 8000, 1, FCSettings.fogstart, sel =>
            {
                var fogstart = UnityEngine.Object.FindObjectOfType<FogProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (fogstart != null)
                {
                    fogstart.m_FogStart = sel;
                }

                // Update and save settings.
                UpdateLabelValue(sliderFogVisibility, sel.ToString());
                FCSettings.fogstart = (int)sel;
                FCSettings.SaveSettings();

            });
            SetSliderLabel(sliderFogVisibility, FCSettings.fogstart.ToString());

            sliderWindSpeed = (UISlider)group.AddSlider("Wind Speed", 0, 0.01f, 0.0001f, FCSettings.windspeed, sel =>
            {
                var windspeed = UnityEngine.Object.FindObjectOfType<FogProperties>();

                // Null check - for e.g. access from main menu options before game has loaded.
                if (windspeed != null)
                {
                    windspeed.m_WindSpeed = sel;
                }

                // Update and save settings.
                UpdateLabelValue(sliderWindSpeed, sel.ToString());
                FCSettings.windspeed = (int)sel;
                FCSettings.SaveSettings();
            });
            SetSliderLabel(sliderWindSpeed, FCSettings.windspeed.ToString());

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
                if (cfog != null) cfog.enabled = sel;

                // Prevent the disabled DisableAtNight causes turn on fog regardless of this.
                checkBoxDisableAtNight.isEnabled = sel;
                if(!sel) ModThreading.disableAtNight = sel;

                // Update and save settings.
                FCSettings.classicfog = sel;
                FCSettings.SaveSettings();

            });

            checkBoxDisableAtNight = (UICheckBox)group2.AddCheckbox("Disable at Night", FCSettings.disableatnight, sel =>
            {
               
                ModThreading.disableAtNight = sel;

                if (!sel)
                {
                    FogEffect fogEffect = UnityEngine.Object.FindObjectOfType<FogEffect>();
                    if (fogEffect != null)
                    {
                        fogEffect.enabled = true;
                    }
                    
                }

                FCSettings.disableatnight = sel;
                FCSettings.SaveSettings();
            });
            checkBoxDisableAtNight.isEnabled = FCSettings.classicfog;
            checkBoxDisableAtNight.checkedBoxObject.parent.position = new Vector3(14f, -3f, 0f);
            checkBoxDisableAtNight.label.padding.left = 14;

            /*var mainPanel = checkBoxDisableAtNight.parent.parent as UIScrollablePanel;
            var uIPanel = mainPanel.AddUIComponent<UIPanel>();
            uIPanel.autoLayout = false;
            checkBoxDisableAtNight.relativePosition = new Vector3(28f, 0f);*/


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
                var inscol = UnityEngine.Object.FindObjectOfType<RenderProperties>();

                if (inscol != null)
                {
                    switch (sel)
                    {
                        case 0:
                            enabledInscol = false;
                            inscol.m_inscatteringColor = new Color(0.5647059f, 0.9254902f, 1f, 1f);
                            break;
                        case 1:
                            enabledInscol = true;
                            break;
                        case 2:
                            enabledInscol = false;
                            inscol.m_inscatteringColor = new Color(FCSettings.ins_r, FCSettings.ins_g, FCSettings.ins_b, 1f);
                            break;
                    }
                }
                // Update and save settings.
                FCSettings.inscatteringcolor = sel;
                FCSettings.SaveSettings();
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
                FCSettings.disableatnight = false;
                ModThreading.disableAtNight = false;
                FCSettings.daynightfog = true;
                FCSettings.daynightedge = true;
                FCSettings.classicedge = true;
                FCSettings.volumefog = true;
                FCSettings.volcustom = true;

                FCSettings.inscatteringcolor = 0;
                enabledInscol = false;

                FCSettings.ins_r = 0.5647059f;
                FCSettings.ins_g = 0.9254902f;
                FCSettings.ins_b = 1f;

                FCSettings.insEx = -1.11457f;
                FCSettings.insTs = 1.72f;

                FCSettings.vol_r = 0.6509804f;
                FCSettings.vol_g = 0.8862745f;
                FCSettings.vol_b = 1f;

                FCSettings.SaveSettings();

                var fc = UnityEngine.Object.FindObjectOfType<FogProperties>();
                if (fc != null)
                {
                    fc.m_ColorDecay = FCSettings.colordecay;
                    fc.m_FogDensity = FCSettings.fogdensity;
                    fc.m_NoiseContribution = FCSettings.noisecontribution;
                    fc.m_edgeFog = FCSettings.daynightedge;
                    fc.m_FogHeight = FCSettings.fogheight;
                    fc.m_HorizonHeight = FCSettings.horizonheight;
                    fc.m_FogStart = FCSettings.fogstart;
                    fc.m_WindSpeed = FCSettings.windspeed;
                }
                
                var fc2 = UnityEngine.Object.FindObjectOfType<FogEffect>();
                if (fc2 != null)
                {
                    fc2.enabled = FCSettings.classicfog;
                    fc2.m_edgeFog = FCSettings.classicedge;
                }

                var fc3 = UnityEngine.Object.FindObjectOfType<DayNightFogEffect>();
                if (fc3 != null)
                {
                    fc3.enabled = FCSettings.daynightfog;
                }

                var fc4 = UnityEngine.Object.FindObjectOfType<RenderProperties>();
                if (fc4 != null)
                {
                    fc4.m_useVolumeFog = FCSettings.volumefog;
                    fc4.m_inscatteringExponent = (float)-Math.Pow(FCSettings.insEx, 5);
                    fc4.m_inscatteringIntensity = FCSettings.insTs;
                    fc4.m_inscatteringColor = new Color(0.5647059f, 0.9254902f, 1f, 1f);
                }
                

                FCSettings.LoadSettings();
                

                /*sliderColorDecay.value = FCSettings.colordecay;
                UpdateLabelValue(sliderColorDecay, FCSettings.colordecay.ToString());

                UIComponent[] ui = sliderColorDecay.parent.parent.GetComponentsInChildren<UIComponent>();
                string s = "";
                foreach (UIComponent u in ui)
                {
                    s += u.name + " ";

                    if(u is UITextField)
                    {
                        var _u = u as UITextField;
                        _u.text = _u.
                    }
                    else if(u is UISlider)
                    {
                        var _u = u as UISlider;
                        _u.
                    }
                }
                ExceptionPanel panel = UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel");
                panel.SetMessage("Debug", s, false);*/
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

            switch (FCSettings.inscatteringcolor)
            {
                case 0:
                    enabledInscol = false;
                    fc4.m_inscatteringColor = new Color(0.5647059f, 0.9254902f, 1f, 1f);
                    break;
                case 1:
                    enabledInscol = true;
                    break;
                case 2:
                    enabledInscol = false;
                    fc4.m_inscatteringColor = new Color(FCSettings.ins_r, FCSettings.ins_g, FCSettings.ins_b, 1f);
                    break;
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

            ModThreading.disableAtNight = FCSettings.disableatnight;
        }
        void UpdateLabelValue(UIComponent uic, string sel)
        {
            UILabel[] ui = uic.GetComponentsInChildren<UILabel>();
            ui[0].text = sel;
        }

        void SetSliderLabel(UISlider uic, string settedValue)
        {
            uic.size = new Vector2(400f, uic.size.y);

            var valueLabel = uic.AddUIComponent<UILabel>();
            valueLabel.text = settedValue;
            valueLabel.position = new Vector3(uic.size.x + 20f, 0f, 0f);

            var minValueLabel = uic.AddUIComponent<UILabel>();
            var maxValueLabel = uic.AddUIComponent<UILabel>();

            minValueLabel.text = uic.minValue.ToString();
            maxValueLabel.text = uic.maxValue.ToString();

            minValueLabel.textScale = 0.8f;
            maxValueLabel.textScale = 0.8f;

            minValueLabel.color = new Color32(minValueLabel.color.r, minValueLabel.color.g, minValueLabel.color.b, 50);
            maxValueLabel.color = new Color32(maxValueLabel.color.r, maxValueLabel.color.g, maxValueLabel.color.b, 50);

            /*minValueLabel.anchor = UIAnchorStyle.Bottom | UIAnchorStyle.Left;
            maxValueLabel.anchor = UIAnchorStyle.Bottom | UIAnchorStyle.Right;

            minValueLabel.pivot = UIPivotPoint.BottomLeft;
            maxValueLabel.pivot = UIPivotPoint.BottomRight;

            minValueLabel.verticalAlignment = UIVerticalAlignment.Bottom;
            maxValueLabel.verticalAlignment = UIVerticalAlignment.Bottom;*/

            /*minValueLabel.position = new Vector3(uic.position.x, minValueLabel.size.y - uic.size.y, 0f);
            maxValueLabel.position = new Vector3(uic.size.x - maxValueLabel.size.x, maxValueLabel.size.y - uic.size.y, 0f);*/

            minValueLabel.position = new Vector3(uic.position.x, -uic.size.y, 0f);
            maxValueLabel.position = new Vector3(uic.size.x - maxValueLabel.size.x, -uic.size.y, 0f);

            uic.parent.size = new Vector2(uic.parent.size.x, uic.parent.size.y + 10f);
        }
    }
}
