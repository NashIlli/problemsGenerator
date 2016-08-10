using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.App
{
    [Serializable]
    public class PedagogicalObjective
    {

        public string Name;
        public int[] Levels;
        public Schema[] Schemas;

        public Problem GenerateProblem(int level)
        {
            return null;
            //return Schemas[UnityEngine.Random.Range(0, Schemas.Length)].GenerateProblem(Levels.ToList().IndexOf(level));
        }

        public List<int> GetLevels()
        {
            return Levels.ToList();
        }

        public void InitSchemas()
        {
            foreach (Schema schema in Schemas)
            {
                schema.InitSchema();
            }
        }
    }
}

  