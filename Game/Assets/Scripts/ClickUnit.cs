using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickUnit : MonoBehaviour 
{
    public bool selected;
    public GameObject player;
    public Map map;
    public int health;
    public int morale;
	public double maxMoveDistance;
	public double movesLeft;
    public double attackRange;
	public Dictionary<string, int> terrainTolorances;
	public Material _grass;
	public static string grass;
	public Material _mountain;
	public static string mountain;
	public Material _dirt;
	public static string dirt;
	public Material _water;
	public static string water;

	Vector2 position;

	void Start()
	{
		movesLeft = maxMoveDistance;

		grass = "Grass (Instance)";
		mountain = "Brown Stony (Instance)";
		dirt = "Brown Stony Light (Instance)";
		water = "Water Deep Blue (Instance)";
		terrainTolorances = new Dictionary<string, int> () 
		{
				{ grass, 1 },
				{ mountain, 2 },
				{ dirt, 1 },
				{ water, 99 }
		};
	}

    void OnMouseUp() 
	{
		position = map.GetPositionFromTransform (this.transform);
        map.ChangeUnit(player);

		print (map.getClickTiles().Length);

		for (int i = 0; i < map.getSizeX(); i++) 
		{
			for (int j = 0; j < map.getSizeY(); j++) 
			{
				ClickTile t = map.getTileOnMap (i, j);
				//within move distance
				if (getPossibleMoves().Contains(t)) 
				{
					
				}
			}
		}
    }

	public ArrayList getPossibleMoves()
	{

		ArrayList possible = new ArrayList();
		ArrayList toCheckSurroundings = new ArrayList ();
		ArrayList numMoves = new ArrayList();

		toCheckSurroundings.Add (position);
		numMoves.Add (0);

		for(int n = 0; n < toCheckSurroundings.Count; n++)
		{
			Vector2 space = (Vector2)toCheckSurroundings[n];

			for (int i = 0, j = -1; i <= 1; j = (j == -1)? 1:0, i = (i == 1)? 100:i, i = (i == -1)? 1:i, i = (j == 0 && i == 0)? -1:i) 
			{
					int checkX = (int)space.x + i;
					int checkY = (int)space.y + j;
					Vector2 check = new Vector2 (checkX, checkY);
					if (map.isOnBoard (check)) 
					{
						ClickTile cT = map.getTileFromVector (check);
						if (this.terrainTolorances [cT.terrain.name] != null) 
						{
							int addedMoves = this.terrainTolorances[cT.terrain.name];
							if((int)numMoves[n] + addedMoves <= this.movesLeft)
							{
								int moveDistance = (int)numMoves [n] + addedMoves;
								if (toCheckSurroundings.Contains (check))
								{
									for (int b = toCheckSurroundings.Count - 1; b >= 0; b--) 
									{
										if (toCheckSurroundings [b].Equals (check)) 
										{
											if (moveDistance < (int)numMoves [b]) 
											{
												possible.Add (new PotentialMove (cT, moveDistance));
												toCheckSurroundings.Add (check);
												numMoves.Add (moveDistance);
											}
											b = -11;
										}
									}
								} 
								else 
								{
									possible.Add (new PotentialMove (cT, moveDistance));
									toCheckSurroundings.Add (check);
									numMoves.Add (moveDistance);
								}
							}
						}
					}	
			}
		}

		return possible;
	}

	public struct PotentialMove
	{
		public ClickTile t;
		public int moves;

		public PotentialMove(ClickTile t, int moves)
		{
			this.t = t;
			this.moves = moves;
		}
	}

}
