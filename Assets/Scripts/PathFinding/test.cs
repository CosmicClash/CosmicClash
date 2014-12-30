using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PathFinding.GridInfo info =new PathFinding.GridInfo(10,10);//constuctore object size 10x10
		info.yAxis=Vector3.forward;//assign the transform the grid lies
		PathFinding.Grid.Build(info);//pass in the Grid info for generating the mesh
		//PathFinding.Grid.test();
		PathFinding.Grid.getNode(8,7).enabled=false;//not walkable
		PathFinding.Grid.getNode(7,8).enabled=false;
		PathFinding.Result path=PathFinding.Grid.buildPath(1,1,8,8); //makes a path from 1,1 to 8,8
		for (int x = 0;x<path.list.Length;x++){
			Debug.Log(path.list[x].x+", "+path.list[x].y);
		}
	}
}
