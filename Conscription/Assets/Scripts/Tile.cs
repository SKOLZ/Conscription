using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Vector2 gridPosition = Vector2.zero;
	public Color mouseOverColor;
	public Unit occupant;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool occupied() {
		return occupant != null;
	}

	void OnMouseEnter() {
		transform.GetComponent<Renderer>().material.color = mouseOverColor;
	}

	void OnMouseExit() {
		transform.GetComponent<Renderer>().material.color = Color.white;
	}

	void OnMouseDown () {
		if (GameManager.instance.selected != null) {
			if(!occupied ()) 
				GameManager.instance.moveCurrentUnit (this);
		} else {
			if(occupant && GameManager.instance.getCurrentPlayer() == occupant.player)
				GameManager.instance.selected = occupant;
		}
	}
}
