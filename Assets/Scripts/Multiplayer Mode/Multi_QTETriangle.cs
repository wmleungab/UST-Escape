using UnityEngine;
using System.Collections;

public class Multi_QTETriangle : MonoBehaviour {

	public Multi_Fields myFields;
	private int success = 0;
	private int num_draw = 2;
	public GameObject ptChild;

	public GameObject baseTriangle;
	Vector3 orgPoint;
	Vector3 touching;
	
	double r;
	bool invalidPt_Pos=false;
	bool arriveCP1=false;
	bool arriveCP2=false;
	private long startTime = 0;


	bool collisionWithEdges ()
	{
		Vector3 minP = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 1));
		Vector3 maxP = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 1));
		
		Vector3 temp = baseTriangle.renderer.bounds.size;
		double sizeX = temp.x/2+0.5;
		double sizeY = temp.y/2+0.5;
		Vector3 point = transform.position;
		


				/*Debug.Log ("parent y: " + transform.position.y);
				Debug.Log ("size y: " + sizeY);
				Debug.Log ("pointy: " + (point.y));
		Debug.Log ("pointy+sizeY: " + (point.y + sizeY));
		Debug.Log ("pointy-sizeY: " + (point.y - sizeY));

				Debug.Log ("maxP y: " + maxP.y);
				Debug.Log ("minP y: " + minP.y);*/
		if ((point.x + sizeX >= maxP.x)
		    || (point.y + sizeY >= maxP.y)
		    || (point.x - sizeX <= minP.x)
		    || (point.y - sizeY <= minP.y)
		    || (point.y - sizeX <= minP.y)
		    || (point.y + sizeX >= maxP.y)) {
			
			//Debug.Log("Collision!");
			return true;
		} else {
			//Debug.Log("NO Collision!");
			return false;
		}
	}
	// Use this for initialization
	void Start () {
		myFields = GameObject.Find ("SharedData").GetComponent<Multi_Fields> ();


		CircleCollider2D temp = ptChild.GetComponents<CircleCollider2D> () [0]as CircleCollider2D;
		r = temp.radius;

		
		long period = 10L * 60L * 1000L * 10000L; // In ticks
		long timeStamp = System.DateTime.Now.Ticks + period;
		startTime = timeStamp;
		
		drawTriangle ();
	}
	void drawTriangle (){
		gameObject.SetActive (false);
		if(orgPoint!=Vector3.zero)ptChild.transform.position = orgPoint;

		//point = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width , Screen.height , 1));
		do {
			float x = Random.Range (0, Screen.width);
			float y = Random.Range (0, Screen.height);
			transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (x, y, 1));
			
		} while (collisionWithEdges());
		int angle = Random.Range (0, 359);
		transform.RotateAround (transform.position, Vector3.forward, angle);
		orgPoint = ptChild.transform.position;

		gameObject.SetActive (true);

	
	}

	public void baseTriggerStay(Collider2D col){


	}
	public void baseTriggerExit(Collider2D col){
		if (col.gameObject == ptChild) {
			invalidPt_Pos=true;
			Debug.Log(invalidPt_Pos);
		}

	}
	public void checkptTriggerEnter(Collider2D col,string name){
		if (name == "checkPt1"&& col.gameObject == ptChild) {
						Debug.Log ("ckpt1");
						if(!invalidPt_Pos&&!arriveCP2){
							arriveCP1=true;
							//Debug.Log("invalidPt_Pos: "+invalidPt_Pos);
						}
						else if(!invalidPt_Pos&&arriveCP2)invalidPt_Pos=true;
				}
		if (name == "checkPt2"&&col.gameObject == ptChild) {
						Debug.Log ("ckpt2");
						if(!invalidPt_Pos&&arriveCP1)arriveCP2=true;
						else if(!invalidPt_Pos&&!arriveCP1)invalidPt_Pos=true;
				}
		}



	bool insideCircle (Vector3 temp)
	{
		if ((temp.x >= ptChild.transform.position.x - r
		     && temp.x <= ptChild.transform.position.x + r)
		    && (temp.y >= ptChild.transform.position.y - r
		    && temp.y <= ptChild.transform.position.y + r))
			return true;
		else
			return false;
	}
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			if (Input.GetTouch (0).phase == TouchPhase.Began || Input.GetTouch (0).phase == TouchPhase.Moved) {									//begin
				touching = Input.GetTouch (0).position;
				
				touching = Camera.main.ScreenToWorldPoint (new Vector3 (touching.x, touching.y, 1));

				
				if (insideCircle (touching)) {
					if (invalidPt_Pos) {
						ptChild.transform.position = orgPoint;
						invalidPt_Pos = false;
					} else
						ptChild.transform.position = touching;
				} 
			}
		}
		Debug.Log("arriveCP1"+arriveCP1);
		Debug.Log("arriveCP2"+arriveCP2);
		Debug.Log("invalidPt_Pos"+invalidPt_Pos);
		Debug.Log("insideCircle (orgPoint)"+insideCircle (orgPoint));

		if (arriveCP1&&arriveCP2&&!invalidPt_Pos&& insideCircle (orgPoint)) {
			success++;
			Debug.Log("here now");
			if (success >= num_draw) {
				/*if (Network.isClient) {
					
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
				}*/
			} else{
				arriveCP1=false;
				arriveCP2=false;
				invalidPt_Pos=false;
				drawTriangle();
				Debug.Log("here now2");

			}
		}
	}
}
