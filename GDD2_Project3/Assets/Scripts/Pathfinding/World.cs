using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class A3D_Node {
	public int X;
	public int Y;
	public int Z;
	
	public A3D_Node(int X, int Y, int Z) {
		this.X = X;
		this.Y = Y;
		this.Z = Z;
	}
	
	public int GetDistanceSquared(A3D_Node node) {
		int dx = this.X - node.X;
		int dy = this.Y - node.Y;
		int dz = this.Z - node.Z;
		return (dx * dx) + (dy * dy) + (dz * dz);            
	}
	
	public override bool Equals(object obj) {            
		if (obj is A3D_Node) {
			A3D_Node node = (A3D_Node)obj;
			return (node.X == this.X && node.Z == this.Z && node.Y == this.Y);
		}
		return false;
	}
	
	public override int GetHashCode() {
		return (X + " " + Y + " " + Z).GetHashCode();
	}
	
	public override string ToString() {
		return X + ", " + Y + ", " + Z;
	}
	
	public static bool operator ==(A3D_Node one, A3D_Node two) {
		return one.Equals(two);
	}

	public static bool operator !=(A3D_Node one, A3D_Node two) {
		return !one.Equals(two);
	}

	public static A3D_Node operator +(A3D_Node one, A3D_Node two) {
		return new A3D_Node(one.X + two.X, one.Y + two.Y, one.Z + two.Z);
	}

	public static A3D_Node operator -(A3D_Node one, A3D_Node two) {
		return new A3D_Node(one.X - two.X, one.Y - two.Y, one.Z - two.Z);
	}

	public static A3D_Node Zero = new A3D_Node(0, 0, 0);
}

public class A3D_World {

	private bool[, ,] worldBlocked;
	
	public int Left { get { return 0; } }
	public int Right { get { return worldBlocked.GetLength(0); } }
	public int Bottom { get { return 0; } }
	public int Top { get { return worldBlocked.GetLength(1); } }
	public int Front { get { return 0; } }
	public int Back { get { return worldBlocked.GetLength(2); } }
	
	public A3D_World(int width, int height) : this(width, height, 1) { }
	
	public A3D_World(int width, int height, int depth) {
		worldBlocked = new bool[width, height, depth];
	}
	
	public void MarkPosition(A3D_Node node, bool value) {
		worldBlocked[node.X, node.Y, node.Z] = value;
	}
	
	public bool PositionIsFree(A3D_Node node) {
		return node.X >= 0 && node.X < worldBlocked.GetLength(0) &&
					node.Y >= 0 && node.Y < worldBlocked.GetLength(1) &&
					node.Z >= 0 && node.Z < worldBlocked.GetLength(2) &&
					!worldBlocked[node.X, node.Y, node.Z];            
	}
}

public class BreadCrumb : System.IComparable<BreadCrumb> {
	public A3D_Node node;
	public BreadCrumb next;
	public int cost = System.Int32.MaxValue;
	public bool onClosedList = false;
	public bool onOpenList = false;
	
	public BreadCrumb(A3D_Node node) {
		this.node = node;
	}

	public BreadCrumb(A3D_Node node, BreadCrumb parent) {
		this.node = node; 
		this.next = parent;
	}
	
	public override bool Equals(object obj) {
		return (obj is BreadCrumb) && ((BreadCrumb)obj).node == this.node;
	}
	
	public override int GetHashCode() {
		return node.GetHashCode();
	}
	
	public int CompareTo(BreadCrumb other) {
		return cost.CompareTo(other.cost);
	}
}

public static class PathFinder {
	public static BreadCrumb FindPath(A3D_World world, A3D_Node start, A3D_Node end) {
	//note we just flip start and end here so you don't have to.            
		return FindPathReversed(world, end, start); 
	}
	
	private static BreadCrumb FindPathReversed(A3D_World world, A3D_Node start, A3D_Node end) {            
		MinHeap<BreadCrumb> openList = new MinHeap<BreadCrumb>(256);            
		BreadCrumb[, ,] brWorld = new BreadCrumb[world.Right, world.Top, world.Back];
		BreadCrumb node;
		A3D_Node tmp;
		int cost;
		int diff;
		
		BreadCrumb current = new BreadCrumb(start);
		current.cost = 0;

		BreadCrumb finish = new BreadCrumb(end);
		brWorld[current.node.X, current.node.Y, current.node.Z] = current;
		openList.Add(current);
		
		
		while (openList.Count > 0) {                
			//Find best item and switch it to the 'closedList'
			current = openList.ExtractFirst();                                                
			current.onClosedList = true;                

			//Find neighbours
			for (int i = 0; i < surrounding.Length; i++) {
				tmp = current.node + surrounding[i];
				if (world.PositionIsFree(tmp)) {       
					//Check if we've already examined a neighbour, if not create a new node for it.
					if (brWorld[tmp.X, tmp.Y, tmp.Z] == null) {
						node = new BreadCrumb(tmp);
						brWorld[tmp.X, tmp.Y, tmp.Z] = node;
					}
					else {
						node = brWorld[tmp.X, tmp.Y, tmp.Z];
					}

					//If the node is not on the 'closedList' check it's new score, keep the best
					if (!node.onClosedList) {                            
						diff = 0;
						if (current.node.X != node.node.X) {
							diff += 1;
						}
						if (current.node.Y != node.node.Y) {
							diff += 1;
						}
						if (current.node.Z != node.node.Z) {
							diff += 1;
						}
						cost = current.cost + diff + node.node.GetDistanceSquared(end);

						if (cost < node.cost) {
							node.cost = cost;
							node.next = current;
						}

						//If the node wasn't on the openList yet, add it 
						if (!node.onOpenList) {
							//Check to see if we're done
							if (node.Equals(finish)) {
								node.next = current;
								return node;
							}
							node.onOpenList = true;
							openList.Add(node);
						}                            
					}
				}
			}
		}
		return null; //no path found
	}
	
	//Neighbour options
	private static A3D_Node[] surrounding = new A3D_Node[]{                   
		//Top slice (Y=1)
		new A3D_Node(-1,1,1), new A3D_Node(0,1,1), new A3D_Node(1,1,1),
		new A3D_Node(-1,1,0), new A3D_Node(0,1,0), new A3D_Node(1,1,0),
		new A3D_Node(-1,1,-1), new A3D_Node(0,1,-1), new A3D_Node(1,1,-1),
		//Middle slice (Y=0)
		new A3D_Node(-1,0,1), new A3D_Node(0,0,1), new A3D_Node(1,0,1),
		new A3D_Node(-1,0,0), new A3D_Node(1,0,0), //(0,0,0) is self
		new A3D_Node(-1,0,-1), new A3D_Node(0,0,-1), new A3D_Node(1,0,-1),
		//Bottom slice (Y=-1)
		new A3D_Node(-1,-1,1), new A3D_Node(0,-1,1), new A3D_Node(1,-1,1),
		new A3D_Node(-1,-1,0), new A3D_Node(0,-1,0), new A3D_Node(1,-1,0),
		new A3D_Node(-1,-1,-1), new A3D_Node(0,-1,-1), new A3D_Node(1,-1,-1)
	};
}
