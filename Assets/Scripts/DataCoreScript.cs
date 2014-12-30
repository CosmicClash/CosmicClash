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

	//PATH FINDING OBJECT
	public static PathFinding.GridInfo _info = new PathFinding.GridInfo(40,40);


	//Information needed for a battle
	public static float	percentDestroyed	= 0.0f;
	public static int	starsAwarded		= 0;
	public static int	timeLeft			= 0;
	public static int	goldStolen			= 0;
	public static int	thoriumStolen		= 0;


	/*Debugging*/
	public static string	debugString		= null;

	public static void _InitializeUsers()
	{
		//Load the Attacker Data.
		//Bullshit some stuff for now. Learn to load from a file later.
		UserScript attacker = new UserScript();
		attacker._LoadAttacker();
		_Users.Add(attacker);

		//Load Defender Data.
		//Bullshit some stuff for now. Learn to load from a file later.
		UserScript defender = new UserScript();
		defender._LoadDefender();
		_Users.Add(defender);
	}
}