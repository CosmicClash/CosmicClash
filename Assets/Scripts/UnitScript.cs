using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitScript : MonoBehaviour
{
	//Object Data
	public Vector2		pos;
	private Animator	anim;
	public bool			isSelected = false;

	//Unit Specific Information
	public int			hp, maxHp;
	public int			attackPwr;
	public float		attackRange;
	public float		movementSpeed;
	public Vector2		directionVector;

	public StructureScript.StructureClass preferredTarget;
	public Component	attackTarget;

	//States
	public enum UnitClass{GroundTroopUnit, RangerUnit, ThiefUnit, MechUnit, GuardianUnit};
	public enum UnitState{Idle = 0, Move = 1, Attack = 2, Searching = 3, Death = 4};
	public enum UnitDirection{N = 0, NE = 1, E = 2, SE = 3, S = 4, SW = 5, W = 6, NW = 7};
	public UnitClass			unitClass;
	public UnitState			unitState;
	public UnitDirection		unitDirection;


	//Path Finding Information
	Vector2 pathFindingTarget;
	public Vector2		moveTarget;
	public Vector2		currentNode;
	public Vector2		NextNode;

	void Awake ()
	{

	}
	void Update ()
	{
		//State machine
		switch (unitState)
		{
		case UnitState.Idle:
			_Idle();
			break;
		case UnitState.Searching:
			if(!attackTarget) _FindNearestPreferredTarget(DataCoreScript._Defenders);
			else if (attackTarget)
			{
				unitState = UnitState.Move;
				anim.SetInteger("animState", (int)unitState);
			}
			break;
		case UnitState.Move:
			if(attackTarget && moveTarget != null) MoveUnit();
			else
			{
				unitState = UnitState.Searching;
			}
			break;
		case UnitState.Attack:
			if (!attackTarget) unitState = UnitState.Searching;
			else 
			{
				Attack(attackTarget);
			}
			break;
		default:
			break;
		}

	}
	public void Initialize (UnitClass unitClass, Vector2 mapPos)
	{
		unitDirection = UnitDirection.N;
		unitClass = unitClass;
		pos = mapPos;
		moveTarget = pos;
		gameObject.transform.position = MapScript._MapToWorldPos(mapPos);

		switch (unitClass)
		{
		case UnitClass.GroundTroopUnit:
			maxHp = 5000;
			hp = maxHp;
			movementSpeed	= 5.0f;
			attackPwr = 30;
			attackRange = 1.0f;
			preferredTarget = StructureScript.StructureClass.Generic;
			break;
		case UnitClass.RangerUnit:
			maxHp = 4000;
			hp = maxHp;
			movementSpeed	= 7.0f;
			attackPwr = 20;
			attackRange = 4.0f;
			preferredTarget = StructureScript.StructureClass.Generic;
			break;
		case UnitClass.ThiefUnit:
			maxHp = 3000;
			hp = maxHp;
			movementSpeed	= 9.0f;
			attackPwr = 15;
			attackRange = 1.0f;
			preferredTarget = StructureScript.StructureClass.Resource;
			break;
		case UnitClass.GuardianUnit:
			maxHp = 3000;
			hp = maxHp;
			movementSpeed	= 6.0f;
			attackPwr = -15;
			attackRange = 5.0f;
			preferredTarget = StructureScript.StructureClass.Resource;
			break;
		case UnitClass.MechUnit:
			maxHp = 7000;
			hp = maxHp;
			movementSpeed	= 3.0f;
			attackPwr = 50;
			attackRange = 1.0f;
			preferredTarget = StructureScript.StructureClass.Resource;
			break;
		default:
			break;
		}

		//Parent it to the UnitContainer Object
		transform.parent = GameObject.Find("UnitContainer").transform;

		//Set the Animator
		anim = transform.GetComponent<Animator>();
		anim.SetFloat("X", 0.0f);
		anim.SetFloat("Y", 0.0f);
		anim.SetInteger("animState", (int)unitState);

		//Go Into Searching State
		unitState = UnitState.Searching;

	}
	public static void Instance (UnitClass unitClass, Vector2 mapPos)
	{
		string unitType = null;
		switch (unitClass)
		{
		case UnitClass.GroundTroopUnit:
			unitType = "GroundTroopUnit";
			break;
		case UnitClass.RangerUnit:
			unitType = "RangerUnit";
			break;
		case UnitClass.ThiefUnit:
			unitType = "ThiefUnit";
			break;
		case UnitClass.MechUnit:
			unitType = "MechUnit";
			break;
		case UnitClass.GuardianUnit:
			unitType = "GuardianUnit";
			break;
			default:
			break;
		}
		GameObject unit = Instantiate(Resources.Load(unitType)) as GameObject;
		unit.GetComponent<UnitScript>().Initialize (unitClass, mapPos);
		DataCoreScript._Attackers.Add(unit.GetComponent<UnitScript>());
	}
	public void _Idle ()
	{
		//Play Idle Animation
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
				for(int i = 0; i < potTargs.Count; i++)
				{
					Vector2 hereToThere = new Vector2 (gameObject.transform.position.x - potTargs[i].transform.position.x,
					                                   gameObject.transform.position.z - potTargs[i].transform.position.z);
					float dist = hereToThere.magnitude;
					if(dist < smallestDist)
					{
						smallestDist = dist;
						closestTarg = i;
					}
				}
				attackTarget = potTargs [closestTarg];
				moveTarget = MapScript._WorldToMapPos(attackTarget.transform.position);
		}
		else
		{
			unitState = UnitState.Idle;
			anim.SetInteger("animState", (int)unitState);
		}
	}
	public void RandomMoveTarget ()
	{
		moveTarget = MapScript._RandomMapPos();
	}
	public void MoveUnit()
	{
		if(!attackTarget) return;
		if(!AmIWithinRange(attackTarget))
		{
			//Move unit to move-target
			Vector2 unitToTarget = moveTarget - pos;
			unitToTarget.Normalize();

			gameObject.transform.Translate(unitToTarget.x * Time.deltaTime * movementSpeed, unitToTarget.y * Time.deltaTime * movementSpeed, 0.0f);
			pos = MapScript._WorldToMapPos(gameObject.transform.position);

			//Set Unit Direction for sprite drawing
			anim.SetFloat("X", unitToTarget.x);
			anim.SetFloat("Y", unitToTarget.y);
		}
		else
		{
			unitState = UnitState.Attack;
			anim.SetInteger("animState", (int)unitState);
		}

	}
	private bool AmIWithinRange (Component atkTarget)
	{
		Vector2 atkTargMapPos = MapScript._WorldToMapPos(atkTarget.transform.position);
		if(MapScript._Distance_int(pos, atkTargMapPos) <= attackRange)
		{
			return true;
		}
		else return false;
	}
	/* SOME ATTACKING FUNCTION FOR NOW */
	public void Attack (Component target)
	{
		if(target != null)
		{
			target.GetComponent<StructureScript>().TakeDamage(10);
			if(target.GetComponent<StructureScript>().hp <= 0) attackTarget = null;
		}
	}
	public void TakeDamage (int dmg)
	{
		if(hp > 0.0)hp -= dmg;
		if(hp <= 0.0)
		{
			DataCoreScript._Attackers.Remove(this);
			Destroy(gameObject);
		}
	}
}
