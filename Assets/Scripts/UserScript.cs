using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserScript : MonoBehaviour
{

	public string name = null;

	public int exp			= 0;
	public int expNeeded	= 0;
	public int numTrophies = 0;

	public int crystals	= 0;
	public int gold		= 0;
	public int maxGold		= 0;
	public int thorium		= 0;
	public int maxThorium	= 0;

	public bool shieldOn	= false;//maybe create a shield class. Or shield timer and/or shield type.

	public List<FortificationScript>	fortifications;
	public List<UnitScript>				units;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
