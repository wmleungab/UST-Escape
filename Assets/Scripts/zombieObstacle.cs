using UnityEngine;
using System.Collections;

public class zombieObstacle : MonoBehaviour {
	
	public Transform player;
	NavMeshAgent agent;
	NavMeshObstacle obstacle;
	
	void Start () {
		agent = GetComponent< NavMeshAgent >();
		obstacle = GetComponent< NavMeshObstacle >();
	}
	
	void Update () {
		
		// Test if the distance between the agent and the player
		// is less than the attack range (or the stoppingDistance parameter)
		if ((player.position - transform.position).sqrMagnitude < Mathf.Pow(agent.stoppingDistance, 2)) {
			// If the agent is in attack range, become an obstacle and
			// disable the NavMeshAgent component
			obstacle.enabled = true;
			agent.enabled = false;
		} else {
			// If we are not in range, become an agent again
			obstacle.enabled = false;
			agent.enabled = true;
		}
	}
}