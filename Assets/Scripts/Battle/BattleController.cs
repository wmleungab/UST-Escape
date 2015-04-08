using UnityEngine;
using System.Collections;

public enum BattleState{BATTLE_STARTING,
				BATTLE_PROGRESSING,
	BATTLE_ENDING_WIN,
	BATTLE_ENDING_LOST,
	BATTLE_ENDING
};

public class BattleController : MonoBehaviour {
	static public BattleState currentBattleState;
	public GameObject EnemySpace;
	public GameObject BackgroundSpace;
	public GameObject PlayerSpace;

	public GameObject LabCorridor;
	public GameObject Atrium;
	public GameObject LG2;
	public GameObject Turkey;

	public GameObject Fryer;
	public GameObject Cleaner;
	public GameObject Void;
	public GameObject Dembeater1;
	public GameObject Dembeater2;
	public GameObject Mickey;
	public GameObject DimJack;
	
	public GameObject []BGmusic;
	
	Vector3 []enemiesPos;


	// Use this for initialization
	void Start () {
		if(GlobalValues.BattleData.isFinalStage)
			BGmusic[0].audio.Play();
		else
			BGmusic[1].audio.Play();
		setBackground ();
		setPosition ();
		createMonster ();
		currentBattleState =BattleState.BATTLE_STARTING;
	}
	void 		setBackground (){

		GameObject o=null;
		switch (GlobalValues.BattleData.battleBackgroundID) {
		case 1: o=LabCorridor; break;			
		case 2: o=Atrium; break;		
		case 3: o=LG2; break;		
		case 4: o=Turkey; break;		
				}
		if (o != null) {
						GameObject s = Instantiate (o, BackgroundSpace.transform.position, Quaternion.identity)as GameObject;
						s.transform.parent = BackgroundSpace.transform;
				}
	}
	void setPosition(){
		
		switch (GlobalValues.BattleData.numOfMonsters) {
		case 1: enemiesPos=new Vector3[1]{new Vector3(0,0,0)}; 										break;
		case 2: enemiesPos=new Vector3[2]{new Vector3(-3f,0,0),new Vector3(3f,0,0)};					 break;
		case 3: enemiesPos=new Vector3[3]{new Vector3(-5f,0,0),new Vector3(0,0,0),new Vector3(5f,0,0)} ;break;


		}
	}

	void createMonster(){
		for (int i=0;i<GlobalValues.BattleData.numOfMonsters;i++) {
			int id=GlobalValues.BattleData.monsterID[i];

			GameObject o=null;
			Vector3 offset=Vector3.zero;
			switch (id) {
			case 1: o=Fryer; offset=new Vector3(0,-1,0); break;
			case 2: o=Cleaner;offset=new Vector3(0,-1,0); break;
			case 3: o=Void;offset=new Vector3(0,0,0); break;
			case 4: o=Dembeater1;offset=new Vector3(0,-0.3f,0); break;
			case 5: o=Dembeater2;offset=new Vector3(0,-0.1f,0); break;
			case 6: o=Mickey;offset=new Vector3(0,-0.5f,0); break;
			case 7: o=DimJack;offset=new Vector3(0,-0.8f,0); break;

			}	
			if(o!=null){
				GameObject s = Instantiate (o, EnemySpace.transform.position+enemiesPos[i]+offset,Quaternion.identity)as GameObject;
				s.transform.parent=EnemySpace.transform;
			}
			
		}

	}

	void Update(){
		if (currentBattleState == BattleState.BATTLE_PROGRESSING) {
			if(EnemySpace.transform.childCount==0)
				currentBattleState=BattleState.BATTLE_ENDING_WIN;
			if(PlayerSpace.GetComponent<HealthBar>().HP==0)
				currentBattleState=BattleState.BATTLE_ENDING_LOST;
		}
	}

}
