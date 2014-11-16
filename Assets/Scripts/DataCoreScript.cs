using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DataCoreScript
{
	/*Timer*/
	//public static float _MaxTime;
	/*List of Game Objects*/
	public static List<UserScript>	_Users				= new List<UserScript>();
	public static List<Component>	_Defenders			= new List<Component>();
	public static List<Component>	_Attackers			= new List<Component>();
	//public static List<int>		unit;
	private static bool				_bPathFinderFound	= false;
	public static GameObject		_PathFinder			= new GameObject();

	/*Debugging*/
	public static string	debugString		= null;

	public static void _InitializeUsers()
	{
		//Load the Attacker Data.
		//Bullshit some stuff for now. Learn to load from a file later.
		UserScript attacker = new UserScript();
		attacker._LoadAttacker();
		DataCoreScript._Users.Add(attacker);

		//Load Defender Data.
		//Bullshit some stuff for now. Learn to load from a file later.
		UserScript defender = new UserScript();
		defender._LoadDefender();
		DataCoreScript._Users.Add(defender);
	}

	//Use to regenerate mesh when a structure is destroyed.
	public static void _GeneratePathFindingMesh()
	{
		if(!DataCoreScript._bPathFinderFound)	DataCoreScript._PathFinder = GameObject.Find("A*");
		DataCoreScript._PathFinder.GetComponent<AstarPath>().Scan();
	}
}