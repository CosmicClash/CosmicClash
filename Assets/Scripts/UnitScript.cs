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

	public enum State{idle, move, attack};

	// Use this for initialization
	void Awake ()
	{
		gameObject = this.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//movePawn();
	}

	public void Initialize (int unitType, Vector2 mapPos)
	{
		this.unitType = unitType;
		this.pos = mapPos;
		gameObject.position = MapScript._MapToWorldPos(mapPos);
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
		if(this.pos != this.moveTarget)
		{
			//Move unit to move-target
			Vector2 unitToTarget = this.moveTarget - this.pos;
			this.gameObject.transform.Translate(unitToTarget * Time.deltaTime);
		}
	}
	
	/* SOME ATTACKING FUNCTION FOR NOW */
	public void attack ()
	{

	}

	public static GameObject Instance ()
	{
		return Instantiate(Resources.Load("unit")) as GameObject;
	}
}
