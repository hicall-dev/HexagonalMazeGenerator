using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RowCol : MonoBehaviour
{
    public void PushUp(TextMeshProUGUI text)
    {
        var i = 0;
        int.TryParse(text.text, out i);
        if( i >= 9 )
        {
            i = 0;
        } else
        {
            i++;
        }
        text.text = i.ToString(); 
    }

    public void PusDown(TextMeshProUGUI text)
    {
        var i = 0;
        int.TryParse(text.text, out i);
        if (i <= 0)
        {
            i = 9;
        }
        else
        {
            i--;
        }
        text.text = i.ToString();
    }

    public void Gen()
    {
        var row1 = GameObject.Find("Row1").GetComponent<TextMeshProUGUI>();
        var row2 = GameObject.Find("Row2").GetComponent<TextMeshProUGUI>();
        var col1 = GameObject.Find("Col1").GetComponent<TextMeshProUGUI>();
        var col2 = GameObject.Find("Col2").GetComponent<TextMeshProUGUI>();
        var row = 0;
        var col = 0;
        int.TryParse(row1.text + row2.text, out row);
        int.TryParse(col1.text + col2.text, out col);
        var Generator = GameObject.Find("TileGenerator");
        var Gen2 = Generator.GetComponent<Generate>();
        var Agent = GameObject.Find("Agent");
        var AgentTarget = Agent.GetComponent<AI>();
        AgentTarget.col = col;
        AgentTarget.row = row;
        Gen2.Set(row, col);
    }

    public void ReGen()
    {
        var Generator = GameObject.Find("TileGenerator");
        var Gen2 = Generator.GetComponent<Generate>();
        Gen2.Run();
    }
}
