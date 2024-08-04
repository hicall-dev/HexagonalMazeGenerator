using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Grid : MonoBehaviour
{
    int Rows;
    int Columns;
    System.Random random = new();
    Cell[,] grid;
    GameObject cell;
    GameObject wall;
    List<Cell> CellList = new List<Cell>();

    public void setGrid(int row, int column, GameObject TileMesh, GameObject WallMesh)
    {
        Rows = row + 2;
        Columns = column + 2;
        cell = TileMesh;
        wall = WallMesh;
        GameObject Tile = new("Tile");
        GameObject Wall = new("Wall");
        grid = PrepareGrid();
        ConfigureCells();
        ConfigureWalls();
        var Maze = gameObject.AddComponent<Algorithm>();
        Maze.CreateMaze(CellList);
    }

    public Cell[,] PrepareGrid()
    {
        grid = new Cell[Rows, Columns];
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                var tempCell = Instantiate(cell);
                grid[row, col] = tempCell.AddComponent<Cell>();
                grid[row, col].SetCell(row, col, tempCell);
            }
        }
        return grid;
    }

    public void ConfigureCells()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (GameObject.Find((row - 1) + "," + col + ""))
                    grid[row, col].West = grid[row - 1, col];
                if (GameObject.Find(+ (row + 1) + "," + col + ""))
                    grid[row, col].East = grid[row + 1, col];

                if (col % 2 != 0)
                {
                    if (GameObject.Find((row) + "," + (col + 1) + ""))
                        grid[row, col].NWest = grid[row, col + 1];
                    if (GameObject.Find((row + 1) + "," + (col + 1) + ""))
                        grid[row, col].NEast = grid[row + 1, col + 1];
                    if (GameObject.Find((row) + "," + (col - 1) + ""))
                        grid[row, col].SWest = grid[row, col - 1];
                    if (GameObject.Find((row + 1) + "," + (col - 1) + ""))
                        grid[row, col].SEast = grid[row + 1, col - 1];
                }
                else
                {
                    if (GameObject.Find((row - 1) + "," + (col + 1) + ""))
                        grid[row, col].NWest = grid[row - 1, col + 1];
                    if (GameObject.Find((row) + "," + (col + 1) + ""))
                        grid[row, col].NEast = grid[row, col + 1];
                    if (GameObject.Find((row - 1) + "," + (col - 1) + ""))
                        grid[row, col].SWest = grid[row - 1, col - 1];
                    if (GameObject.Find((row) + "," + (col - 1) + ""))
                        grid[row, col].SEast = grid[row, col - 1];
                }
                if (!IsEdge(grid[row, col])){
                    var Parent = GameObject.Find("Tile");
                    grid[row,col].transform.SetParent(Parent.transform);
                }
            }
        }
    }
    public void ConfigureWalls()
    {
        for(int row = 0; row < Rows; row++)
        {
            for(int col = 0; col < Columns; col++)
            {
                if (grid[row, col].East && !(IsEdge(grid[row, col].East) && IsEdge(grid[row, col])))
                {
                    var tempWall = Instantiate(wall);
                    var Wall = tempWall.AddComponent<Wall>();
                    Wall.SetWall(grid[row, col], grid[row, col].East, tempWall);
                }
                if (grid[row, col].NWest && !(IsEdge(grid[row, col].NWest) && IsEdge(grid[row, col])))
                {
                    var tempWall = Instantiate(wall);
                    var Wall = tempWall.AddComponent<Wall>();
                    Wall.SetWall(grid[row, col], grid[row, col].NWest, tempWall);
                    tempWall.transform.Rotate(0, 60f, 0);
                }
                if (grid[row, col].NEast && !(IsEdge(grid[row, col].NEast) && IsEdge(grid[row, col])))
                {
                    var tempWall = Instantiate(wall);
                    var Wall = tempWall.AddComponent<Wall>();
                    Wall.SetWall(grid[row, col], grid[row, col].NEast, tempWall);
                    tempWall.transform.Rotate(0, -60f, 0);
                }
            }
        }
        Path();
        DeleteCells();
    }

    public void DeleteCells()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (grid[row, col].West && IsEdge(grid[row, col].West)) 
                    grid[row, col].West = null;
                if (grid[row, col].NWest && IsEdge(grid[row, col].NWest)) 
                    grid[row, col].NWest = null;
                if (grid[row, col].SWest && IsEdge(grid[row, col].SWest))
                    grid[row, col].SWest = null;
                if (grid[row, col].East && IsEdge(grid[row, col].East)) 
                    grid[row, col].East = null;
                if (grid[row, col].NEast && IsEdge(grid[row, col].NEast)) 
                    grid[row, col].NEast = null;
                if (grid[row, col].SEast && IsEdge(grid[row, col].SEast))
                    grid[row, col].SEast = null;
                if (IsEdge(grid[row,col]))
                {
                    DestroyImmediate(grid[row, col].gameObject);
                    grid[row, col] = null;
                }
                else
                {
                    CellList.Add(grid[row, col]);
                }
            }
        }
        
    }   

    public bool IsEdge(Cell tempCell)
    {
        int row = tempCell.Row;
        int col = tempCell.Column;
        return row == 0 || row == Rows - 1 || col == 0 || col == Columns - 1;
    }

    public Cell GetCell(int row, int col)
    {
        return grid[row, col];
    }

    public void Path()
    {
        var Finish = GameObject.Find((Rows - 2) + "," + (Columns - 2));
        Finish.tag = "Finish";
        var Start = GameObject.Find(1 + "," + 1);
        var cubeRenderer = Start.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", Color.red);
        cubeRenderer = Finish.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", Color.green);
    }
}