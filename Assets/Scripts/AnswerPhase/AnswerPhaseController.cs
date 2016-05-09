using System.Globalization;
using Assets.Scripts.AnalysisPhase;
using Assets.Scripts.App;
using Assets.Scripts.Sound;
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
        [SerializeField] private Image trashImage;
        [SerializeField] private InputField answerInputField;
        [SerializeField] private Button ticButton;


        void Awake()
        {
            if (answerPhaseController == null) answerPhaseController = this;
            else if (this != answerPhaseController) Destroy(this);
        }

        void Update()
        {
         /*   if (answerPanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)) { OnClickNumber(0); }
                else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) { OnClickNumber(1); }
                else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) { OnClickNumber(2); }
                else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) { OnClickNumber(3); }
                else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) { OnClickNumber(4); }
                else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) { OnClickNumber(5); }
                else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) { OnClickNumber(6); }
                else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) { OnClickNumber(7); }
                else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) { OnClickNumber(8); }
                else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)) { OnClickNumber(9); }
                else if (Input.GetKeyDown(KeyCode.Backspace)) { OnClickClearBtn(); }
            }*/
            
        }

        


        void Start()
        {
            
            string[] elementsToDrag = ProblemController.GetController().GetElementsToDrag();
            var j = 0;
            for (; j < elementsToDrag.Length; j++)
            {
                draggableImages[j].sprite = Resources.Load<Sprite>("Elements/" + elementsToDrag[j]);
            }
            for (; j < draggableImages.Length; j++)
            {
                draggableImages[j].gameObject.SetActive(false);
            }
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
            PlayClickSound();
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
                FormatableWord[] formatableWords = formatableLines[i].GetComponentsInChildren<FormatableWord>();
                for (int j = 0; j < formatableWords.Length; j++)
                {
                    if(formatableWords[j].GetText().Length > 0 && formatableWords[j].GetText()[0] == '¿') formatableText.AddLine();
                    formatableText.AddWord(formatableWords[j]).GetComponent<EventTrigger>().enabled = false;

                }
            }
        }

        public void DestroyOldLines()
        {
            FormatableLine[] oldLines = formatableText.GetComponentsInChildren<FormatableLine>();
            foreach (var oldLine in oldLines)
            {
                Destroy(oldLine.gameObject);
            }
        }

        public void ReInstantiate(GameObject o, int position)
        {
            GameObject ins = Instantiate(o);
            ViewController.FitObjectTo(ins, boardPanel.transform);
            ins.transform.SetSiblingIndex(position);
        }

        internal void ShowTrash(bool visibility)
        {
            trashImage.gameObject.SetActive(visibility);
        }

        public void OnClickNumber(int number)
        {
            if (answerInputField.text.Length < 4)
            {
                PlayClickSound();
                answerInputField.text += number;
            }
            
        }

        private void OnClickClearBtn()
        {
            PlayClickSound();
            answerInputField.text = "";
        }

        private void PlayClickSound()
        {
            SoundController.GetController().PlayClickSound();
        }

        public void UpdateTicButton()
        {
            ticButton.interactable = answerInputField.text.Length > 0;
        }
    }
}
