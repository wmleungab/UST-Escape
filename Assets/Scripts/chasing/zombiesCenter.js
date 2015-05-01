#pragma strict
 
 function Start() {
 }
  
 function Update ()  {
	 Debug.Log("center running ");
     var gos = GameObject.FindGameObjectsWithTag("Enemy");
 	 //Debug.Log(gos.Length);
    this.transform.position = FindCenterPoint(gos);
 }
 
 function FindCenterPoint(zombiesInrange : GameObject[]) : Vector3 {
	var center : Vector3 = new Vector3(0, 0, 0);
	 var count : float = 0;
	 //Debug.Log(zombiesInrange.Length);
	 for (var zombieInrange : GameObject in zombiesInrange){
		center += zombieInrange.transform.position;
		count++;
	 }
	 var theCenter = center / count;
	 
	 return theCenter;
	 
 }