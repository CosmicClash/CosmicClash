using UnityEngine;
using System.Collections;

public class MapScript : MonoBehaviour
{
	public GameObject map;

	public static int	mapWidth		= 40;		//Num of tiles wide
	public static int	mapHeight		= 40;
	public static float	tileWidth		= 1.0f;		//Width of a single tile
	public static float	tileHeight		= 1.0f;
	public static float	actualMapWidth	= 40.0f;		//Size of map
	public static float	actualMapHeight = 40.0f;
	private Vector2 worldUp = new Vector2(1,1);
	public float tileAmount = 40.0f;

	void Start ()
	{
		worldUp.Normalize();
		actualMapWidth		= mapWidth	* tileWidth;
		actualMapHeight		= mapHeight * tileHeight;
		map = this.gameObject;
		map.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(tileAmount/2.0f, tileAmount/2.0f);
		map.GetComponent<Transform>().localScale = new Vector3 (tileAmount * tileWidth, tileAmount * tileHeight, tileAmount);
	}

	/*UTILITY FUNCTIONS*/
	//Convert map space to world space
	public static Vector3 _MapToWorldPos (Vector2 mapPos)
	{
		Vector3 worldPos;
		worldPos.x		=	0.0f - (actualMapWidth/2.0f);
		worldPos.x		+=	(mapPos.x * tileWidth);
		worldPos.x		+=	(tileWidth/2.0f);
		worldPos.y		=	0.0f - (actualMapHeight/2.0f);
		worldPos.y		+=	(mapPos.y * tileHeight);
		worldPos.y		+=	(tileHeight/2.0f);
		worldPos.z		=	-0.75f;
		
		return worldPos;
	}
	//Convert world space to map space
	public static Vector2 _WorldToMapPos (Vector3 worldPos)
	{
		Vector2 mapPos;
		mapPos.x = Mathf.Floor(worldPos.x + mapWidth/2.0f);
		mapPos.y = Mathf.Floor(worldPos.y + mapHeight/2.0f);

		return mapPos;
	}
	public static Vector2 _RandomMapPos()
	{
		int xPos = Mathf.FloorToInt(Random.value * MapScript.actualMapWidth);
		int yPos = Mathf.FloorToInt(Random.value * MapScript.actualMapHeight);
		return new Vector2(xPos, yPos);
	}
	public static float _Distance_float (Vector2 mapPosA, Vector2 mapPosB)
	{
		//Sqrt of the sum of the square diffs
		Vector2 diff = mapPosB - mapPosA;
		float aSquared = diff.x * diff.x;
		float bSquared = diff.y * diff.y;
		float c = Mathf.Sqrt (aSquared + bSquared);
		return c;
	}
	public static int _Distance_int (Vector2 mapPosA, Vector2 mapPosB)
	{
		//Sqrt of the sum of the square diffs
		Vector2 diff = mapPosB - mapPosA;
		float aSquared = diff.x * diff.x;
		float bSquared = diff.y * diff.y;
		float c = Mathf.Sqrt (aSquared + bSquared);
		return (int)c;
	}
}
