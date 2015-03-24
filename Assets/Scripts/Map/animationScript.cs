using UnityEngine;
using System.Collections;

public class animationScript : MonoBehaviour {
	
	private Animator animator;
	private GameObject player;
	public NavMeshAgent myAgent;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
		float angle = Vector3.Angle(myAgent.velocity.normalized, -1*player.transform.forward);
		 if (myAgent.velocity.normalized.x < -1*player.transform.forward.x)
		 {
			 angle *= -1;
		 }
		 angle = (angle + 180.0f) % 360.0f;
		
		if(myAgent.velocity.x == 0 && myAgent.velocity.y == 0){
			animator.SetInteger("Direction",0);
		}
		else if (angle > 315.0f || angle <= 45.0f){ //North
			animator.SetInteger("Direction",1);
		}
		else if (angle > 135.0f && angle <= 225.0f){ //South
			animator.SetInteger("Direction",2);
		}
		else if (angle > 225.0f && angle <= 315.0f){ //East
			animator.SetInteger("Direction",3);
		}
		else if (angle > 45.0f && angle <= 135.0f){ //West
			animator.SetInteger("Direction",4);
		}
		
		//Debug.Log("animationScript: direction: " + animator.GetInteger("Direction") +" angle: " + angle);
	}
}

