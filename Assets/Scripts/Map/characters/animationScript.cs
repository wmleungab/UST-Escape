using UnityEngine;
using System.Collections;

public class animationScript : MonoBehaviour {
	
	private Animator animator;
	public bool useOnPlayer = true;
	public GameObject player;
	NavMeshAgent myAgent;
	
	// angles in anti-clockwise from north
	public float angleNW = 45.0f;
	public float angleSW = 135.0f;
	public float angleSE = 225.0f;
	public float angleNE = 315.0f;
	public float angleBuffer = 15.0f;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
        if (useOnPlayer) player = GameObject.FindGameObjectWithTag("Player");
		myAgent = player.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
		float angle = Vector3.Angle(myAgent.velocity.normalized, -1*Vector3.forward);
		 if (myAgent.velocity.normalized.x < -1*Vector3.forward.x)
		 {
			 angle *= -1;
		 }
		 angle = (angle + 180.0f) % 360.0f;
		
		if(myAgent.velocity.x == 0 && myAgent.velocity.y == 0){
			animator.SetInteger("Direction",0);
		}
		else if (angle > angleNE+angleBuffer || angle <= angleNW-angleBuffer){ //North
			animator.SetInteger("Direction",1);
		}
		else if (angle > angleSW+angleBuffer && angle <= angleSE-angleBuffer){ //South
			animator.SetInteger("Direction",2);
		}
		else if (angle > angleSE+angleBuffer && angle <= angleNE-angleBuffer ){ //East
			animator.SetInteger("Direction",3);
		}
		else if (angle > angleNW+angleBuffer && angle <= angleSW-angleBuffer ){ //West
			animator.SetInteger("Direction",4);
		}
		else if (animator.GetInteger("Direction") == 0) {
			if (myAgent.velocity.x > 0){
				animator.SetInteger("Direction",3);
			}
			else {
				animator.SetInteger("Direction",4);
			}				
		}
		
		//Debug.Log("animationScript: direction: " + animator.GetInteger("Direction") +" angle: " + angle);
	}
}

