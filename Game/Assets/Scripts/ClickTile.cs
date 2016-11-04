﻿using UnityEngine;
using System.Collections;

public class ClickTile : MonoBehaviour {
    public int tileX;
    public int tileY;
    public Map map;
    public GameObject Tile;
	public Transform trans;
	public Material terrain;
	public ClickUnit containedUnit;
	public int movesTo;

	void Start()
	{
		trans = this.GetComponent<Transform> ();
		terrain = (this.GetComponent<MeshRenderer> ()).material;
	}

    void OnMouseUp() {
        if (!Tile.Equals(null))
        {
            if (Tile.tag.Equals("Move"))
            {
				map.MoveUnitTo(tileX, tileY);
				map.cc.movesLeft -= movesTo;
            }
            if (Tile.tag.Equals("Attack"))
            {
                //Attack function.
            }
        }
    }

	public bool IsOccupied()
	{
		if (containedUnit != null) 
		{
			return true;
		}

		return false;
	}

}

