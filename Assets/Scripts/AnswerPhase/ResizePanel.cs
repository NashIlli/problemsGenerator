using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.AnswerPhase
{
    public class ResizePanel : MonoBehaviour, IPointerDownHandler, IDragHandler {
	
        public Vector2 minSize = new Vector2 (100, 100);
        public Vector2 maxSize = new Vector2 (400, 400);
	
        private RectTransform panelRectTransform;
        private Vector2 originalLocalPointerPosition;
        private Vector2 originalSizeDelta;
	
        void Awake () {
            panelRectTransform = transform.parent.GetComponent<RectTransform> ();
        }
	
        public void OnPointerDown (PointerEventData data) {
            originalSizeDelta = panelRectTransform.sizeDelta;
            RectTransformUtility.ScreenPointToLocalPointInRectangle (panelRectTransform, data.position, data.pressEventCamera, out originalLocalPointerPosition);
        }
	
        public void OnDrag (PointerEventData data) {
            if (panelRectTransform == null)
                return;
		
            Vector2 localPointerPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle (panelRectTransform, data.position, data.pressEventCamera, out localPointerPosition);
            Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
		
            Vector2 sizeDelta = originalSizeDelta + new Vector2 (offsetToOriginal.x, -offsetToOriginal.y);
            sizeDelta = new Vector2 (
                Mathf.Clamp (sizeDelta.x, minSize.x, maxSize.x),
                Mathf.Clamp (sizeDelta.y, minSize.y, maxSize.y)
                );
		
            panelRectTransform.sizeDelta = sizeDelta;
            ClampToWindow();
        }

        void ClampToWindow()
        {
            Vector3 pos = transform.parent.localPosition;

            Vector3 minPosition = transform.parent.parent.GetComponent<RectTransform>().rect.min - transform.parent.GetComponent<RectTransform>().rect.min;
            Vector3 maxPosition = transform.parent.parent.GetComponent<RectTransform>().rect.max - transform.parent.GetComponent<RectTransform>().rect.max;

            pos.x = Mathf.Clamp(transform.parent.GetComponent<RectTransform>().localPosition.x, minPosition.x, maxPosition.x);
            pos.y = Mathf.Clamp(transform.parent.GetComponent<RectTransform>().localPosition.y, minPosition.y, maxPosition.y);

            transform.parent.GetComponent<RectTransform>().localPosition = pos;
        }
    }
}