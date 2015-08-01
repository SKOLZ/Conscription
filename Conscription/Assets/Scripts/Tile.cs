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

	void OnMouseDown () {
		if (GameManager.instance.selected != null) {
			if(!occupied ()) 
				GameManager.instance.moveCurrentUnit (this);
			if(occupied () && occupant.player != GameManager.instance.getCurrentPlayer())
				GameManager.instance.selected.currentTile.occupant.attackUnit(occupant);
		} else {
			if(occupant && GameManager.instance.getCurrentPlayer() == occupant.player)
				GameManager.instance.selectUnit(occupant);
		}
	}
}
