using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FormatableWord : MonoBehaviour {

    [SerializeField]
    private Text visibleText;

	public void ApplyFormat()
    {
        switch (FormatableTextController.GetController().GetCurrentFormat())
        {
            case FormatType.Paint:
                Text myText = GetComponentsInChildren<Text>()[1];
                myText.color = Color.green;
                break;
            case FormatType.Highlight:
                GetComponentsInChildren<Image>()[0].color = FormatableTextController.GetSelectedColor();
                break;
            case FormatType.Clear:
                GetComponentsInChildren<Image>()[0].color = new Color32(0, 0, 0, 0);
                GetComponentsInChildren<Text>()[1].color = Color.black;
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
