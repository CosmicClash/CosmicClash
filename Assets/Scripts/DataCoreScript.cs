using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataCoreScript : MonoBehaviour
{
	/*List of Game Objects*/
	public List<UnitScript> myUnits;
	public GameObject mapObject;

	/*Selecting Units variables*/
	public GameObject	selectedUnit		= null;
	public UnitScript	selectedUnitScript	= null;
	public bool			isUnitSelected		= false;
	private	RaycastHit	hit;
	
	public enum ClassType{Unit1 = 0, Unit2 = 1, Unit3 = 2};
	ClassType spawnClassType;
	
	/*World and map data*/
	public static int	mapWidth		= 40;		//Num of tiles wide
	public static int	mapHeight		= 40;
	public static float	tileWidth		= 1.0f;	//Width of a single tile
	public static float	tileHeight		= 1.0f;
	public static float	actualMapWidth	= 0.0f;		//Size of map
	public static float	actualMapHeight = 0.0f;
	private Vector2 worldUp = new Vector2(1,1);

	/*Debugging*/
	private string	debugString		= null;


	void Start ()
	{
		worldUp.Normalize();
		actualMapWidth		= mapWidth	* tileWidth;
		actualMapHeight		= mapHeight * tileHeight;
	}

	void OnGUI ()
	{
		// Make a background box
		GUI.Box(new Rect(10,10,150,135), "Unit Selection");
		//Unit Type Buttons
		if(GUI.Button(new Rect(20,40,100,20), "Unit 1"))
		{
			spawnClassType = ClassType.Unit1;
		}
		if(GUI.Button(new Rect(20,65,100,20), "Unit 2"))
		{
			spawnClassType = ClassType.Unit2;
		}
		if(GUI.Button(new Rect(20,90,100,20), "Unit 3"))
		{
			spawnClassType = ClassType.Unit3;
		}
		if(GUI.Button(new Rect(20,120,120,20), "Special Command"))
		{

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
				/* IF NO UNIT IS SELECTED */
				if (!isUnitSelected)
				{
					debugString += "_";
						/* SELECT THE RAY-HIT UNIT */
						if (hit.transform.gameObject.name == "unit")
						{
								debugString += "Unit Selected";
								selectedUnit = hit.transform.gameObject;
								selectedUnitScript = selectedUnit.GetComponent<UnitScript>();
								selectedUnitScript.Select();
								isUnitSelected = true;
						}
	
						//Instance a new troop if one is available
//						if (hit.transform.gameObject.name == "mapTest" && !isUnitSelected)
//						{
//							GameObject unit = Instantiate(Resources.Load("unit")) as GameObject;
//							unit.GetComponent<UnitScript>().pos = _WorldToMapPos(hit.point);
//						}
				}

				/* IF A UNIT IS ALREADY SELECTED */
				if (isUnitSelected)
				{
					//select a new unit if a new unit is hit
					if (hit.transform.gameObject.name == "unit" && hit.transform.gameObject != selectedUnit)
					{
							debugString += "New Unit selected";
							selectedUnitScript.Deselect();									//deselect current pawn
							selectedUnit = hit.transform.gameObject;
							selectedUnitScript = selectedUnit.GetComponent<UnitScript>();
							selectedUnitScript.Select();									//call new pawn's select function
					}

					/*	SETS WAYPOINT IF GROUND IS SELECTED	*/
					if (hit.transform.gameObject.name == "mapTest")
					{
							debugString += "Waypoint set";
							//selectedPawnScript.moveTargetSelected	= true;
							//selectedPawnScript.moveTarget.x			= hit.point.x;
							//selectedPawnScript.moveTarget.z			= hit.point.z;
					}

				}//end if unit was selected
			}
		}//End mouse click

		/*	IF RIGHT MOUSE BUTTON IS PRESSED	*/
		if (Input.GetMouseButtonDown(1))
		{
			if (isUnitSelected)
			{
				debugString				= "Deselected";
				selectedUnitScript.Deselect();
				selectedUnit			= null;
				selectedUnitScript		= null;
				isUnitSelected			= false;
			}
			else if (!isUnitSelected)
			{
				debugString += "Menu/Query";
			}
		}

		/*	PRINT DEBUG LOG IF DEBUGSTRING HAS SOMETHING IN IT.	*/
		if (debugString != null)
		{
			Debug.Log(debugString);
			Vector2 mapPos = _WorldToMapPos(hit.point);
			Debug.Log("Pos: (" + mapPos.x + ", " + mapPos.y + ")");
			debugString = null;
		}

	}//END UPDATE

	//OTHER FUNCTIONS
//public static class WorldMap

	public static Vector3 _MapToWorldPos (Vector2 mapPos, int objType)
	{
		Vector3 worldPos;
		worldPos.x		=	0.0f - (actualMapWidth/2.0f);
		worldPos.x		+=	(mapPos.x * tileWidth);
		worldPos.x		+=	(tileWidth/2.0f);
		worldPos.y		=	0.0f - (actualMapHeight/2.0f);
		worldPos.y		+=	(mapPos.y * tileHeight);
		worldPos.y		+=	(tileHeight/2.0f);
		worldPos.z		=	-0.05f;
		
		return worldPos;
	}

	public static Vector2 _WorldToMapPos (Vector3 worldPos)
	{
		Vector2 mapPos;
		float secondTermX = (tileWidth + actualMapWidth)/2.0f;
		mapPos.x		=	-1.0f * Mathf.Floor(Mathf.Abs((worldPos.x - secondTermX)/tileWidth));
		float secondTermY = (tileHeight + actualMapHeight)/2.0f;
		mapPos.y		=	-1.0f * Mathf.Floor(Mathf.Abs((worldPos.y - secondTermY)/tileHeight));
		
		return mapPos;
	}

}

















