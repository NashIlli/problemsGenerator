using Assets.Scripts.AnalysisPhase;
using Assets.Scripts.App;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.AnswerPhase
{
    public class AnswerPhaseController : MonoBehaviour
    {
        [SerializeField] private FormatableText formatableText;
        [SerializeField] private Image[] draggableImages;
        [SerializeField] private GameObject boardPanel;
        [SerializeField] private GameObject answerPanel;
        [SerializeField] private Text questioText;
        [SerializeField] private InputField answerInputField;



        void OnEnable()
        {
            FormatableWord[] formatableWords = ProblemController.GetController().GetFormattedText().GetComponentsInChildren<FormatableWord>();
            for (int i = 0; i < formatableWords.Length; i++)
            {
                formatableText.AddWord(formatableWords[i]).GetComponent<EventTrigger>().enabled = false;
            }

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


    }
}
