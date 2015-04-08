using UnityEngine;
using System.Collections;

public class Multi_HealthBar : MonoBehaviour
{
		public GameObject hb;
		public Multi_Fields myFields;
		public int fullHP;
		public int HP;
		public Vector3 offset;
		public float hpLengthFactor;
		float hpInitScaleX;
		Vector3 pos;
		GameObject s;

	public AudioClip beingAttack;

	int lastHP;
		// Use this for initialization
		void Start ()
		{
				myFields = GameObject.Find ("SharedData").GetComponent<Multi_Fields> ();
				pos = gameObject.transform.position;
				hpInitScaleX = hb.transform.GetChild (1) .localScale.x;
				s = Instantiate (hb, pos + offset, Quaternion.identity)as GameObject;
				s.transform.localScale = new Vector3 (s.transform.localScale.x * hpLengthFactor, s.transform.localScale.y, 1);
				s.transform.parent = gameObject.transform;
				//s.transform.position = new Vector3 (s.transform.position.x,s.transform.position.y,s.transform.position.z-2);
		
		}
	
		void healthbar ()
		{
		lastHP = HP;
				if (Network.isClient) {
						if (name == "Enemy")
								HP = myFields.ServerHP;
						else
								HP = myFields.ClientHP;
				} else if (Network.isServer) {
						if(name=="Player")HP = myFields.ServerHP;
						else	HP = myFields.ClientHP;
				}
				if (lastHP > HP && gameObject.audio != null) {
			AudioClip ac=gameObject.audio.clip;
			gameObject.audio.clip=beingAttack;
						gameObject.audio.Play ();
			gameObject.audio.clip=ac;
				}
				if (HP < 0)
						HP = 0;
		
				float r = (float)HP / (float)fullHP;
				float ns = hpInitScaleX * r;
		
				s.transform.GetChild (1).localScale = new Vector3 (ns, s.transform.GetChild (1).localScale.y, s.transform.GetChild (1).localScale.z);
		}
		// Update is called once per frame
		void Update ()
		{
				healthbar ();
		}
}
