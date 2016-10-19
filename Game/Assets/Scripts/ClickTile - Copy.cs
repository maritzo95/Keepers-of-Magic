using UnityEngine;
using System.Collections;

public class ClickTile : MonoBehaviour {
    public int tileX;
    public int tileY;
    public Map map; 
    void OnMouseUp() {
        map.MoveUnitTo(tileX, tileY);
    }
}
