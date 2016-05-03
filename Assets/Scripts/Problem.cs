using UnityEngine;
using System.Collections;
using NCalc;

public class Problem
{
    private string title;
    private string concreteText;
    private string concreteQuestion;
    private string concreteAnswer;

    public Problem(string title,string concreteText, string concreteQuestion, string concreteAnswer)
    {
        this.title = title;
        this.concreteText = concreteText;
        this.concreteQuestion = concreteQuestion;
        this.concreteAnswer = concreteAnswer;
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
}
