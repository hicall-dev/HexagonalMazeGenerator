using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cell : MonoBehaviour
{
    public int Row;
    public int Column;
    public GameObject Mesh;
    private MeshRenderer render;
    private Vector3 size;
    private string CellPos;
    public Cell West, NWest, SWest, East, NEast, SEast;

    public List<Cell> Neighbors
    {
       get
        {
            var list = new List<Cell>();
            if (West != null) list.Add(West);
            if (NWest != null) list.Add(NWest);
            if (SWest != null) list.Add(SWest);
            if (East != null) list.Add(East);
            if (NEast != null) list.Add(NEast);
            if (SEast != null) list.Add(SEast);
            return list;
        }
    }

    public void SetCell(int row, int column, GameObject cell)
    {
        Row = row;
        Column = column;
        Mesh = cell;
        render = Mesh.GetComponent<MeshRenderer>();
        size = render.bounds.size;
        BuildCell(Row, Column);
    }

    public void SetPos(float row, float col)
    {
        CellPos = row + "," + col;
    }

    public void BuildCell(int x, int z)
    {
        float xPos;
        float zPos;
        if (z % 2 == 0)
        {
            xPos = x * size.x;
            zPos = z * (size.z * 0.75f);
        }
        else
        {
            xPos = x * size.x + size.x / 2f;
            zPos = z * (size.z * 0.75f);
        }
        Mesh.transform.position = new Vector3(xPos, 0, zPos);
        SetPos(x, z);
        Mesh.name = x.ToString() + "," + z.ToString();
    }
}
