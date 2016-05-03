using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProblemController : MonoBehaviour
{

    private static ProblemController problemController;

    [SerializeField] private Text titleText;
    [SerializeField] private FormatableText formatableText;
    private Problem currentProblem;

    [SerializeField] private GameObject analysisPhase;
    [SerializeField] private GameObject answerPhase;

    void Awake()
    {
        if (problemController == null) problemController = this;
        else if (this != problemController) Destroy(this);
    }


    public void SetProblem(Problem problem)
    {
        currentProblem = problem;
        titleText.text = currentProblem.GetTitle();
        formatableText.ShowText(currentProblem.GetText());
        ShowTextAnalysisPhase();
    }

    public void ShowTextAnalysisPhase()
    {
        analysisPhase.SetActive(true);
        answerPhase.SetActive(false);
    }

    public void ShowAnswerPhase()
    {
        analysisPhase.SetActive(false);
        answerPhase.SetActive(true);
    }

    public static ProblemController GetController()
    {
        return problemController;
    }

    public FormatableText GetFormattedText()
    {
        return formatableText;
    }
}
