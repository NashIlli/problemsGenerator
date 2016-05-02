using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour
{

    public void OnDrop()
    {
        Debug.Log(gameObject.name);
        if (DragHandeler.itemBeingDragged){
            DragHandeler.itemBeingDragged.transform.SetParent(transform);
        }             

    }
}
