using UnityEngine;
using System.Collections;

public class ClickTile : MonoBehaviour {
    public int tileX;
    public int tileY;
    public Map map;
    public GameObject Tile;
    
    void OnMouseUp() {
        if (Tile.tag.Equals("Move"))
        {
            map.MoveUnitTo(tileX, tileY);
        }
        if (Tile.tag.Equals("Attack")) {
            //Attack function.
        }
    }
}
}
