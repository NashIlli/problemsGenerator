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
        if (startParent == board.transform && transform.parent != board.transform)
        {
            transform.SetParent(board.transform);
            transform.position = startPosition;
         }

        /*  Debug.Log("transform pos: " + transform.position);
          Debug.Log("board pos: " + board.transform.position);*/
        Vector3 boardPosition = board.transform.localPosition;
        Rect boardSize = board.GetComponent<RectTransform>().rect;
        if(transform.localPosition.x < boardPosition.x - boardSize.width || transform.localPosition.x > boardPosition.x + boardSize.width)
        {
             transform.position = startPosition;
        }
        if (transform.localPosition.y > boardPosition.y + boardSize.height || transform.localPosition.y < boardPosition.y - boardSize.height)
        {
            transform.position = startPosition;

        }

        /*   Debug.Log("rect: " + board.GetComponent<RectTransform>().rect);
           Debug.Log("postion: " + transform.position);
           Debug.Log("contains: " + board.GetComponent<RectTransform>().rect.Contains(transform.position));
           if (!board.GetComponent<RectTransform>().rect.Contains(transform.position))
           {
               transform.position = startPosition;
           }*/
    }

}
