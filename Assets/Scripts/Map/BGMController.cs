using UnityEngine;
using System.Collections;

public class BGMController : MonoBehaviour {
	public AudioClip secondBGM; 
	AudioClip temp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeBGM(){
		gameObject.audio.Stop ();
		temp = gameObject.audio.clip;
		gameObject.audio.clip = secondBGM;
		gameObject.audio.Play  ();
	}
}
