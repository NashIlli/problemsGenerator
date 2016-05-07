using Assets.Scripts.App;
using UnityEngine;

namespace Assets.Scripts.AnalysisPhase
{
    public class FormatableText : MonoBehaviour {

        [SerializeField]
        private GameObject linePrefab;

        float charWidth = 29;

        void Start()
        {
/*
        charWidth = 29;
*/
        }

        public void ShowText(string text)
        {
            string[] splittedText = text.Split(' ');
            for (int i = 0; i < splittedText.Length; i++)
            {
                AddWord(splittedText[i]);
            }
        }


        public void AddWord(string word)
        {
            FormatableLine[] lines = GetComponentsInChildren<FormatableLine>();
            if (lines.Length == 0) {
                AddLine();
                lines = GetComponentsInChildren<FormatableLine>();
            }
            FormatableLine currentLine = lines[lines.Length - 1];

            {
                if (word == "\n")
                {
                    AddLine();
                    return;
                }
                if (currentLine.CanAdd(word.Length * charWidth))
                {
                    currentLine.AddWord(word);

                } else
                {
                    AddLine();
                    lines = GetComponentsInChildren<FormatableLine>();
                    lines[lines.Length - 1].AddWord(word);
                }            
            }
        }

        internal void AddLine()
        {
            GameObject newLine = Instantiate(linePrefab);
            newLine.transform.SetParent(gameObject.transform, true);
            newLine.transform.localPosition = Vector3.zero;
            newLine.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            newLine.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            newLine.transform.localScale = Vector3.one;
            newLine.GetComponent<FormatableLine>().SetWidths(gameObject.GetComponent<RectTransform>().rect.width, charWidth);
        }

        public FormatableWord AddWord(FormatableWord formatableWord)
        {
            FormatableLine[] lines = GetComponentsInChildren<FormatableLine>();
            if (lines.Length == 0)
            {
                AddLine();
                lines = GetComponentsInChildren<FormatableLine>();
            }
            FormatableLine currentLine = lines[lines.Length - 1];

            {
                if (currentLine.CanAdd(formatableWord.GetText().Length * charWidth))
                {
                    return currentLine.AddWord(formatableWord);

                }
                else
                {
                    AddLine();
                    lines = GetComponentsInChildren<FormatableLine>();
                    return lines[lines.Length - 1].AddWord(formatableWord);
                }
            }
        }
    }
}
