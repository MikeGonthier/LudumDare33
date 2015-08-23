using UnityEngine;
using System.Collections;

public class scr_npc : scr_character {
	public enum npcType {Kid, Employee};
	public npcType myType;
	public enum state {Sleeping, Eating, Playing, Crying};
	private state myState;

	public string npcName;
	public float speedWalking;
	public float speedRunning;
	private scr_manager manager;
	public scr_action[] schedule;

	// Use this for initialization
	void Start () {
		Agent = this.GetComponent<NavMeshAgent>();
		MyNavType = navigationType.Waypoint;
		MySpriteDirection = spriteDirection.Down;
		MoveTrigger = false;
		MyAnimator = this.GetComponentInChildren<Animator>();
		Transform objManager = GameObject.FindGameObjectWithTag("Manager").transform;
		manager = objManager.GetComponent<scr_manager>();
		WaypointManager = schedule[0].placeToGO;
	}
	
	// Update is called once per frame
	void Update () {
		if(!MoveTrigger)
		{
			WaypointID = 0;
			StartMoving();
		}
		GetDistance();
	}

	public void SetNewAction(int id)
	{
		if(schedule.Length - 1 >= id && schedule[id] != null)
		{
			WaypointManager = schedule[id].placeToGO;
			WaypointID = 0;
			StartMoving ();
		}
	}

	public npcType MyType
	{
		get {return myType;}
	}

	public string NpcName
	{
		get {return npcName;}
	}
}
