using UnityEngine;
using System.Collections;

public class ClickUnit : MonoBehaviour 
{
    public bool selected;
    public GameObject player;
    public Map map;
    public int health;
    public int morale;
	public double maxMoveDistance;
    void OnMouseUp() 
	{
        map.ChangeUnit(player);

		print (map.getClickTiles().Length);

		for (int i = 0; i < map.getSizeX(); i++) 
		{
			for (int j = 0; j < map.getSizeY(); j++) 
			{
				ClickTile t = map.getTileOnMap (i, j);
				//within move distance
				if ((double)map.Distance((int)this.transform.position.x, (int)this.transform.position.y, (int)t.transform.position.x, (int)t.transform.position.y) < maxMoveDistance) 
				{
					t.Tile.tag = "Move";
				}
			}
		}
    }
}
