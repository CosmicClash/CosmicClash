using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitScript : MonoBehaviour
{

	public GameObject obj;
	public Vector2 pos;
	public Vector2 moveTarget;
	public Component attackTarget;

	public int attackPwr;
	public float attackRange;
	public float movementSpeed;
	public int favoriteTarget;
	public bool isSelected = false;

	public enum UnitClass{Unit1, Unit2, Unit3};
	public enum UnitState{Idle, Move, Searching, Attack, Death};
	public UnitClass			unitClass;
	public UnitState			unitState;

	public StructureScript.StructureClass preferredTarget;


	void Awake ()
	{
	}
	void Update ()
	{
		//State machine
		if(this.unitState == UnitState.Searching)
		{
			if(this.attackTarget == null)
			{
				//this.FindTarget(GameObject.Find("DataCoreObject").GetComponent<BattleSceneScript>().structures);//BattleSceneScript.structures
				this.FindTarget(BattleSceneScript.structures);
			}
			else unitState = UnitState.Move;
		}
		if(this.unitState == UnitState.Move)
		{
			if(this.attackTarget != null && this.moveTarget != null) this.MoveUnit();
		}
		if(this.unitState == UnitState.Attack)
		{
			if (this.attackTarget == null)
			{
				this.unitState = UnitState.Searching;
			}
			//else this.Attack(this.attackTarget);

		}

	}
	public void Initialize (UnitClass unitClass, Vector2 mapPos)
	{
		this.unitState = UnitState.Searching;
		this.unitClass = unitClass;
		this.pos = mapPos;
		this.moveTarget = this.pos;
		this.obj = this.transform.gameObject;
		this.obj.transform.position = MapScript._MapToWorldPos(mapPos);
		if(unitClass == UnitClass.Unit1)
		{
			this.movementSpeed	= 5.0f;
			this.attackPwr = 10;
			this.attackRange = 1.0f;
			this.preferredTarget = StructureScript.StructureClass.Generic;
			GraphicsCoreScript._SetMaterial(this.obj, "Unit1Mat");
		}
		else if(unitClass == UnitClass.Unit2)
		{
			this.movementSpeed	= 9.0f;
			this.attackPwr = 5;
			this.attackRange = 4.0f;
			this.preferredTarget = StructureScript.StructureClass.Resource;
			GraphicsCoreScript._SetMaterial(this.obj, "Unit2Mat");
		}
		else if(unitClass == UnitClass.Unit3)
		{
			this.movementSpeed	= 4.0f;
			this.attackPwr = 30;
			this.attackRange = 1.0f;
			this.preferredTarget = StructureScript.StructureClass.Offensive;
			GraphicsCoreScript._SetMaterial(this.obj, "Unit3Mat");
		}
		//Parent it to the UnitContainer Object
		this.transform.parent = GameObject.Find("UnitContainer").transform;
	}
	public static GameObject Instance (UnitClass unitClass, Vector2 mapPos)
	{
		GameObject unit = Instantiate(Resources.Load("unit")) as GameObject;
		unit.GetComponent<UnitScript>().Initialize (unitClass, mapPos);
		return unit;
	}
	public void Select()
	{
		isSelected = true;
	}

	public void Deselect()
	{
		isSelected = false;
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
					Vector2 hereToThere = new Vector2 (this.obj.transform.position.x - potTargs[i].transform.position.x,
					                                   this.obj.transform.position.y - potTargs[i].transform.position.y);
					float dist = hereToThere.magnitude;
					if(dist < smallestDist)
					{
						smallestDist = dist;
						closestTarg = i;
					}
				}
				this.attackTarget = potTargs [closestTarg];
				this.moveTarget = MapScript._WorldToMapPos(this.attackTarget.transform.position);
		}
		//Make moveTarget be the range distance or less away from the attack target
		//Vector from unit to the attack target
//		Vector2 unitToAtkTarg = MapScript._WorldToMapPos(this.attackTarget.transform.position) - this.pos;
//		float magnitude = unitToAtkTarg.magnitude;
//		if(magnitude > this.attackRange)
//		{
//			Vector2 directionVector = unitToAtkTarg.normalized * magnitude;
//			this.moveTarget = MapScript._WorldToMapPos(directionVector);
//		}
//		else this.moveTarget = this.pos;
//		Debug.Log("Mag: " + (int)magnitude);
	}
	public void RandomMoveTarget ()
	{
		this.moveTarget = MapScript._RandomMapPos();
	}
	public void MoveUnit()
	{
		if(!AmIWithinRange(this.attackTarget))
		{
			//Move unit to move-target
			Vector2 unitToTarget = this.moveTarget - this.pos;
			unitToTarget.Normalize();
			this.obj.transform.Translate(unitToTarget * Time.deltaTime * this.movementSpeed);
			this.pos = MapScript._WorldToMapPos(this.obj.transform.position);
		}
		else this.unitState = UnitState.Attack;
	}
	private bool AmIWithinRange (Component atkTarget)
	{
		Vector2 atkTargMapPos = MapScript._WorldToMapPos(atkTarget.transform.position);
		if(MapScript._Distance_int(this.pos, atkTargMapPos) <= this.attackRange)
		{
			return true;
		}
		else return false;
	}
	/* SOME ATTACKING FUNCTION FOR NOW */
	public void Attack (GameObject target)
	{
		Debug.Log("Unit: Attacking");
		if(target != null)
		{
			target.GetComponent<StructureScript>().TakeDamage(10);
		}
	}
}
