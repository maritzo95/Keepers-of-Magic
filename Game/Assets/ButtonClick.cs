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
    }
}
