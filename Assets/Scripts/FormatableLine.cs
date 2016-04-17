using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

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
    }
    
    internal bool isEmpty()
    {
        return GetComponentsInChildren<FormatableWord>().Length == 0;
    }

    internal void AddWord(string word)
    {
        currentWidht += charWidth * word.Length;
        currentChilds++;
        float old = gameObject.GetComponent<RectTransform>().rect.height;
        GameObject newWord = Instantiate(wordPrefab.gameObject);
        newWord.transform.SetParent(gameObject.transform, true);
        newWord.transform.localPosition = Vector3.zero;
        newWord.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        newWord.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        newWord.transform.localScale = Vector3.one;
        newWord.GetComponent<FormatableWord>().SetText(word);
     
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
}
