﻿using System;
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
        private string[] elementsToDrag;
        private string[] positiveResults;
        private string[] negativeResults;
        private string[] integerResults;

        public Problem(string title,string concreteText, string concreteQuestion, string concreteAnswer, string[] elementsToDrag, string[] positiveResults, string[] negativeResults, string[] integerResults)
        {
            this.title = title;
            this.concreteText = concreteText;
            this.concreteQuestion = concreteQuestion;
            this.concreteAnswer = concreteAnswer;
            this.elementsToDrag = elementsToDrag;
            this.positiveResults = positiveResults;
            this.negativeResults = negativeResults;
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
            float result = float.Parse(new Expression(concreteAnswer).Evaluate().ToString());
            return Math.Abs(result - answer) < 0.05;
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

            foreach (string negativeResult in negativeResults)
            {
                if (float.Parse(new Expression(negativeResult).Evaluate().ToString()) >= 0) return false;
            }

            foreach (string integerResult in integerResults)
            {
                decimal result = decimal.Parse(new Expression(integerResult).Evaluate().ToString());
                decimal resto = result%1;
                if (resto != 0) return false;
            }
            return true;
        }

        public void AddElements(string[] extraElements)
        {
            var z = new string[elementsToDrag.Length + extraElements.Length];
            elementsToDrag.CopyTo(z, 0);
            extraElements.CopyTo(z, elementsToDrag.Length);
            elementsToDrag = z;
        }
    }
}
