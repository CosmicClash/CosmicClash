using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataCoreScript : MonoBehaviour
{
	public UserScript attacker;
	public UserScript defender;

	/*List of Game Objects*/
	public List<UnitScript> SelectedUnits;
	public GameObject mapObject;

	/*Selecting Units variables*/
	public GameObject	selectedUnit		= null;
	public UnitScript	selectedUnitScript	= null;
	public bool			isUnitSelected		= false;
	private	RaycastHit	hit;
	
	public enum UnitClass{Unit1 = 0, Unit2 = 1, Unit3 = 2};
	UnitClass spawnType = UnitClass.Unit1;
	public List<int> Units;

	/*Debugging*/
	private string	debugString		= null;
	private int		x = 0;
	private int		y = 0;

	void Start ()
	{
		//Battle Variables
		spawnType = UnitClass.Unit1;
		Units.Add (10);
		Units.Add (15);
		Units.Add (05);
		//load attacker's data

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
		//Unit Class switching buttons
		if (GUI.Button (new Rect (10, 40, 120, 25), "Unit 1: " + Units[0]) && Units[0] > 0)
		{
			spawnType = UnitClass.Unit1;
		}
		else if (GUI.Button (new Rect (10, 70, 120, 25), "Unit 2: " + Units[1]) && Units[1] > 0)
		{
			spawnType = UnitClass.Unit2;
		}
		else if (GUI.Button (new Rect (10, 100, 120, 25), "Unit 3: " + Units[2]) && Units[2] > 0)
		{
			spawnType = UnitClass.Unit3;
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
				if(hit.transform.gameObject.name == "map" && Units[(int)spawnType] > 0)
				{
					Vector2 pos = MapScript._WorldToMapPos(hit.point);
					UnitScript.Instance(spawnType ,pos);
					if(spawnType == UnitClass.Unit1) {Units[0] -= 1;}
					if(spawnType == UnitClass.Unit2) {Units[1] -= 1;}
					if(spawnType == UnitClass.Unit3) {Units[2] -= 1;}
				}
			}
			
			Vector2 mapPos = MapScript._WorldToMapPos(hit.point);
			x = (int)mapPos.x;
			y = (int)mapPos.y;

		}//End mouse click

	}//END UPDATE

}

//GETTER AND SETTER EXAMPLE BY BRANDON
//	private float ptest =0.0f;
//	public static float test{
//		get{return ptest;}
//		set{
//			
//			ptest = value;
//			
//			
//		}
//	}
//	public static float test2{
//		get{return ptest*2.0f;}
//		set{
//			
//			ptest = value*0.5f;
//			
//			
//		}
//	}

//	void updateTest(float old,float now){
//		if (now == "main menu") {
//
//		}
//	}
//DataCoreScript.test = 1.0f;















