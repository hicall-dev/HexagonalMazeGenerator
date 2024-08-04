using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Algorithm : MonoBehaviour
{
    System.Random Random = new();
    List<Cell> Visited = new List<Cell>();
    List<Cell> Unvisited = new List<Cell>();
    string Finish;

    public void CreateMaze(List<Cell> UnvisitedList)
    {
        Finish = UnvisitedList[UnvisitedList.Count - 1].name;
        var startTime = Time.realtimeSinceStartup;
        Unvisited = UnvisitedList;
        AddVisited(GetRandom(Unvisited));
        var CurrentCell = GetRandom(Unvisited);
        while (Unvisited.Contains(CurrentCell))
        {
            List<Cell> Path = new List<Cell>() { CurrentCell };
            while (!Path.Find(Visited.Contains))
            {
                CurrentCell = GetRandom(CurrentCell.Neighbors);
                Path.Add(CurrentCell);
            }
            RemoveLoops(Path);
            foreach (var item in Path)
            {
                AddVisited(item);
            }
            for (int i = 0; i < Path.Count - 1; i++)
            {
                DestroyWallBetween(Path[i], Path[i + 1]);
            }
            if(Unvisited.Count != 0) CurrentCell = GetRandom(Unvisited);
        }
        var endTime = Time.realtimeSinceStartup;
        var elapsedTime = endTime - startTime;
        /*var Data = gameObject.GetComponent<Writer>();
        Data.Write(elapsedTime);*/
        Debug.Log("Waktu yang dibutuhkan: " + elapsedTime + " detik");
    }

    void RemoveLoops(List<Cell> Path)
    {
        for (int i = 0; i < Path.Count; i++)
        {
            if (Path.IndexOf(Path[i]) == Path.LastIndexOf(Path[i]))
            {
                continue;
            }
            Path.RemoveRange(Path.IndexOf(Path[i]), Path.LastIndexOf(Path[i]) - Path.IndexOf(Path[i]));
        }
    }

    Cell GetRandom(List<Cell> Unvisited)
    {
        var cell = Unvisited[Random.Next(Unvisited.Count)];
        return cell;
    }

    void AddVisited(Cell CurrentCell)
    {
        if (!Visited.Contains(CurrentCell))
        {
            Visited.Add(CurrentCell);
            Unvisited.Remove(CurrentCell);
        }
    }

    void DestroyWallBetween(Cell Hex1, Cell Hex2)
    {
        string wall;
        if (GameObject.Find(Hex1.name + "|" + Hex2.name))
        {
            wall = Hex1.name + "|" + Hex2.name;
        }
        else
        {
            wall = Hex2.name + "|" + Hex1.name;
        }
        var Wall = GameObject.Find(wall);
        DestroyImmediate(Wall);
    }
}
