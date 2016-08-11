using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace Assets.Scripts.App
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _gameManager;
        private string[] _subjects;
        private Tuple<string, string>[] _imagePaths;
        private PedagogicalObjective[] _pedagogicalObjectives;
        private Problem _currentProblem;

        void Awake()
        {
            if (_gameManager == null) _gameManager = this;
            else if(_gameManager != this) Destroy(this);
            LoadSubjects();
            LoadImages();
            LoadObjectives();
        }

        private void LoadImages()
        {
            TextAsset jsonTextAsset = Resources.Load<TextAsset>("images");
            JSONNode data = JSON.Parse(jsonTextAsset.text);
            _imagePaths = new Tuple<string, string>[data["images"].Count];
            for (int i = 0; i < data["images"].Count; i++)
            {
                _imagePaths[i] =  new Tuple<string, string>(data["images"][i]["id"], data["images"][i]["path"]);
            }
        }

        private void LoadSubjects()
        {
            TextAsset jsonTextAsset = Resources.Load<TextAsset>("subjects");
            JSONNode data = JSON.Parse(jsonTextAsset.text);
            _subjects = new string[data["subjects"].Count];
            for (int i = 0; i < data["subjects"].Count; i++)
            {
                _subjects[i] = data["subjects"][i];
            }
        }

        private void LoadObjectives()
        {
            List<JSONNode> datas = new List<JSONNode>();
            foreach (TextAsset textAsset in Resources.LoadAll<TextAsset>("PedagogicalObjectives/"))
            {
                datas.Add(JSON.Parse(textAsset.text));
            }
                   
            _pedagogicalObjectives = new PedagogicalObjective[datas.Count];
            for (int i = 0; i < datas.Count; i++)
            {
                _pedagogicalObjectives[i] = ObtainPedagogicalObjective(datas[i]);
            }
        /*    foreach (PedagogicalObjective pedagogicalObjective in _pedagogicalObjectives)
            {
                pedagogicalObjective.InitSchemas();
            }*/
        }

        private PedagogicalObjective ObtainPedagogicalObjective(JSONNode jsonNode)
        {
            PedagogicalObjective pedagogicalObjective = new PedagogicalObjective();

            pedagogicalObjective.Name = jsonNode["name"];
            pedagogicalObjective.Levels = LoadLevels(jsonNode["levels"]);
            pedagogicalObjective.Schemas = LoadSchemas(jsonNode["schemas"], pedagogicalObjective.Levels.Length);

            return pedagogicalObjective;
        }

        private Schema[] LoadSchemas(JSONNode schemas, int levels)
        {
            Schema[] schemasArray = new Schema[schemas.Count];
            for (int i = schemas.Count - 1; i >= 0; i--)
            {
                schemasArray[i] = LoadSchema(schemas[i], levels);
            }
            return schemasArray;
        }

        private Schema LoadSchema(JSONNode schemaAsset, int levels)
        {
            Schema schema = new Schema();
            schema.Title = schemaAsset["title"];
            schema.Levels = new Dictionary<string, Level>(levels);

            for (int i = levels - 1; i >= 0; i--)
            {
                Tuple<string, Level> level = (LoadLevel(schemaAsset, i));
                schema.Levels.Add(level.First, level.Second);
            }

            schema.Places = ObtainStringTuple(schemaAsset["places"], "text", "id");
            schema.Verbs = ObtainStringTuple(schemaAsset["verbs"], "text", "id");
            schema.Containers = ObtainStringTuple(schemaAsset["containers"], "text", "id");
            schema.Elements = ObtainStringMatrix(schemaAsset["elements"], "text", "id"); 
            
            return schema;

        }

        private Tuple<string, string>[][] ObtainStringMatrix(JSONNode jsonNode, string key1, string key2)
        {
            Tuple<string, string>[][] values = new Tuple<string, string>[jsonNode.Count][];
            for (int i = jsonNode.Count - 1; i >= 0; i--)
            {
                values[i] = new Tuple<string, string>[jsonNode[i].Count];
                for (int j = jsonNode[i].Count - 1; j >= 0; j--)
                {
                    values[i][j] = new Tuple<string, string>(jsonNode[i][j][key1], jsonNode[i][j][key2]);
                }
            }

            return values;
        }


        private Tuple<string, string>[] ObtainStringTuple(JSONNode jsonNode, string key1, string key2)
        {
            Tuple<string, string>[] values = new Tuple<string, string>[jsonNode.Count];
            for (int i = jsonNode.Count - 1; i >= 0; i--)
            {
                values[i] = new Tuple<string, string>(jsonNode[i][key1], jsonNode[i][key2]);
            }

            return values;
        }

        private Tuple<string, Level> LoadLevel(JSONNode schemaAsset, int index)
        {
            string key = schemaAsset["variables"].AsObject.GetKeys()[index].ToString();

            Level level = new Level();

            level.Variables = ObtainStringArray(schemaAsset["variables"][key]);
            level.BaseText = ObtainBaseText(key, schemaAsset["baseText"]);
            level.BaseQuestions = ObtainStringTuple(schemaAsset["baseQuestions"][key], "question", "question");
            level.NumberesValues = ObtainNumberValues(key, schemaAsset["numberValues"]);

            level.IntegerResults = ObtainStringArray(schemaAsset["integerResults"][key]);
            level.NonIntegerResults = ObtainStringArray(schemaAsset["nonIntegerResults"][key]);
            level.PositiveResults = ObtainStringArray(schemaAsset["positiveResults"][key]);
            level.ZeroOrPositiveResults = ObtainStringArray(schemaAsset["zeroOrPositiveResults"][key]);
            level.NegativeResults = ObtainStringArray(schemaAsset["negativeResults"][key]);
            level.ZeroOrNegativeResults = ObtainStringArray(schemaAsset["zeroOrNegativeResults"][key]);
            level.ZeroResults = ObtainStringArray(schemaAsset["zeroResults"][key]);

            return new Tuple<string, Level>(key, level);

        }

        private Dictionary<string, INumberGenerator> ObtainNumberValues(string key, JSONNode numberNode)
        {
            JSONClass numberValues = numberNode[key].AsObject;
            ArrayList keys = numberValues.GetKeys();
            Dictionary<string, INumberGenerator> values = new Dictionary<string, INumberGenerator>(keys.Count);

            for (int j = keys.Count - 1; j >= 0; j--)
            {
                INumberGenerator generator;
                if (numberValues[keys[j].ToString()]["type"].Value
                    .Equals("discrete", StringComparison.InvariantCultureIgnoreCase)) generator = new SetNumberGenerator(numberValues[keys[j].ToString()]);
                else generator = new RangeNumberGenerator(numberValues[keys[j].ToString()]); ;
                                   
                values.Add(keys[j].ToString(), generator);
            }
            return values;
        }

        private string ObtainBaseText(string key, JSONNode jsonNode)
        {
            return jsonNode[key].ToString();
        }

        private string[] ObtainStringArray(JSONNode variablesNode)
        {
            string[] values = new string[variablesNode.Count];
            for (int j = variablesNode.Count - 1; j >= 0; j--)
            {
                values[j] = variablesNode[j];
            }
            return values;
        }

      /*  private Dictionary<string, string[]> ObtainDictionaryStringToArray(JSONClass asset)
        {
            ArrayList keys = asset.GetKeys();
            Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>(keys.Count);
            for (int i = keys.Count - 1; i >= 0; i--)
            {
                string[] values = new string[asset[keys[i].ToString()].Count];
                for (int j = asset[keys[i].ToString()].Count - 1; j >= 0; j--)
                {
                    values[j] = asset[keys[i].ToString()][j];
                }
                dictionary.Add(keys[i].ToString(), values);
            }
            return dictionary;
        }*/


        private Dictionary<string, string> ObtainDictionaryStringToString(JSONClass asset)
        {
            ArrayList keys = asset.GetKeys();
            Dictionary<string, string> dictionary = new Dictionary<string, string>(keys.Count);
            for (int i = keys.Count - 1; i >= 0; i--)
            {                
                dictionary.Add(keys[i].ToString(), asset[keys[i].ToString()].ToString());
            }
            return dictionary;
        }


        private int[] LoadLevels(JSONNode levels)
        {
            int[] levelsInts = new int[levels.Count];
            for (int i = levels.Count - 1; i >= 0; i--)
            {
                levelsInts[i] = int.Parse(levels[i]);
            }
            return levelsInts;
        }

        public string GetText()
        {
            return _currentProblem.GetText();
        }

        public static GameManager GetManager()
        {
            return _gameManager;
        }

        public List<string> GetObjectives(int level)
        {
            List<string> objectivesList = new List<string>(_pedagogicalObjectives.Length);
            for (int i = 0; i < _pedagogicalObjectives.Length; i++)
            {
                if(_pedagogicalObjectives[i].GetLevels().Contains(level)) objectivesList.Add(_pedagogicalObjectives[i].Name);
            }
            return objectivesList;
        }

        public void SetSelectedObjective(int level, int selectedObjective)
        {
            List<PedagogicalObjective> currrentPedagogicalObjectives = new List<PedagogicalObjective>();
            for (int i = 0; i < _pedagogicalObjectives.Length; i++)
            {
                if (_pedagogicalObjectives[i].GetLevels().Contains(level)) currrentPedagogicalObjectives.Add(_pedagogicalObjectives[i]);
            }
            _currentProblem = currrentPedagogicalObjectives[selectedObjective].GenerateProblem(level);
            ViewController.GetController().StartProblem();
            ProblemController.GetController().SetProblem(_currentProblem);
        }

        public Problem GetCurrentProblem()
        {
            return _currentProblem;
        }

        public void GenerateOtherProblem()
        {
            ProblemController.GetController().SetProblem(_currentProblem);
            ProblemController.GetController().ShowTextAnalysisPhase();

        }

        public string[] GetSubjects()
        {
            return _subjects;
        }
    }
}
