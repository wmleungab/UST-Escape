using UnityEngine;
using System.Collections;

public class Multi_Fields : MonoBehaviour
{

		public enum States
		{
				CONNECTION_ESTABLISHED=0,
				SERVER_READY_TO_START,
				CLIENT_READY_TO_START,
				ROUND_STARTS,
				SERVER_READY_TO_START_ROUND,
				CLIENT_READY_TO_START_ROUND,
				ROUND_IN_PROGRESS,
				ROUND_END,
				ATTACK_ROUND,
				DEFENSE_ROUND,
				SERVER_FINISH,
				CLIENT_FINISH,
				SERVER_SUCCESS
		}
		public int QTEmode = 0;
		public string ServerFinishTime;
		public string ClientFinishTime;
		public string internServerFinishTime;
		public string internClientFinishTime;
		public bool[] stateInfo; // declare numbers as an int array of any size

		// Use this for initialization
		void Start ()
		{
				stateInfo = new bool[32];
				for (int i=0; i<32; i++)
						stateInfo [i] = false;
				internServerFinishTime = "0";
				internClientFinishTime = "0";

		}

		public void changeState (States index, bool value)
		{
				int myIndex = (int)index;
				stateInfo [myIndex] = value;
		}

		public void syncState (States index, bool value)
		{
				int myIndex = (int)index;
				networkView.RPC ("mySyncState", 
	                      RPCMode.AllBuffered, 
		                 myIndex, value);
		}

		[RPC]
		void mySyncState (int index, bool value)
		{

				stateInfo [index] = value;
		}
		
		public void syncTime (long time, bool isServer)
		{

				networkView.RPC ("mySyncTime", 
		                 RPCMode.AllBuffered, 
		                 time.ToString (), isServer);
		}

		[RPC]
		void mySyncTime (string time, bool isServer)
		{
				if (isServer) {

						ServerFinishTime = time;
						internServerFinishTime = time;

				} else {
						internClientFinishTime = time;
						ClientFinishTime = time;
				}
		}
		
		public void syncQTEMode (int mode)
		{
		
		networkView.RPC ("mySyncQTEMode", 
		                 RPCMode.AllBuffered, 
		                 mode);
		}

		[RPC]	
		void mySyncQTEMode (int mode)
		{
				QTEmode = mode;
		}
}
