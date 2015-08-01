using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public string name;
	public int health;
	public int attack;
	public int cost;
	public int movement;
	public Player player;

	public void getHit(int damage) {
		health -= damage;
		if (health <= 0)
			die ();
	}

	public void die() {
		// TODO
		Debug.Log (name + " has died.");
	}

	public void attackUnit(Unit unit) {
		unit.getHit (attack);
		getHit (unit.attack);
	}

	public void move() {
		// TODO
	}
}
