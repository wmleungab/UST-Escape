using UnityEngine;
using System.Collections;

public class Fryer : MonoBehaviour {
	public AudioClip flyfrysound;
	public AudioClip defendsound;

	public GameObject prompt;

	public GameObject defendSheildpf;
	
	 GameObject defendSheild;
	public bool defendState=false;
	public float defendmin=2;
	public float defendfactor=2;
	public float defendfrequency=1;
	public float defendlength=2;
	public float defendTimer;
	public float sheildsize=0.5f;

	public bool startFlag=true;
	public bool dieFlag=true;

	
	public float lastAttack=0;
	public float canAttackRange=0.5f;

	public GameObject fry;

	public bool attacking=true;
	public float frequency=1f;
	public float min=2f;
	public float factor=4f;
	Vector3 pos;
	
	bool endFlag=false;

	// Use this for initialization
	void Start () {
	
	}
	void init(){
		pos = gameObject.transform.position;
		
		invokeAttack ();

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

	void Attack(){
		GameObject p = Instantiate (prompt, new Vector3 (pos.x+0.3f , pos.y+3.2f, pos.z-1),Quaternion.identity)as GameObject;
	
		GameObject s = Instantiate (fry, new Vector3 (pos.x + 0.43f, pos.y + 0.78f, pos.z),Quaternion.identity)as GameObject;
		s.transform.parent = 	GameObject.Find("Weapons").transform;
		//continue attack
		AudioSource.PlayClipAtPoint (flyfrysound, pos);
		if (attacking) {
			invokeAttack();
				}

	}
	void invokeAttack(){
		
		if (dieFlag && BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING) {
						float ran = Random.value;
						Invoke ("Attack", frequency * (min + ran * factor));
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

	void OnMouseDown(){
		if (BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING && dieFlag && !defendState&& Time.time-lastAttack>canAttackRange) {
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
		CancelInvoke ("Defend");
		CancelInvoke ("DefendDisappear");
		CancelInvoke ("Attack");
		StartCoroutine ("dieAnim");
	}
	
	IEnumerator dieAnim ()
	{
		Renderer [] component = gameObject.GetComponentsInChildren<Renderer>();
		
		for (float i=0; i<=1f; i+=0.05f) {
		foreach (Renderer r in component) {
				r.material.color = new Vector4 (r.material.color.r, r.material.color.g, r.material.color.b, 1-i);
				yield return new WaitForSeconds (0.01f);}
		}


		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}

}
