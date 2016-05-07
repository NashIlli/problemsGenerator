using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.App
{
    public class ViewController : MonoBehaviour
    {
        private static ViewController viewController;

        public GameObject viewPanel;

        private GameObject currentGameObject;

        private GameObject inGameMenuScreen;
        private GameObject instructionsScreen;
        private GameObject hintScreen;

        void Awake()
        {
            if (viewController == null) viewController = this;
            else if (viewController != this) Destroy(gameObject);
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            LoadCover();
        }

        internal void LoadMainMenu()
        {
            ChangeCurrentObject(LoadPrefab("MainMenu"));
        }

        private GameObject LoadPrefab(string name)
        {
            return Resources.Load<GameObject>("Prefabs/" + name);
        }

        internal void LoadCover()
        {
            ChangeCurrentObject(LoadPrefab("Cover"));
        }

        private void ChangeCurrentObject(GameObject newObject)
        {
            GameObject child = Instantiate(newObject);
            FitObjectTo(child, viewPanel.transform);
            Destroy(currentGameObject);
            currentGameObject = child;
        }

        internal void ShowInGameMenu()
        {
            if (inGameMenuScreen == null)
            {
                HideInstructions();
                inGameMenuScreen = Instantiate(LoadPrefab("IngameMenu"));
                FitObjectTo(inGameMenuScreen, viewPanel.transform);
            }

        }

        public static void FitObjectTo(GameObject child, Transform transform)
        {
            child.transform.SetParent(transform, true);
            child.transform.localPosition = Vector3.zero;
            child.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            child.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            child.transform.localScale = Vector3.one;
        }

        internal void LoadSettings()
        {
            ChangeCurrentObject(LoadPrefab("Settings"));
        }

        internal void LoadLogin()
        {
            ChangeCurrentObject(LoadPrefab("Login"));
        }

        internal void StartProblem()
        {
            ChangeCurrentObject(LoadPrefab("Problem"));
        }

        internal void ShowInstructions()
        {
            instructionsScreen = Instantiate(LoadPrefab("Instructions"));
            FitObjectTo(instructionsScreen, viewPanel.transform);
        }

        internal void HideInGameMenu()
        {
            Destroy(inGameMenuScreen);
        }

        internal void HideInstructions()
        {
            Destroy(instructionsScreen);
        }

        internal void LoadLevelCompleted()
        {
            ChangeCurrentObject(LoadPrefab("LevelCompleted"));
        }


        public static ViewController GetController()
        {
            return viewController;
        }


        public void ShowHint()
        {
            hintScreen = Instantiate(LoadPrefab("Hint"));
            FitObjectTo(hintScreen, viewPanel.transform);
        }

        public void HideHint()
        {
            Destroy(hintScreen);
        }
    }
}
