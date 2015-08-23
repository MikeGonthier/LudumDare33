using UnityEngine;
using System.Collections;

public class scr_item : MonoBehaviour {
	public enum itemType {Scissors, 
							Water, 
							Crayons, 
							Pills, 
							Hair, 
							EmptyBucket, 
							StickyHair,  
							Glue, 
							StickyBucket,
							TV,
							Chair,
							Floor,
							Wall,
							Toilet,
							DinerFood,
							SliperyFloor,
							StickyChair,
							WaterBucket,
							FuryBucket};
	public itemType myItemType;
	private bool grabbed;
	private Transform owner;
	private scr_player ownerScript;
	private NavMeshObstacle obstacle;
	private float originalY;
	public scr_emptySpaceChecker emptySpaceChecker;

	// Use this for initialization
	void Start () {
		grabbed = false;
		obstacle = GetComponentInChildren<NavMeshObstacle>();
		originalY = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(grabbed)
		{
			if(!ownerScript)
			{
				ItemDropped(this.transform.position);
			}
			Vector3 newPosition = new Vector3( owner.transform.position.x,
			                                  owner.transform.position.y + 0.1f, //Layer
			                                  owner.transform.position.z + 0.5f); 
			this.transform.position = newPosition;
		}
	}

	public void ItemGrabbed(Transform newOwner)
	{
		grabbed = true;
		owner = newOwner;
		ownerScript = owner.transform.GetComponent<scr_player>();
		this.obstacle.enabled = false;
	}

	public void ItemDropped(Vector3 newPosition)
	{
		Vector3 refreshPosition = new Vector3( newPosition.x,
		                                      originalY,
		                                      newPosition.z);
		scr_emptySpaceChecker empty = Instantiate(emptySpaceChecker, newPosition, Quaternion.identity) as scr_emptySpaceChecker;
		if(empty.CheckCollisions())
		{
			grabbed = false;
			owner = null;
			this.obstacle.enabled = true;
			this.transform.position = refreshPosition;
		}
		else
		{
			Debug.Log ("there is something i can do with that??????");
		}
		empty.Destroy();
	}

	public void SetDestroy()
	{
		Destroy(this.gameObject);
	}

	public bool Grabbed
	{
		get {return grabbed;}
		set {grabbed = value;}
	}

	public Transform Owner
	{
		get {return owner;}
		set {owner = value;}		
	}

	public NavMeshObstacle Obstacle
	{
		get {return obstacle;}
		set {obstacle = value;}
	}
}
