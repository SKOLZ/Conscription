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
	public List<List<Tile>> map = new List<List<Tile>>();
	public GuiManager guiManager;
	public float roundTime;
	public GameObject uimPrefab;

	private float currentRoundTime;
	private List<Tile> possibleMoveTiles = new List<Tile>();
	private List<Tile> possibleAtgtacksTiles = new List<Tile>();
	public List <Unit> units = new List<Unit>();
	public bool end = false;
	public GameObject endGamePanel;

	public GameObject[] benchedUnits;

	void Awake (){
		instance = this;
	}
	void Start () {
		generateMap ();
		generateUnits ();
		restartRoundTimer ();
		getCurrentPlayer().addMoreMana (1);
		for (int i = 0; i < players.Length; i++) {
			guiManager.setPlayerMana (i, players[i].mana);
		}
		guiManager.updateBench (getCurrentPlayer ().benchedUnits);
	}

	public void endTurn() {
		// TODO: ITERATE END OF TURN EFFECTS
		deselect ();
		getCurrentPlayer ().clearSummonZone ();
		turnNumber++;
		currentPlayer = (currentPlayer + 1) % players.Length;
		getCurrentPlayer ().addMoreMana ((turnNumber + 1) / 2);
		guiManager.updateBench (getCurrentPlayer ().benchedUnits);
		guiManager.setPlayerMana (currentPlayer, getCurrentPlayer ().mana);
		clearMovements ();
		clearHighlightedMoves ();
		clearHighlightedAttacks ();
		updateNames ();
		restartRoundTimer ();
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
		currentRoundTime -= Time.deltaTime;
		if (currentRoundTime > 0) {
			guiManager.updateGuiTimer(currentRoundTime);
			if (Input.GetMouseButtonDown (1))
				deselect ();
			foreach (Unit u in units) {
				u.move ();
			}
		} else {
			endTurn ();
		}
	}

	public void deselect() {
		if (selected != null && selected.portrait != null)
			selected.portrait.GetComponent<Image> ().color = Color.black;
		guiManager.deselectUnit ();
		getCurrentPlayer ().clearSummonZone ();
		clearHighlightedMoves ();
		clearHighlightedAttacks ();
		selected = null;
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
			if(i == 0)
				players[0].summoningZone = row;
			if(i == (mapSize - 1))
				players[1].summoningZone = row;
			map.Add(row);
		}
	}

	public void moveCurrentUnit(Tile destTile){	
		if (selected.moved || !possibleMoveTiles.Contains (destTile))
				return;
		TilePath path = new TilePath (selected.currentTile, selected.movement, destTile);
		selected.currentTile.occupant = null;
		selected.currentTile.transform.GetComponent<Renderer>().material.color = Tile.defaultColor;
		selected.path = path;
		selected.moveDestination = path.getNext().transform.position + new Vector3(0, selected.transform.position.y, 0);
		destTile.occupant = selected;
		destTile.transform.GetComponent<Renderer>().material.color = selected.player.color;
		selected.currentTile = destTile;
		selected.moved = true;
		deselect ();
	}

	public void selectUnit(Unit unit) {
		clearHighlightedMoves ();
		clearHighlightedAttacks ();
		getCurrentPlayer ().clearSummonZone ();
		selected = unit;
		guiManager.selectUnit (unit);
		if (!selected.moved)
			highlightPossibleMoves (unit.currentTile, unit.movement);
		else if (!selected.attacked)
			highlightPossibleAttacks (unit.currentTile, unit.range);
	}

	public void clearHighlightedMoves() {
		foreach (Tile tile in possibleMoveTiles) {
			tile.transform.GetComponent<Renderer>().material.color = Tile.defaultColor;
			tile.colorBuffer = Tile.defaultColor;
		}
	}

	public void clearHighlightedAttacks() {
		foreach (Tile tile in possibleAtgtacksTiles) {
			tile.transform.GetComponent<Renderer>().material.color = Tile.defaultColor;
			tile.colorBuffer = Tile.defaultColor;
		}
	}

	public void highlightPossibleAttacks(Tile tile, int range) {
		possibleMoveTiles.Clear ();
		recursiveAttackTileSet (tile, 0, range, possibleAtgtacksTiles);
	}
	
	public void recursiveAttackTileSet (Tile tile, int level, int range, List<Tile> list) {
		if (level >= range)
			return;
		foreach (Tile neighbor in tile.neighbors) {
			neighbor.transform.GetComponent<Renderer>().material.color = Color.red;
			list.Add(neighbor);
			recursiveAttackTileSet(neighbor, level + 1, range, list);
		}
	}

	public void highlightPossibleMoves (Tile tile, int range) {
		possibleMoveTiles.Clear ();
		if(selected.summoned)
			recursiveTileSet (tile, 0, range, possibleMoveTiles);
	}

	public void recursiveTileSet(Tile tile, int level, int range, List<Tile> list) {
		if (level >= range)
			return;
		foreach (Tile neighbor in tile.neighbors) {
			if(!neighbor.occupied()) {
				neighbor.transform.GetComponent<Renderer>().material.color = Color.green;
				list.Add(neighbor);
				recursiveTileSet(neighbor, level + 1, range, list);
			}
		}
	}

	private void generateUnits(){
		for (int i = 0; i < players.Length; i++)
			generateBench (players [i]);
		Unit unit;
		UnitInfoManager uim;
		unit = ((GameObject)Instantiate(unitPrefab, new Vector3(- Mathf.Floor(mapSize/2), unitPrefab.transform.position.y, - Mathf.Floor(mapSize/2)+1), unitPrefab.transform.rotation)).GetComponent<Unit>();
		uim = ((GameObject)Instantiate(uimPrefab, new Vector3(- Mathf.Floor(mapSize/2), unitPrefab.transform.position.y + 1f, - Mathf.Floor(mapSize/2)+1), uimPrefab.transform.rotation)).GetComponent<UnitInfoManager>();
		uim.target = unit;
		units.Add (unit);
		map [0] [7].occupant = unit;
		unit.currentTile = map [0] [7];
		unit.player = players [0];
		unit.summoned = true;
		players [0].lord = unit;
		unit = ((GameObject)Instantiate(unitPrefab, new Vector3(Mathf.Floor (mapSize/2) - 1, unitPrefab.transform.position.y, Mathf.Floor (mapSize/2)), unitPrefab.transform.rotation)).GetComponent<Unit>();
		uim = ((GameObject)Instantiate(uimPrefab, new Vector3(Mathf.Floor (mapSize/2) - 1, unitPrefab.transform.position.y + 1f, Mathf.Floor (mapSize/2)), uimPrefab.transform.rotation)).GetComponent<UnitInfoManager>();
		uim.target = unit;
		units.Add (unit);
		map [7] [0].occupant = unit;
		unit.currentTile = map [7] [0];
		unit.player = players [1];
		unit.summoned = true;
		players [1].lord = unit;
	}

	private void generateBench(Player player) {
		foreach(GameObject benchedUnit in benchedUnits) {
			Unit unit;
			unit = ((GameObject)Instantiate(benchedUnit, new Vector3(999, benchedUnit.transform.position.y, 999), unitPrefab.transform.rotation)).GetComponent<Unit>();
			player.benchedUnits.Add(unit);
			unit.player = player;
		}
	}

	public void clearMovements() {
		foreach (Unit u in units) {
			u.moved = false;
			u.attacked = false;
		}
	}

	public void checkEndGame() {
		Text winnerLabel = endGamePanel.transform.FindChild ("winnerLabel").gameObject.GetComponent<Text>();
		if (players [0].lord == null && players [1].lord == null) {
			winnerLabel.text = "It's a Draw!";
			SFXManager.instance.playGameEnd();
			endGamePanel.SetActive (true);
		} else if (players [0].lord == null && players [1].lord != null) {
			winnerLabel.text = "PLAYER 2 WINS!";
			SFXManager.instance.playGameEnd();
			endGamePanel.SetActive (true);
		} else if (players [0].lord != null && players [1].lord == null) {
			winnerLabel.text = "PLAYER 1 WINS!";
			SFXManager.instance.playGameEnd();
			endGamePanel.SetActive (true);
		}
	}

	private void restartRoundTimer() {
		currentRoundTime = roundTime;
		guiManager.updateGuiTimer (currentRoundTime);
	}

}
