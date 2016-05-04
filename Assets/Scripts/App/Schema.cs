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
        public string[] subjects;
        public bool canBeNegative;

        public Problem GenerateProblem()
        {
            List<string> usedList = new List<string>();
            string concreteText = baseText;
            string concreteQuestion = baseQuestion;
            string concreteAnswer = baseAnswer;
            List<string> currentElements = new List<string>();

            foreach (string variable in variables)
            {
                string concreteVariable;
                do
                {
                    concreteVariable = MakeConcrete(variable);
                } while (usedList.Contains(concreteVariable));

                if(variable.Contains("o")) currentElements.Add(concreteVariable);

                concreteText = concreteText.Replace(variable, concreteVariable);
                concreteQuestion = concreteQuestion.Replace(variable, concreteVariable);
                concreteAnswer = concreteAnswer.Replace(variable, concreteVariable);
                usedList.Add(concreteVariable);
            }

            return new Problem(title, concreteText, concreteQuestion, concreteAnswer, currentElements.ToArray());
        }

        private string MakeConcrete(string variable)
        {
            // TODO set random range by json data
        
            if (variable.ToLower().Contains("n")) return UnityEngine.Random.Range(minNumber, maxNumber).ToString();
            if (variable.ToLower().Contains("s")) return subjects[UnityEngine.Random.Range(0, subjects.Length)];
            if (variable.ToLower().Contains("o")) return elements[UnityEngine.Random.Range(0, elements.Length)];
            if (variable.ToLower().Contains("p")) return places[UnityEngine.Random.Range(0, places.Length)];
            if (variable.ToLower().Contains("v")) return verbs[UnityEngine.Random.Range(0, verbs.Length)];
            if (variable.ToLower().Contains("c")) return containers[UnityEngine.Random.Range(0, containers.Length)];
            return "Error";

        }
    }
}
