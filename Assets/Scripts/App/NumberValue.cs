
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEngine;

namespace Assets.Scripts.App
{
    public interface INumberGenerator
    {
        float GetNumber();
    }

    // Defines a range of numbers whit a arbitrary step

    public class RangeNumberGenerator : INumberGenerator
    {
        private readonly float _start;
        private readonly float _end;
        private readonly float _step;

        public RangeNumberGenerator(JSONNode jsonNode)
        {
            _start = float.Parse(jsonNode["start"]);
            _end = float.Parse(jsonNode["end"]);
            _step = float.Parse(jsonNode["step"]);
        }

        public float GetNumber()
        {
            return _start +  _step * Mathf.RoundToInt(Random.Range(0, (_end - _start) / _step)) ;
        }

        
    }


    // Defines a set of numbers which can be obtained
    public class SetNumberGenerator : INumberGenerator
    {

        private readonly float[] _possibleNumbers;

        public SetNumberGenerator(JSONNode jsonNode)
        {
            _possibleNumbers = new float[jsonNode["possibleNumbers"].Count] ;
            for (int i = jsonNode["possibleNumbers"].Count - 1; i >= 0; i--)
            {
                _possibleNumbers[i] = float.Parse(jsonNode["possibleNumbers"][i]);
            }
        }

        public float GetNumber()
        {
            return _possibleNumbers[Random.Range(0, _possibleNumbers.Length)];
        }
    }
}