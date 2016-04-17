using UnityEngine;
using System.Collections;
using System;

public class FormatableText : MonoBehaviour {

    [SerializeField]
    private GameObject linePrefab;

    float charWidth;
    float height;

    void Start()
    {
        charWidth = GetComponentsInChildren<FormatableWord>()[0].GetComponent<RectTransform>().rect.width;
        height = GetComponentsInChildren<FormatableWord>()[0].GetComponent<RectTransform>().rect.height;
        Destroy(GetComponentsInChildren<FormatableLine>()[0].gameObject);
        charWidth = 29;

        ShowText();

    }

    private void ShowText()
    {
        string[] text = GameManager.GetText().Split(' ');
        for (int i = 0; i < text.Length; i++)
        {
            AddWord(text[i]);
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

    private void AddLine()
    {
        GameObject newLine = Instantiate(linePrefab);
        newLine.transform.SetParent(gameObject.transform, true);
        newLine.transform.localPosition = Vector3.zero;
        newLine.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        newLine.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        newLine.transform.localScale = Vector3.one;
        newLine.GetComponent<FormatableLine>().SetWidths(gameObject.GetComponent<RectTransform>().rect.width, charWidth);
    }

    internal static FormatType GetCurrentFormat()
    {
        return FormatType.Highlight;
    }

    internal static Color GetSelectedColor()
    {
        return Color.red;
    }
}
