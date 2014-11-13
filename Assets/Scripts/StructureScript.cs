using UnityEngine;
using System.Collections;

public class StructureScript : MonoBehaviour
{
	
	public GameObject obj;
	public Vector2 pos;
	public UnitScript.UnitClass favoriteTarget;
	public int hp, maxHp;
	public enum StructureClass	{Wall, Resource, Offensive, Generator, Generic};
	public StructureClass	structClass;

	void Start ()
	{

	}
	void Update ()
	{

	}
	public void Initialize (StructureClass structureClass, Vector2 mapPos)
	{
		structClass = structureClass;
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
		BattleSceneScript._Structures.Add(structure.GetComponent<StructureScript>());
	}
	public void Attack ()
	{
		
	}
	public void TakeDamage (int dmg)
	{
		if(hp > 0.0)hp -= dmg;
		if(hp <= 0.0)
		{
			//Destroy(BattleSceneScript._Structures[0].gameObject);
			BattleSceneScript._Structures.Remove(this);
			Debug.Log(BattleSceneScript._Structures.Count);
			Destroy(gameObject);
		}
	}
}
