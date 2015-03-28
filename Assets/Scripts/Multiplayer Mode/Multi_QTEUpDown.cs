using UnityEngine;
using System.Collections;

public class Multi_QTEUpDown : MonoBehaviour {
	public Multi_Fields myFields;
	public GameObject counterChild;
	bool movingDown=false;
	int success=10;
	int done=0;

	private long startTime = 0;
	// Use this for initialization
	void Start () {
		myFields = GameObject.Find ("SharedData").GetComponent<Multi_Fields> ();
		long period = 10L * 60L * 1000L * 10000L; // In ticks
		long timeStamp = System.DateTime.Now.Ticks + period;
		startTime = timeStamp;
		Vector3 myV = new Vector3 (-0.7f, -3.48f, 0);
		transform.Translate (myV);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.acceleration.z < 0 && !movingDown)
						movingDown = true;
		if (Input.acceleration.z > 0 && movingDown) {
						movingDown = false;
			done++;

			if(done==success){
				if (Network.isClient) {
					
					myFields.syncState (Multi_Fields.States.CLIENT_FINISH, true);
					long period = 10L * 60L * 1000L * 10000L; // In ticks
					long timeStamp = System.DateTime.Now.Ticks + period;
					timeStamp -= startTime;
					myFields.syncTime (timeStamp, false);
					Destroy (gameObject);
					
				} else {
					myFields.syncState (Multi_Fields.States.SERVER_FINISH, true);
					long period = 10L * 60L * 1000L * 10000L; // In ticks
					long timeStamp = System.DateTime.Now.Ticks + period;
					timeStamp -= startTime;
					myFields.syncTime (timeStamp, true);
					Destroy (gameObject);
				}
			}
		}
		TextMesh tm=counterChild.GetComponent<TextMesh> ();
		tm.text="x"+(success-done);
	}


}
