using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AnalysisPhase
{
    public class FormatableLine : MonoBehaviour {

        [SerializeField]
        private FormatableWord wordPrefab;

        private float maxWidth;
        private float charWidth;

        private float currentWidht;
        private int currentChilds;

        internal void SetWidths(float maxWidth, float charWidht)
        {
            this.maxWidth = maxWidth;
            this.charWidth = charWidht;

            Debug.Log("Max width: " + maxWidth);
            Debug.Log("Char width: " + charWidht);
            Debug.Log("Entran: " + maxWidth/charWidht);
        }
    
        internal bool isEmpty()
        {
            return GetComponentsInChildren<FormatableWord>().Length == 0;
        }

        internal void AddWord(string word)
        {
            currentWidht += charWidth * word.Length;
            currentChilds++;
            GameObject newWord = Instantiate(wordPrefab.gameObject);
            newWord.GetComponent<FormatableWord>().SetText(word);
            newWord.transform.SetParent(gameObject.transform, true);
            newWord.transform.localPosition = Vector3.zero;
            newWord.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            newWord.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            newWord.transform.localScale = Vector3.one;
     
        }

        internal bool CanAdd(float width)
        {
            return currentWidht + width+ gameObject.GetComponent<HorizontalLayoutGroup>().spacing * (currentChilds + 1) < maxWidth;
        }

        internal string RemoveLastWord()
        {
            FormatableWord last = GetComponentsInChildren<FormatableWord>()[GetComponentsInChildren<FormatableWord>().Length - 1];
            string toReturn = last.GetText();
            Destroy(GetComponentsInChildren<FormatableWord>()[GetComponentsInChildren<FormatableWord>().Length - 1].gameObject);
            return toReturn;
        }

        internal FormatableWord AddWord(FormatableWord formatableWord)
        {
            currentWidht += charWidth * formatableWord.GetText().Length;
            currentChilds++;
            GameObject newWord = Instantiate(wordPrefab.gameObject);
            newWord.GetComponent<FormatableWord>().SetText(formatableWord.GetText());
            newWord.GetComponent<FormatableWord>().SetFontColor(formatableWord.GetFontColor());
            newWord.GetComponent<FormatableWord>().SetHighlightColor(formatableWord.GetHighlightColor());
            newWord.GetComponent<FormatableWord>().SetText(formatableWord.GetText());
            newWord.transform.SetParent(gameObject.transform, true);
            newWord.transform.localPosition = Vector3.zero;
            newWord.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            newWord.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            newWord.transform.localScale = Vector3.one;
            return newWord.GetComponent<FormatableWord>();
        }
    }
}
