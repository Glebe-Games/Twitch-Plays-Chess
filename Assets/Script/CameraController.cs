using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject chessBoard;
    private BoardManager variables;
    private float move = -2f;
    private float rotate = 0f;
    public float speed;
    private int mode;

    void Start()
    {
        variables = chessBoard.GetComponent<BoardManager>();
    }
    void LateUpdate() {
        mode = modePicker.whichMode;
        if (mode == 3 || mode == 4)
        {
            transform.position = new Vector3(0, 9, move);
            transform.eulerAngles = new Vector3(60, rotate);
        }
        else if(mode == 1)
        {
            transform.position = new Vector3(4, 9, move);
            transform.eulerAngles = new Vector3(60, rotate);
        }
        else{
            transform.position = new Vector3(4, 9, move);
            transform.eulerAngles = new Vector3(60, rotate);
            if (variables.isWhiteTurn)
            {
                if (move > -2)
                    move -= 0.1f * speed;
                else
                    move = -2;
                if (rotate > 0)
                    rotate -= 1.6f * speed;
                else
                    rotate = 0;
            }
            if (!variables.isWhiteTurn)
            {
                if (move < 11)
                    move += 0.1f * speed;
                else
                    move = 11;
                if (rotate < 180)
                    rotate += 1.6f * speed;
                else
                    rotate = 180;
            }
        }
    }
}
