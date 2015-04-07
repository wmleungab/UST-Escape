 using UnityEngine;
 using System.Collections;
 
 public class SmoothCamera2D : MonoBehaviour {
     
     public float dampTime = 0.15f;
     private Vector3 velocity = Vector3.zero;
     private Transform target;
 
	private ViewDrag viewDrag;
	private ClampCamera clampCamera;

	void Start() {
		target = GameObject.FindWithTag("Player").transform;
		
		clampCamera = (ClampCamera)GetComponent ("ClampCamera");
		
		viewDrag = (ViewDrag)GetComponent("ViewDrag");

	}

     // Update is called once per frame
     void Update () 
     {
		 //if (viewDrag == null || (viewDrag && !viewDrag.isDragging)) {
		if( !GamePause.isPause() ){
			 if (target)
			 {
				 Vector3 point = camera.WorldToViewportPoint(target.position);
				 Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
				 Vector3 destination = transform.position + delta;
				if (clampCamera!=null) destination = clampCamera.clampDes(destination);
				 transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			 }
		 }
     }
 }