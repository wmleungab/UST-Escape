using UnityEngine;
using System.Collections;

public class lightControl : MonoBehaviour {
    public float duration = 1.0F;
    public Light lt;

	public float minIntensity = 0.0f;
	public float maxIntensity = 0.3f;

	private bool triggerEnter;
	private bool lightup;
	private float startTime;
    void Start() {
        lt = lt.GetComponent<Light>();
    }
    void Update() {
		if(triggerEnter && ((lightup && lt.intensity < maxIntensity) || (!lightup && lt.intensity > minIntensity)) ) {
			float deltaValue = (Time.time - startTime) / duration;
			if (lightup == false) deltaValue *= -1.0f;
			float amplitude = lt.intensity + deltaValue*0.1f;
			lt.intensity = amplitude;
		}
    }
	void OnTriggerExit () {
		Debug.Log("Trigger exit " + lt.intensity);
		triggerEnter = true;
		if (lt.intensity <= minIntensity) lightup = true;
		else lightup = false;
		
		startTime = Time.time;
	}
}