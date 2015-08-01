using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public GameObject tilePrefab;
	public GameObject unitPrefab;
	public int mapSize = 8; 
	private List<List<Tile>> map = new List<List<Tile>>();
	List <Unit> units = new List<Unit>();

	void Awake (){
		instance = this;
	}
	void Start () {
		generateMap ();
		generateUnits ();
	}
	
	// Update is called once per frame
	void Update () {
		units [0].move ();
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

	public void moveCurrentUnit(Tile destTile){
		units[0].moveDestination = destTile.transform.position + 1.5f * Vector3.up;
	}


	private void generateUnits(){
		Unit unit;
		unit = ((GameObject)Instantiate(unitPrefab, new Vector3(Mathf.Floor (mapSize/2), 2, Mathf.Floor (mapSize/2)+ 0.5f), unitPrefab.transform.rotation)).GetComponent<Unit>();
		units.Add (unit);
	}


}
