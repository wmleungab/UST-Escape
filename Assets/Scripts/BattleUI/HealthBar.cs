﻿using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public GameObject hb;

	public int fullHP;
	public int HP;
	public Vector3 offset;
	public float hpLengthFactor;
	
	float hpInitScaleX;
	Vector3 pos;

	GameObject s;

	// Use this for initialization
	void Start () {
		
		pos = gameObject.transform.position;
		hpInitScaleX = hb.transform.GetChild (1) .localScale.x;
		s = Instantiate (hb, pos+offset,Quaternion.identity)as GameObject;
		s.transform.localScale = new Vector3 (s.transform.localScale.x * hpLengthFactor, s.transform.localScale.y, 1);
		s.transform.parent = gameObject.transform;
		s.transform.position = new Vector3 (s.transform.position.x,s.transform.position.y,s.transform.position.z+2);

	}

	void Addhp(int hp){
		HP += hp;
		if (HP > fullHP)
						HP = fullHP;
	}
	
	void decreaseHP(int hp){
		HP -= hp;
		if (HP < 0)
			HP = 0;
	}

	void healthbar(){
		if (HP < 0)
						HP = 0;

		float r = (float)HP / (float)fullHP;
		float ns = hpInitScaleX * r;

		s.transform.GetChild (1).localScale = new Vector3 (ns, s.transform.GetChild (1).localScale.y, s.transform.GetChild (1).localScale.z);
		}
	// Update is called once per frame
	void Update () {
		healthbar ();
	}
}
