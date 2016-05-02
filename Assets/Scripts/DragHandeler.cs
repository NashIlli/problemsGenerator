using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;
    public GameObject board;


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameObject.GetComponentsInChildren<PostIt>().Length != 0) gameObject.GetComponentsInChildren<PostIt>()[0].OnEndEdit();
        Debug.Log(gameObject.name);
        itemBeingDragged = gameObject.GetComponentInParent<DragHandeler>().gameObject;
        
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent != board.transform) {
            Debug.Log("dist");
            transform.position = startPosition;
         }
        if(transform.position.x < board.transform.position.x || transform.position.x > board.transform.position.x + board.GetComponent<RectTransform>().rect.size.x)
        {
            transform.position = startPosition;
        }
        if (transform.position.y > board.transform.position.y + board.GetComponent<RectTransform>().rect.size.y)
        {

        }
    }

}
