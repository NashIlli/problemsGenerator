using UnityEngine;
using UnityEngine.UI;

public class paintText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Paint()
    {
        gameObject.GetComponent<Text>().color =new Color32((byte) Random.Range(0,256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256),255);
    }
}
