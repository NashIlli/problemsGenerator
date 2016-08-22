using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
        public Dictionary<string, Tuple<string, string>[]> Elements;



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
             Level level = Levels["level" + numericLevel];

             // Tuple with strings <question, answer>
             Tuple<string, string> currentQuestion =
                 level.BaseQuestions[UnityEngine.Random.Range(0, level.BaseQuestions.Length)];
             Problem toReturn;
             do
             {
                 List<Tuple<string, string>> usedList = new List<Tuple<string, string>>();
                 string concretTitle = Title;
                 string concreteText = level.BaseText;
                 string concreteQuestion = currentQuestion.First; 
                 string concreteAnswer = currentQuestion.Second;
                // only its id
                 List<string> elementsWithImage = new List<string>();
                 string[] currentPositiveResults = (string[]) level.PositiveResults.Clone();
                 string[] currentZeroOrPositiveResults = (string[]) level.ZeroOrPositiveResults.Clone();
                 string[] currentNegativeResults = (string[]) level.NegativeResults.Clone();
                 string[] currentZeroOrNegativeResults = (string[]) level.ZeroOrNegativeResults.Clone();
                 string[] currentIntegerResults = (string[]) level.IntegerResults.Clone();
                 string[] currentNonIntegerResults = (string[]) level.NonIntegerResults.Clone();
                 string[] currentZeroResults = (string[]) level.ZeroResults.Clone();


                 foreach (string variable in level.Variables)
                 {
                     Tuple<string, string> concreteVariable;
                     do concreteVariable = MakeConcrete(level, variable);
                     while (usedList.Contains(concreteVariable));
                     // if the variable has and 'id' to search its image
                     if (concreteVariable.Second != "") elementsWithImage.Add(concreteVariable.Second);

                     concretTitle = ReplaceVariableWithCorrectString(concretTitle, variable, concreteVariable.First);
                     concreteText = ReplaceVariableWithCorrectString(concreteText, variable, concreteVariable.First);
                     concreteQuestion = ReplaceVariableWithCorrectString(concreteQuestion, variable, concreteVariable.First);
                     if (variable.Contains("number"))
                     {
                         concreteAnswer = ReplaceNumericVariable(concreteAnswer, variable, concreteVariable.First,
                             currentPositiveResults, currentZeroOrPositiveResults, currentNegativeResults,
                             currentZeroOrNegativeResults, currentZeroResults, currentIntegerResults,
                             currentNonIntegerResults);
                     }
                     else
                     {
                        usedList.Add(concreteVariable);
                    }
                }

                 toReturn = new Problem(concretTitle, concreteText, concreteQuestion, concreteAnswer, elementsWithImage.ToArray(), currentPositiveResults, currentZeroOrPositiveResults, currentNegativeResults, currentZeroOrNegativeResults, currentZeroResults, currentIntegerResults, currentNonIntegerResults);
             } while (!toReturn.CheckRestrictons());
/*
             toReturn.AddElements(extraElements);
*/
             return toReturn;
        }

        private string ReplaceNumericVariable(string concreteAnswer, string variable, string concreteVariable, string[] currentPositiveResults, string[] currentZeroOrPositiveResults, string[] currentNegativeResults, string[] currentZeroOrNegativeResults, string[] currentZeroResults, string[] currentIntegerResults, string[] currentNonIntegerResults)
        {
            concreteAnswer = concreteAnswer.Replace(variable, concreteVariable);
            
            for (int i = 0; i < currentPositiveResults.Length; i++)
            {
                currentPositiveResults[i] = currentPositiveResults[i].Replace(variable, concreteVariable);
            }
            for (int i = 0; i < currentZeroOrPositiveResults.Length; i++)
            {
                currentZeroOrPositiveResults[i] = currentZeroOrPositiveResults[i].Replace(variable, concreteVariable);
            }
            for (int i = 0; i < currentNegativeResults.Length; i++)
            {
                currentNegativeResults[i] = currentNegativeResults[i].Replace(variable, concreteVariable);
            }

            for (int i = 0; i < currentZeroOrNegativeResults.Length; i++)
            {
                currentZeroOrNegativeResults[i] = currentZeroOrNegativeResults[i].Replace(variable, concreteVariable);
            }
            for (int i = 0; i < currentZeroResults.Length; i++)
            {
                currentZeroResults[i] = currentZeroResults[i].Replace(variable, concreteVariable);
            }
            for (int i = 0; i < currentIntegerResults.Length; i++)
            {
                currentIntegerResults[i] = currentIntegerResults[i].Replace(variable, concreteVariable);
            }
            for (int i = 0; i < currentNonIntegerResults.Length; i++)
            {
                currentNonIntegerResults[i] = currentNonIntegerResults[i].Replace(variable, concreteVariable);
            }

          /*    for (int i = 0; i < extraElements.Length; i++)
              {
                  if (extraElements[i].Contains(variable.ToLower()))
                  {
                      extraElements[i] = extraElements[i].Replace(variable, concreteVariable);
                  }
              }*/
            return concreteAnswer;
        }

        private string ReplaceVariableWithCorrectString(string text, string variable, string concreteVariable)
        {
            // prevword and index
            List<Tuple<string, int>> variablesWithPrevWord = GetVariablesWithPrevword(text, variable);
            string[] splitted = text.Split(' ');
            foreach (Tuple<string, int> tuple in variablesWithPrevWord)
            {
                concreteVariable = ObtainCorrectString(tuple.First, concreteVariable);
                splitted[tuple.Second + 1] = concreteVariable;
            }
            return string.Join(" ", splitted);
        }

        private string ObtainCorrectString(string prevWord, string word)
        {
            // word is always in singular
            // todo obtener plural en casos que sean necesarios
            return word;
        }

        private List<Tuple<string, int>> GetVariablesWithPrevword(string text, string variable)
        {
            List<Tuple<string, int>> variablesWithPreWord = new List<Tuple<string, int>>();
            string[] splitted = text.Split(' ');
            for (int i = splitted.Length - 1; i >= 1; i--)
            {
                if (splitted[i].Equals(variable, StringComparison.InvariantCultureIgnoreCase))
                {
                    variablesWithPreWord.Add(new Tuple<string, int>(splitted[i - 1], i - 1));
                }
            }
            if(splitted[0].Equals(variable, StringComparison.InvariantCultureIgnoreCase)) variablesWithPreWord.Add(new Tuple<string, int>("", -1));
            return variablesWithPreWord;
        }

      

        private Tuple<string, string> MakeConcrete(Level level, string variable)
        {
            string lowerVariable = variable.ToLower();
            if (lowerVariable.Contains("number")) return new Tuple<string, string>(level.NumberesValues[variable].GetNumber().ToString(CultureInfo.InvariantCulture), "");
            if (lowerVariable.Contains("subject")) return new Tuple<string, string>(GameManager.GetManager().GetSubjects()[UnityEngine.Random.Range(0, GameManager.GetManager().GetSubjects().Length)], "");
            if (lowerVariable.Contains("element"))
            {
                string group = lowerVariable.Substring(lowerVariable.IndexOf("_", StringComparison.Ordinal) + 1);
                return Elements[group][UnityEngine.Random.Range(0, Elements[group].Length)];
            }
            if (lowerVariable.Contains("place")) return Places[UnityEngine.Random.Range(0, Places.Length)];
            if (lowerVariable.Contains("verb")) return Verbs[UnityEngine.Random.Range(0, Verbs.Length)];
            if (lowerVariable.Contains("container")) return Containers[UnityEngine.Random.Range(0, Containers.Length)];
            throw new Exception("Trying to conretize a variable and it doesn't match with any pattern");
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
