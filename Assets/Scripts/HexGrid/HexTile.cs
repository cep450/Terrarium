using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[ExecuteInEditMode]
public class HexTile : MonoBehaviour
{
	public HexMap map;
	public Vector2 position;
	public Cube myCube;
	public int myCubeIndex;
	public List<Cube> myNeighbors;
	public List<int> myNeighborsIndices;
	public Cube goal;
	bool isMoving = false;
	Queue<Cube> currentPath;
	public float movementSpeed = 0.5f;
	float timer;
	// Use this for initialization
	void Start()
	{
		timer = movementSpeed;
		Vector2 startingPos = new Vector2(Random.Range(0, map.size/2), Random.Range(0, map.size/2));
		position = startingPos;
		myCube = HexGrid.HexToCube(new HexField(position.x, position.y));
		//goal = new Cube(new Vector3(Random.Range(-map.size, map.size), Random.Range(-map.size, map.size), Random.Range(-map.size, map.size)));


		


	}

	// Update is called once per frame
	void Update()
	{
		
		if (map != null)
		{
			transform.position = map.grid.HexToCenter(myCube).position;
		}

		//zeru's code


		//Debug.Log("My cube's position is " + myCube.position);
		foreach (Cube c in map.grid.Hexes)
		{
			//Debug.Log("Cube c's index is " + map.grid.Hexes.IndexOf(c) + " and its position is " + c.position);
			if (c.position == myCube.position)
			{
				myCubeIndex = map.grid.Hexes.IndexOf(c);
				Debug.Log("my cube in the list is number " + myCubeIndex);
				break;
			}
		}
		Debug.Log("I'm at " + myCube.position);


		myNeighbors = new List<Cube>();
		myNeighborsIndices = new List<int>();
		for (int i = 0; i < 6; i++)
		{
			if (myCube.GetNeighborFromDirection(i) != null)
			{

				foreach (Cube c in map.grid.Hexes)
				{
					if (c.position == myCube.GetNeighborFromDirection(i).position)
					{
						myNeighbors.Add(c);
						myNeighborsIndices.Add(map.grid.Hexes.IndexOf(c));
						break;
					}
				}
				//Debug.Log("My " + i + "th neighbor is at " + myCube.GetNeighborFromDirection(i).position + " and its index is " + myNeighborsIndices[i]);

			}

		}
		if (Input.GetKeyDown(KeyCode.Alpha1) && !isMoving)
		{
			isMoving = true;
			currentPath = new Queue<Cube>(Search(myCube, Cube.landscape.concrete));
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) && !isMoving)
		{
			isMoving = true;
			currentPath = new Queue<Cube>(Search(myCube, Cube.landscape.water));
		}
		if (Input.GetKeyDown(KeyCode.Alpha3) && !isMoving)
		{
			isMoving = true;
			currentPath = new Queue<Cube>(Search(myCube, Cube.landscape.fertile));
		}

		if (isMoving)
		{
			if (timer<=0)
			{
				timer = movementSpeed;
				if (currentPath.Count>0)
				{
					myCube = currentPath.Dequeue();
				}
				else
				{
					Debug.Log("I have reached " + myCube.position);
					myCube.myLandscape = Cube.landscape.acquired;
					isMoving = false;
				}
				
			}
			timer -= Time.deltaTime;
		}
		
	}

	List<Cube> Search(Cube start, Cube.landscape goalLandscape)
	{
		Queue<Cube> frontier = new Queue<Cube>();
		frontier.Enqueue(start);
		Dictionary<Cube, Cube> came_from = new Dictionary<Cube, Cube>();
		came_from[start] = null;
		Cube current;
		while (frontier.Count > 0)
		{
			current = frontier.Dequeue();

			if (current.myLandscape == goalLandscape)
			{
				// do something!
				goal = current;
				Debug.Log("found goal at " + current.position + " and it's a " + goalLandscape);
				break;
			}

			foreach (Cube next in map.grid.Neighbors(current))
			{
				if (!came_from.ContainsKey(next))
				{
					frontier.Enqueue(next);
					came_from[next] = current;
				}
			}
		}

		//Retrace
		current = goal;
		List<Cube> path = new List<Cube>();
		while (current != start)
		{
			path.Add(current);
			current = came_from[current];
		}

		path.Add(start);
		path.Reverse();
		return path;
	}
	/*
	private void OnDrawGizmos()
	{
		for (int i = 0; i < 6; i++)
		{
			if (myNeighbors != null && myNeighbors.ElementAtOrDefault(i) != null)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawSphere(map.grid.HexToCenter(myNeighbors[i]).position, 0.1f);
			}

		}
	}*/
	
	
}
