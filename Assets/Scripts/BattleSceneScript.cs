using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleSceneScript : MonoBehaviour
{
	/*List of Game Objects*/
	public UnitScript.UnitClass spawnType;
	public List<int> unit;
	public enum BattleState{Initializing = 0, Main = 1, DefenderWin = 2, AttackerWin = 3, BattleQuit = 4};
	public BattleState battleState	= BattleState.Initializing;

	private	RaycastHit	hit;
	
	void OnGUI ()
	{
		//if(TimerScript._On)GUI.Box (new Rect (10, 5, 120, 25), "Time Left: " + TimerScript._GetTimeLeft());
		//Unit Class switching buttons
		if (GUI.Button (new Rect (10, 40, 120, 25), "Ground Troops " + unit[0]) && unit[0] > 0)
		{
			spawnType = UnitScript.UnitClass.GroundTroopUnit;
		}
		else if (GUI.Button (new Rect (10, 70, 120, 25), "Guardians " + unit[1]) && unit[1] > 0)
		{
			spawnType = UnitScript.UnitClass.RangerUnit;
		}
		else if (GUI.Button (new Rect (10, 100, 120, 25), "Thiefs " + unit[2]) && unit[2] > 0)
		{
			spawnType = UnitScript.UnitClass.ThiefUnit;
		}
	}

	void Update ()
	{
		switch (battleState)
		{
		case BattleState.Initializing:
			_Initialize();
			break;
		case BattleState.Main:
			_MainPhase();
			break;
		default:
			break;
		}
	}
	
	private void _Initialize ()
	{
		//TimerScript._Initialize (15.0f, true);
		//load attacker's data
		DataCoreScript._InitializeUsers();

		spawnType = UnitScript.UnitClass.GroundTroopUnit;
		unit.Add (100);
		unit.Add (100);
		unit.Add (100);

		//Go to main battle phase
		battleState = BattleState.Main;

		//Initialize Path Finding
		DataCoreScript._info.yAxis = Vector3.up; //Assign transform of grid
		PathFinding.Grid.Build(DataCoreScript._info); //Generates the mesh with the grid info
		foreach (Component defender in DataCoreScript._Defenders)
		{
			Vector2 pos = defender.GetComponent<StructureScript>().pos;
			PathFinding.Grid.getNode((int)pos.x, (int)pos.y).enabled = false;
		}
	}

	private void _MainPhase ()
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
				//Only spawn it if the ray hits within the map bounds
				float minX = -MapScript.actualMapWidth	/2.0f + MapScript.tileWidth		/2.0f;
				float minY = -MapScript.actualMapWidth	/2.0f + MapScript.tileWidth		/2.0f;
				float maxX =  MapScript.actualMapHeight	/2.0f - MapScript.tileHeight	/2.0f;
				float maxY =  MapScript.actualMapHeight	/2.0f - MapScript.tileHeight	/2.0f;

				if(hit.transform.position.x >= minX && hit.transform.position.x <= maxX &&
				   hit.transform.position.z >= minY && hit.transform.position.z <= maxY &&
				   unit[(int)spawnType] > 0)
				{
					Vector2 pos = MapScript._WorldToMapPos(hit.point);
					UnitScript.Instance(spawnType ,pos);
					if(spawnType == UnitScript.UnitClass.GroundTroopUnit) {unit[0] -= 1;}
					if(spawnType == UnitScript.UnitClass.RangerUnit) {unit[1] -= 1;}
					if(spawnType == UnitScript.UnitClass.ThiefUnit) {unit[2] -= 1;}
				}
			}
		}
	}
}









