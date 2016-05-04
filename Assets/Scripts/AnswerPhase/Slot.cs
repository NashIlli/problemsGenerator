﻿using UnityEngine;

namespace Assets.Scripts.AnswerPhase
{
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
}
