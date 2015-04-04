#pragma strict

@script AddComponentMenu ("Inventory/Other/ChangeScene")

function Awake () 
{
	//var temp : Transform;
	//temp = GameObject.Find(PlayerName).transform;
	//DontDestroyOnLoad (transform.root.temp);
}

function Update ()
{
	if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Menu))
	{
		var temp = GameObject.Find("Victim");
		if (temp == null) Debug.Log("player not found");
		else {
			//temp.name = "VictimClone";
			//temp.tag = null;
			var navComponent = (temp.transform.GetComponent ("NavMeshAgent") as NavMeshAgent);
			navComponent.enabled = true;
			DontDestroyOnLoad(temp);
		}

		GameObject.Find("Inventory").SendMessage ("toMapMode");

		Application.LoadLevel("lab_stage");
	}
}