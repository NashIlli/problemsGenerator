using Assets.Scripts.AnalysisPhase;
using Assets.Scripts.AnswerPhase;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.App
{
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

        void Start()
        {
           // SetProblem(GameManager.GetManager().GetCurrentProblem());
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
            AnswerPhaseController.GetController().ShowText(formatableText);
        }

        public static ProblemController GetController()
        {
            return problemController;
        }

        public FormatableText GetFormattedText()
        {
            return formatableText;
        }

        public string[] GetElementsToDrag()
        {
            return currentProblem.GetElementsToDrag();
        }

        public string GetQuestion()
        {
            return currentProblem.GetQuestion();
        }

        public void CheckAnswer(float answer)
        {
            Debug.Log("Correct ? " + currentProblem.CheckAnswer(answer));
        }
    }
}
