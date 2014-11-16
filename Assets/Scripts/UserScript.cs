using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserScript
{

	public string name = null;

	public int exp				= 0;
	public int expNeeded		= 0;
	public int numTrophies		= 0;

	public int crystals			= 0;
	public int gold				= 0;
	public int maxGold			= 0;
	public int thorium			= 0;
	public int maxThorium		= 0;

	public bool shieldOn		= false;//maybe create a shield class. Or shield timer and/or shield type.


	public enum UserRole{Attacker, Defender};
	public class UnitDrawer
	{
		public UnitScript.UnitClass unitType;
		public int amount;
	};
	public List<UnitDrawer>			unitCupboard;
	public List<StructureScript>	fortifications;
	public UserRole userRole;

	public void Initialize ()
	{

	}
	public void _LoadAttacker()
	{
		userRole = UserRole.Attacker;
		
		UnitDrawer aUnit_1 = new UnitDrawer();
		UnitDrawer aUnit_2 = new UnitDrawer();
		UnitDrawer aUnit_3 = new UnitDrawer();
		
		aUnit_1.unitType	= UnitScript.UnitClass.Unit1;
		aUnit_1.amount		= 100;
		aUnit_2.unitType	= UnitScript.UnitClass.Unit2;
		aUnit_2.amount		= 100;
		aUnit_3.unitType	= UnitScript.UnitClass.Unit3;
		aUnit_3.amount		= 100;

		//unitCupboard.Add(aUnit_1);
		//unitCupboard.Add(aUnit_1);
		//unitCupboard.Add(aUnit_2);
		//unitCupboard.Add(aUnit_3);
	}
	public void _LoadDefender()
	{
		userRole = UserRole.Defender;

		int randomNum = Random.Range(10, 30);
		for(int i = 0; i < randomNum; i++)
		{
			int randomClass = (int)Random.Range(0, 3);
			StructureScript.Instance((StructureScript.StructureClass)randomClass,	MapScript._RandomMapPos() );
		}
		Debug.Log("Num Structures: " + randomNum);
	}
}
