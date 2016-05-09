using System;
using System.Collections.Generic;

namespace Assets.Scripts.App
{
    [Serializable]
    public class Schema
    {
        public string title;
        public string[] variables;
        public int minNumber;
        public int maxNumber;
        public string baseText;
        public string baseQuestion;
        public string baseAnswer;
        public string[] elements;
        public string[] places;
        public string[] containers;
        public string[] verbs;
        public string[] positiveResults;
        public string[] integerResults;

        public Problem GenerateProblem()
        {
            Problem toReturn;
            do
            {
                List<string> usedList = new List<string>();
                string concreteText = baseText;
                string concreteQuestion = baseQuestion;
                string concreteAnswer = baseAnswer;
                List<string> currentElements = new List<string>();
                string[] currentPositiveResults = (string[]) positiveResults.Clone();
                string[] currentIntegerResults = (string[]) integerResults.Clone();


                foreach (string variable in variables)
                {
                    string concreteVariable;
                    do
                    {
                        concreteVariable = MakeConcrete(variable);
                    } while (usedList.Contains(concreteVariable));

                    if (variable.Contains("o") || variable.Contains("c")) currentElements.Add(concreteVariable);

                    concreteText = concreteText.Replace(variable, concreteVariable);
                    concreteQuestion = concreteQuestion.Replace(variable, concreteVariable);
                    if (variable.Contains("n"))
                    {
                        concreteAnswer = concreteAnswer.Replace(variable, concreteVariable);
                        for (int i = 0; i < positiveResults.Length; i++)
                        {
                            currentPositiveResults[i] = currentPositiveResults[i].Replace(variable, concreteVariable);
                        }
                        for (int i = 0; i < integerResults.Length; i++)
                        {
                            currentIntegerResults[i] = currentIntegerResults[i].Replace(variable, concreteVariable);
                        }
                    }
                    
                    usedList.Add(concreteVariable);
                }

                toReturn = new Problem(title, concreteText, concreteQuestion, concreteAnswer, currentElements.ToArray(), currentPositiveResults, currentIntegerResults);
            } while (!toReturn.CheckRestrictons());

            return toReturn;

        }

        private string MakeConcrete(string variable)
        {        
            if (variable.ToLower().Contains("n")) return UnityEngine.Random.Range(minNumber, maxNumber).ToString();
            if (variable.ToLower().Contains("s")) return GameManager.GetManager().GetSubjects()[UnityEngine.Random.Range(0, GameManager.GetManager().GetSubjects().Length)];
            if (variable.ToLower().Contains("o")) return elements[UnityEngine.Random.Range(0, elements.Length)];
            if (variable.ToLower().Contains("p")) return places[UnityEngine.Random.Range(0, places.Length)];
            if (variable.ToLower().Contains("v")) return verbs[UnityEngine.Random.Range(0, verbs.Length)];
            if (variable.ToLower().Contains("c")) return containers[UnityEngine.Random.Range(0, containers.Length)];
            return "Error";

        }
    }
}
