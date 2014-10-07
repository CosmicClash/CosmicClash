using UnityEngine;
using System.Collections;

public class UnitScript : MonoBehaviour
{

	public Transform gameObject;
	public Vector2 pos;
	public Vector2 moveTarget;
	public FortificationScript attackTarget;
	public int movementSpeed;
	public int sightDistance;
	public int favoriteTarget;
	public bool isSelected = false;
	public enum State{idle, move, attack};
	public DataCoreScript.UnitClass unitType;



	void Awake ()
	{
		gameObject = this.transform;
	}
	void Update ()
	{

	}
	public void Initialize (DataCoreScript.UnitClass unitType, Vector2 mapPos)
	{
		this.unitType = unitType;
		this.pos = mapPos;
		gameObject.position = MapScript._MapToWorldPos(mapPos);
		if(unitType == DataCoreScript.UnitClass.Unit1)
		{
			this.movementSpeed	= 2;
			// Assigns a material named "Assets/Resources/DEV_Orange" to the object.
			Material red = Resources.Load("Unit1Mat", typeof(Material)) as Material;
			this.gameObject.renderer.material = red;
		}
		else if(unitType == DataCoreScript.UnitClass.Unit2)
		{
			this.movementSpeed	= 4;
			Material green = Resources.Load("Unit2Mat", typeof(Material)) as Material;
			this.gameObject.renderer.material = green;
		}
		else if(unitType == DataCoreScript.UnitClass.Unit3)
		{
			this.movementSpeed	= 1;
			Material blue = Resources.Load("Unit3Mat", typeof(Material)) as Material;
			this.gameObject.renderer.material = blue;
		}
	}
	public static GameObject Instance (DataCoreScript.UnitClass unitType, Vector2 mapPos)
	{
		GameObject unit = Instantiate(Resources.Load("unit")) as GameObject;
		unit.GetComponent<UnitScript>().Initialize (unitType, mapPos);
		return unit;
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
}
