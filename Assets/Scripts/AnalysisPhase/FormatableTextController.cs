using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AnalysisPhase
{
    public class FormatableTextController : MonoBehaviour {

        private static FormatableTextController formatableTextController;

        [SerializeField]
        private List<Toggle> formatToggles;
        [SerializeField]
        private GameObject letterColourPanel;
        [SerializeField]
        private GameObject highlightColourPanel;

        void Awake()
        {
            if (formatableTextController == null) formatableTextController = this;
            else if (this != formatableTextController) Destroy(this);
        }

        internal FormatType GetCurrentFormat()
        {
            for (int i = 0; i < formatToggles.Count; i++)
            {
                if (formatToggles[i].isOn)
                {
                    return (FormatType)i;

                }
            }
            return FormatType.NoOne;
        }

        internal Color GetLetterColorSelected()
        {
            return GetColourSelectedOf(letterColourPanel.GetComponentsInChildren<Toggle>());
        }

        internal Color GetHighlightColorSelected()
        {
            return GetColourSelectedOf(highlightColourPanel.GetComponentsInChildren<Toggle>());
        }

        private Color GetColourSelectedOf(Toggle[] colourToggles)
        {
            for (int i = 0; i < colourToggles.Length; i++)
            {
                if (colourToggles[i].isOn) return colourToggles[i].GetComponentsInChildren<Image>()[1].color;
            }

            return Color.black;
        }

        public void ShowLetterColourPanelColourPanel()
        {
            letterColourPanel.SetActive(true);
            highlightColourPanel.SetActive(false);
        }

        public void ShowHighlightColourPanelColourPanel()
        {
            letterColourPanel.SetActive(false);
            highlightColourPanel.SetActive(true);
        }

        public void HideColourPanel()
        {
            letterColourPanel.SetActive(false);
            highlightColourPanel.SetActive(false);
        }

        public static FormatableTextController GetController()
        {
            return formatableTextController;
        }


    }
}
