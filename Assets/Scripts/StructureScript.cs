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

	void Awake ()
	{

	}
	void Update ()
	{

	}
	public void Initialize (StructureClass structureClass, Vector2 mapPos)
	{
		this.structClass = structureClass;
		this.maxHp	= 100;
		this.hp		= maxHp;
		this.pos = mapPos;
		this.obj = this.transform.gameObject;
		this.obj.transform.position = MapScript._MapToWorldPos(this.pos);
		GraphicsCoreScript._SetMaterial(this.obj, "struct1Mat");
		//Parent it to the UnitContainer Object
		this.transform.parent = GameObject.Find("StructureContainer").transform;
	}
	public static void Instance (StructureClass structClass, Vector2 mapPos)
	{
		GameObject structure = Instantiate(Resources.Load("structure")) as GameObject;
		structure.GetComponent<StructureScript>().Initialize (structClass, mapPos);
		BattleSceneScript.structures.Add(structure.GetComponent<StructureScript>());
		//
	}
	public void Attack ()
	{
		
	}
	public void TakeDamage (int dmg)
	{
		Debug.Log("Structure: Taking damage");
		if(this.hp > 0.0)this.hp -= dmg;
	}
}
