using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.AnswerPhase
{
    public class PostIt : MonoBehaviour, IBeginDragHandler
    {

        bool mouseClicksStarted = false; int mouseClicks = 0; float mouseTimerLimit = .25f;

        public Image cover;

        void Update()
        {
      
        }

        public void OnClick()
        {
            mouseClicks++;
            if (mouseClicksStarted)
            {
                return;
            }
            mouseClicksStarted = true;
            Invoke("checkMouseDoubleClick", mouseTimerLimit);
        }


        private void checkMouseDoubleClick()
        {
            if (mouseClicks > 1)
            {
                Debug.Log("Double Clicked");
                cover.gameObject.SetActive(false); 
                gameObject.GetComponentInChildren<InputField>().readOnly = false;
                gameObject.GetComponentInChildren<InputField>().Select();

            }

            mouseClicksStarted = false;
            mouseClicks = 0;
        }

        public void OnEndEdit()
        {
            cover.gameObject.SetActive(true);
            gameObject.GetComponentInChildren<InputField>().readOnly = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnEndEdit();
        }
    }
}
