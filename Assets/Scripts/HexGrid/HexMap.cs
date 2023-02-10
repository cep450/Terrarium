using System.Collections;
using UnityEngine;
using static HexGrid;

[ExecuteInEditMode]
public class HexMap : MonoBehaviour, IInitialize
{
	public HexGrid grid;

	[UpdateWhenChanged]
	public int size = 10;

	[UpdateWhenChanged]
	public GridShape shape = GridShape.Hexagonal;

	// Use this for initialization
	void Awake() //changed from Start to Awake -J 
	{
		Init();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Init()
	{
		var oldgrid = grid;
		switch (shape)
		{
			case GridShape.Hexagonal:
				grid = new HexGrid(HexGrid.HexagonalShape(size));
				break;
			case GridShape.Triangle:
				grid = new HexGrid(HexGrid.TriangularShape(size));
				break;
			case GridShape.Trapeze:
				grid = new HexGrid(HexGrid.TrapezoidalShape(0, size, 0, size, HexGrid.OddQToCube));
				break;
		}

		if (oldgrid != null)
		{
			grid.orientation = oldgrid.orientation;
			grid.scale = oldgrid.scale;
		}
	}

	void Reset()
	{
		Init();
	}

	void OnDrawGizmos()
	{
		var poly = grid.PolygonVertices();
		foreach (Cube cube in grid.Hexes)
		{
			ScreenCoordinate pos = grid.HexToCenter(cube);
			ScreenCoordinate first = null;
			ScreenCoordinate last = null;
			foreach (ScreenCoordinate coord in poly)
			{
				if (last == null)
				{
					first = coord + pos;
					last = coord + pos;
					continue;
				}
				ScreenCoordinate next = coord + pos;
				Gizmos.color = Color.black;
				Gizmos.DrawLine(new Vector3(last.position.x, last.position.y), new Vector3(next.position.x, next.position.y));
				last = next;
			}

			Gizmos.DrawLine(new Vector3(last.position.x, last.position.y), new Vector3(first.position.x, first.position.y));
			//zeru's code
			switch (cube.myLandscape)
			{
				case Cube.landscape.water:
					Gizmos.color = Color.blue;
					break;
				case Cube.landscape.concrete:
					Gizmos.color = Color.gray;
					break;
				case Cube.landscape.fertile:
					Gizmos.color = Color.green;
					break;
				case Cube.landscape.impassable:
					Gizmos.color = Color.red;
					break;
				case Cube.landscape.acquired:
					Gizmos.color = Color.yellow;
					break;
				default:
					Gizmos.color = Color.white;
					break;

			}
			Gizmos.DrawWireSphere(pos.position, 0.2f);
		}
	}
}
