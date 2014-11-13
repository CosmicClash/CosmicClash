using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitScript : MonoBehaviour
{
	public Vector2 pos;
	public Vector2 moveTarget;
	public Component attackTarget;
	
	public int hp, maxHp;
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
		if(unitState == UnitState.Searching)
		{
			if(!attackTarget) FindTarget(BattleSceneScript._Structures);
			else unitState = UnitState.Move;
		}
		if(unitState == UnitState.Move)
		{
			if(attackTarget && moveTarget != null) MoveUnit();
			else unitState = UnitState.Searching;
		}
		if(unitState == UnitState.Attack)
		{
			if (!attackTarget) unitState = UnitState.Searching;
			else Attack(attackTarget);

		}

	}
	public void Initialize (UnitClass unitClass, Vector2 mapPos)
	{
		unitState = UnitState.Searching;
		unitClass = unitClass;
		pos = mapPos;
		moveTarget = pos;
		gameObject.transform.position = MapScript._MapToWorldPos(mapPos);
		if(unitClass == UnitClass.Unit1)
		{
			maxHp = 5000;
			hp = maxHp;
			movementSpeed	= 5.0f;
			attackPwr = 10;
			attackRange = 1.0f;
			preferredTarget = StructureScript.StructureClass.Generic;
			GraphicsCoreScript._SetMaterial(gameObject, "Unit1Mat");
		}
		else if(unitClass == UnitClass.Unit2)
		{
			maxHp = 4000;
			hp = maxHp;
			movementSpeed	= 9.0f;
			attackPwr = 5;
			attackRange = 4.0f;
			preferredTarget = StructureScript.StructureClass.Resource;
			GraphicsCoreScript._SetMaterial(gameObject, "Unit2Mat");
		}
		else if(unitClass == UnitClass.Unit3)
		{
			maxHp = 3000;
			hp = maxHp;
			movementSpeed	= 4.0f;
			attackPwr = 30;
			attackRange = 1.0f;
			preferredTarget = StructureScript.StructureClass.Offensive;
			GraphicsCoreScript._SetMaterial(gameObject, "Unit3Mat");
		}
		//Parent it to the UnitContainer Object
		transform.parent = GameObject.Find("UnitContainer").transform;
	}
	public static void Instance (UnitClass unitClass, Vector2 mapPos)
	{
		GameObject unit = Instantiate(Resources.Load("unit")) as GameObject;
		unit.GetComponent<UnitScript>().Initialize (unitClass, mapPos);
		BattleSceneScript._Units.Add(unit.GetComponent<UnitScript>());
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
		//Make moveTarget be the range distance or less away from the attack target
		//Vector from unit to the attack target
//		Vector2 unitToAtkTarg = MapScript._WorldToMapPos(attackTarget.transform.position) - pos;
//		float magnitude = unitToAtkTarg.magnitude;
//		if(magnitude > attackRange)
//		{
//			Vector2 directionVector = unitToAtkTarg.normalized * magnitude;
//			moveTarget = MapScript._WorldToMapPos(directionVector);
//		}
//		else moveTarget = pos;
//		Debug.Log("Mag: " + (int)magnitude);
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

			gameObject.transform.Translate(unitToTarget.x * Time.deltaTime * movementSpeed, 0.0f, unitToTarget.y * Time.deltaTime * movementSpeed);
			pos = MapScript._WorldToMapPos(gameObject.transform.position);
		}
		else unitState = UnitState.Attack;
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
			//Destroy(BattleSceneScript._Structures[0].gameObject);
			BattleSceneScript._Units.Remove(this);
			//Debug.Log(BattleSceneScript._Units.Count);
			Destroy(gameObject);
		}
	}
}
