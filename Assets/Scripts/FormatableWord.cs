using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FormatableWord : MonoBehaviour {

    [SerializeField]
    private Text visibleText;

    [SerializeField]
    private Image background;

	public void ApplyFormat()
    {
        switch (FormatableText.GetCurrentFormat())
        {
            case FormatType.Paint:
                Text myText = GetComponentsInChildren<Text>()[0];
                myText.color = FormatableText.GetSelectedColor();
                break;
            case FormatType.Highlight:
                GetComponentsInChildren<Image>()[0].color = FormatableText.GetSelectedColor();
                break;
            case FormatType.Clear:
                GetComponentsInChildren<Image>()[0].color = new Color32(0, 0, 0, 0);
                GetComponentsInChildren<Text>()[0].color = Color.black;
                break;


            default:
                break;
        }
    }

    internal void SetText(string word)
    {
        gameObject.GetComponent<Text>().text = word;
        visibleText.text = word;
    
    }

    internal string GetText()
    {
        return gameObject.GetComponent<Text>().text;
    }
}
