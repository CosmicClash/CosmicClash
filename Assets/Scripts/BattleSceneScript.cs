using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleSceneScript : MonoBehaviour
{
	/*List of Game Objects*/
	public GameObject mapObject;
	public UnitScript.UnitClass spawnType;
	public List<int> unit;
	public static List<Component> structures = new List<Component>();
	public List<Component> units;
	//public static List<Component> units			= new List<Component>();
	
	private	RaycastHit	hit;
	
	/*Debugging*/
	private string	debugString		= null;
	private int		x = 0;
	private int		y = 0;
	
	void Start ()
	{
		//Battle Variables
		//load attacker's data
		spawnType = UnitScript.UnitClass.Unit1;
		unit.Add (50);
		unit.Add (60);
		unit.Add (60);
		
		//load defender's base
		_LoadDefenderBase();
	}
	
	void OnGUI ()
	{		
		GUI.Box(new Rect(10,10,120,25), "Pos (" + x + ", " + y + ")");
		//Unit Class switching buttons
		if (GUI.Button (new Rect (10, 40, 120, 25), "Unit 1: " + unit[0]) && unit[0] > 0)
		{
			spawnType = UnitScript.UnitClass.Unit1;
		}
		else if (GUI.Button (new Rect (10, 70, 120, 25), "Unit 2: " + unit[1]) && unit[1] > 0)
		{
			spawnType = UnitScript.UnitClass.Unit2;
		}
		else if (GUI.Button (new Rect (10, 100, 120, 25), "Unit 3: " + unit[2]) && unit[2] > 0)
		{
			spawnType = UnitScript.UnitClass.Unit3;
		}
	}

	void Update ()
	{
		/* IF THE LEFT MOUSE BUTTON IS DOWN */
		if (Input.GetMouseButtonDown (0))
		{
			/* CREATE A RAY FROM MOUSE TO WORLD */
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Debug.DrawRay (ray.origin, ray.direction * 35.0f, Color.yellow);
			
			/* DOES IT HIT ANYTHING IN THE SCENE? */
			if (Physics.Raycast (ray, out hit))
			{
				if(hit.transform.gameObject.name == "map" && unit[(int)spawnType] > 0)
				{
					Vector2 pos = MapScript._WorldToMapPos(hit.point);
					units.Add(UnitScript.Instance(spawnType ,pos).GetComponent<UnitScript>());
					if(spawnType == UnitScript.UnitClass.Unit1) {unit[0] -= 1;}
					if(spawnType == UnitScript.UnitClass.Unit2) {unit[1] -= 1;}
					if(spawnType == UnitScript.UnitClass.Unit3) {unit[2] -= 1;}
				}
			}
			
			Vector2 mapPos = MapScript._WorldToMapPos(hit.point);
			x = (int)mapPos.x;
			y = (int)mapPos.y;
			
		}//End mouse click
		
	}//END UPDATE

	public void SetSpawnType (string unitType)
	{
		if(unitType == "GroundTroop")
		{
			this.spawnType = UnitScript.UnitClass.Unit1;
		}
		if(unitType == "Ranger")
		{
			this.spawnType = UnitScript.UnitClass.Unit2;
		}
		if(unitType == "Thief")
		{
			this.spawnType = UnitScript.UnitClass.Unit3;
		}
	}
	
	private void _LoadDefenderBase ()
	{
		//Read file and load shit
		StructureScript.Instance(StructureScript.StructureClass.Resource,
		                                             MapScript._RandomMapPos() );
//		this.structures.Add(StructureScript.Instance(StructureScript.StructureClass.Generic,
//		                                             MapScript._RandomMapPos() ).GetComponent<StructureScript>());
//		this.structures.Add(StructureScript.Instance(StructureScript.StructureClass.Offensive,
//		                                             MapScript._RandomMapPos() ).GetComponent<StructureScript>());
//		this.structures.Add(StructureScript.Instance(StructureScript.StructureClass.Offensive,
//		                                             MapScript._RandomMapPos() ).GetComponent<StructureScript>());
//		this.structures.Add(StructureScript.Instance(StructureScript.StructureClass.Generic,
//		                                             MapScript._RandomMapPos() ).GetComponent<StructureScript>());
//		this.structures.Add(StructureScript.Instance(StructureScript.StructureClass.Resource,
//		                                             MapScript._RandomMapPos() ).GetComponent<StructureScript>());
//		this.structures.Add(StructureScript.Instance(StructureScript.StructureClass.Resource,
//		                                             MapScript._RandomMapPos() ).GetComponent<StructureScript>());
	}
	
	
	
	
	
	
	
	
	
	
	//	static public Component _GetClosestEnemyToUnit (Component unit, out float fDistance)
	//	{	
	//		Component closest = null;
	//		foreach (Component enemy in this.structures)
	//		{
	//			if(closest == null) closest = enemy;
	//		}
	//		fDistance = 0.0f;
	//		return unit;
	//	}
	//		//Loop through potential targets and return the closest
	//		//First loop through them and get the preferred ones and get the closest of those
	//		if(potentialTargets.Count > 0)
	//		{
	//			List<GameObject> potTargs = potentialTargets;
	//			int closestTarg = 0;
	//			float smallestDist = (float)MapScript.mapWidth;
	//			for(int i = 0; i < potTargs.Count; i++)
	//			{
	//				Vector2 hereToThere = new Vector2 (this.obj.transform.position.x - potTargs[i].transform.position.x,
	//				                                   this.obj.transform.position.y - potTargs[i].transform.position.y);
	//				float dist = hereToThere.magnitude;
	//				if(dist < smallestDist)
	//				{
	//					smallestDist = dist;
	//					closestTarg = i;
	//				}
	//			}
	//			this.attackTarget = potTargs [closestTarg];
	//			this.moveTarget = MapScript._WorldToMapPos(this.attackTarget.transform.position);
	//		}
	//Make moveTarget be the range distance or less away from the attack target
	//Vector from unit to the attack target
	//		Vector2 unitToAtkTarg = MapScript._WorldToMapPos(this.attackTarget.transform.position) - this.pos;
	//		float magnitude = unitToAtkTarg.magnitude;
	//		if(magnitude > this.attackRange)
	//		{
	//			Vector2 directionVector = unitToAtkTarg.normalized * magnitude;
	//			this.moveTarget = MapScript._WorldToMapPos(directionVector);
	//		}
	//		else this.moveTarget = this.pos;
	//		Debug.Log("Mag: " + (int)magnitude);
	
}










