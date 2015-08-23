using UnityEngine;
using System.Collections;

public class scr_emptySpaceChecker : MonoBehaviour {
	private SphereCollider sphereTrigger;

	// Use this for initialization
	void Start () {
		sphereTrigger = this.GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool CheckCollisions()
	{
		bool checkedOK = true;
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, 0.5f);
		foreach(Collider collider in colliders)
		{
			if(collider.transform.tag == "Item" || collider.transform.tag == "NPC")
			{
				Debug.Log ("There is item or NPC... cannot drop item.");
				checkedOK = false;
			}
		}
		return checkedOK;
	}

	public void Destroy()
	{
		Destroy (this.gameObject);
	}
	
}
