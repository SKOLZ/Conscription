﻿using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public string name;
	public int health = 10;
	public int attack = 5;
	public int cost;
	public int movement;
	public Player player;
	public Vector3 moveDestination;
	public Tile currentTile;
	public float movSpeed = 5.0f;
	public bool moved;
	public bool attacked;
	public bool dead = false;
	public Sprite image;
	public bool lord;
	public TilePath path;

	void Awake () {
		moveDestination = transform.position;
	}


	public void getHit(int damage) {
		health -= damage;
		if (health <= 0)
			die ();
	}

	public void die() {
		dead = true;
		GameManager.instance.units.Remove (this);
		if (this.lord)
			this.player.lord = null;
		GameManager.instance.clearHighlightedMoves ();
		GameManager.instance.clearHighlightedAttacks ();
		Destroy (this.gameObject);
	}

	public void attackUnit(Unit unit) {
		if (attacked)
			return;
		unit.getHit (attack);
		Debug.Log (unit.name + " has attacked for " + unit.attack + " damage");
		GameManager.instance.clearHighlightedAttacks ();
		moved = true;
		attacked = true;
		getHit (unit.attack);
		GameManager.instance.checkEndGame ();
	}

	public virtual void move() {

		if (Vector3.Distance (moveDestination, transform.position) > 0.1f) {
			transform.position += (moveDestination - transform.position).normalized * movSpeed * Time.deltaTime;

			if (Vector3.Distance (moveDestination, transform.position) <= 0.2f) {
				transform.position = moveDestination;
				if(path != null && !path.isEmpty()) {
					moveDestination = path.getNext().transform.position + new Vector3(0, transform.position.y, 0);
				}
			}
		}
	}

	void OnMouseDown () {
		if(GameManager.instance.selected == null && GameManager.instance.getCurrentPlayer() == player)
			GameManager.instance.selectUnit(this);
	}
		
}
