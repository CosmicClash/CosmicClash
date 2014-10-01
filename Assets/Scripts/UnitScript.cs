using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour
{
	public Transform gameObject;
	public Vector2 pos;
	public Vector2 moveTarget;
	public FortificationScript attackTarget;
	
	public string className = "null";
	public int unitType;

	public int movementSpeed;
	public int sightDistance;
	
	public int favoriteTarget;

	public bool isSelected = false;

	// Use this for initialization
	void Awake ()//Start
	{
		//moveTarget = pos;
		gameObject = this.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if(moveTarget != pos)
//		{
//			pos = moveTarget;
//		}
		gameObject.position = DataCoreScript._MapToWorldPos(pos, 0);
	}

	public void Select()
	{
		isSelected = true;
	}

	public void Deselect()
	{
		isSelected = false;
	}
	
	
	public void movePawn()
	{

	}
	
	/* SOME ATTACKING FUNCTION FOR NOW */
	public void attack ()
	{

	}
}
