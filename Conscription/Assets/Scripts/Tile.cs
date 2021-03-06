using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	public Vector2 gridPosition = Vector2.zero;
	public static Color mouseOverColor = Color.blue;
	public static Color highlightMoveColor = Color.green;
	public static Color defaultColor = Color.white;
	public Color colorBuffer;
	public Unit occupant;
	public List<Tile> neighbors;

	// Use this for initialization
	void Start () {
		calculateNeighbors ();
	}
	
	// Update is called once per frame
	void Update () {
		if (occupied())
			transform.GetComponent<Renderer> ().material.color = occupant.player.color;
	}

	public void calculateNeighbors() {
		neighbors = new List<Tile>();
		
		//up
		if (gridPosition.y > 0) {
			Vector2 n = new Vector2(gridPosition.x, gridPosition.y - 1);
			neighbors.Add(GameManager.instance.map[(int)Mathf.Round(n.x)][(int)Mathf.Round(n.y)]);
		}
		//down
		if (gridPosition.y < GameManager.instance.mapSize - 1) {
			Vector2 n = new Vector2(gridPosition.x, gridPosition.y + 1);
			neighbors.Add(GameManager.instance.map[(int)Mathf.Round(n.x)][(int)Mathf.Round(n.y)]);
		}		
		
		//left
		if (gridPosition.x > 0) {
			Vector2 n = new Vector2(gridPosition.x - 1, gridPosition.y);
			neighbors.Add(GameManager.instance.map[(int)Mathf.Round(n.x)][(int)Mathf.Round(n.y)]);
		}
		//right
		if (gridPosition.x < GameManager.instance.mapSize - 1) {
			Vector2 n = new Vector2(gridPosition.x + 1, gridPosition.y);
			neighbors.Add(GameManager.instance.map[(int)Mathf.Round(n.x)][(int)Mathf.Round(n.y)]);
		}
	}

	public bool occupied() {
		return occupant != null;
	}

	void OnMouseEnter() {
		colorBuffer = transform.GetComponent<Renderer> ().material.color;
		transform.GetComponent<Renderer>().material.color = mouseOverColor;
	}

	void OnMouseExit() {
		transform.GetComponent<Renderer> ().material.color = colorBuffer;
	}

	public void debugPosition() {
		Debug.Log ("[" + gridPosition.x + ", " + gridPosition.y + "]");
	}

	void OnMouseDown () {
		Unit selected = GameManager.instance.selected;
		if (selected != null) {
			if(!occupied ()) { 
				if(selected.summoned) 
					GameManager.instance.moveCurrentUnit (this);
				if(!selected.summoned && GameManager.instance.getCurrentPlayer().summoningZone.Contains(this)) {
					GameManager.instance.getCurrentPlayer().summonUnit(selected, this);
				}
			}
			if(occupied () && occupant.player != GameManager.instance.getCurrentPlayer() && inRange(GameManager.instance.selected.currentTile))
				GameManager.instance.selected.currentTile.occupant.attackUnit(occupant);
			if(occupant && GameManager.instance.getCurrentPlayer() == occupant.player) {
				GuiManager.instance.deselectUnit ();
				GameManager.instance.deselect ();
				GameManager.instance.selectUnit(occupant);
			}
		} else {
			if(occupant && GameManager.instance.getCurrentPlayer() == occupant.player)
				GameManager.instance.selectUnit(occupant);
		}
	}

	public bool inRange(Tile tile){
		if (tile == null)
			return false;
		return  (Mathf.Abs (this.gridPosition.x - tile.gridPosition.x) + Mathf.Abs (this.gridPosition.y - tile.gridPosition.y)) <= tile.occupant.range;
	}

}
