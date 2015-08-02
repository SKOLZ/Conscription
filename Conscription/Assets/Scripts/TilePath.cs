using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TilePath {

	private List<Tile> tileList;

	public TilePath(Tile origin, int range, Tile dest) {
		tileList = new List<Tile> ();
		recursivePathSearch (origin, 0, range, tileList, dest);
	}

	public bool isEmpty() {
		return tileList.Count == 0;
	}

	public Tile getNext() {
		Tile t = tileList [0];
		tileList.RemoveAt (0);
		return t;
	}

	public int recursivePathSearch (Tile tile, int level, int range, List<Tile> path, Tile dest) {
		int found;
		if(tile == dest) {
			return 1;
		}
		if (level >= range) {
			return -1;
		}
		foreach (Tile neighbor in tile.neighbors) {
			path.Add(neighbor);
			found = recursivePathSearch(neighbor, level + 1, range, path, dest);
			if(found > 0)
				return found;
			path.Remove (neighbor); 
		}
		return -1;
	}
}
