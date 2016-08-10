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
            pedagogicalObjective.Schemas = LoadSchemas(jsonNode["schemas"]);

            return pedagogicalObjective;

        }

        private Schema[] LoadSchemas(JSONNode schemas)
        {
            Schema[] schemasArray = new Schema[schemas.Count];
            for (int i = schemas.Count - 1; i >= 0; i--)
            {
                schemasArray[i] = LoadSchema(schemas[i]);
            }
            return schemasArray;
        }

        private Schema LoadSchema(JSONNode schemaAsset)
        {
            Schema schema = new Schema();
            schema.Title = schemaAsset["title"];
            schema.Variables = ObtainDictionaryStringToArray(schemaAsset["variables"].AsObject);
            schema.BaseText = ObtainDictionaryStringToString(schemaAsset["baseText"].AsObject);
            schema.BaseQuestions = ObtainBaseQuestions(schemaAsset["baseQuestions"].AsObject);

            return schema;

        }

        private Dictionary<string, Question[]> ObtainBaseQuestions(JSONClass asset)
        {
         /*   ArrayList keys = asset.GetKeys();
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
            return dictionary;*/
            // todo 
            return null;
        }

        private Dictionary<string, string[]> ObtainDictionaryStringToArray(JSONClass asset)
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
        }


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
