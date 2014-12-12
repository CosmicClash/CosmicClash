// Pathfinding.cs
// <author>Brandon Farrell</author>
// <email>Brandon@BPFarrell.com</email>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PathFinding : MonoBehaviour {
	public static class Grid{
		public static int sizeX = 0;
		public static int sizeY = 0;
		public static Vector3 xAxis;
		public static Vector3 yAxis;
		public static Vector2 center;
		public static IList<Node> Nodes = new List<Node>();
		public static IList<Node> openList = new List<Node>();
		public static IList<Node> closedList = new List<Node>();
		public static void Build(GridInfo info){
			Grid.sizeX=info.sizeX;
			Grid.sizeY=info.sizeY;
			Grid.center=info.center;
			Grid.xAxis=info.xAxis;
			Grid.yAxis=info.yAxis;
			for(int y = 0;y<Grid.sizeY;y++){
				for(int x = 0;x<Grid.sizeX;x++){
					Node tempNode = new Node(x,y);
					Nodes.Add(tempNode);
					int index = x+y*Grid.sizeX;
					if(x>0){
						tempNode.addConections(Nodes[index-1]);
					}
					if(y>0){
						tempNode.addConections(Nodes[index-Grid.sizeX]);
					}
				}
			}
		}
		public static Node getNode(int x,int y){
			return Nodes[x+y*sizeX];
		}
		//public static void test(){
		//	for(int x = 0;x<Nodes.Count;x++){
		//		GameObject tempObj = Instantiate(Resources.Load("meshNode")) as GameObject;
		//		tempObj.transform.position=xAxis*Nodes[x].x+yAxis*Nodes[x].y;
		//		Nodes[x].meshNode=tempObj;
		//		Nodes[x].scriptNode=Nodes[x].meshNode.GetComponent<TestNode>();
		//	}
		//	for(int x = 0;x<Nodes.Count;x++){
		//		Nodes[x].scriptNode.nodes=Nodes[x].neighbor;
		//		Nodes[x].scriptNode.buildArray();
		//	}
		//}
		public static void addToOpenList(Node newNode){
			if(!openList.Contains(newNode)&&!closedList.Contains(newNode)){
				openList.Add(newNode);
			}
		}
		public static Node getFurthestNode(){
			int value = int.MaxValue;
			Node node=null;
			for(int x = 0;x<openList.Count;x++){
				if(openList[x].cost<value){
					value=openList[x].cost;
					node=openList[x];
				}
			}
			return node;
		}
		public static void moveToClosedList(Node newNode){
			if(openList.Contains(newNode)){
				openList.Remove(newNode);
			}
			closedList.Add(newNode);
		}
		public static Result buildPath(int startX,int startY,int endX,int endY){
			openList = new List<Node>();
			closedList = new List<Node>();
			for(int x = 0;x<Nodes.Count;x++){
				Nodes[x].getHValue(endX,endY);
			}
			Node workingNode=getNode(startX,startY);
			workingNode.steps=0;
			workingNode.start=true;
			int count=0;
			bool failed =false;
			while (true){
				count++;
				//Debug.Log(count+": "+openList.Count+"-"+workingNode.x+", "+workingNode.y);
				workingNode.checkNeighbors();
				if(openList.Count>0){
					workingNode=getFurthestNode();
					if(workingNode.h==0){
						break;
					}
				}else{
					Debug.LogError("Could not find a valid path from "+startX+", "+startY+" to "+endX+", "+endY);
					workingNode=closedList[0];
					failed=true;
					break;
				}
			}
			IList<Node> resultPath = new List<Node>();
			count=0;
			while (true){
				count++;
				//Debug.Log(count+": "+workingNode.steps+"-"+workingNode.x+", "+workingNode.y);
				resultPath.Add(workingNode);
				if(workingNode.start){
					break;
				}else{
					workingNode=workingNode.parent;
				}
			}
			Result path = new Result(resultPath);
			path.failed=failed;
			return path;
		}
	}
	public class Result{
		public Vector2[] list;
		public bool failed;
		public int index=0;
		public Result(IList<Node> path){
			this.list=new Vector2[path.Count];
			for(int x = 0;x<list.Length;x++){
				int pathIndex = path.Count-1-x;
				list[x]=new Vector2(path[pathIndex].x,path[pathIndex].y);
			}
		}
		public Vector2 Get(){
			return this.list[this.index];
		}
		public Vector2 Next(){
			if(index<list.Length-1){
				index++;
			}
			return this.Get();
		}

	}
	public class GridInfo{
		public GridInfo(int sizeX,int sizeY){
			this.sizeX=sizeX;
			this.sizeY=sizeY;
			this.center=Vector2.zero;
			this.xAxis=Vector3.right;
			this.yAxis=Vector3.forward;
		}
		public int sizeX;
		public int sizeY;
		public Vector3 xAxis;
		public Vector3 yAxis;
		public Vector2 center;

	}
	public class Node{
		public int x=0;
		public int y=0;
		public bool enabled=true;
		public Node parent=null;
		public int h=-1;
		public int steps=-1;
		public int cost{get{ return this.h+this.steps;}}
		public bool start=false;
		public GameObject meshNode;
		//public TestNode scriptNode;
		public IList<Node> neighbor = new List<Node>();
		public Node(int x,int y){
			this.x=x;
			this.y=y;
			this.enabled=true;
		}
		public void addConections(Node newNode){
			this.addSingleConnection(newNode);
			newNode.addSingleConnection(this);
		}
		public void addSingleConnection(Node newNode){
			this.neighbor.Add(newNode);
		}
		public void getHValue(int x,int y){
			this.h=Mathf.Abs(this.x-x)+Mathf.Abs(this.y-y);
//			this.scriptNode.h=this.h;
			this.steps=-1;
			this.parent=null;
			this.start=false;
		}
		public void nearestSegment(Node newParent){
			this.parent=newParent;
			this.steps=newParent.steps+1;
//			this.scriptNode.steps=this.steps;
//			this.scriptNode.path=this.parent.meshNode;
		}
		public void checkNeighbors(){
			for( int x = 0;x<this.neighbor.Count;x++){
				Node current = this.neighbor[x];
				if(current.enabled==false){
					continue;
				}
				if(current.parent==null||current.steps>this.steps+1){
					current.nearestSegment(this);
					Grid.addToOpenList(current);
					continue;
				}
			}
			Grid.moveToClosedList(this);
		}
	}
}
