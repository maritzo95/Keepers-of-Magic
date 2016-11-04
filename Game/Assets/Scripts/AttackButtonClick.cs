using UnityEngine;
using System.Collections;

public class AttackButtonClick : MonoBehaviour
{
    public Map map;

    public void Click()
    {
        map.moveMode = false;
    }
}
