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
        // get gameobject with tag Board
        GameObject board = GameObject.FindWithTag("Board");
        NeutreekoLogic boardScript = board.GetComponent<NeutreekoLogic>();

        // find all objects with tag Circle and destroy them
        GameObject[] circles = GameObject.FindGameObjectsWithTag("Circle");
        foreach (GameObject circle in circles){
            Destroy(circle);
        }

        // get script from board
        boardScript.makeMove(this.gameObject);
    }
}
