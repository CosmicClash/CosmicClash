using UnityEngine;
using System.Collections;

public class HomeBaseSceneScript : MonoBehaviour
{

	//Selecting objects
	public bool				_objSelect = false;
	public Component		_selectedObj;
	private	RaycastHit		hit;
	public enum UserState {Initializing, MainPhase};
	private UserState userState;

	public Vector2 fingerPos = new Vector2();

	void Start ()
	{
		//Instance Map and Camera
			//Map
		Instantiate(Resources.Load("Map/map") as GameObject);
				//Change map size based on user map size


			//Camera
		Instantiate(Resources.Load("camera") as GameObject);
		GameObject.Find("camera(Clone)").name = "camera";
		//Load the user info
			//Structures

			//Units

		//Set user state
		userState = UserState.Initializing;
	}

	void Update ()
	{
		//State machine
		switch (userState)
		{
		case UserState.Initializing:
			_Initialize ();
			break;
		case UserState.MainPhase:
			_MainPhase ();
			break;
		default:
			break;
		}
		//Check input
		_InputManager ();
	}//Update



	private void _InputManager ()
	{
		//If left mouse is down, create ray and check if it hits stuff within the map bounds
		if (Input.GetMouseButtonDown (0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit))
			{
				float minX = -MapScript.actualMapWidth	/2.0f + MapScript.tileWidth		/2.0f;
				float minY = -MapScript.actualMapWidth	/2.0f + MapScript.tileWidth		/2.0f;
				float maxX =  MapScript.actualMapHeight	/2.0f - MapScript.tileHeight	/2.0f;
				float maxY =  MapScript.actualMapHeight	/2.0f - MapScript.tileHeight	/2.0f;
				
				if(hit.transform.position.x >= minX && hit.transform.position.x <= maxX &&
				   hit.transform.position.z >= minY && hit.transform.position.z <= maxY)
				{
					Vector2 pos = MapScript._WorldToMapPos(hit.point);
				}
			}
		}
	}//End Input Manager

	
	private void _Initialize ()
	{

		userState = UserState.MainPhase;
	}
	private void _MainPhase ()
	{

	}



}
