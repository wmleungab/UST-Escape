using UnityEngine;
using System.Collections;

public class animationScript : MonoBehaviour {
	
	private Animator animator;
	private float lastAngle;
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
		
		if(myAgent.velocity.magnitude == 0){
			animator.SetInteger("Direction",0);
		}
		else if( Mathf.Abs(lastAngle - angle) > angleBuffer ){
			if (angle > angleNE || angle <= angleNW){ //North
				animator.SetInteger("Direction",1);
				lastAngle = angle;
			}
			else if (angle > angleSW && angle <= angleSE){ //South
				animator.SetInteger("Direction",2);
				lastAngle = angle;
			}
			else if (angle > angleSE && angle <= angleNE ){ //East
				animator.SetInteger("Direction",3);
				lastAngle = angle;
			}
			else if (angle > angleNW && angle <= angleSW ){ //West
				animator.SetInteger("Direction",4);
				lastAngle = angle;
			}
		}
		else if (animator.GetInteger("Direction") == 0) {
				if (myAgent.velocity.x > 0 || myAgent.velocity.y > 0){
					animator.SetInteger("Direction",3);
					lastAngle = angle;
				}
				else {
					animator.SetInteger("Direction",4);
					lastAngle = angle;
				}				
		}
	}
}

