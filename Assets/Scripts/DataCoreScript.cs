using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataCoreScript : MonoBehaviour
{
	public UserScript attacker;
	public UserScript defender;

	/*List of Game Objects*/
	public List<UnitScript> myUnits;
	public List<UnitScript> SelectedUnits;
	public GameObject mapObject;

	/*Selecting Units variables*/
	public GameObject	selectedUnit		= null;
	public UnitScript	selectedUnitScript	= null;
	public bool			isUnitSelected		= false;
	private	RaycastHit	hit;
	
	public enum ClassType{Unit1 = 0, Unit2 = 1, Unit3 = 2};
	ClassType spawnType = ClassType.Unit1;

	/*Debugging*/
	private string	debugString		= null;
	private int		x = 0;
	private int		y = 0;

	void Start ()
	{
		//Battle Variables
		spawnType = ClassType.Unit1;

		//load defender's base
		defender.name = "Defender";
		defender.gold = 100;
		defender.thorium = 100;
		defender.numTrophies = 10;
	}

	void OnGUI ()
	{
//		if(GUI.Button(new Rect(20,40,100,20), "Unit 1"))
//		{
//			spawnClassType = ClassType.Unit1;
//		}
		
		GUI.Box(new Rect(10,10,120,25), "Pos (" + x + ", " + y + ")");
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
				GameObject unit		= UnitScript.Instance();
				Vector2 pos			= MapScript._WorldToMapPos(hit.point);
				unit.GetComponent<UnitScript>().Initialize (0, pos);
			}
			
			Vector2 mapPos = MapScript._WorldToMapPos(hit.point);
			x = (int)mapPos.x;
			y = (int)mapPos.y;

		}//End mouse click

	}//END UPDATE

}

















