using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.AnswerPhase
{
    public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

        public static GameObject itemBeingDragged;
        public int index;
        Vector3 startPosition;
        Transform startParent;
        public GameObject board;

        public void OnBeginDrag(PointerEventData eventData)
        {
            
            AnswerPhaseController.GetController().ShowTrash(true);
            if (gameObject.GetComponentsInChildren<PostIt>().Length != 0) gameObject.GetComponentsInChildren<PostIt>()[0].OnEndEdit();
            itemBeingDragged = gameObject.GetComponentInParent<DragHandeler>().gameObject;
        
            startPosition = transform.position;
            startParent = transform.parent;
            if(startParent == board.transform) gameObject.transform.SetAsLastSibling();
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
            if (transform.parent == board.transform) ClampToWindow();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            AnswerPhaseController.GetController().ShowTrash(false);
            itemBeingDragged = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (startParent == board.transform && transform.parent != board.transform && transform.parent.tag != "trash")
            {
                transform.SetParent(board.transform);
                transform.position = startPosition;
            }
            if (startParent.transform != board.transform && startParent != transform.parent)
            {
                AnswerPhaseController.GetController().ReInstantiate(gameObject, index);
            }

            if (transform.parent.tag == "trash") Destroy(gameObject);

        }

        // Clamp panel to area of parent
        void ClampToWindow()
        {
            Vector3 pos = transform.localPosition;

            Vector3 minPosition = transform.parent.GetComponent<RectTransform>().rect.min - GetComponent<RectTransform>().rect.min;
            Vector3 maxPosition = transform.parent.GetComponent<RectTransform>().rect.max - GetComponent<RectTransform>().rect.max;

            pos.x = Mathf.Clamp(GetComponent<RectTransform>().localPosition.x, minPosition.x, maxPosition.x);
            pos.y = Mathf.Clamp(GetComponent<RectTransform>().localPosition.y, minPosition.y, maxPosition.y);

            GetComponent<RectTransform>().localPosition = pos;
        }

    }
}
