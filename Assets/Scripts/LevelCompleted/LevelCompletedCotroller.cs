using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Assets.Scripts.App;
using Assets.Scripts.Sound;
using UnityEngine;

namespace Assets.Scripts.LevelCompleted
{
    public class LevelCompletedCotroller : MonoBehaviour {

        void Start()
        {
            SoundController.GetController().PlayRightAnswerSound();
        }

        public void OnClickMainMenuButton()
        {
            SoundController.GetController().PlayClickSound();
            ViewController.GetController().LoadMainMenu();
        }
    }
}
