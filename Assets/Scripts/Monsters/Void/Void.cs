using UnityEngine;
using System.Collections;

public class Void : MonoBehaviour {
	public GameObject defendSheildpf;
	
	public AudioClip defendsound;
	AudioSource defendSound;
	
	public GameObject prompt;

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

	public bool startFlag=true;
	public bool dieFlag=true;

	public float min;
	public  float factor;
	public  float frequency;

	public GameObject magicball;
	public Vector2 magicballoffset;
	
	bool endFlag=false;
	Vector3 pos;
	void init(){
		pos = gameObject.transform.position;
		invokeAttack ();

		float ran = Random.value;
		Invoke ("Defend", defendfrequency * (defendmin + ran * defendfactor));
	}
	void Start(){
		defendSound = GetComponents<AudioSource>()[1];

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
		GameObject p = Instantiate (prompt, new Vector3 (transform.position.x+0f , transform.position.y+3.2f, transform.position.z-1),Quaternion.identity)as GameObject;

		GameObject mb = Instantiate (magicball, new Vector3 (pos.x+magicballoffset.x , pos.y+magicballoffset.y , pos.z), Quaternion.identity)as GameObject;
		mb.transform.parent = 	GameObject.Find("Weapons").transform;
		mb.GetComponent<Magicball> ().parentMon = gameObject;
	}

 public	void invokeAttack(){
		if (dieFlag && BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING) {
						float r = Random.value;
						Invoke ("Attack", frequency * (min + r * factor));
				}
		}
	void Update(){
		if (BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING ) {
			if(startFlag){
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
		if (BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING && dieFlag && !defendState && Time.time-lastAttack>canAttackRange) {
						gameObject.GetComponent<HealthBar> ().HP--;
			audio.Play ();
			lastAttack=Time.time;
				}
		if (defendState) {
		//	AudioSource.PlayClipAtPoint (defendsound, pos);
		
			defendSound.clip = defendsound;
			defendSound.Play ();
		}
	}
	void die()
	{

		StartCoroutine ("dieAnim");
		CancelInvoke ("Defend");
		CancelInvoke ("DefendDisappear");
		CancelInvoke ("Attack");
	}


	IEnumerator dieAnim ()
	{
		Renderer [] component = gameObject.GetComponentsInChildren<Renderer>();
		
		for (float i=0; i<=1f; i+=0.05f) {
			foreach (Renderer r in component) {
				if(r !=null)r.material.color = new Vector4 (r.material.color.r, r.material.color.g, r.material.color.b, 1-i);
				yield return new WaitForSeconds (0.01f);}
		}
		
		
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
		}
}
