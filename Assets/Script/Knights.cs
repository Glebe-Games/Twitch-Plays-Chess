using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knights : ChessMan {
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        if (!BoardManager.checkForChecking)
        {
            //UpLeft
            KnightMove(CurrentX - 1, CurrentY + 2, ref r);

            //UpRight
            KnightMove(CurrentX + 1, CurrentY + 2, ref r);

            //RightUp
            KnightMove(CurrentX + 2, CurrentY + 1, ref r);

            //RightDown
            KnightMove(CurrentX + 2, CurrentY - 1, ref r);

            //DownLeft
            KnightMove(CurrentX - 1, CurrentY - 2, ref r);

            //DownRight
            KnightMove(CurrentX + 1, CurrentY - 2, ref r);

            //LeftUp
            KnightMove(CurrentX - 2, CurrentY + 1, ref r);

            //LeftDown
            KnightMove(CurrentX - 2, CurrentY - 1, ref r);
        }
        return r;
    }

    public void KnightMove(int x, int y,ref bool[,] r)
    {
        ChessMan c;
        if(x >= 0 && x< 8 && y>= 0 && y < 8)
        {
            c = BoardManager.Instance.chessMans[x, y];
            if (c == null)
                r[x, y] = true;
            else if (isWhite != c.isWhite)
            {
                r[x, y] = true;
            }
            else if (BoardManager.checkForKing)
            {
                r[x, y] = true;
            }
        }
    }
}
