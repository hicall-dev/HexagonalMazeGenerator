using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Generate : MonoBehaviour
{
    public GameObject Tile;
    public int Row, Col;
    public GameObject Wall;

    public void Set(int row, int col)
    {
        Row = row;
        Col = col;
        Run();
    }

    public void Run()
    {
        Destroy();
        var grid = gameObject.AddComponent<Grid>();
        grid.setGrid(Row, Col, Tile, Wall);
        Nav(Row,Col);
    }

    void Destroy()
    {
        if (GameObject.Find("Wall"))
        {
            var wall = GameObject.Find("Wall");
            DestroyImmediate(wall);
        }
        if (GameObject.Find("Tile"))
        {
            var tile = GameObject.Find("Tile");
            DestroyImmediate(tile);
        }
        if (GetComponent<Grid>())
        {
            DestroyImmediate(GetComponent<Grid>());
        }
        if (GetComponent<Algorithm>())
        {
            DestroyImmediate(GetComponent<Algorithm>());
        }
        if (GetComponent<NavMeshSurface>())
        {
            DestroyImmediate(GetComponent<NavMeshSurface>());
        }
    }

    void Nav(int row, int col)
    {
        var Surface = gameObject.AddComponent<NavMeshSurface>();
        if (Surface.navMeshData)
        {
            Surface.RemoveData();
        }
        Surface.BuildNavMesh();
        if(GameObject.Find("Agent"))
        {
            var other = GameObject.Find("Agent").GetComponent<AI>();
            other.row = row;
            other.col = col;
        }
    }
}