using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public string name;
	public int health;
	public int attack;
	public int cost;
	public int movement;
	public Player player;
	public Vector3 moveDestination;
	public Tile currentTile;
	public float movSpeed = 10.0f;
	public bool moved; 

	void Awake () {
		moveDestination = transform.position;
	}


	public void getHit(int damage) {
		health -= damage;
		if (health <= 0)
			die ();
	}

	public void die() {
		// TODO
		Debug.Log (" has died.");
	}

	public void attackUnit(Unit unit) {
		unit.getHit (attack);
		Debug.Log ("hash attacked");
		moved = true;
		getHit (unit.attack);
	}

	public virtual void move() {

		if (Vector3.Distance (moveDestination, transform.position) > 0.1f) {
			transform.position += (moveDestination - transform.position).normalized * movSpeed * Time.deltaTime;

			if (Vector3.Distance (moveDestination, transform.position) <= 0.1f)
				transform.position = moveDestination;
		}
	}

	void OnMouseDown () {
		if(GameManager.instance.selected == null && GameManager.instance.getCurrentPlayer() == player)
			GameManager.instance.selectUnit(this);
	}
		
}
