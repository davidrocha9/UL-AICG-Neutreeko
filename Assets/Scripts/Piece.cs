using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
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
        boardScript.generatePossibleMoves(this.gameObject);
    }

    void returnPossibleMoves()
    {
        /*List<int> coords = getCoordsMatrix(piece);

        int x = coords[0];
        int y = coords[1];*/

        
    }

    List<int> getCoordsMatrix(GameObject piece)
    {
        int x = (int) (piece.transform.position.x / 1.4f + 2.0f);
        int y = (int) (4 - (piece.transform.position.y + 2.8f) / 1.4f);

        // return x and y in a list
        List<int> coords = new List<int>();
        coords.Add(x);
        coords.Add(y);

        return coords;
    }
}
