using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject tilePrefab;
	public int mapSize = 8; 
	private List<List<Tile>> map = new List<List<Tile>>();


	void Start () {
		generateMap ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void generateMap() {
		map = new List<List<Tile>> ();
		for (int i = 0; i < mapSize; i++) {
			List<Tile> row = new List<Tile> ();
			for (int j = 0; j < mapSize; j++) {
				Tile tile = ((GameObject)Instantiate(tilePrefab, new Vector3(i - Mathf.Floor (mapSize/2), 0, -j + Mathf.Floor (mapSize/2)), Quaternion.Euler (new Vector3 ()))).GetComponent<Tile>();
				tile.gridPosition = new Vector2(i,j);
				row.Add(tile);
			}
			map.Add(row);
		}
	}
}
