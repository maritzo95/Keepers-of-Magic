using UnityEngine;
using System.Collections;

public class ClickUnit : MonoBehaviour {
    public bool selected;
    public GameObject player;
    public Map map;
    public int health;
    void OnMouseUp() {
        map.ChangeUnit(player);
    }
}
