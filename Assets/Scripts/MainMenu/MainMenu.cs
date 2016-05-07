using Assets.Scripts.App;
using Assets.Scripts.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MainMenu
{
    public class MainMenu : MonoBehaviour
    {

        [SerializeField] private Dropdown objectivesDropdown;
        [SerializeField] private Button ticButton;

        void Start()
        {
            ticButton.interactable = false;
        }

        public void OnClickTicButton()
        {
            PlayClickSound();
            GameManager.GetManager().SetSelectedObjective(objectivesDropdown.value);
            
        }

        public void OnClickLevel(int level)
        {
            PlayClickSound();
            ticButton.interactable = true;
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
