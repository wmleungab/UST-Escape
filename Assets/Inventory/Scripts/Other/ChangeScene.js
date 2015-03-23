#pragma strict

@script AddComponentMenu ("Inventory/Other/ChangeScene")

function Awake () 
{
		var temp = GameObject.Find("Victim");
		if (temp == null) Debug.Log("player not found");
		DontDestroyOnLoad(temp);
		
		if (FindObjectsOfType(GetType()).Length > 1)
         {
             Destroy(gameObject);
         }
}

function Update ()
{
	if (Input.GetKeyDown(KeyCode.T))
	{

		Application.LoadLevel("battle");
	}
}