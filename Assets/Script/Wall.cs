using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wall : MonoBehaviour
{
    public GameObject Hex1, Hex2, Mesh; 

    public void SetWall(Cell hex1, Cell hex2, GameObject wall)
    {
        Mesh = wall;
        Hex1 = hex1.Mesh;
        Hex2 = hex2.Mesh;
        BuildWall();
    }

    public void BuildWall()
    {
        var PosXHex1 = Hex1.transform.position.x;
        var PosXHex2 = Hex2.transform.position.x;
        var PosZHex1 = Hex1.transform.position.z;
        var PosZHex2 = Hex2.transform.position.z;
        var Pos = new Vector3( (PosXHex1 + PosXHex2) / 2f , 0 , (PosZHex1 + PosZHex2) / 2f);
        Mesh.transform.position = Pos;
        Mesh.name = (Hex1.name + "|" + Hex2.name);
        var Parent = GameObject.Find("Wall");
        Mesh.transform.SetParent(Parent.transform);
    }

    public void DestroyWall()
    {
        Destroy(Mesh);
    }
}
