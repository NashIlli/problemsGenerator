using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assets.Scripts.App
{
    [Serializable]
    public class Schema
    {
        public string title;
        public string[] variables;
        public string[] stringNumbers;
        private List<List<int>> numbers;
        public string baseText;
        public string[] baseQuestions;
        public string[] baseAnswers;
        public string[] stringElements;
        private List<List<string>> elements;
        public string[] stringPlaces;
        private List<List<string>> places;
        public string[] stringContainers;
        private List<List<string>> containers;
        public string[] stringVerbs;
        private List<List<string>> verbs;
        public string[] extraElements;
        public string[] positiveResults;
        public string[] negativeResults;
        public string[] integerResults;


        public void InitSchema()
        {
            numbers = new List<List<int>>();
            for (int i = 0; i < stringNumbers.Length; i++)
            {
                numbers.Add(new List<int>());
                string[] split = stringNumbers[i].Split(',');
                for (int j = 0; j < split.Length; j++)
                {
                    numbers[i].Add(int.Parse(split[j].Trim()));
                }
            }

            verbs = new List<List<string>>();
            for (int i = 0; i < stringVerbs.Length; i++)
            {
                verbs.Add(new List<string>());
                string[] split = stringVerbs[i].Split(',');
                for (int j = 0; j < split.Length; j++)
                {
                    verbs[i].Add(split[j].Trim());
                }
            }

            elements = new List<List<string>>();
            for (int i = 0; i < stringElements.Length; i++)
            {
                elements.Add(new List<string>());
                string[] split = stringElements[i].Split(',');
                for (int j = 0; j < split.Length; j++)
                {
                    elements[i].Add(split[j].Trim());
                }
            }

            places = new List<List<string>>();
            for (int i = 0; i < stringPlaces.Length; i++)
            {
                places.Add(new List<string>());
                string[] split = stringPlaces[i].Split(',');
                for (int j = 0; j < split.Length; j++)
                {
                    places[i].Add(split[j].Trim());
                }
            }

            containers = new List<List<string>>();
            for (int i = 0; i < stringContainers.Length; i++)
            {
                containers.Add(new List<string>());
                string[] split = stringContainers[i].Split(',');
                for (int j = 0; j < split.Length; j++)
                {
                    containers[i].Add(split[j].Trim());
                }
            }
        }

        public Problem GenerateProblem(int indexLevel)
        {
            int currentQuestion = UnityEngine.Random.Range(0, baseAnswers.Length);
            Problem toReturn;
            do
            {
                List<string> usedList = new List<string>();
                string concretTitle = title;
                string concreteText = baseText;
                string concreteQuestion = baseQuestions[currentQuestion];
                if (concreteQuestion.Contains("gastaron"))
                {
                    int a = 1;
                }
                string concreteAnswer = baseAnswers[currentQuestion];
                List<string> currentElements = new List<string>();
                string[] currentPositiveResults = (string[]) positiveResults.Clone();
                string[] currentNegativeResults = (string[]) negativeResults.Clone();
                string[] currentIntegerResults = (string[]) integerResults.Clone();


                foreach (string variable in variables)
                {
                    string concreteVariable;
                    do
                    {
                        concreteVariable = MakeConcrete(variable);
                    } while (!variable.Contains("n") && usedList.Contains(concreteVariable));

                    if (variable.Contains("o") || variable.Contains("c")) currentElements.Add(concreteVariable);

                    concretTitle = concretTitle.Replace(variable, concreteVariable);
                    concreteText = concreteText.Replace(variable, concreteVariable);
                    concreteQuestion = concreteQuestion.Replace(variable, concreteVariable);
                    
                    if (variable.Contains("n"))
                    {
                        concreteAnswer = concreteAnswer.Replace(variable, concreteVariable);
                        for (int i = 0; i < positiveResults.Length; i++)
                        {
                            currentPositiveResults[i] = currentPositiveResults[i].Replace(variable, concreteVariable);
                        }
                        for (int i = 0; i < negativeResults.Length; i++)
                        {
                            currentNegativeResults[i] = currentNegativeResults[i].Replace(variable, concreteVariable);
                        }
                        for (int i = 0; i < integerResults.Length; i++)
                        {
                            currentIntegerResults[i] = currentIntegerResults[i].Replace(variable, concreteVariable);
                        }

                        for (int i = 0; i < extraElements.Length; i++)
                        {
                            if (extraElements[i].Contains(variable.ToLower()))
                            {
                                extraElements[i] = extraElements[i].Replace(variable, concreteVariable);
                            }
                        }
                    }
                    
                    usedList.Add(concreteVariable);
                }

                toReturn = new Problem(concretTitle, concreteText, concreteQuestion, concreteAnswer, currentElements.ToArray(), currentPositiveResults, currentNegativeResults, currentIntegerResults);
            } while (!toReturn.CheckRestrictons());

            toReturn.AddElements(extraElements);

            return toReturn;

        }

        private string MakeConcrete(string variable)
        {
            string lowerVariable = variable.ToLower();
            int group = ObtainGroup(lowerVariable);
            if (lowerVariable.Contains("n"))
            {
                if (numbers[group].Count == 2)
                {
                    return UnityEngine.Random.Range(numbers[group][0], numbers[group][1]).ToString();
                } 
                return numbers[group][UnityEngine.Random.Range(0, numbers[group].Count)].ToString();
            }
            if (lowerVariable.Contains("s")) return GameManager.GetManager().GetSubjects()[UnityEngine.Random.Range(0, GameManager.GetManager().GetSubjects().Length)];
            if (lowerVariable.Contains("o")) return elements[group][UnityEngine.Random.Range(0, elements[group].Count)];
            if (lowerVariable.Contains("p")) return places[group][UnityEngine.Random.Range(0, places[group].Count)];
            if (lowerVariable.Contains("v")) return verbs[group][UnityEngine.Random.Range(0, verbs[group].Count)];
            if (lowerVariable.Contains("c")) return containers[group][UnityEngine.Random.Range(0, containers[group].Count)];
            return "Error";

        }

        private int ObtainGroup(string lowerVariable)
        {
            int index = lowerVariable.IndexOf("_", StringComparison.Ordinal);
            if (index < 0)
            {
                return -1;
            }
            return int.Parse(lowerVariable.Substring(index+1)) - 1;
        }
    }
}
