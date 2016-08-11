using System;
using System.Collections.Generic;
using System.Globalization;

namespace Assets.Scripts.App
{
    public class Schema
    {
        public string Title;
        // key is level
        public Dictionary<string, Level> Levels;    
        public Tuple<string, string>[] Places;
        public Tuple<string, string>[] Containers;
        public Tuple<string, string>[] Verbs;
        public Tuple<string, string>[][] Elements;



       /* public void InitSchema()
        {
            numbers = new List<List<int>>();
            for (int i = 0; i < StringNumbers.Length; i++)
            {
                numbers.Add(new List<int>());
                string[] split = StringNumbers[i].Split(',');
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
        }*/

        public Problem GenerateProblem(int numericLevel)
        {
           /* Level level = Levels["level" + numericLevel];

            // Tuple with strings <question, answer>
            Tuple<string, string> currentQuestion =
                level.BaseQuestions[UnityEngine.Random.Range(0, level.BaseQuestions.Length)];
            Problem toReturn;
            do
            {*/
                /*      List<string> usedList = new List<string>();
                      string concretTitle = Title;
                      string concreteText = level.BaseText;
                      string concreteQuestion = currentQuestion.First; 
                      string concreteAnswer = currentQuestion.Second;

                      List<string> currentElements = new List<string>();
                      string[] currentPositiveResults = (string[]) level.PositiveResults.Clone();
                      string[] currentZeroOrPositiveResults = (string[]) level.ZeroOrPositiveResults.Clone();
                      string[] currentNegativeResults = (string[]) level.NegativeResults.Clone();
                      string[] currentZeroOrNegativeResults = (string[]) level.ZeroOrNegativeResults.Clone();
                      string[] currentIntegerResults = (string[]) level.IntegerResults.Clone();
                      string[] currentNonIntegerResults = (string[]) level.NonIntegerResults.Clone();
                      string[] currentZeroResults = (string[]) level.ZeroResults.Clone();


                      foreach (string variable in level.Variables)
                      {
                          string concreteVariable;
                          do concreteVariable = MakeConcrete(variable);
                          while (!variable.Contains("n") && usedList.Contains(concreteVariable));

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
                  */
                return null;

            
        }

        private string MakeConcrete(Level level, string variable)
        {
           /* string lowerVariable = variable.ToLower();
            if (lowerVariable.Contains("number")) return level.NumberesValues[variable].GetNumber().ToString(CultureInfo.InvariantCulture);
            if (lowerVariable.Contains("subject")) return GameManager.GetManager().GetSubjects()[UnityEngine.Random.Range(0, GameManager.GetManager().GetSubjects().Length)];
            if (lowerVariable.Contains("element"))
            {
                return le[UnityEngine.Random.Range(0, elements[group].Count)];
            }
            if (lowerVariable.Contains("p")) return places[group][UnityEngine.Random.Range(0, places[group].Count)];
            if (lowerVariable.Contains("v")) return verbs[group][UnityEngine.Random.Range(0, verbs[group].Count)];
            if (lowerVariable.Contains("c")) return containers[group][UnityEngine.Random.Range(0, containers[group].Count)];*/
            return "Error";

        }
/*
        private int ObtainGroup(string lowerVariable)
        {
            int index = lowerVariable.IndexOf("_", StringComparison.Ordinal);
            if (index < 0)
            {
                return -1;
            }
            return int.Parse(lowerVariable.Substring(index+1)) - 1;
        }*/
    }

   

}
