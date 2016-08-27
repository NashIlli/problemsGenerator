using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NCalc;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.App
{
    public class Problem
    {
        private string title;
        private string concreteText;
        private string concreteQuestion;
        private string concreteAnswer;
        private List<string> elementsToDrag;
        private string[] positiveResults;
        private string[] zeroOrPositiveResults;
        private string[] negativeResults;
        private string[] zeroOrNegativeResults;
        private string[] zeroResults;
        private string[] integerResults;
        private string[] nonIntegerResults;

        public Problem(string title,string concreteText, string concreteQuestion, string concreteAnswer, List<string> elementsToDrag, string[] positiveResults, string[] zeroOrPositiveResults, string[] negativeResults, string[] zeroOrNegativeResults, string[] zeroResults, string[] integerResults, string[] nonIntegerResults)
        {
            this.title = title;
            this.concreteText = concreteText;
            this.concreteQuestion = concreteQuestion;
            this.concreteAnswer = concreteAnswer;
            this.elementsToDrag = elementsToDrag;
            this.positiveResults = positiveResults;
            this.zeroOrPositiveResults = zeroOrPositiveResults;
            this.negativeResults = negativeResults;
            this.zeroOrNegativeResults = zeroOrNegativeResults;
            this.zeroResults = zeroResults;
            this.integerResults = integerResults;
            this.nonIntegerResults = nonIntegerResults;
        }


        public string GetText()
        {
            return concreteText + " \n " + concreteQuestion;
        }

        public string GetTitle()
        {
            return title;
        }

        public List<string> GetElementsToDrag()
        {
            return elementsToDrag;
        }

        public string GetQuestion()
        {
            return concreteQuestion;
        }

        public bool CheckAnswer(float answer)
        {
            float result = float.Parse(new Expression(concreteAnswer).Evaluate().ToString());
            return Math.Abs(result - answer) < 0.05;
        }

        public float GetResult()
        {
            return float.Parse(new Expression(concreteAnswer).Evaluate().ToString());
        }

        public bool CheckRestrictons()
        {
            foreach (string result in positiveResults)
            {
                if (float.Parse(new Expression(result).Evaluate().ToString()) <= 0) return false;
            }
            foreach (string result in zeroOrPositiveResults)
            {
                if (float.Parse(new Expression(result).Evaluate().ToString()) < 0) return false;
            }

            foreach (string result in negativeResults)
            {
                if (float.Parse(new Expression(result).Evaluate().ToString()) >= 0) return false;
            }

            foreach (string result in zeroOrNegativeResults)
            {
                if (float.Parse(new Expression(result).Evaluate().ToString()) > 0) return false;
            }

            foreach (string result in zeroResults)
            {
                if (Math.Abs(float.Parse(new Expression(result).Evaluate().ToString())) > 0.00001) return false;
            }

            foreach (string integerResult in integerResults)
            {
                decimal result = decimal.Parse(new Expression(integerResult).Evaluate().ToString());
                decimal resto = result%1;
                if (resto != 0) return false;
            }

            foreach (string nonIntegerResult in nonIntegerResults)
            {
                decimal result = decimal.Parse(new Expression(nonIntegerResult).Evaluate().ToString());
                decimal resto = result % 1;
                if (resto == 0) return false;
            }
            return true;
        }

     /*   public void AddElements(string[] extraElements)
        {
            var z = new string[elementsToDrag.Length + extraElements.Length];
            elementsToDrag.CopyTo(z, 0);
            extraElements.CopyTo(z, elementsToDrag.Length);
            elementsToDrag = z;
        }*/
    }
}
