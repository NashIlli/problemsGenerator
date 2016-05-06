using Assets.Scripts.App;
using Assets.Scripts.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainMenu
{
    public class MainMenu : MonoBehaviour
    {

        [SerializeField] private Dropdown objectivesDropdown;

        public void OnClickTicButton()
        {
            PlayClickSound();
            ViewController.GetController().ShowProblemPanel();
            GameManager.GetManager().SetSelectedObjective(objectivesDropdown.value);
            
        }

        public void OnClickLevel(int level)
        {
            PlayClickSound();
            LoadObjectives(level);
        }

        private void LoadObjectives(int level)
        {
            objectivesDropdown.ClearOptions();
            objectivesDropdown.AddOptions(GameManager.GetManager().GetObjectives(level));
        }

        public void PlayClickSound()
        {
            SoundController.GetController().PlayClickSound();
        }

    }
}
