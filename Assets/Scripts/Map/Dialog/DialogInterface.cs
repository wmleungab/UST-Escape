using UnityEngine;
using System.Collections;

public class DialogInterface : MonoBehaviour {

	DialogSystem dsObj;

	// Use this for initialization
	void Start () {
		dsObj = GameObject.Find("DialogSystem").GetComponent<DialogSystem>();
	}
	
	void getItemDialog(){
		string temp = "Item " + this.name + "X1 get";
		string[] nameString = new string[]{"System"};
		string[] dialogString = new string[]{temp};
		dsObj.startDialog(nameString, dialogString);
	}
	void dropItemDialog(){
		string temp = "Item " + this.name + "X1 dropped";
		string[] nameString = new string[]{"System"};
		string[] dialogString = new string[]{temp};
		dsObj.startDialog(nameString, dialogString);
	}
}
