using System;
using NCalc;
using UnityEngine;

namespace Assets.Scripts.App
{
    public class Problem
    {
        private string title;
        private string concreteText;
        private string concreteQuestion;
        private string concreteAnswer;
        private string[] elementsToDrag;
        private string[] positiveResults;
        private string[] integerResults;

        public Problem(string title,string concreteText, string concreteQuestion, string concreteAnswer, string[] elementsToDrag, string[] positiveResults, string[] integerResults)
        {
            this.title = title;
            this.concreteText = concreteText;
            this.concreteQuestion = concreteQuestion;
            this.concreteAnswer = concreteAnswer;
            this.elementsToDrag = elementsToDrag;
            this.positiveResults = positiveResults;
            this.integerResults = integerResults;
        }


        public string GetText()
        {
            return concreteText + " \n " + concreteQuestion;
        }

        public string GetTitle()
        {
            return title;
        }

        public string[] GetElementsToDrag()
        {
            return elementsToDrag;
        }

        public string GetQuestion()
        {
            return concreteQuestion;
        }

        public bool CheckAnswer(float answer)
        {
            return Math.Abs(float.Parse(new Expression(concreteAnswer).Evaluate().ToString()) - answer) < 0.001;
        }

        public float GetResult()
        {
            return float.Parse(new Expression(concreteAnswer).Evaluate().ToString());
        }

        public bool CheckRestrictons()
        {
            foreach (string positiveResult in positiveResults)
            {
                if (float.Parse(new Expression(positiveResult).Evaluate().ToString()) <= 0) return false;
            }
            foreach (string integerResult in integerResults)
            {
                decimal result = decimal.Parse(new Expression(integerResult).Evaluate().ToString());
                decimal resto = result%1;
                if (resto != 0) return false;
            }
            return true;
        }
    }
}
