using Assets.Scripts.App;
using Assets.Scripts.Sound;
using UnityEngine;

namespace Assets.Scripts.Instructions
{
    public class Instructions : MonoBehaviour {

        public void OnClickBackToGame()
        {
            ClickSound();
            ViewController.GetController().HideInstructions();
        }

        private void ClickSound()
        {
            SoundController.GetController().PlayClickSound();
        }
    }
}
