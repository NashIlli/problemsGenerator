using System.Globalization;
using Assets.Scripts.AnalysisPhase;
using Assets.Scripts.App;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.AnswerPhase
{
    public class AnswerPhaseController : MonoBehaviour
    {

        private static AnswerPhaseController answerPhaseController;

        [SerializeField] private FormatableText formatableText;
        [SerializeField] private Image[] draggableImages;
        [SerializeField] private GameObject boardPanel;
        [SerializeField] private GameObject answerPanel;
        [SerializeField] private Text questioText;
        [SerializeField] private InputField answerInputField;

        void Awake()
        {
            if (answerPhaseController == null) answerPhaseController = this;
            else if (this != answerPhaseController) Destroy(this);
        }



        void OnEnable()
        {
            

            string[] elementsToDrag = ProblemController.GetController().GetElementsToDrag();
            var j = 0;
            for (; j < elementsToDrag.Length; j++)
            {
                Debug.Log("Elements/" + elementsToDrag[j]);
                draggableImages[j].sprite = Resources.LoadAll<Sprite>("Elements/elements")[int.Parse(""+elementsToDrag[j][elementsToDrag[j].Length - 1])];
            }
            for (; j < draggableImages.Length; j++)
            {
                draggableImages[j].gameObject.SetActive(false);
            }

            questioText.text = ProblemController.GetController().GetQuestion();
        }

        public void OnClickBoardButton()
        {
            boardPanel.SetActive(true);
            answerPanel.SetActive(false);
        }

        public void OnClickAnswerPanelButton()
        {
            boardPanel.SetActive(false);
            answerPanel.SetActive(true);
        }

        public void OnClickTic()
        {
            ProblemController.GetController().CheckAnswer(float.Parse(answerInputField.text));
        }

        public static AnswerPhaseController GetController()
        {
            return answerPhaseController;
        }


        public void ShowText(FormatableText formattedText)
        {
            FormatableLine[] formatableLines = formattedText.GetComponentsInChildren<FormatableLine>();
            for (int i = 0; i < formatableLines.Length; i++)
            {
                if (i == formatableLines.Length - 1) formatableText.AddLine();
                FormatableWord[] formatableWords = formatableLines[i].GetComponentsInChildren<FormatableWord>();
                for (int j = 0; j < formatableWords.Length; j++)
                {
                    formatableText.AddWord(formatableWords[j]).GetComponent<EventTrigger>().enabled = false;

                }
            }
        }
    }
}
