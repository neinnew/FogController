using System;
using System.IO;
using UnityEngine;
using System.Xml.Serialization;

// copied from algernon's code help: https://github.com/neinnew/ShadowDistanceFix/blob/master/Settings.cs

namespace FogController
{
    [XmlRoot(ElementName = "FogController", Namespace = "", IsNullable = false)]
    public class FCSettingsFile
    {
        // Version.
        [XmlAttribute("version")]
        public int version = 0;

        [XmlElement("colordecay")]
        public float colordecay { get => FCSettings.colordecay; set => FCSettings.colordecay = value; }

        [XmlElement("fogdensity")]
        public float fogdensity { get => FCSettings.fogdensity; set => FCSettings.fogdensity = value; }

        [XmlElement("noisecontribution")]
        public float noisecontribution { get => FCSettings.noisecontribution; set => FCSettings.noisecontribution = value; }

        [XmlElement("fogheight")]
        public int fogheight { get => FCSettings.fogheight; set => FCSettings.fogheight = value; }

        [XmlElement("horizonheight")]
        public int horizonheight { get => FCSettings.horizonheight; set => FCSettings.horizonheight = value; }

        [XmlElement("fogstart")]
        public int fogstart { get => FCSettings.fogstart; set => FCSettings.fogstart = value; }

        [XmlElement("windspeed")]
        public float windspeed { get => FCSettings.windspeed; set => FCSettings.windspeed = value; }

        [XmlElement("daynightedge")]
        public bool daynightedge { get => FCSettings.daynightedge; set => FCSettings.daynightedge = value; }

        [XmlElement("classicedge")]
        public bool classicedge { get => FCSettings.classicedge; set => FCSettings.classicedge = value; }

        [XmlElement("volumefog")]
        public bool volumefog { get => FCSettings.volumefog; set => FCSettings.volumefog = value; }

        [XmlElement("daynightfog")]
        public bool daynightfog { get => FCSettings.daynightfog; set => FCSettings.daynightfog = value; }

        [XmlElement("classicfog")]
        public bool classicfog { get => FCSettings.classicfog; set => FCSettings.classicfog = value; }

        [XmlElement("inscatteringcolor")]
        public int inscatteringcolor { get => FCSettings.inscatteringcolor; set => FCSettings.inscatteringcolor = value; }

        [XmlElement("insEx")]
        public float insEx { get => FCSettings.insEx; set => FCSettings.insEx = value; }

        [XmlElement("insTs")]
        public float insTs { get => FCSettings.insTs; set => FCSettings.insTs = value; }

        [XmlElement("ins_r")]
        public float ins_r { get => FCSettings.ins_r; set => FCSettings.ins_r = value; }

        [XmlElement("ins_g")]
        public float ins_g { get => FCSettings.ins_g; set => FCSettings.ins_g = value; }

        [XmlElement("ins_b")]
        public float ins_b { get => FCSettings.ins_b; set => FCSettings.ins_b = value; }

        [XmlElement("vol_r")]
        public float vol_r { get => FCSettings.vol_r; set => FCSettings.vol_r = value; }

        [XmlElement("vol_g")]
        public float vol_g { get => FCSettings.vol_g; set => FCSettings.vol_g = value; }

        [XmlElement("vol_b")]
        public float vol_b { get => FCSettings.vol_b; set => FCSettings.vol_b = value; }

        [XmlElement("volcustom")]
        public bool volcustom { get => FCSettings.volcustom; set => FCSettings.volcustom = value; }

    }


    internal static class FCSettings
    {
        private static readonly string SettingsFileName = "FogController.xml";

        internal static float colordecay = 0.2f;
        internal static float fogdensity = 0.00223f;
        internal static float noisecontribution = 1f;
        internal static float windspeed = 0.001f;
        internal static int fogheight = 1000;
        internal static int horizonheight = 800;
        internal static int fogstart = 194;

        internal static bool classicfog = false;
        internal static bool daynightfog = true;
        internal static bool daynightedge = true;
        internal static bool classicedge = true;
        internal static bool volumefog = true;
        internal static bool volcustom = true;

        internal static int inscatteringcolor = 0;

        internal static float ins_r = 0.5647059f;
        internal static float ins_g = 0.9254902f;
        internal static float ins_b = 1f;

        internal static float insEx = 1f;
        internal static float insTs = 1.72f;

        internal static float vol_r = 0.6509804f;
        internal static float vol_g = 0.8862745f;
        internal static float vol_b = 1f;

        /// <summary>
        /// Load settings from XML file.
        /// </summary>
        internal static void LoadSettings()
        {
            // Check to see if configuration file exists.
            if (File.Exists(SettingsFileName))
            {
                // Read it.
                using (StreamReader reader = new StreamReader(SettingsFileName))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(FCSettingsFile));
                    if (!(xmlSerializer.Deserialize(reader) is FCSettingsFile spdSettingsFile))
                    {
                        Debug.Log("Fog Controller: couldn't deserialize settings file");
                    }
                }
            }
        }


        /// <summary>
        /// Save settings to XML file.
        /// </summary>
        internal static void SaveSettings()
        {
            try
            {
                // Pretty straightforward.  Serialisation is within GBRSettingsFile class.
                using (StreamWriter writer = new StreamWriter(SettingsFileName))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(FCSettingsFile));
                    xmlSerializer.Serialize(writer, new FCSettingsFile());
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}