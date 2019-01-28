using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {
	public static bool check=true;
	public Transform target;
	public Transform seeker1;
	public Transform seeker2;
	public Transform seeker3;
	public Transform seeker4;
	public Transform wall;
	float speed=0.5f;
	Grid grid;

	void Awake(){
		grid = GetComponent<Grid> ();

	}

	void Update(){
		if (check == true) {

			FindPath (seeker1.position, target.position);
			FindPath (seeker2.position, target.position);
			FindPath (seeker3.position, target.position);
			FindPath (seeker4.position, target.position);
		}
		if (check == false) {
			FindPath (seeker1.position, wall.position);
			FindPath (seeker2.position, wall.position);
			FindPath (seeker3.position, wall.position);
			FindPath (seeker4.position, wall.position);
		}
	}

	void FindPath(Vector3 startPos, Vector3 targetPos){
		Node startNode = grid.NodeFromWorldPoint (startPos);
		Node targetNode = grid.NodeFromWorldPoint (targetPos);

		List<Node> openSet = new List<Node> ();
		HashSet<Node> closedSet = new HashSet<Node> ();
		openSet.Add (startNode);
		Node currentNode = openSet [0];
		while (openSet.Count > 0) {

			float minf = float.MaxValue; 
			int index = 0;

			for (int k = 0; k < openSet.Count; k++) {
				if (openSet [k].fCost < minf) {
					minf = openSet [k].fCost;
					index = k;
					continue;
				}
				if (minf== openSet [k].fCost) {
					if (openSet[index].hCost > openSet [k].hCost && index!=k) {
						minf = openSet [k].fCost;
						index = k;
						currentNode = openSet [k];
					}

				}
			}
			currentNode = openSet [index];
			openSet.RemoveAt (index);
			closedSet.Add (currentNode);

			if (currentNode == targetNode) {
				RetracePath (startNode, targetNode);
				return;
			}

			foreach (Node neighbor in grid.GetNeighbors(currentNode)) {
				if (!neighbor.walkable || closedSet.Contains (neighbor)) {
					continue;
				}
				if (!openSet.Contains (neighbor) && !closedSet.Contains (neighbor)) {
					neighbor.gCost = GetDistance (currentNode, neighbor);
					neighbor.hCost = GetDistance (neighbor, targetNode);
					neighbor.fCost.Equals(neighbor.gCost + neighbor.hCost);
					neighbor.parent = currentNode;
					openSet.Add (neighbor);

				}


			}
		}
	}

	void followPath(List<Node>path)
	{
		for (int i = 0; i < path.Count; i++) {
			Vector3 pos = path[i].worldPos;
			float step = speed * Time.deltaTime;
			seeker1.position = Vector3.MoveTowards (seeker1.position, pos, step*4/3);
			seeker2.position = Vector3.MoveTowards (seeker2.position, pos, step);
			seeker3.position = Vector3.MoveTowards (seeker3.position, pos, step*3/2);
			seeker4.position = Vector3.MoveTowards (seeker4.position, pos, step*2);
		}

	}
		
	void RetracePath(Node startNode, Node endNode){
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();
		grid.path = path;
		followPath (path);
	}

	int GetDistance(Node nodeA, Node nodeB){

		int distX = Mathf.Abs(nodeA.XGrid - nodeB.XGrid);
		int distY = Mathf.Abs(nodeA.YGrid - nodeB.YGrid);
		return distX + distY;
	}


}
