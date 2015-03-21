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
	if (Input.GetKeyDown(KeyCode.T))
	{
		var temp = GameObject.Find("Victim");
		if (temp == null) Debug.Log("player not found");
		else {
			temp.name = "VictimClone";
			temp.tag = null;
			//DontDestroyOnLoad(temp);
		}
		Application.LoadLevel("lab_stage");
	}
}