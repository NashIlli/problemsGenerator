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
        private string[] subjects;
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

            subjects = new string[data["subjects"].Count];
            for (int i = 0; i < data["subjects"].Count; i++)
            {
                subjects[i] = data["subjects"][i];
            }
            pedagogicalObjectives = new PedagogicalObjective[data["pedagogicalObjectives"].Count];
            for (int i = 0; i < data["pedagogicalObjectives"].Count; i++)
            {
                pedagogicalObjectives[i] = JsonUtility.FromJson<PedagogicalObjective>(data["pedagogicalObjectives"][i].ToString());
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
                if(pedagogicalObjectives[i].GetLevels().Contains(level)) objectivesList.Add(pedagogicalObjectives[i].name);
            }
            return objectivesList;
        }

        public void SetSelectedObjective(int level, int selectedObjective)
        {
            this.selectedObjective = selectedObjective;
            List<PedagogicalObjective> currrentPedagogicalObjectives = new List<PedagogicalObjective>();
            for (int i = 0; i < pedagogicalObjectives.Length; i++)
            {
                if (pedagogicalObjectives[i].GetLevels().Contains(level)) currrentPedagogicalObjectives.Add(pedagogicalObjectives[i]);
            }
            currentProblem = currrentPedagogicalObjectives[selectedObjective].GenerateProblem();
            ViewController.GetController().StartProblem();
            ProblemController.GetController().SetProblem(currentProblem);
        }

        public Problem GetCurrentProblem()
        {
            return currentProblem;
        }

        public void GenerateOtherProblem()
        {
            ProblemController.GetController().SetProblem(currentProblem);
            ProblemController.GetController().ShowTextAnalysisPhase();

        }

        public string[] GetSubjects()
        {
            return subjects;
        }
    }
}
