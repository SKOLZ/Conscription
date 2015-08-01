using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public Unit selected;

	public static GameManager instance;
	public GameObject tilePrefab;
	public Player[] players;
	public Text[] playerNameDisplays;
	public int currentPlayer;
	public int turnNumber;
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

	public void endTurn() {
		// TODO: ITERATE END OF TURN EFFECTS
		selected = null;
		turnNumber++;
		currentPlayer = (currentPlayer + 1) % players.Length;
		getCurrentPlayer ().addMoreMana ((turnNumber + 1) / 2);
		updateNames ();
	}
	
	public Player getCurrentPlayer() {
		return players [currentPlayer];
	}

	public void updateNames() {
		int i;
		for(i = 0; i < playerNameDisplays.Length ; i++) {
			if(i == currentPlayer)
				playerNameDisplays[i].text = players[i].activePlayerName;
			else
				playerNameDisplays[i].text = players[i].playerName;
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1))
			selected = null;
		foreach(Unit u in units)
			u.move ();
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
		selected.currentTile.occupant = null;
		selected.moveDestination = destTile.transform.position + 1.5f * Vector3.up;
		destTile.occupant = selected;
		selected.currentTile = destTile; 
	}

	public void selectUnit(Unit unit) {
		selected = unit;
		highlightPossibleMoves(unit.currentTile, unit.movement);
	}

	public void highlightPossibleMoves (Tile tile, int range) {

	}

	private void generateUnits(){
		Unit unit;
		unit = ((GameObject)Instantiate(unitPrefab, new Vector3(Mathf.Floor (mapSize/2) - 1, 1, Mathf.Floor (mapSize/2)+ 0.5f), unitPrefab.transform.rotation)).GetComponent<Unit>();
		units.Add (unit);
		map [7] [0].occupant = unit;
		unit.currentTile = map [7] [0];
		unit.player = players [0];
		unit = ((GameObject)Instantiate(unitPrefab, new Vector3(4 - Mathf.Floor(mapSize/2),1.5f, -4 + Mathf.Floor(mapSize/2)), unitPrefab.transform.rotation)).GetComponent<Unit>();
		units.Add (unit);
		map [4] [4].occupant = unit;
		unit.currentTile = map [4] [4];
		unit.player = players [1];
	}


}
