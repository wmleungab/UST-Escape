using UnityEngine;
using System.Collections;

public class DimJack : MonoBehaviour {
	public GameObject defendSheildpf;
	
	public GameObject prompt;
	
	public AudioClip hitsound;
	public AudioClip defendsound;

	GameObject defendSheild;
	public bool defendState=false;
	public float defendmin=2;
	public float defendfactor=2;
	public float defendfrequency=1;
	public float defendlength=2;
	public float defendTimer;
	public float sheildsize=0.5f;

	
	public float lastAttack=0;
	public float canAttackRange=0.5f;


	bool endFlag=false;

	public bool startFlag=true;
	public bool dieFlag=true;
	

	public float min;
	public float factor;
	public float frequency;

	public float threshold;
	
	Vector3 pos;

	public GameObject maclok1;
	public GameObject maclok2;

	Animator anim;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		
		invokeAttack1 ();
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

	void invokeAttack1 ()
	{

		if (dieFlag && BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING) {
			float r = Random.value;
			Invoke ("attack1", frequency * (min + factor * r));
		}
	}

	void Update(){
		if (BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING ) {
			if(startFlag)			{
				init ();
				startFlag=false;
				
			}
			if (GetComponent<HealthBar> ().HP <= 0 && dieFlag) {
				die();
				dieFlag=false;
			}
		}
		if (BattleController.currentBattleState == BattleState.BATTLE_ENDING_LOST &&!endFlag) {
			
			CancelInvoke ("Defend");
			CancelInvoke ("DefendDisappear");
			CancelInvoke ("readyTofight");
			CancelInvoke ("counting");
			endFlag=true;
		}

	}
	void init(){
		pos = gameObject.transform.position;
		invokeAttack1 ();

		float ran = Random.value;
		Invoke ("Defend", defendfrequency * (defendmin + ran * defendfactor));
	}


	void attack1(){
		anim.SetTrigger ("attack1");
		GameObject p = Instantiate (prompt, new Vector3 (transform.position.x+0.3f , transform.position.y+3.7f, transform.position.z-1),Quaternion.identity)as GameObject;

	}

	void attack1_createmaclok(){
		AudioSource.PlayClipAtPoint (hitsound, transform.position);
		GameObject w = GameObject.Find ("Weapons");
		GameObject s = Instantiate (maclok1, new Vector3 (pos.x + 1, pos.y , w.transform.position.z),Quaternion.identity)as GameObject;
		GameObject s2 = Instantiate (maclok2, new Vector3 (pos.x - 1.3f, pos.y , w.transform.position.z),Quaternion.identity)as GameObject;

		Animator sa = s.GetComponentInChildren<Animator> ();
		Animator s2a = s.GetComponentInChildren<Animator> ();

		int r = (int)(2 * Random.value) + 1;
		if (r == 1) {
						sa.SetBool ("isleft", false);
						s2a.SetBool ("isleft", false);
				} else {
						sa.SetBool ("isleft", true);
						s2a.SetBool ("isleft", true);
				}

		s.transform.parent = 	w.transform;
		s2.transform.parent = 	w.transform;

	}

	void OnMouseDown ()
	{
		if (BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING && dieFlag&& !defendState&& Time.time-lastAttack>canAttackRange) {
			gameObject.GetComponent<HealthBar> ().HP--;
			audio.Play();
			lastAttack=Time.time;
			
		}
		if (defendState) {
			AudioSource.PlayClipAtPoint (defendsound, pos);
		}
	}
	void die()
	{
		StartCoroutine ("dieAnim");
		CancelInvoke ("Defend");
		CancelInvoke ("DefendDisappear");
		CancelInvoke ("attack1");

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
