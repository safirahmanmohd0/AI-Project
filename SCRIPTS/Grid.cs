using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public Vector2 gridSize;
	public float nodeRadius;
	public LayerMask unwalkableMask;
	Node[,] grid;

	float diameter;
	int gridSizeX, gridSizeY;

	void Start(){
		diameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridSize.x / diameter);
		gridSizeY = Mathf.RoundToInt(gridSize.y / diameter);
		CreateGrid ();
	}

	void CreateGrid(){
		grid = new Node[gridSizeX,gridSizeY];
		Vector3 BottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;

		for (int x = 0; x < gridSizeX; x++){
			for (int y = 0; y < gridSizeY; y++){
				Vector3 worldPoint = BottomLeft + Vector3.right * (x * diameter + nodeRadius) + Vector3.forward * (y * diameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere (worldPoint, nodeRadius, unwalkableMask));
				grid [x, y] = new Node (walkable, worldPoint,x,y);
			}
		}
	}

	public List<Node> GetNeighbors (Node node)
	{
		List<Node> neighbors = new List<Node> ();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0) {
					continue;
				}
				int checkX = node.XGrid + x;
				int checkY = node.YGrid + y;

				if (checkX >= 0 && checkY < gridSizeY && checkY >= 0 && checkX < gridSizeX) {
					neighbors.Add (grid [checkX, checkY]);
				}
			}
		}
		return neighbors;
	}

	public Node NodeFromWorldPoint(Vector3 worldPos){
		float percentX = (worldPos.x + gridSize.x / 2) / gridSize.x;
		float percentY = (worldPos.z + gridSize.y / 2) / gridSize.y;
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid [x, y];
	}

	public List<Node> path;

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position,new Vector3(gridSize.x,1,gridSize.y));

		if (grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				if (path != null) {
					if (path.Contains (n)) {
						Gizmos.color = Color.black;
					}
				}
				Gizmos.DrawCube (n.worldPos, Vector3.one * (diameter - .1f));
			}
		}
	}
}
