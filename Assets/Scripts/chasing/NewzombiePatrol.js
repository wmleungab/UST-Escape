#pragma strict

public var patrolPoints : Vector3[];
private var predictPlayer : boolean = true;
private var surroundPlayer : boolean = true;
private var surroundRadius : float = 0.5f;
private var cDistReactVal : float = 5.0f;
private var distCoef : float = 0.2f;
private var maxDistVal : float = 2.0f;
private var distReactVal : float = 10.0f;
public var marker : Transform;
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

private var isRotating : boolean = false;

function Start () {

	motionState = 0;
	patrolIndex = 0;
	enemyInsight = false;

	victim = GameObject.FindWithTag("Player").transform;
	navComponent = this.transform.GetComponent(NavMeshAgent);
	marker = GameObject.Find("marker").transform;
	Debug.Log("in start: " + patrolIndex +" "+ patrolPoints.Length);

}

function Update () {

	if (navComponent.enabled == true) {
		if(motionState == 0) {
			patrolling();
		}
		else {
			attacking();
		}
	}

}

function attacking() {

	if (enemyInsight) {
		var distance = (victim.transform.position - transform.position).sqrMagnitude;
		if (distance > Mathf.Pow(navComponent.stoppingDistance, 2)) {
			var playerPos = victim.position;
			if (predictPlayer) playerPos += victim.transform.GetComponent(NavMeshAgent).velocity;
			if(surroundPlayer){
				var fromCenter = (transform.position - marker.position);
				var offVector = fromCenter.normalized;
				Debug.DrawLine (transform.position, marker.position, Color.blue);
				var cDistance = fromCenter.sqrMagnitude;
				if(cDistance<cDistReactVal)offVector *= 1.0f/cDistance * surroundRadius;
				//else offVector = Vector3.zero;
				if(distance<distReactVal)offVector *= Mathf.Max((distance) * distCoef, maxDistVal);
				playerPos += offVector;
			}
	    	navComponent.SetDestination(playerPos);
			attackTimer = 0;
			transform.parent.SendMessage("sendAlert");
			//Debug.Log(navComponent.velocity);
			Debug.DrawLine (transform.position, playerPos, Color.red);
		}
		else{
			transform.parent.SendMessage("gotoBattle");
			Destroy(transform.parent.gameObject);
		}
	}
	else {
		if (navComponent.remainingDistance <= navComponent.stoppingDistance) {
			attackTimer += Time.deltaTime;
			StartRotateAround();

			if(attackTimer >= attackMaxTime) {
				StopRotateAround();
				motionState = 0;
			}
		}
	}

}

function patrolling() {

	//Debug.Log(this.transform.parent.name);
	if (navComponent.remainingDistance <= navComponent.stoppingDistance) {
		if(patrolIndex >= patrolPoints.Length - 1)
			patrolIndex = 0;
		else
			patrolIndex++;
	}

	navComponent.SetDestination(patrolPoints[patrolIndex]);
}

function OnTriggerStay(other:Collider) {

	if(other.gameObject.tag=="Player"){
		//Debug.Log("meet Player");
		var forward = transform.forward;
		var toOther = victim.transform.position - transform.position;
		var angle = Vector3.Dot(forward, toOther);

		if ( angle > viewThreshold) {
			if (!Physics.Linecast (transform.position, victim.transform.position, layerMask)) {
				//Debug.Log("see Player");
				detectPlayer();
				transform.parent.SendMessage("sendAlert");
			}
			else {
				lostPlayer();
			}
		}
	}

	if(other.gameObject.tag=="Item"){
		Debug.Log(other.name);
		switch (other.name){
			case "mace":
				Destroy(transform.parent.gameObject);    
				Destroy(other.gameObject);
				break;
		}
	}

}

function detectPlayer(){
	motionState = 1;
	enemyInsight = true;
}

function lostPlayer(){
	enemyInsight = false;
}

 private function RotateTowards (target: Transform) {
        var direction = (target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navComponent.angularSpeed);
 }

 private function StartRotateAround(){
	 if(!isRotating){
		 isRotating = true;
		 RotateAround();
	 }
 }
 private function StopRotateAround(){
	 isRotating = false;
 }
 private function RotateAround(){
	 Debug.Log("Finding Player");
	 var duration = 2.0f;
	 var rotA = Quaternion.LookRotation(-transform.right);
	 var rotB = Quaternion.LookRotation(transform.right);
	 var startTime = Time.time;
	 for (var t = 0f; t < .75; t = (Time.time - startTime) / duration ) {
		transform.rotation = Quaternion.Slerp(transform.rotation, rotA, t);
		yield;
	 }
	 
	 duration = 3.0f;
	 startTime = Time.time;
	 for (var t1 = 0f; t1 < .75; t1 = (Time.time - startTime) / duration ) {
		transform.rotation = Quaternion.Slerp(transform.rotation, rotB, t1);
		yield;
	 }	 
 }