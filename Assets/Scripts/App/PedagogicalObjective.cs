using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Assets.Scripts.App
{
    [Serializable]
    public class PedagogicalObjective
    {

        public string name;
        public int[] levels;
        public Schema[] schemas;

        public Problem GenerateProblem()
        {
            Schema schema = schemas[UnityEngine.Random.Range(0, schemas.Length)];
            return schema.GenerateProblem();
        }

        public List<int> GetLevels()
        {
            return levels.ToList();
        }
    }
}

  