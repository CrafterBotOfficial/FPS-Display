using MonkeStatistics.API;
using UnityEngine;

namespace FPS.Pages
{
    [DisplayInMainMenu("FPS Settings")]
    internal class Primary : Page
    {
        public override void OnPageOpen()
        {
            base.OnPageOpen();
            SetTitle("FPS Settings");
            SetAuthor("By Crafterbot");

            Build();
            SetLines();
        }

        private void Build()
        {
            TextLines = new Line[0]; // force reset lines
            AddLine("[Enabled]", new ButtonInfo(OnToggleEnable, 0, ButtonInfo.ButtonType.Toggle, Main.Enabled));
            AddLine(1);
            AddLine("Adjust Offset");
            AddLine("Primary", new ButtonInfo(OnOffsetAdjust, 0));
            AddLine("Secondary", new ButtonInfo(OnOffsetAdjust, 1));
        }

        private void OnToggleEnable(object Sender, object[] Args)
        {
            Main.Enabled = (bool)Args[1];
            Main.TextObj.gameObject.SetActive(Main.Enabled);
        }
        private void OnOffsetAdjust(object Sender, object[] Args)
        {
            int Index = (int)Args[0];
            Vector3 NewOffset = Index == 0 ? new Vector3(-0.2406f, -0.2454f, 0.3655f) : new Vector3(-0.3406f, -0.3454f, 0.3655f);
            Main.TextObj.transform.parent.localPosition = NewOffset;
        }
    }
}
