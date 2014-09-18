//#pragma strict
//
////PAWN SCRIPT
///*	OBJECTS	*/
//var pawn					: GameObject;
//var aura					: GameObject;				//The aura object
//private var auraDistPlane	: GameObject;				//The ring to instantiate
//
///*	GAME LOGIC VARIABLES	*/
//private var isSelected		: boolean		= false;
//var moveTargetSelected		: boolean		= false;
//var wayPoint				: GameObject;
//var moveTarget				: Vector3;					//Where it should move to.
//var actionTargetSelected	: boolean		= false;
//var actionTarget			: GameObject;				//This is who this pawn is targeting, either friend or foe
//
///*	PAWN STATS	*/
//var auraDist				: float			= 5.0;		//How far the aura goes
//private var speed			: float			= 3.0;		//How quickly the unit moves
//var range					: float			= 3.0;		//How far pawn can attack
////////////////////////////////////////////
////////////////////////////////////////////
////////////////////////////////////////////
//
//
//function Start ()
//{
//	/*	INITIALIZE THE AURA	*/
//	auraDistPlane = GameObject.Find("auraDistPlane");
//	//auraDistPlane.renderer.enabled = false;
//	moveTarget = pawn.transform.position;
//	/*	INITIALIZE WAYPOINT	*/
//	wayPoint.transform.position		= moveTarget;
//	wayPoint.transform.position.y	= 3.0;
//	wayPoint.active					= false;
//}
//
//function Update ()
//{
//	/*	WHEN THE PAWN IS SELECTED	*/
//	if(isSelected == true)
//	{
//		/*	scale the aura. I will call this later as a separate function.	*/
//		aura.transform.localScale.x = auraDist;
//		aura.transform.localScale.y = auraDist;
//		aura.transform.localScale.z = auraDist;
//	}
//
//	if(moveTargetSelected == true)
//	{
//		movePawn();
//		wayPoint.active = true;
//		wayPoint.GetComponent(wayPointScript).rendering = true;
//		wayPoint.transform.position.x		= moveTarget.x;
//		wayPoint.transform.position.z		= moveTarget.z;
//		
//		if(pawn.transform.position == moveTarget)
//		{
//			moveTargetSelected			= false;
//			wayPoint.active				= false;
//			wayPoint.GetComponent(wayPointScript).rendering = false;
//		}
//	}
//	
//	if(actionTargetSelected == true)
//	{
//		attack();
//	}
//}
//
////////////////////////////////////////////
////////////////////////////////////////////
////////////////////////////////////////////
//
///*	CUSTOM FUNCTIONS	*/
//
//function select ()
//{
//	isSelected = true;
//	Debug.Log("PAWN SELECTED");
//	//auraDistPlane.renderer.enabled = true;
//}
//
//function deselect ()
//{
//	isSelected = false;
//	Debug.Log("PAWN SELECTED");
//	//auraDistPlane.renderer.enabled = false;
//}
//
//function movePawn()
//{
//	/*	Face target	*/
//	/*if(actionTargetSelected == true && Vector3.Distance(actionTarget.transform.position, pawn.transform.position) < Vector3.Distance(moveTarget.transform.position, pawn.transform.position)
//	{
//		Debug.Log("face actionTarget");
//	}*/
//
//	/*	Move pawn to its target	*/
//	var distance : float = Vector3.Distance(pawn.transform.position, moveTarget);
//	if(distance > 0.0)
//	{
//    	pawn.transform.position = Vector3.Lerp (pawn.transform.position, moveTarget, Time.deltaTime * speed/distance);
//	}
//}
//
///* SOME ATTACKING FUNCTION FOR NOW */
//function attack ()
//{
//	if(Vector3.Distance(pawn.transform.position, actionTarget.transform.position) <= range)
//	{
//		/*var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//		cube.transform.position = Vector3 (pawn.transform.position.x, 1.5, pawn.transform.position.z);
//		cube.transform.localScale.y = 0.25;
//		cube.transform.localScale.x = 0.25;
//		cube.transform.localRotation = pawn.transform.localRotation;
//		cube.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
//		cube.rigidbody.velocity = transform.forward * 2.0;*/
//	}
//}