﻿using UnityEngine;
using System.Collections;

public class Mickey : MonoBehaviour {
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
	
	
	public float min;
	public float factor;
	public float frequency;
	
	public float threshold;
	
	Vector3 pos;
	
	public GameObject box;
	
	Animator anim;
	
	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator> ();

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
			Invoke ("attack", frequency * (min + factor * r));
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
	}
	void init(){
		pos = gameObject.transform.position;
		invokeAttack1 ();
		float ran = Random.value;
		Invoke ("Defend", defendfrequency * (defendmin + ran * defendfactor));
	}
	
	void OnMouseDown ()
	{
		if (BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING && dieFlag && !defendState) {
			gameObject.GetComponent<HealthBar> ().HP--;
			audio.Play();
		}
	}


	void attack(){
		anim.SetTrigger ("attack");
	}
	
	void attack_createbox(){
		GameObject w = GameObject.Find ("Weapons");
		GameObject s = Instantiate (box, new Vector3 (pos.x , pos.y+2.3f , w.transform.position.z),Quaternion.identity)as GameObject;
		s.transform.parent = w.transform;
	}

	void die()
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
