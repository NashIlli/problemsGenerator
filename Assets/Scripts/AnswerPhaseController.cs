using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class AnswerPhaseController : MonoBehaviour
{
    [SerializeField] private FormatableText formatableText;

    void OnEnable()
    {
        FormatableWord[] formatableWords = ProblemController.GetController().GetFormattedText().GetComponentsInChildren<FormatableWord>();
        for (int i = 0; i < formatableWords.Length; i++)
        {
            formatableText.AddWord(formatableWords[i]).GetComponent<EventTrigger>().enabled = false;
        }
    }
}
