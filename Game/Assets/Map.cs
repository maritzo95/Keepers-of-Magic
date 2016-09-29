using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
    public GameObject selectedUnit;
    public TileType[] tileType;
    public ClickUnit cc;
    int[,] tiles;

    int sizeX = 10;
    int sizeY = 10;
    void Start() {
        //Create map tiles
        tiles = new int[sizeX, sizeY];

        /*
         * for(int x = 0; x < imgWidth; x += tileSizeX){
         * for(int y = 0; y < imgHeight; x += tileSizeY){
         * if(Gameobject.GetPixels(x,y).color = Color.RGB(r,g,b)
         * titles[x/tilesizeX,y/tilesizeY] = (Whatever terrian)
        */
        //Initialize map tiles
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                tiles[x, y] = 0;
            }
        }
        //mountain
        tiles[4, 4] = 1;
        tiles[3, 4] = 1;
        tiles[5, 4] = 1;
        tiles[3, 5] = 1;
        tiles[5, 5] = 1;

        //dirt
        tiles[2, 2] = 2;
        tiles[2, 3] = 2;
        tiles[3, 2] = 2;
        tiles[3, 3] = 2;

        GenerateMap();
    }
    void GenerateMap() {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                TileType tt = tileType[tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tt.tileVisual, new Vector3(x, y, 0), Quaternion.identity);
                ClickTile ct = go.GetComponent<ClickTile>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
            }
        }
    }
    public void MoveUnitTo(int x, int y) {
        if (cc.selected) { 
        if (Distance((int)selectedUnit.transform.position.x, (int)selectedUnit.transform.position.y, x, y) <= 3)
            selectedUnit.transform.position = new Vector3(x, y, (float)-0.75);
    }
}
    public void ChangeUnit(GameObject Cu) {
        cc = Cu.GetComponent<ClickUnit>();
        cc.map = this;
        selectedUnit = Cu;
        if (selectedUnit.GetComponent<Renderer>().material.color != Color.blue)
        {
            selectedUnit.GetComponent<Renderer>().material.color = Color.blue;
            cc.selected = true;
        }
        else {
            selectedUnit.GetComponent<Renderer>().material.color = Color.black;
            cc.selected = false;
        }
    }
    public double Distance(int x1, int y1, int x2, int y2) {
        double dis = 0;
        dis = Mathf.Sqrt(((x1 - x2) * (x1 - x2)) + ((y1 - y2)* (y1 - y2)));
        return dis;
    }
}
