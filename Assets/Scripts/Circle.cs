using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        // find all objects with tag Circle and destroy them
        GameObject[] circles = GameObject.FindGameObjectsWithTag("Circle");
        foreach (GameObject circle in circles){
            Destroy(circle);
        }

        // get gameobject with tag Board
        GameObject board = GameObject.FindWithTag("Board");
        // get script from board
        NeutreekoLogic boardScript = board.GetComponent<NeutreekoLogic>();
        Debug.Log(this.gameObject);
        boardScript.makeMove(this.gameObject);
    }
}
