using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StructureScript : MonoBehaviour
{
	
	public GameObject obj;
	public Vector2 pos;
	public Component attackTarget;
	
	public int hp, maxHp;
	public int attackPwr;
	public float attackRange;
	public bool isSelected = false;

	public UnitScript.UnitClass preferredTarget;
	public enum StructureClass	{Wall = 0, Resource = 1, Offensive = 2, Generator = 3, Generic = 4};
	public enum StructureState{Idle, Searching, Attack, Death};
	public StructureClass	structClass;
	public StructureState	structState;

	void Start ()
	{

	}
	void Update ()
	{
		//State machine
		switch (structState)
		{
		case StructureState.Idle:
			_Idle();
			break;
		case StructureState.Searching:
			if(!attackTarget) _FindNearestPreferredTarget(DataCoreScript._Attackers);
			else structState = StructureState.Attack;
			break;
		case StructureState.Attack:
			if (!attackTarget) structState = StructureState.Searching;
			else Attack(attackTarget);
			break;
		default:
			break;
		}
	}
	public void Initialize (StructureClass structureClass, Vector2 mapPos)
	{
		structState = StructureState.Searching;
		structClass = structureClass;
		attackPwr = 10;
		attackRange = 4.0f;
		maxHp	= 1000;
		hp		= maxHp;
		pos = mapPos;
		obj = transform.gameObject;
		obj.transform.position = MapScript._MapToWorldPos(pos);
		//GraphicsCoreScript._SetMaterial(obj, "struct1Mat");
		//Parent it to the UnitContainer Object
		transform.parent = GameObject.Find("StructureContainer").transform;
	}
	public static void Instance (StructureClass structClass, Vector2 mapPos)
	{
		GameObject structure = Instantiate(Resources.Load("Structures/structure")) as GameObject;
		structure.GetComponent<StructureScript>().Initialize (structClass, mapPos);
		DataCoreScript._Defenders.Add(structure.GetComponent<StructureScript>());
	}
	public void _Idle ()
	{
		//Do Idling stuff here, perhaps with animations
	}
	public void _FindNearestPreferredTarget (List<Component> potentialTargets)
	{
		//Loop through potential targets and return the closest
		//First loop through them and get the preferred ones and get the closest of those
		if(potentialTargets.Count > 0)
		{
			List<Component> potTargs = potentialTargets;
			int closestTarg = 0;
			float smallestDist = (float)MapScript.mapWidth;
			float dist = 0.0f;
			for(int i = 0; i < potTargs.Count; i++)
			{
				Vector2 hereToThere = new Vector2 (gameObject.transform.position.x - potTargs[i].transform.position.x,
				                                   gameObject.transform.position.z - potTargs[i].transform.position.z);
				dist = hereToThere.magnitude;
				if(dist < smallestDist && dist <= attackRange)
				{
					smallestDist = dist;
					closestTarg = i;
				}
			}
			if(dist <= attackRange) attackTarget = potTargs [closestTarg];
			else attackTarget = null;
		}
		//else structState = StructureState.Idle;
	}
	/* SOME ATTACKING FUNCTION FOR NOW */
	public void Attack (Component target)
	{
		if(target != null)
		{
			target.GetComponent<UnitScript>().TakeDamage(10);
			if(target.GetComponent<UnitScript>().hp <= 0) attackTarget = null;
		}
	}
	public void TakeDamage (int dmg)
	{
		if(hp > 0.0)hp -= dmg;
		//If the object's health falls below zero...
		if(hp <= 0.0)
		{
			//Update Path Finding Mesh
			PathFinding.Grid.getNode((int)pos.x, (int)pos.y).enabled = true;

			//Remove the structure from the list of defending objects in the scene
			DataCoreScript._Defenders.Remove(this);
			Destroy(gameObject);
		}
	}
}

/*
    IEnumerator Start() {
        StartCoroutine("DoSomething", 2.0F);
        yield return new WaitForSeconds(1);
        StopCoroutine("DoSomething");
    }
    IEnumerator DoSomething(float someParameter) {
        while (true) {
            print("DoSomething Loop");
            yield return null;
        }
    }
 */