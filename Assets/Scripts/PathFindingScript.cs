using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PathFindingScript
{
	//Nodes that have been searched through
	public static ArrayList	closed = new ArrayList();
	//Nodes not yet considered
	public static SortedList open = new SortedList();

	//Map being searched

	//Max search depth before giving up
	private static int maxSearchDist;
}


//public class Node
//{
//	public static bool open;
//	public static float cost;
//	public List Neighbors;
//}
