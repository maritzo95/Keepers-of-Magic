using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour {
    public Map map;

    public void Click() {
        if (map.firstPlayerTurn)
        {
            map.firstPlayerTurn = false;
        }
        else if (!map.firstPlayerTurn)
        {
            map.firstPlayerTurn = true;
        }
        map.DestroyTiles();

		foreach (GameObject g in map.player1Units) 
		{
			ClickUnit u = g.GetComponent<ClickUnit> ();
			u.movesLeft = u.maxMoveDistance;
		}
		foreach (GameObject g in map.player2Units) 
		{
			ClickUnit u = g.GetComponent<ClickUnit> ();
			u.movesLeft = u.maxMoveDistance;
		}
    }
}
