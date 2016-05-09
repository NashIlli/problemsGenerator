using Assets.Scripts.App;
using Assets.Scripts.Sound;
using UnityEngine;

namespace Assets.Scripts.InGameMenu
{
    public class InGameMenuController : MonoBehaviour {
  
        public void OnMainMenuClic()
        {
            PlayClickSound();
            ViewController.GetController().LoadMainMenu();
            ViewController.GetController().HideInGameMenu();
        }

        public void OnInstructionsClic()
        {
            PlayClickSound();
            ViewController.GetController().HideInGameMenu();
            ViewController.GetController().ShowInstructions();
        }

        public void OnClickRestartGame()
        {
            PlayClickSound();
            GameManager.GetManager().GenerateOtherProblem();
            ViewController.GetController().HideInGameMenu();

        }

        public void OnClicBackToGame()
        {
            PlayClickSound();
            ViewController.GetController().HideInGameMenu();

        }

        private void PlayClickSound()
        {
            SoundController.GetController().PlayClickSound();
        }
    }
}
