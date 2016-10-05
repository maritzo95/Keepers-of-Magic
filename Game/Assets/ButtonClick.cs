using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour {
    public Map map;

    public void Click() {
        if (map.firstPlayerTurn)
        {
            map.firstPlayerTurn = false;
            map.secondPlayerTurn = true;
        }
        else if (map.secondPlayerTurn)
        {
            map.firstPlayerTurn = true;
            map.secondPlayerTurn = false;
        }
    }
}
