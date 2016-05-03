using System;
using System.Collections;

[Serializable]
public class PedagogicalObjective
{

    public string name;
    public Schema[] schemas;

    public Problem GenerateProblem()
    {
        Schema schema = schemas[UnityEngine.Random.Range(0, schemas.Length)];
        return schema.GenerateProblem();
    }
}

  