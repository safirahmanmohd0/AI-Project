using System.Collections;
using UnityEngine;

public class Node{

	public bool walkable;
	public Vector3 worldPos;
	public int XGrid;
	public int YGrid;

	public int gCost;
	public int hCost;
	public Node parent;

	public Node(bool inputwalkable, Vector3 inputworldPos, int inputXGrid, int inputYGrid){
		walkable = inputwalkable;
		worldPos = inputworldPos;
		XGrid = inputXGrid;
		YGrid = inputYGrid;
	}

	public int fCost {
		get {
			return gCost + hCost;
		}
	}
}
