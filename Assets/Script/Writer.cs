using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
public class Writer : MonoBehaviour
{
    public string filename = "7x10";
    TextMeshProUGUI text;
    private void Start()
    {
        filename = Application.dataPath + "/" + filename + ".csv";
    }

    public void Write(float time)
    {

        GameObject target = GameObject.Find("WAKTU");
        text = target.GetComponent<TextMeshProUGUI>();
        text.text = time.ToString();
        TextWriter tw = new StreamWriter(filename, true);
        tw.WriteLine(time);
        tw.Close();
    }
}
