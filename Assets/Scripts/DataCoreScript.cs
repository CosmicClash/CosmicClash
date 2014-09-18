using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataCoreScript : MonoBehaviour
{

	//GAME OBJECTS
	private Camera mainCamera;
	public List<UnitClassScript> myUnits;

	//Unit Selecting Variables
	public GameObject selectedUnit = null;
	public UnitClassScript selectedUnitScript = null;
	public bool isUnitSelected = false;

	//For Debugging
	private string debugString = null;

	// Use this for initialization
	void Start ()
	{
		mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
	{
		debugString = null;
		/*IF LEFT MOUSE BUTTON DOWN*/
		if (Input.GetMouseButtonDown(0))
		{
			debugString += "LMB Clicked";
			/*CREATE RAY FROM MOUSE TO WORLD*/
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay (ray.origin, ray.direction * 5.0f, Color.yellow);

			/*DETERMINE IF THE RAY HITS ANYTHING*/
			if (Physics.Raycast (ray, out hit, 2.0f))
			{
				debugString += " & Ray hit object:";
				debugString += hit.collider.gameObject.name;	
				/*IF NO PAWN IS SELECTED*/
				if (!isUnitSelected)
				{
					/*	SELECT THE PAWN HIT	*/
					if (hit.collider.gameObject.CompareTag("unit"))
					{
							selectedUnit		= hit.transform.parent.gameObject;
//							//selectedUnitScript	= selectedUnit.GetComponent(UnitClassScript);
//							//selectedPawnScript.select();
							isUnitSelected		= true;
					}

				}
				/*IF A PAWN IS ALREADY SELECTED*/
				if (isUnitSelected)
				{
					/*	SELECT NEW PAWN IF NEW PAWN HIT	*/
					if (hit.collider.gameObject.CompareTag("unit") && hit.collider.gameObject != selectedPawn)
					{
							debugString			+= " & DESELECT CURRENT PAWN & SELECT NEW PAWN";
							//selectedUnitScript.deselect();									//deselect current pawn
							selectedUnit		= hit.transform.parent.gameObject;			//set new pawn into variables
							//selectedUnitScript	= selectedPawn.GetComponent(pawnScript);	//select new pawn's script
							//selectedUnitScript.select();									//call new pawn's select function
					}
					
//					/*	SETS WAYPOINT IF GROUND IS SELECTED	*/
//					if (hit.transform.gameObject.name == "ground")
//					{
//						debugString								+= " & MOVE PAWN HERE";
//						selectedPawnScript.moveTargetSelected	= true;
//						selectedPawnScript.moveTarget.x			= hit.point.x;
//						selectedPawnScript.moveTarget.z			= hit.point.z;
//					}
//					
//					/* IF THE USER SELECTS AN ENEMY PAWN */
//					if (hit.transform.parent.gameObject.CompareTag("enemyPawn"))
//					{
//						debugString								+= " & SET THIS PAWN AS TARGET";
//						selectedPawnScript.actionTargetSelected	= true;
//						selectedPawnScript.actionTarget			= hit.transform.parent.gameObject;
//						selectedPawnScript.moveTargetSelected	= true;
//						selectedPawnScript.moveTarget.x			= hit.point.x;
//						selectedPawnScript.moveTarget.z			= hit.point.z;
//					}
				}
			}
		}
			}
		}

		if (debugString != null)Debug.Log (debugString);
	}
}



//	
//	/*	IF RIGHT MOUSE BUTTON IS PRESSED	*/
//	if (Input.GetMouseButtonDown(1))
//	{
//		if (isPawnSelected == true)
//		{
//			debugString				= "DE-SELECT &";
//			selectedPawnScript.deselect();
//			selectedPawn			= null;
//			selectedPawnScript		= null;
//			isPawnSelected			= false;
//		}
//		if (isPawnSelected == false)
//		{
//			debugString += " BRING UP MENU/QUERY";
//		}
//	}
//	
//	/*	PRINT DEBUG LOG IF DEBUGSTRING HAS SOMETHING IN IT.	*/
//	if (debugString != null)
//	{
//		Debug.Log(debugString);
//	}
//}