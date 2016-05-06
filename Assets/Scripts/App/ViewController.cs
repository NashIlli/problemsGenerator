using UnityEngine;

namespace Assets.Scripts.App
{
    public class ViewController : MonoBehaviour
    {
        private static ViewController viewController;

        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject problemPanel;


        void Awake()
        {
            if (viewController == null) viewController = this;
            else if(viewController != this) Destroy(this);
        }

        void Start () {
            ShowMainMenu();
        }

        internal void ShowMainMenu()
        {
            mainMenu.SetActive(true);
            problemPanel.SetActive(false);
        }

        internal void ShowProblemPanel()
        {
            mainMenu.SetActive(false);
            problemPanel.SetActive(true);
            
        }

        public static ViewController GetController()
        {
            return viewController;
        }
    }
}
