#pragma strict

public var patrolPoints : Vector3[];
private var victim : Transform;

private var navComponent : NavMeshAgent;
private var patrolIndex : int;
private var motionState : int;  // 0: patrol, 1: follow player
private var enemyInsight : boolean;
private var attackTimer : float;

private var viewThreshold: float = 1.5; // 0 = back
// This would cast rays only against colliders in layer 8
private var layerMask = 1 << 8;
private var attackMaxTime : float = 5;

function Start () {

	motionState = 0;
	patrolIndex = 0;
	enemyInsight = false;

	victim = GameObject.FindWithTag("Player").transform;
	navComponent = this.transform.GetComponent(NavMeshAgent);
	Debug.Log("in start: " + patrolIndex +" "+ patrolPoints.Length);

}

function Update () {

	if(motionState == 0) {
		patrolling();
	}
	else {
		attacking();
	}

}

function attacking() {

	if (enemyInsight) {
		if ((victim.transform.position - transform.position).sqrMagnitude > Mathf.Pow(navComponent.stoppingDistance, 2)) {
	    	navComponent.SetDestination(victim.position);
			attackTimer = 0;
			Debug.DrawLine (transform.position, victim.transform.position, Color.red);
		}
	}
	else {
		if (navComponent.remainingDistance <= navComponent.stoppingDistance) {
			attackTimer += Time.deltaTime;
			RotateTowards(victim);

			if(attackTimer >= attackMaxTime) {
				motionState = 0;
			}
		}
	}

}

function patrolling() {

	if (navComponent.remainingDistance <= navComponent.stoppingDistance) {
		if(patrolIndex >= patrolPoints.Length - 1)
			patrolIndex = 0;
		else
			patrolIndex++;
	}

	navComponent.SetDestination(patrolPoints[patrolIndex]);
}

function OnTriggerStay() {

	var forward = transform.forward;
	var toOther = victim.transform.position - transform.position;
	var angle = Vector3.Dot(forward, toOther);

	if ( angle > viewThreshold) {
		if (!Physics.Linecast (transform.position, victim.transform.position, layerMask)) {
			motionState = 1;
			enemyInsight = true;
		}
		else {
			enemyInsight = false;
		}
	}

}

 private function RotateTowards (target: Transform) {
        var direction = (target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navComponent.angularSpeed);
 }