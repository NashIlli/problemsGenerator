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

        public Problem GenerateProblem(int level)
        {
            /*
                        Schema schema = schemas[UnityEngine.Random.Range(0, schemas.Length)];
            */
            Schema schema = schemas[0];

            return schema.GenerateProblem(levels.ToList().IndexOf(level));
        }

        public List<int> GetLevels()
        {
            return levels.ToList();
        }

        public void InitSchemas()
        {
            foreach (Schema schema in schemas)
            {
                schema.InitSchema();
            }
        }
    }
}

  