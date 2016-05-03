using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Dropdown objectivesDropdown;

    void Start()
    {
        objectivesDropdown.ClearOptions();
        objectivesDropdown.AddOptions(GameManager.GetManager().GetObjectives());
    }

    public void OnClickTicButton()
    {
        ViewController.GetController().ShowProblemPanel();
        GameManager.GetManager().SetSelectedObjective(objectivesDropdown.value);

    }

}
