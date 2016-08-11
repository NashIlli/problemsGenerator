using System.Collections.Generic;

namespace Assets.Scripts.App
{
    public class Level
    {
        public string[] Variables;
        public string BaseText;
        // question and answer
        public Tuple<string, string>[] BaseQuestions;
        public Dictionary<string, INumberGenerator> NumberesValues;

        public string[] IntegerResults;
        public string[] NonIntegerResults;
        public string[] PositiveResults;
        public string[] ZeroOrPositiveResults;
        public string[] NegativeResults;
        public string[] ZeroOrNegativeResults;
        public string[] ZeroResults;

    }
}