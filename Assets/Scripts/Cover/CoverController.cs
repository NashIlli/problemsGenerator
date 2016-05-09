using Assets.Scripts.App;
using Assets.Scripts.Sound;
using UnityEngine;

namespace Assets.Scripts.Cover
{
    public class CoverController : MonoBehaviour
    {

        [SerializeField] private GameObject coverScreen;
        [SerializeField] private GameObject aboutScreen;
        [SerializeField] private GameObject oxScreen;

        // Use this for initialization
        void Start () {
            coverScreen.SetActive(true);
            aboutScreen.SetActive(false);
            oxScreen.SetActive(false);	
        }

        public void OnClickOxButton()
        {
            PlayClickSound();
            oxScreen.SetActive(true);
            coverScreen.SetActive(false);
        }

        public void OnClickOxScreen()
        {
            PlayClickSound();
            oxScreen.SetActive(false);
            coverScreen.SetActive(true);
        }

        public void OnClickAboutButton()
        {
            PlayClickSound();
            aboutScreen.SetActive(true);
            coverScreen.SetActive(false);
        }

        public void OnClickAboutScreen()
        {
            PlayClickSound();
            aboutScreen.SetActive(false);
            coverScreen.SetActive(true);
        }

        public void OnClickStartButton()
        {
            PlayClickSound();
            ViewController.GetController().LoadMainMenu();
        }

        private void PlayClickSound()
        {
            SoundController.GetController().PlayClickSound();
        }
    }
}
