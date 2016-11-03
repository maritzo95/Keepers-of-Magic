using UnityEngine;
using System.Collections;

public class MoveButtonClick : MonoBehaviour
{
    public Map map;

    public void Click()
    {
        map.moveMode = true;
        map.DestroyTiles();
        map.CreateTile();
    }
}
