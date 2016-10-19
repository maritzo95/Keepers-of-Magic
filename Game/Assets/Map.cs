using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
    public bool firstPlayerTurn = true;
    public GameObject selectedUnit;
    public GameObject TileMove;
    public TileType[] tileType;
    public ClickUnit cc;
    public Texture2D background;
    public GameObject[] gameObjects; 
  	private ClickTile[,] clickTiles;
    public GameObject[] player1Units;
    public GameObject[] player2Units;
    int[,] tiles;
   // int[,] moveTiles;
    int sizeX = 9;
    int sizeY = 9;
    int tileSizeX = 200;
    int tileSizeY = 200;
    int player1Morale = 100, player2Morale = 100;

    void Start() {
        //Create map tiles
        tiles = new int[sizeX, sizeY];
		clickTiles = new ClickTile[sizeX, sizeY];

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
        generateMap();
        generatePlayers();
    }
    void generateMap() {
		clickTiles = new ClickTile[sizeX,sizeY];
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
				clickTiles [x,y] = ct;
            }
        }
    }
    public void generatePlayers() {
        player1Units = GameObject.FindGameObjectsWithTag("Player 1");
        player2Units = GameObject.FindGameObjectsWithTag("Player 2");
    }
    public void MoveUnitTo(int x, int y) {
     
            //checks  to see if it is selected
            if ((cc.selected && firstPlayerTurn && cc.tag.Equals("Player 1")) || (cc.selected && !firstPlayerTurn && cc.tag.Equals("Player 2")))
            {
                //if the distance is within three tiles horizontally or two diagonally move it
                if (Distance((int)selectedUnit.transform.position.x, (int)selectedUnit.transform.position.y, x, y) <= 3)
                    //moves the selected unit. Note the -.75 is for the unit to appear on the grid. It does not move in the z direction
                    selectedUnit.transform.position = new Vector3(x, y, (float)-0.75);
            DestroyTiles();
            cc.selected = false;
            }

     
}
    public void ChangeUnit(GameObject Cu) {
        cc = Cu.GetComponent<ClickUnit>();
        cc.map = this;
        selectedUnit = Cu;
        if (cc.selected)
        {
            cc.selected = false;
            DestroyTiles();
        }
        else
        {
            cc.selected = true;
            DestroyTiles();
            CreateTile();
        }
    }
    public double Distance(int x1, int y1, int x2, int y2) {
        double dis = 0;
        dis = Mathf.Sqrt(((x1 - x2) * (x1 - x2)) + ((y1 - y2)* (y1 - y2)));
        return dis;
    }
    public void DestroyTiles() {
        gameObjects = GameObject.FindGameObjectsWithTag("Move");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
    public void CreateTile() {//change so one doesnt spawn on other objects.
        if ((cc.selected && firstPlayerTurn && cc.tag.Equals("Player 1")) || (cc.selected && !firstPlayerTurn && cc.tag.Equals("Player 2")))
        {
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (Distance((int)selectedUnit.transform.position.x, (int)selectedUnit.transform.position.y, x, y) <= cc.maxMoveDistance && Distance((int)selectedUnit.transform.position.x, (int)selectedUnit.transform.position.y, x, y) > 0 && tileOpen(clickTiles[x,y]))
                    {
                        //Debug.Log("X =" + x + "Y=" + y);
                        GameObject go = (GameObject)Instantiate(TileMove, new Vector3(x, y, (float)-.5), Quaternion.identity);
                        ClickTile ct = go.GetComponent<ClickTile>();
                        ct.tileX = x;
                        ct.tileY = y;
                        ct.map = this;

                    }
                }
            }
        }
    /* for (int x = 0; x < moveTiles.Length; x++) {
            for (int y = 0; y < moveTiles.Length; y++)
            {
                if (moveTiles[x, y] == 1)
                    Instantiate(TileMove, new Vector3(x, y, (float)-.5), Quaternion.identity); 
            }
        }
     */
    }
    public void moraleChange(ClickUnit cu) {
        if (cu.tag.Equals("Player1")) {
            player1Morale -= cu.morale;
        }
        if (cu.tag.Equals("Player2"))
        {
            player2Morale -= cu.morale;
        }
    }
    public bool tileOpen(ClickTile ct) {
        bool open = true;
        // search through player units then if x and y are same then not clickable.'
        for (int i = 0; i < player1Units.Length; i++)
        {
            if (player1Units[i].transform.position.x == ct.transform.position.x && player1Units[i].transform.position.y == ct.transform.position.y)
            {
                open = false;
            }
            if (player2Units[i].transform.position.x == ct.transform.position.x && player2Units[i].transform.position.y == ct.transform.position.y)
            {
                open = false;
            }
        }
        return open;
    }

	public ClickTile getTileOnMap(int x, int y)
	{
		return clickTiles [x, y];
	}

	public ClickTile[,] getClickTiles()
	{
		return clickTiles;
	}

	public int getSizeX()
	{
		return sizeX;
	}

	public int getSizeY()
	{
		return sizeY;
	}
}
