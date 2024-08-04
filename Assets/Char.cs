using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Char : MonoBehaviour
{
    CharacterController Chara;
    public float Speed = 5f;
    // Start is called before the first frame update

    void Start()
    {
        Chara = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Chara.Move(move * Time.deltaTime * Speed);
    }

    void OnTriggerStay (Collider Cell)
    {
        if (Cell.gameObject.CompareTag("Finish"))
        {
            var Finish = GameObject.Find("Canvas").transform.Find("Finish Menu");
            var Menu = GameObject.Find("Canvas").transform.Find("InGame Menu");
            Finish.gameObject.SetActive(true);
            Menu.gameObject.SetActive(false);
            var Hint = GameObject.Find("Agent");
            var Line = Hint.GetComponent<LineRenderer>();
            Line.enabled = false;
        }
    }

    public void ResetPos()
    {
        var cc = GetComponent<CharacterController>();
        var Agent = GetComponent<NavMeshAgent>();
        /*GameObject Start = GameObject.Find("1,1");*/
        cc.enabled = false;
        var Pos = GameObject.Find("1,1").transform.position;
        transform.position = Pos;
        Agent.Warp(Pos);
        cc.enabled = true;
    }
}
