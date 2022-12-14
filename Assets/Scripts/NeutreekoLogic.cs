using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutreekoLogic : MonoBehaviour
{
    public GameObject r1, r2, r3;
    public GameObject b1, b2, b3;
    public GameObject circle, selectedPiece;
    private int[,] board = new int[5,5];
    private List<List<int>> possibleMoves = new List<List<int>>();
    private int playerTurn = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int getPlayerTurn(){
        return playerTurn;
    }

    

    void updateBoard(GameObject piece)
    {
        selectedPiece = piece;
        
        // get all gameobjects with tag RedPiece and BlackPiece
        GameObject[] redPieces = GameObject.FindGameObjectsWithTag("RedPiece");
        GameObject[] blackPieces = GameObject.FindGameObjectsWithTag("BlackPiece");

        // merge redPieces and blackPieces
        GameObject[] allPieces = new GameObject[redPieces.Length + blackPieces.Length];
        redPieces.CopyTo(allPieces, 0);
        blackPieces.CopyTo(allPieces, redPieces.Length);

        // initialize 5x5 matriz padded with 0
        for (int i = 0; i < 5; i++){
            for (int j = 0; j < 5; j++){
                board[i,j] = 0;
            }
        }

        // iterate through allPieces and set board values
        foreach (GameObject _piece in allPieces){
            // get coords from getCoordsMatrix and store it in a list
            List<int> coords = getCoordsMatrix(_piece);

            int x = coords[0];
            int y = coords[1];

            if (_piece.tag == "RedPiece"){
                board[y,x] = 1;
            } else if (_piece.tag == "BlackPiece"){
                board[y,x] = 2;
            }
        }
        
    }
    
    public void generatePossibleMoves(GameObject piece)
    {
        updateBoard(piece);
        // clear possibleMoves list
        possibleMoves.Clear();

        List<int> coords = getCoordsMatrix(piece);

        checkMovesUp(coords);
        checkMovesDown(coords);
        checkMovesRight(coords);
        checkMovesLeft(coords);
        checkMovesUpRight(coords);
        checkMovesUpLeft(coords);
        checkMovesDownRight(coords);
        checkMovesDownLeft(coords);

        drawPossibleMoves();
    }

    void drawPossibleMoves()
    {
        List<float> coords3D = new List<float>();

        foreach (List<int> move in possibleMoves){
            coords3D = getInvCoordsMatrix(move);

            float x = coords3D[0];
            float y = coords3D[1];
            float z = -0.5f;

            // instantiate circle with coords x, y, z
            Instantiate(circle, new Vector3(x, y, z), Quaternion.identity);
        }
    }

    void checkWin() {
        // get all gameobjects with tag RedPiece and BlackPiece
        GameObject[] redPieces = GameObject.FindGameObjectsWithTag("RedPiece");
        GameObject[] blackPieces = GameObject.FindGameObjectsWithTag("BlackPiece");

        List<List<int>> redPiecesCoords = new List<List<int>>();
        List<List<int>> blackPiecesCoords = new List<List<int>>();

        foreach (GameObject _piece in redPieces){
            List<int> coords = getCoordsMatrix(_piece);
            redPiecesCoords.Add(coords);
        }

        foreach (GameObject _piece in blackPieces){
            List<int> coords = getCoordsMatrix(_piece);
            blackPiecesCoords.Add(coords);
        }

        if (checkRowColDiag(redPiecesCoords) != 1)
        {
            if (checkRowColDiag(blackPiecesCoords) == 1)
                Debug.Log("Black wins!");
            }
        else
            Debug.Log("Red wins!");
    }

    public int checkRowColDiag(List<List<int>> pieces)
    {
        // sort redPiecesCoords in ascending order
        pieces.Sort((a, b) => a[0].CompareTo(b[0]));
        
        if (pieces[0][0] + 1 == pieces[1][0] && pieces[1][0] + 1 == pieces[2][0])
        {
            if (pieces[0][1] == pieces[1][1] && pieces[1][1] == pieces[2][1])
                return 1;
            
            // sort redPiecesCoords in ascending order
            pieces.Sort((a, b) => a[1].CompareTo(b[1]));

            if (pieces[0][1] + 1 == pieces[1][1] && pieces[1][1] + 1 == pieces[2][1])
                return 1;
        }

        if (pieces[0][1] == pieces[1][1] && pieces[1][1] == pieces[2][1]) {
            if (pieces[0][0] + 1 == pieces[1][0] && pieces[1][0] + 1 == pieces[2][0])
                return 1;
        }

        // sort redPiecesCoords in ascending order
        pieces.Sort((a, b) => a[1].CompareTo(b[1]));

        if (pieces[0][0] == pieces[1][0] && pieces[1][0] == pieces[2][0])
            if (pieces[0][1] + 1 == pieces[1][1] && pieces[1][1] + 1 == pieces[2][1])
                return 1;

        return 0;
    }

    public void makeMove(GameObject circle)
    {
        // make selectedPiece position same as circle position
        selectedPiece.transform.position = circle.transform.position;
        playerTurn = (playerTurn + 1) % 2;
        checkWin();
    }

    List<float> getInvCoordsMatrix(List<int> coords)
    {
        List<float> newCoords = new List<float>();
        
        float x = 2.8f - 1.4f * coords[0]; //
        float y = 1.4f * coords[1] - 2.8f;

        newCoords.Add(y);
        newCoords.Add(x);
        
        return newCoords;
    }

    void checkMovesUp(List<int> coords)
    {
        int x = coords[0];
        int y = coords[1];
        List<int> newCoords = new List<int>();

        if (y == 0 || board[y-1,x] != 0)
            return;

        // increase x by 1 and check matrix in [x,y] until you found a number different than 0
        while (true){
            y -= 1;
            if (y < 0)
                break;
            else if (board[y,x] == 0)
                continue;
            else
                break;
        }

        //return list with x and y
        newCoords.Add(y + 1);
        newCoords.Add(x);

        possibleMoves.Add(newCoords);
    }

    void checkMovesDown(List<int> coords)
    {
        int x = coords[0];
        int y = coords[1];
        List<int> newCoords = new List<int>();

        if (y == 4 || board[y+1,x] != 0)
            return;

        // increase x by 1 and check matrix in [x,y] until you found a number different than 0
        while (true){
            y += 1;
            if (y > 4)
                break;
            else if (board[y,x] == 0)
                continue;
            else
                break;
        }

        //return list with x and y
        newCoords.Add(y - 1);
        newCoords.Add(x);

        possibleMoves.Add(newCoords);
    }

    void checkMovesRight(List<int> coords)
    {
        int x = coords[0];
        int y = coords[1];
        List<int> newCoords = new List<int>();

        if (x == 4 || board[y,x+1] != 0)
            return;

        // increase x by 1 and check matrix in [x,y] until you found a number different than 0
        while (true){
            x += 1;
            if (x > 4)
                break;
            else if (board[y,x] == 0)
                continue;
            else
                break;
        }

        //return list with x and y
        newCoords.Add(y);
        newCoords.Add(x - 1);

        possibleMoves.Add(newCoords);
    }

    void checkMovesLeft(List<int> coords)
    {
        int x = coords[0];
        int y = coords[1];
        List<int> newCoords = new List<int>();

        if (x == 0 || board[y,x-1] != 0)
            return;

        // increase x by 1 and check matrix in [x,y] until you found a number different than 0
        while (true){
            x -= 1;
            if (x < 0)
                break;
            else if (board[y,x] == 0)
                continue;
            else
                break;
        }

        //return list with x and y
        newCoords.Add(y);
        newCoords.Add(x + 1);

        possibleMoves.Add(newCoords);
    }

    void checkMovesUpRight(List<int> coords)
    {
        int x = coords[0];
        int y = coords[1];
        List<int> newCoords = new List<int>();

        if (x == 4 || y == 0 || board[y-1,x+1] != 0)
            return;

        // increase x by 1 and check matrix in [x,y] until you found a number different than 0
        while (true){
            y -= 1;
            x += 1;
            if (x > 4 || y < 0)
                break;
            else if (board[y,x] == 0)
                continue;
            else
                break;
        }

        //return list with x and y
        newCoords.Add(y + 1);
        newCoords.Add(x - 1);

        possibleMoves.Add(newCoords);
    }

    void checkMovesUpLeft(List<int> coords)
    {
        int x = coords[0];
        int y = coords[1];
        List<int> newCoords = new List<int>();

        if (x == 0 || y == 0 || board[y-1,x-1] != 0)
            return;

        // increase x by 1 and check matrix in [x,y] until you found a number different than 0
        while (true){
            y -= 1;
            x -= 1;
            if (x < 0 || y < 0)
                break;
            else if (board[y,x] == 0)
                continue;
            else
                break;
        }

        //return list with x and y
        newCoords.Add(y + 1);
        newCoords.Add(x + 1);

        possibleMoves.Add(newCoords);
    }
    
    void checkMovesDownRight(List<int> coords)
    {
        int x = coords[0];
        int y = coords[1];
        List<int> newCoords = new List<int>();

        if (x == 4 || y == 4 || board[y+1,x+1] != 0)
            return;

        // increase x by 1 and check matrix in [x,y] until you found a number different than 0
        while (true){
            y += 1;
            x += 1;
            if (x > 4 || y > 4)
                break;
            else if (board[y,x] == 0)
                continue;
            else
                break;
        }

        //return list with x and y
        newCoords.Add(y - 1);
        newCoords.Add(x - 1);

        possibleMoves.Add(newCoords);
    }

    void checkMovesDownLeft(List<int> coords)
    {
        int x = coords[0];
        int y = coords[1];
        List<int> newCoords = new List<int>();

        if (x == 0 || y == 4 || board[y+1,x-1] != 0)
            return;

        // increase x by 1 and check matrix in [x,y] until you found a number different than 0
        while (true){
            y += 1;
            x -= 1;
            if (x < 0 || y > 4)
                break;
            else if (board[y,x] == 0)
                continue;
            else
                break;
        }

        //return list with x and y
        newCoords.Add(y - 1);
        newCoords.Add(x + 1);

        possibleMoves.Add(newCoords);
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
