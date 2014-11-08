using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserScript : MonoBehaviour
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

	public List<StructureScript>	fortifications;
	public int numUnit1			= 0;
	public int numUnit2			= 0;
	public int numUnit3			= 0;
	public int maxUnitStorage	= 15;

	void Start ()
	{
	
	}
	void Update ()
	{
	
	}
	public void Initialize ()
	{
		//test initialization
		this.numUnit1 = 10;
		this.numUnit2 = 10;
		this.numUnit3 = 10;
	}
}
