using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
public class Menu : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void Hint()
    {
        if (GameObject.Find("Agent").GetComponent<LineRenderer>().enabled == true)
        {
            var Hint = GameObject.Find("Agent");
            var Line = Hint.GetComponent<LineRenderer>();
            Line.enabled = false;
        }
        else
        {
            var Hint = GameObject.Find("Agent");
            var Line = Hint.GetComponent<LineRenderer>();
            Line.enabled = true;
        }
    }

    public void Cam()
    {
        var Camera = GameObject.Find("Main Camera");
        if (Camera.transform.parent == null)
        {
            var Parent = GameObject.Find("Agent");
            Camera.transform.SetParent(Parent.transform);
        }
        else
        {
            Camera.transform.parent = null;
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
