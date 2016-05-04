using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.App
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager gameManager;
        private PedagogicalObjective[] pedagogicalObjectives;
        private Problem currentProblem;
        private int selectedObjective;

        void Awake()
        {
            if (gameManager == null) gameManager = this;
            else if(gameManager != this) Destroy(this);
            LoadObjectives();
        }

        private void LoadObjectives()
        {
            TextAsset JSONstring = Resources.Load("PedagogicalObjectives") as TextAsset;
            JSONNode data = JSON.Parse(JSONstring.text);

            pedagogicalObjectives = new PedagogicalObjective[data["pedagogialObjectives"].Count];
            for (int i = 0; i < data["pedagogialObjectives"].Count; i++)
            {
                pedagogicalObjectives[i] = JsonUtility.FromJson<PedagogicalObjective>(data["pedagogialObjectives"][i].ToString());
            }
        }

        public string GetText()
        {
            return currentProblem.GetText();
            return "Juan tenía tres pelotas. Decidió darle una a cada uno de sus amigos. Juan no quiso ser egoísta por lo que pensó la mejor forma de hacerlo. Juan hizo una división. El dijo: si tengo tres pelotas y tengo tres amigos tengo que hacer 3 divido en 3. ¿Cuántas pelotas le dio a cada amigo?";
        }

        public static GameManager GetManager()
        {
            return gameManager;
        }

        public List<string> GetObjectives(int level)
        {
            List<string> objectivesList = new List<string>(pedagogicalObjectives.Length);
            for (int i = 0; i < pedagogicalObjectives.Length; i++)
            {
                if(pedagogicalObjectives[i].GetLevels().Contains(i)) objectivesList.Add(pedagogicalObjectives[i].name);
            }
            return objectivesList;
        }

        public void SetSelectedObjective(int selectedObjective)
        {
            this.selectedObjective = selectedObjective;
            currentProblem = pedagogicalObjectives[selectedObjective].GenerateProblem();
            ProblemController.GetController().SetProblem(currentProblem);

        }
    }
}
