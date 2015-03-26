using UnityEngine;
using System.Collections;

public class Dembeater2 : MonoBehaviour {
	public GameObject defendSheildpf;
	
	GameObject defendSheild;
	public bool defendState=false;
	public float defendmin=2;
	public float defendfactor=2;
	public float defendfrequency=1;
	public float defendlength=2;
	public float defendTimer;
	public float sheildsize=0.5f;

	
	public GameObject explosion;
	
	public bool startFlag = true;
	public bool dieFlag = true;
	public float readyTime;
	public float min;
	public  float factor;
	public  float frequency;
	public float threshold;
	public int damage;
	float time;
	Animator anim;
	Vector3 mouseStart;
	Vector3 mouseCurrent;
	Vector3 ms;
	Vector3 mc;
	bool allowShake = false;
	
	public int fingTimeThreshold;
	
	bool allowFing=true;
	int fingFrameTime=0;
	
	Vector3 pos;
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	void  	init ()
	{
		anim = GetComponent<Animator> ();
		invokeReadyToFight ();
		
		mouseStart = Vector3.zero;
		mouseCurrent = Vector3.zero;
		allowShake = false;
		pos = gameObject.transform.position;

		float ran = Random.value;
		Invoke ("Defend", defendfrequency * (defendmin + ran * defendfactor));
	}

	
	void Defend(){
		defendSheild = Instantiate (defendSheildpf, new Vector3 (pos.x , pos.y, pos.z-1),Quaternion.identity)as GameObject;
		defendSheild.transform.localScale = new Vector3 (sheildsize, sheildsize, 1);
		defendSheild.transform.parent = gameObject.transform;
		defendState = true;
		Invoke ("DefendDisappear", defendlength);
	}
	
	void DefendDisappear(){
		Destroy (defendSheild);
		defendState = false;
		float ran = Random.value;
		Invoke ("Defend", defendfrequency * (defendmin + ran * defendfactor));
	}
	
	void readyTofight ()
	{
		time = readyTime;
		anim.SetTrigger ("ReadyToFight");
		InvokeRepeating ("counting", 1, 1f);
		allowShake = true;
		mouseStart = Vector3.zero;
		mouseCurrent = Vector3.zero;
		
	}
	
	void counting ()
	{
		
		time--;
		if (time == 0) {
			CancelInvoke ("counting");
			fight ();
		}
	}
	
	void fight ()
	{
		anim.SetTrigger ("ToFight");
		resetVar ();
	}
	
	void invokeReadyToFight ()
	{
		if (dieFlag && BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING) {
			float r = Random.value;
			Invoke ("readyTofight", frequency * (min + factor * r));
		}
	}
	// Update is called once per frame
	void defendSucceed ()
	{
		anim.SetTrigger ("defend_succeed");
		resetVar ();
		CancelInvoke ("counting");
		invokeReadyToFight ();
		
	}
	
	void resetVar ()
	{
		allowShake = false;
		mouseStart = Vector3.zero;
		mouseCurrent = Vector3.zero;
	}
	
	void Update ()
	{
		
		//cursor
		if (allowShake) {
			
			if (Vector3.Distance (Vector3.zero, Input.acceleration) > threshold)
				fingFrameTime++;
			
			if (fingFrameTime > fingTimeThreshold){
				
				defendSucceed ();
				allowFing=false;
			}
			
		}
		if (BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING) {
			if (startFlag) {
				init ();
				startFlag = false;
			}
			
			if (GetComponent<HealthBar> ().HP <= 0 && dieFlag) {
				print ("s");
				die ();
				dieFlag = false;
			}
		}
		
		
		
		
	}
	
	void OnMouseDown ()
	{
		if (BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING && dieFlag&& !defendState) {
			gameObject.GetComponent<HealthBar> ().HP--;
			
		}
	}
	
	void playerdamage ()
	{
		GameObject.Find ("Player").GetComponent<HealthBar> ().HP -= damage;
		GameObject exp = Instantiate (explosion, new Vector3 (pos.x-0.3f, pos.y+2.5f, pos.z),Quaternion.identity)as GameObject;
		exp.transform.localScale = new Vector3 (2f, 2f, 1);
		GameObject exp2 = Instantiate (explosion, new Vector3 (pos.x+0.2f, pos.y+2.6f, pos.z),Quaternion.identity)as GameObject;
		exp2.transform.localScale = new Vector3 (2f, 2f, 1);
	}
	
	void die ()
	{
		StartCoroutine ("dieAnim");
		CancelInvoke ("Defend");
		CancelInvoke ("DefendDisappear");
	}



	IEnumerator dieAnim ()
	{
		Renderer [] component = gameObject.GetComponentsInChildren<Renderer> ();
		
		for (float i=0; i<=1f; i+=0.05f) {
			foreach (Renderer r in component) {
				r.material.color = new Vector4 (r.material.color.r, r.material.color.g, r.material.color.b, 1 - i);
				yield return new WaitForSeconds (0.01f);
			}
		}
		
		
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}
}
