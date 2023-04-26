using BepInEx;
using GorillaLocomotion;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
    [BepInPlugin(GUID, Name, Version)]
    [BepInDependency("Crafterbot.MonkeStatistics")]
    public class Main : BaseUnityPlugin
    {
        internal const string
            GUID = "crafterbot.fps",
            Name = "FPS Counter",
            Version = "1.0.0";
        internal static bool Enabled;

        internal static Text TextObj;
        private void Awake()
        {
            Logger.LogInfo("Init : " + Name);
            Utilla.Events.GameInitialized += Events_GameInitialized;

            MonkeStatistics.API.Registry.AddAssembly();
        }

        private void Events_GameInitialized(object sender, System.EventArgs e)
        {
            Transform TextTransform = Instantiate(GetTextAsset()).transform;
            TextTransform.SetParent(Player.Instance.headCollider.transform);
            TextTransform.localPosition = new Vector3(-0.2406f, -0.2454f, 0.3655f);
            TextTransform.localRotation = Quaternion.Euler(0, 0, 0);

            TextObj = TextTransform.GetComponentInChildren<Text>();
            TextObj.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (TextObj == null)
                return;
            if (TextObj.gameObject.activeSelf)
            {
                int FramesPerSecond = (int)(1f / Time.deltaTime);
                TextObj.text = $"FPS:{FramesPerSecond}";
            }
        }

        private GameObject GetTextAsset()
        {
            string Url = "FPS.Resources.fps";
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Url);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            return AssetBundle.LoadFromMemory(buffer).LoadAsset("TextObj") as GameObject;
        }
    }
}
/*
// Offset: -0.517,-0.138,0.608
Middle: -0.2406, -0.2454, 0.3655
Secondary: -0.3406, -0.3454, 0.3655
*/