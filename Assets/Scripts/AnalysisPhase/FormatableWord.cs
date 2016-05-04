﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AnalysisPhase
{
    public class FormatableWord : MonoBehaviour {

        [SerializeField]
        private Text visibleText;

        public void ApplyFormat()
        {
            switch (FormatableTextController.GetController().GetCurrentFormat())
            {
                case FormatType.Paint:
                    Text myText = GetComponentsInChildren<Text>()[1];
                    myText.color = new Color32(255, 152, 0, 255);
                    break;
                case FormatType.Highlight:
                    GetComponentsInChildren<Image>()[0].color = new Color32(104, 192, 255, 255);
                    break;
                case FormatType.Clear:
                    GetComponentsInChildren<Image>()[0].color = new Color32(0, 0, 0, 0);
                    GetComponentsInChildren<Text>()[1].color = Color.black;
                    break;
            }
        }

        internal void SetText(string word)
        {
            gameObject.GetComponent<Text>().text = word;
            visibleText.text = word;
    
        }

        internal void SetFontColor(Color color)
        {
            Text myText = GetComponentsInChildren<Text>()[1];
            myText.color = color;
        }

        internal void SetHighlightColor(Color color)
        {
            GetComponentsInChildren<Image>()[0].color = color;

        }

        internal string GetText()
        {
            return gameObject.GetComponent<Text>().text;
        }

        public Color GetFontColor()
        {
            return GetComponentsInChildren<Text>()[1].color;
        }

        public Color GetHighlightColor()
        {
            return GetComponentsInChildren<Image>()[0].color;
        }
    }
}
