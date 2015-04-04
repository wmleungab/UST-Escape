using UnityEngine;
using System.Collections;

public class LockerScript : MonoBehaviour
{

		public GameObject password_Text;
		public GameObject number1;
		public GameObject number2;
		public GameObject number3;
		public GameObject number4;
		public GameObject number5;
		public GameObject number6;
		public GameObject number7;
		public GameObject number8;
		public GameObject number9;
		public GameObject number0;
		public GameObject btn_OK;
		public GameObject btn_Cancel;
		public int Password = 0;
		int myNum;
		TextMesh textMest;
		bool num1Entered;
		bool num2Entered;
		bool num3Entered;
		bool num4Entered;

		// Use this for initialization
		void Start ()
		{
				myNum = 0;
				num1Entered = false;
				num2Entered = false;
				num3Entered = false;
				num4Entered = false;

				textMest = password_Text.GetComponent<TextMesh> ();
				textMest.text = "----";
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void onChildrenTouched (string name)
		{
				if (name == "cal_0") {
						addNo (0);
				} else if (name == "cal_1") {
						addNo (1);
				} else if (name == "cal_2") {
						addNo (2);
				} else if (name == "cal_3") {
						addNo (3);
				} else if (name == "cal_4") {
						addNo (4);
				} else if (name == "cal_5") {
						addNo (5);
				} else if (name == "cal_6") {
						addNo (6);
				} else if (name == "cal_7") {
						addNo (7);
				} else if (name == "cal_8") {
						addNo (8);
				} else if (name == "cal_9") {
						addNo (9);
				} else if (name == "cal_OK") { 
						if (checkCorrect ()) {
								//pw matched do sth
								transform.parent.gameObject.SendMessage("openLocker");
						}
						closePanel();
				} else if (name == "cal_c") {
						resetPw ();
				}

		}

		void addNo (int x)
		{

				if (!num1Entered) {
						myNum = x;
						textMest.text = x + "---";
						num1Entered = true;
				} else if (!num2Entered) {
						myNum = myNum * 10 + x;
						textMest.text = myNum + "--";
						num2Entered = true;
				} else if (!num3Entered) {
						myNum = myNum * 10 + x;
						textMest.text = myNum + "-";
						num3Entered = true;
				} else if (!num4Entered) {
						myNum = myNum * 10 + x;
						textMest.text = myNum + "";
						num4Entered = true;
				}


		}

		void resetPw ()
		{
				textMest.text = "----";
				myNum = 0;
				num1Entered = false;
				num2Entered = false;
				num3Entered = false;
				num4Entered = false;
		}

		bool checkCorrect ()
		{
				if (num1Entered && num2Entered && num3Entered && num4Entered) {
						if (myNum == Password)
								return true;
						else {
								resetPw ();
								return false;
						}
								
				} else {
						resetPw ();
						return false;
				}
		}
		
		public void showPanel() {
			GlobalVal.GamePause = true;
			moveToCamera();
			gameObject.SetActiveRecursively(true);
		}
		public void closePanel() {
			GlobalVal.GamePause = false;
			gameObject.SetActiveRecursively(false);
		}
		void moveToCamera () {
			Vector3 currentpos = transform.position;
			Vector3 camerapos = Camera.main.transform.position;
			currentpos.x = camerapos.x;
			currentpos.z = camerapos.z;
			
			transform.position = currentpos;
		}
}
