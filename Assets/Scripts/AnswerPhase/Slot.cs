using UnityEngine;

namespace Assets.Scripts.AnswerPhase
{
    public class Slot : MonoBehaviour
    {

        public void OnDrop()
        {            
            if (DragHandeler.itemBeingDragged){
                DragHandeler.itemBeingDragged.transform.SetParent(transform);
            }

        }
    }
}
