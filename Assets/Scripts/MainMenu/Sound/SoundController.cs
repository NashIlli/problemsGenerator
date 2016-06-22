using UnityEngine;

namespace Assets.Scripts.Sound{

    public class SoundController : MonoBehaviour{
	    private static SoundController soundController;

	    public AudioClip wrongAnswerSound;
	    public AudioClip rightAnswerSound;
	    public AudioClip clickSound;

        public AudioSource soundSource;

        void Awake(){
            if (soundController == null){
                soundController = this;
            }
            else if (soundController != this){
                Destroy(gameObject);
            }
        } 

        public void PlayFailureSound(){
            soundSource.clip = wrongAnswerSound;
            soundSource.Play();      
        }

        public void PlayClickSound(){
		    soundSource.clip = clickSound;
		    soundSource.Play();
        }

        public void PlayRightAnswerSound(){ 
		    soundSource.clip = rightAnswerSound;
		    soundSource.Play();
        }   

        public void StopSound(){
            soundSource.Stop();
        }

        public static SoundController GetController(){
            return soundController;
        }
    }
}