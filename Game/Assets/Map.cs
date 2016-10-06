using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
    public bool firstPlayerTurn = true;
    public GameObject selectedUnit;
    public TileType[] tileType;
    public ClickUnit cc;
    public Texture2D background;
    int[,] tiles;
    int sizeX = 9;
    int sizeY = 9;
    int tileSizeX = 200;
    int tileSizeY = 200;

    void Start() {
        //Create map tiles
        tiles = new int[sizeX, sizeY];

        //Moves tile to tile checking
        for (int x = 100; x < sizeX * tileSizeX; x += tileSizeX)
        {

            for (int y = 100; y < sizeY *tileSizeY; y += tileSizeY)
            {
                Debug.Log(background.GetPixel(x, y));
                //checks if it is green and assigns the grass tile
                if (background.GetPixel(x, y) == new Color(0,1,0,1))
                {
                    tiles[(x-100)/tileSizeX , (y-100)/tileSizeY] = 0;
                }
                //checks if it is blue and assigns the water tile
                else if (background.GetPixel(x, y) == new Color(0,0,1,1))
                {
                    tiles[(x-100)/tileSizeX, (y-100)/tileSizeY] = 1;
                }
                //checks if it is red and assigns the dirt tile
                else if (background.GetPixel(x, y) == new Color(1,0,0,1))
                {
                    tiles[(x-100)/tileSizeX, (y-100)/tileSizeY] = 2;
                }
                
            }
        }
        //Initialize map tiles
       /* for (int x = 0; x < sizeX; x++)
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
        */
        GenerateMap();
    }
    void GenerateMap() {
        //goes through the grid
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                //finds the tile type and creates a clone of it in the grid section it is assigned to and gives it an x and y coord.
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
     
            //checks  to see if it is selected
            if ((cc.selected && firstPlayerTurn && cc.tag.Equals("Player 1")) || (cc.selected && !firstPlayerTurn && cc.tag.Equals("Player 2")))
            {
                //if the distance is within three tiles horizontally or two diagonally move it
                if (Distance((int)selectedUnit.transform.position.x, (int)selectedUnit.transform.position.y, x, y) <= 3)
                    //moves the selected unit. Note the -.75 is for the unit to appear on the grid. It does not move in the z direction
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
