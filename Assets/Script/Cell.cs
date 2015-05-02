using UnityEngine;
using System.Collections;

public class Cell  {

	Office office;
	public bool wallNorth, wallEast,wallSouth,wallWest;
	int posX,posY;
	public bool check=false;
    public bool locked = false;
	public enum CellType {Bathroom,Bossroom,Coffeeroom,Corridor, Elevator,Box};

	public CellType cellType;

	// Use this for initialization
	public Cell(Office office,int posX,int posY,CellType type ){
		this.office = office;
		
		this.posX = posX;
		this.posY = posY;
		this.cellType = type;
		wallNorth = wallEast = wallSouth = wallWest = false;

	}

	public void init(Office office,int posX,int posY, bool north,bool east,bool south,bool west,CellType type ){
		this.office = office;
		
		this.posX = posX;
		this.posY = posY;

		this.wallNorth = north;
		this.wallEast = east;
		this.wallSouth = south;
		this.wallWest = west;
	}

	public void init(Office office,int posX,int posY,CellType type ){
		this.office = office;

		this.posX = posX;
		this.posY = posY;
		this.cellType = type;		
	}

}