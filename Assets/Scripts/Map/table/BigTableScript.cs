using UnityEngine;
using System.Collections;

public class BigTableScript : MonoBehaviour {

	public int bigTableNo = -1;
	public int noOfTables = 4;

	public virtual bool openTable(int tableNo) {
		if (bigTableNo>=0 && noOfTables>=0) {
			return transform.parent.GetComponent<BigTableScript>().openTable(bigTableNo*noOfTables + tableNo);
		}
		else{
			Debug.LogError("bigTableNo " + bigTableNo + " or noOfTables not correctly set");
		}
		return false;
	}
}
