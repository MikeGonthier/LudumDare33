using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scr_manager : MonoBehaviour {
	public enum dayEvents {Play1, Nap1, Lunch, Outside, Nap2, Play2, End};
	private dayEvents dayEvent;
	private int eventID;
	private Dictionary<string, scr_npc> childrenList = new Dictionary<string, scr_npc>();
	private Dictionary<string, scr_npc> employeeList = new Dictionary<string, scr_npc>();

	private float timeCounter;
	public float timeLimit;
	private float timeStart;
	private bool triggerPause;
	public float[] timeSteps;
	private int timeStepId;

	// Use this for initialization
	void Start () {
		dayEvent = dayEvents.Play1;
		GameObject[] npcList = GameObject.FindGameObjectsWithTag("NPC");
		eventID = 0;
		foreach(GameObject npc in npcList)
		{
			scr_npc character = npc.transform.GetComponent<scr_npc>();
			if(character.MyType == scr_npc.npcType.Employee)
			{
				employeeList.Add(character.npcName, character);
			}
			if(character.MyType == scr_npc.npcType.Kid)
			{
				childrenList.Add(character.NpcName, character);
			}
		}
		timeCounter = 0;
		triggerPause = false;
		timeStepId = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Managetime();
	}

	private void Managetime()
	{
		if(!triggerPause)
		{
			timeCounter += Time.deltaTime;
			if(timeCounter >= timeLimit)
			{
				triggerPause = true;
				foreach(KeyValuePair<string, scr_npc> child in childrenList)
				{
					child.Value.Stop();
				}
				foreach(KeyValuePair<string, scr_npc> employee in employeeList)
				{
					employee.Value.Stop();
				}
			}
			else
			{
				if(timeCounter > timeSteps[timeStepId])
				{	
					eventID++;
					foreach(KeyValuePair<string, scr_npc> child in childrenList)
					{
						child.Value.SetNewAction(eventID);
					}
					foreach(KeyValuePair<string, scr_npc> employee in employeeList)
					{
						employee.Value.SetNewAction(eventID);
					}
				}
			}
		}
	}

	public void UseItem(scr_item a, scr_item b)
	{
		switch (a.myItemType)
		{
		case scr_item.itemType.EmptyBucket:
			if(b.myItemType == scr_item.itemType.Glue)
			{
				a.myItemType = scr_item.itemType.StickyBucket;
				b.SetDestroy();
			}
			if(b.myItemType == scr_item.itemType.Toilet)
			{
				a.myItemType = scr_item.itemType.WaterBucket;
			}
			break;
		case scr_item.itemType.Glue:
			if(b.myItemType == scr_item.itemType.EmptyBucket)
			{
				a.myItemType = scr_item.itemType.StickyBucket;
				b.SetDestroy();
			}
			if(b.myItemType == scr_item.itemType.Hair)
			{
				a.myItemType = scr_item.itemType.StickyHair;
				b.SetDestroy();
			}
			break;
		case scr_item.itemType.Hair:
			if(b.myItemType == scr_item.itemType.Glue)
			{
				a.myItemType = scr_item.itemType.StickyHair;
				b.SetDestroy();
			}
			if(b.myItemType == scr_item.itemType.EmptyBucket)
			{
				a.myItemType = scr_item.itemType.FuryBucket;
				b.SetDestroy();
			}
			break;

		default:
			Debug.Log ("CANNOT COMBINE");
			break;
		}
	}

	public dayEvents DayEvent
	{
		get {return dayEvent;}
	}
}
