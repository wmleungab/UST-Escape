using UnityEngine;
using System.Collections;

public class TableDialogInterface : DialogInterface {
	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finished with selection result "+selection);

	}

}
