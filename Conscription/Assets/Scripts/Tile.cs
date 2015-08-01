using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Vector2 gridPosition = Vector2.zero;
	public Color mouseOverColor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter() {
		transform.GetComponent<Renderer>().material.color = mouseOverColor;
	}

	void OnMouseExit() {
		transform.GetComponent<Renderer>().material.color = Color.white;
	}

	void OnMouseDown () {
		GameManager.instance.moveCurrentUnit (this);
	}

}
