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

        public Problem(string title,string concreteText, string concreteQuestion, string concreteAnswer, string[] elementsToDrag)
        {
            this.title = title;
            this.concreteText = concreteText;
            this.concreteQuestion = concreteQuestion;
            this.concreteAnswer = concreteAnswer;
            this.elementsToDrag = elementsToDrag;
            Debug.Log(new Expression(concreteAnswer).Evaluate());
        }


        public string GetText()
        {
            return concreteText;
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
    }
}
