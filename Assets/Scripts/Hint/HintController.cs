using Assets.Scripts.App;
using Assets.Scripts.Sound;
using UnityEngine;

namespace Assets.Scripts.Hint
{
    public class HintController : MonoBehaviour {

        public void OnClickHintScreen()
        {
            SoundController.GetController().PlayClickSound();
            ViewController.GetController().HideHint();
        }
    }
}
