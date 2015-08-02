using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Unit : MonoBehaviour {

	public string name;
	public int health = 10;
	public int attack = 5;
	public int cost;
	public int movement;
	public int range;
	public Player player;
	public Vector3 moveDestination;
	public Tile currentTile;
	public float movSpeed = 5.0f;
	public bool moved;
	public bool attacked;
	public bool dead = false;
	public bool summoned = false;
	public Sprite image;
	public bool lord;
	public TilePath path;
	public UnitPortrait portrait;

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
		if (this.lord) {
			this.player.lord = null;
		}
		GameManager.instance.clearHighlightedMoves ();
		GameManager.instance.clearHighlightedAttacks ();
		GameManager.instance.units.Remove (this);
		Destroy (this.gameObject);
	}

	public void attackUnit(Unit unit) {
		if (attacked)
			return;
		unit.getHit (attack);
		Debug.Log (this.name + " has attacked for " + this.attack + " damage");
		GameManager.instance.clearHighlightedAttacks ();
		moved = true;
		attacked = true;
		GameManager.instance.clearHighlightedMoves ();
		GameManager.instance.clearHighlightedAttacks ();
		if (inAttackRange (unit)) {
			getHit (unit.attack);
			Debug.Log (unit.name + " has attacked for " + unit.attack + " damage");
		}
		GameManager.instance.checkEndGame ();
		GameManager.instance.deselect();
		GuiManager.instance.deselectUnit();

	}

	public bool inAttackRange(Unit unit){
		return  (Mathf.Abs (this.currentTile.gridPosition.x - unit.currentTile.gridPosition.x) + Mathf.Abs (this.currentTile.gridPosition.y - unit.currentTile.gridPosition.y)) <= unit.range;
		
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
