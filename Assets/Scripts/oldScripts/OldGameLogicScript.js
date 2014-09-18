//#pragma strict
//
///* LIST OF GAME OBJECTS */
//var mainCamera			: Camera;
//
//
///* SELECTING PAWN VARIABLES */
//var selectedPawn		: GameObject	= null;
//var selectedPawnScript	: pawnScript	= null;
//var isPawnSelected		: boolean		= false;
//var layerMask			: LayerMask;
//
//var debugString			: String = null;	/*	USED TO DETERMINE WHAT IS HAPPENING. Erase when finished debugging.	*/
//
//function Start ()
//{
//	mainCamera	= Camera.main;
//}
//
//function Update ()
//{
//	debugString = null;
//	/*	IF LEFT MOUSE BUTTON IS PRESSED	*/
//	if (Input.GetMouseButtonDown(0))
//	{
//		debugString = "LMB CLICKED";
//		/*	CREATE A RAY FROM THE MOUSE INTO THE WORLD	*/
//		var hit : RaycastHit;
//		var ray : Ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//		Debug.DrawRay (ray.origin, ray.direction * 100, Color.yellow);
//		
//		/*	DETERMINE IF THE RAY HITS ANYTHING	*/
//		if (Physics.Raycast (ray.origin, ray.direction, hit, 10000.0, layerMask))
//		{
//			debugString += " & RAY HIT OBJECT";
//			
//			/*IF NO PAWN IS SELECTED*/
//			if (isPawnSelected == false)
//			{
//				/*	SELECT THE PAWN HIT	*/
//				if (hit.transform.parent.gameObject.CompareTag("pawn"))
//				{
//						debugString			+= " & HIT A PAWN";
//						selectedPawn		= hit.transform.parent.gameObject;
//						selectedPawnScript	= selectedPawn.GetComponent(pawnScript);
//						selectedPawnScript.select();
//						isPawnSelected		= true;
//				}
//				
//				/*	AND YOU HIT THE GROUND	*/
//				if (hit.transform.gameObject.name == "ground" && isPawnSelected == false)
//				{
//					debugString += " & HIT THE GROUND";
//				}
//			}
//			
//			/*IF A PAWN IS ALREADY SELECTED*/
//			if (isPawnSelected == true)
//			{
//				/*	SELECT NEW PAWN IF NEW PAWN HIT	*/
//				if (hit.transform.parent.gameObject.CompareTag("pawn") && hit.transform.parent.gameObject != selectedPawn)
//				{
//						debugString			+= " & DESELECT CURRENT PAWN & SELECT NEW PAWN";
//						selectedPawnScript.deselect();									//deselect current pawn
//						selectedPawn		= hit.transform.parent.gameObject;			//set new pawn into variables
//						selectedPawnScript	= selectedPawn.GetComponent(pawnScript);	//select new pawn's script
//						selectedPawnScript.select();									//call new pawn's select function
//				}
//				
//				/*	SETS WAYPOINT IF GROUND IS SELECTED	*/
//				if (hit.transform.gameObject.name == "ground")
//				{
//					debugString								+= " & MOVE PAWN HERE";
//					selectedPawnScript.moveTargetSelected	= true;
//					selectedPawnScript.moveTarget.x			= hit.point.x;
//					selectedPawnScript.moveTarget.z			= hit.point.z;
//				}
//				
//				/* IF THE USER SELECTS AN ENEMY PAWN */
//				if (hit.transform.parent.gameObject.CompareTag("enemyPawn"))
//				{
//					debugString								+= " & SET THIS PAWN AS TARGET";
//					selectedPawnScript.actionTargetSelected	= true;
//					selectedPawnScript.actionTarget			= hit.transform.parent.gameObject;
//					selectedPawnScript.moveTargetSelected	= true;
//					selectedPawnScript.moveTarget.x			= hit.point.x;
//					selectedPawnScript.moveTarget.z			= hit.point.z;
//				}
//			}
//		}
//	}
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