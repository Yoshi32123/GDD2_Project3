using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public A3D_World world = null;
	public BreadCrumb crumb2;
	
	// Use this for initialization
	void Start () {
		world = new A3D_World(10, 10, 10);
		Random random = new Random();
		for (int x = 0; x < world.Right; x++) {
			for (int y = 0; y < world.Top; y++) {
				for (int z = 0; z < world.Back; z++) {
					//prevent the starting square from being blocked
					if ((x + y + z) % 3 == 0 && (x + y + z) != 0)
					{
						world.MarkPosition(new A3D_Node(x, y, z), true);
					}
				}
			}
		}
		
		//BreadCrumb crumb2 = PathFinder.FindPath(world, A3D_Node.Zero, new A3D_Node(5, 8, 9));
		

	}
	
	void Update () {
		while (crumb2 != null && crumb2.next != null) {
			Debug.Log("Route: " + crumb2.next.node.ToString());
			crumb2 = crumb2.next;
		}
		//Debug.Log("Finished at: " + crumb2.node.ToString());
	}
	
	void OnGUI () {
		if (GUI.Button(new Rect(10,10,50,50), "CLICK")) {
			crumb2 = PathFinder.FindPath(world, A3D_Node.Zero, new A3D_Node(5, 1, 9));
			Debug.Log("Start: " + crumb2.node.ToString());
		}
	}
	
	void OnDrawGizmos () {
		if (world != null) {
			for (int x = 0; x < world.Right; x++) {
				for (int y = 0; y < world.Top; y++) {
					for (int z = 0; z < world.Back; z++) {
						if (world.PositionIsFree(new A3D_Node(x,y,z)))
							Gizmos.DrawWireCube(new Vector3(x, y, z), new Vector3(1, 1, 1));
						else
							Gizmos.DrawCube(new Vector3(x, y, z), new Vector3(1, 1, 1));
					}
				}
			}
			
		}
	}
}
