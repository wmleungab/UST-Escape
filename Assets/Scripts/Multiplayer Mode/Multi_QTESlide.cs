using UnityEngine;
using System.Collections;

public class Multi_QTESlide : MonoBehaviour
{
		public Multi_Fields myFields;
		private int success = 0;
		private int num_Slide = 3;
		public GameObject ptChild1;
		public GameObject ptChild2;
		public GameObject slot;
		Vector3 orgPoint1;
		Vector3 orgPoint2;
		Vector3 touching;
		int angle=0;
		double r1;
		double r2;
		bool invalidPt1_Pos=false;
		bool invalidPt2_Pos=false;

		private long startTime = 0;

		void Start ()
		{
		
		
				myFields = GameObject.Find ("SharedData").GetComponent<Multi_Fields> ();
		
				touching = Vector3.zero;


				CircleCollider2D temp1 = ptChild1.GetComponents<CircleCollider2D> () [0]as CircleCollider2D;
				r1 = temp1.radius;
				CircleCollider2D temp2 = ptChild2.GetComponents<CircleCollider2D> () [0]as CircleCollider2D;
				r2 = temp2.radius;

				long period = 10L * 60L * 1000L * 10000L; // In ticks
				long timeStamp = System.DateTime.Now.Ticks + period;
				startTime = timeStamp;
				
				drawSlideLocus ();
		
		}

		bool collisionWithEdges ()
		{
				Vector3 minP = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 1));
				Vector3 maxP = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 1));
		
				Vector3 temp = slot.renderer.bounds.size;
				double sizeX = temp.x/2;
				double sizeY = temp.y/2;
				Vector3 point = transform.position;
	

				if ((point.x + sizeX >= maxP.x)
						|| (point.y + sizeY >= maxP.y)
						|| (point.x - sizeX <= minP.x)
						|| (point.y - sizeY <= minP.y)
						|| (point.y - sizeX <= minP.y)
						|| (point.y + sizeX >= maxP.y)) {
			

						return true;
				} else {
			
						
						return false;
				}
		}

		void drawSlideLocus ()
		{

		gameObject.SetActive (false);
		if (orgPoint1 != Vector3.zero && orgPoint2 != Vector3.zero ) {
						ptChild1.transform.position = orgPoint1;
						ptChild2.transform.position = orgPoint2;
				}
		transform.RotateAround (transform.position, Vector3.forward, -angle);
		
				//point = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width , Screen.height , 1));
				do {
						float x = Random.Range (0, Screen.width);
						float y = Random.Range (0, Screen.height);
						transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (x, y, 1));
			
				} while (collisionWithEdges());
				angle = Random.Range (0, 359);
				transform.RotateAround (transform.position, Vector3.forward, angle);
		orgPoint1 = ptChild1.transform.position;
		orgPoint2 = ptChild2.transform.position;

				gameObject.SetActive (true);

	
		}

		public void slotTriggerEnter (Collider2D col)
		{

		}
	
		public void slotTriggerStay (Collider2D col)
		{
			/*	if (col.gameObject == mouseCurrent && success < num_Swipe) {
						if (started && !failed) {
								moving = true;
								Debug.Log ("moving on tracking");
						}
				}*/
		}
	
		public void slotTriggerExit (Collider2D col)
		{

				if (col.gameObject == ptChild1) {
			invalidPt1_Pos=true;

				}
		else if (col.gameObject == ptChild2) {
			invalidPt2_Pos=true;
		
		}
		}

		public void PtTriggerEnter (Collider2D col)
		{
		/*
			if (col.gameObject == ptChild1) {

			success++;
			Debug.Log("success: "+success);
			if (success >= num_Slide) {
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
			else drawSlideLocus();
		}*/
	}
	
	public void PtTriggerStay (Collider2D col)
		{

		}
	
		public void PtTriggerExit (Collider2D col)
		{
		}

		bool insideCircle1 (Vector3 temp)
		{
				if ((temp.x >= ptChild1.transform.position.x - r1
						&& temp.x <= ptChild1.transform.position.x + r1)
						&& (temp.y >= ptChild1.transform.position.y - r1
						&& temp.y <= ptChild1.transform.position.y + r1))
						return true;
				else
						return false;
		}

		bool insideCircle2 (Vector3 temp)
		{
				if ((temp.x >= ptChild2.transform.position.x - r2
						&& temp.x <= ptChild2.transform.position.x + r2)
						&& (temp.y >= ptChild2.transform.position.y - r2
						&& temp.y <= ptChild2.transform.position.y + r2))
						return true;
				else
						return false;
		}


		// Update is called once per frame
		void Update ()
		{

				if (Input.touchCount == 2) {
						if (Input.GetTouch (0).phase == TouchPhase.Began || Input.GetTouch (0).phase == TouchPhase.Moved) {									//begin
								touching = Input.GetTouch (0).position;
						
								touching = Camera.main.ScreenToWorldPoint (new Vector3 (touching.x, touching.y, 1));
						
			
				
								if (insideCircle1 (touching)) {
										if (invalidPt1_Pos) {
												ptChild1.transform.position = orgPoint1;
												invalidPt1_Pos = false;
										} else
												ptChild1.transform.position = touching;

								} else if (insideCircle2 (touching)) {
										if (invalidPt2_Pos) {
												ptChild2.transform.position = orgPoint2;
												invalidPt2_Pos = false;
										} else
												ptChild2.transform.position = touching;
								}
			

						}
						if (Input.GetTouch (1).phase == TouchPhase.Began || Input.GetTouch (1).phase == TouchPhase.Moved) {
																
								touching = Input.GetTouch (1).position;
		
								touching = Camera.main.ScreenToWorldPoint (new Vector3 (touching.x, touching.y, 1));

					
								if (insideCircle1 (touching)) {
										if (invalidPt1_Pos) {
												ptChild1.transform.position = orgPoint1;
												invalidPt1_Pos = false;
										} else
												ptChild1.transform.position = touching;
								} else if (insideCircle2 (touching)) {
										if (invalidPt2_Pos) {
												ptChild2.transform.position = orgPoint2;
												invalidPt2_Pos = false;
										} else
												ptChild2.transform.position = touching;
								}
								
		

						}
					
				}

				if (Vector3.Distance (ptChild1.transform.position, ptChild2.transform.position) < 1.5 * (r1 + r2)) {
						success++;
						if (success >= num_Slide) {
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
						} else
								drawSlideLocus ();
				}
		}
	

}