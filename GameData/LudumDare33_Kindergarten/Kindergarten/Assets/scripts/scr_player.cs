using UnityEngine;
using System.Collections;

public class scr_player : scr_character {	 
	public float distanceInteraction;
	public float distanceInteractionNPC;
	public float distanceToDrop;
	private scr_item grabbedItem;
	private bool holdItem;
	private bool triggerRightClick;
	private scr_manager manager;

	// Use this for initialization
	void Start () 
	{
		Agent = this.GetComponent<NavMeshAgent>();
		MyNavType = navigationType.Objective;
		MySpriteDirection = spriteDirection.Down;
		MyAnimator = this.GetComponentInChildren<Animator>();
		triggerRightClick = true;
		holdItem = false;
		Transform objManager = GameObject.FindGameObjectWithTag("Manager").transform;
		manager = objManager.GetComponent<scr_manager>();

	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(MoveTrigger) SetDirection();
		if(Input.GetMouseButtonDown(0))
		{
			Ray rayLeft = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit[] hitsLeft;
			hitsLeft = Physics.RaycastAll(rayLeft, 300f);
			if(hitsLeft.Length > 0)
			{
				foreach(RaycastHit hit in hitsLeft)
				{
					float hitDistance = Vector3.Distance(hit.point, this.transform.position);
					if(hitDistance > distanceInteraction)
					{
						StartMoving(hit);
					}
				}
			}
			PreviousPosition = this.transform.position;
		}
		if(Input.GetMouseButtonDown(1))
		{
			Ray rayRight = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit[] hitsRight;
			hitsRight = Physics.RaycastAll(rayRight, 300f);
			if(triggerRightClick && hitsRight.Length > 0)
			{
				foreach(RaycastHit hit in hitsRight)
				{
					if(triggerRightClick)
					{
						string hitTag = hit.transform.tag;
						float hitDistance = Vector3.Distance(hit.point, this.transform.position);
						switch(hitTag)
						{
						case "NPC":
							if(hitDistance < distanceInteractionNPC)
							{
								if(holdItem)
								{	
									Debug.Log ("I can use item on this NPC");
									//manager.UseItem();
								}
								triggerRightClick = false;
							}
							triggerRightClick = false;
							break;
						case "Item":
							if(hitDistance < distanceInteraction)
							{
								if(!holdItem && grabbedItem == null)
								{	
									holdItem = true;
									grabbedItem = hit.transform.GetComponentInChildren<scr_item>();
									grabbedItem.ItemGrabbed(this.transform);
								}
								else
								{	
									scr_item targetItem = hit.transform.GetComponent<scr_item>();
									manager.UseItem(grabbedItem, targetItem);
								}
								triggerRightClick = false;
							}
							break;
						case "Ground":
							if(grabbedItem && hitDistance < distanceToDrop)
							{
								grabbedItem.ItemDropped(hit.point);
								grabbedItem = null;
								holdItem = false;
							}
							triggerRightClick = false;
							break;
						default:
							break;
						}
					}
				}
				triggerRightClick = true;
			}
		}
		MyAnimator.SetFloat ("speed", Agent.velocity.magnitude);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Item")
		{
			//make name appear

		}
		if(other.transform.tag == "NPC")
		{
			//make name && meter appear
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.transform.tag == "Item")
		{
			//make name appear
		}
		if(other.transform.tag == "NPC")
		{
			//make name && meter appear
		}
	}

	public scr_item GrabbedItem
	{
		get {return grabbedItem;}
		set {grabbedItem = value;}
	}
}
