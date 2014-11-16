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
	public enum StructureClass	{Wall, Resource, Offensive, Generator, Generic};
	public enum StructureState{Idle, Searching, Attack, Death};
	public StructureClass	structClass;
	public StructureState	structState;

	void Start ()
	{

	}
	void Update ()
	{
		//State machine
		if(structState == StructureState.Searching)
		{
			if(!attackTarget) FindTarget(DataCoreScript._Attackers);
			else structState = StructureState.Attack;
		}
		if(structState == StructureState.Attack)
		{
			if (!attackTarget) structState = StructureState.Searching;
			else Attack(attackTarget);
			
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
		GraphicsCoreScript._SetMaterial(obj, "struct1Mat");
		//Parent it to the UnitContainer Object
		transform.parent = GameObject.Find("StructureContainer").transform;
	}
	public static void Instance (StructureClass structClass, Vector2 mapPos)
	{
		GameObject structure = Instantiate(Resources.Load("structure")) as GameObject;
		structure.GetComponent<StructureScript>().Initialize (structClass, mapPos);
		DataCoreScript._Defenders.Add(structure.GetComponent<StructureScript>());
	}
	public void FindTarget (List<Component> potentialTargets)
	{
		//Loop through potential targets and return the closest
		//First loop through them and get the preferred ones and get the closest of those
		if(potentialTargets.Count > 0)
		{
			List<Component> potTargs = potentialTargets;
			int closestTarg = 0;
			float smallestDist = (float)MapScript.mapWidth;
			for(int i = 0; i < potTargs.Count; i++)
			{
				Vector2 hereToThere = new Vector2 (gameObject.transform.position.x - potTargs[i].transform.position.x,
				                                   gameObject.transform.position.z - potTargs[i].transform.position.z);
				float dist = hereToThere.magnitude;
				if(dist < smallestDist && dist <= attackRange)
				{
					smallestDist = dist;
					closestTarg = i;
				}
			}
			attackTarget = potTargs [closestTarg];
		}
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
		if(hp <= 0.0)
		{
			//Destroy(BattleSceneScript._Defenders[0].gameObject);
			DataCoreScript._Defenders.Remove(this);
			//Debug.Log(BattleSceneScript._Defenders.Count);
			Destroy(gameObject);
		}
	}
}
