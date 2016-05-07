using Assets.Scripts.AnalysisPhase;
using Assets.Scripts.AnswerPhase;
using Assets.Scripts.Sound;
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
            ShowTextAnalysisPhase();
        }


        internal void SetProblem(Problem problem = null)
        {
            if (problem == null) problem = GameManager.GetManager().GetCurrentProblem();
            currentProblem = problem;
            titleText.text = currentProblem.GetTitle();
            formatableText.ShowText(currentProblem.GetText());
        }

        public void ShowTextAnalysisPhase()
        {
            analysisPhase.SetActive(true);
            answerPhase.SetActive(false);
            answerPhase.GetComponent<AnswerPhaseController>().DestroyOldLines();
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
            if (currentProblem.CheckAnswer(answer))
            {
                ViewController.GetController().LoadLevelCompleted();
            } else {
                SoundController.GetController().PlayFailureSound();
            }
        }

        public void OnClickMenuButton()
        {
            PlayClickSoud();
            ViewController.GetController().ShowInGameMenu();
        }

        public void OnClickHintButton()
        {
            PlayClickSoud();
            ViewController.GetController().ShowHint();
        }

        public void PlayClickSoud()
        {
            SoundController.GetController().PlayClickSound();
        }
    }
}
