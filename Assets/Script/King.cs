using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessMan {
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
            
        ChessMan c,c2;
        int i, j;
        


        // Top Side
        i = CurrentX - 1;
        j = CurrentY + 1;
        if(CurrentY != 7)
        {
            for(int k = 0; k < 3; k++)
            {
                if (i >= 0 && i < 8)
                {
                    c = BoardManager.Instance.chessMans[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }
                i++;
            }
        }

        // Down Side
        i = CurrentX - 1;
        j = CurrentY - 1;
        if (CurrentY != 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 && i < 8)
                {
                    c = BoardManager.Instance.chessMans[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }
                i++;
            }
        }

        
            //Middle Left
            if (CurrentX != 0)
        {
            c = BoardManager.Instance.chessMans[CurrentX - 1, CurrentY];
            if (c == null)
                r[CurrentX - 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX - 1, CurrentY] = true;
        }

        //Middle Right
        if (CurrentX != 7)
        {
            c = BoardManager.Instance.chessMans[CurrentX + 1, CurrentY];
            if (c == null)
                r[CurrentX + 1, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX + 1, CurrentY] = true;
        }

        if (!BoardManager.whiteKingAndRookMoved || !BoardManager.blackKingAndRookMoved)
        {
            if (CurrentX == 3 && CurrentY == 0)
            {
                c = BoardManager.Instance.chessMans[CurrentX - 1, CurrentY];
                c2 = BoardManager.Instance.chessMans[CurrentX - 2, CurrentY];
                if (c == null && c2 == null)
                    r[CurrentX - 2, CurrentY] = true;
            }
            if (CurrentX == 4 && CurrentY == 7)
            {
                c = BoardManager.Instance.chessMans[CurrentX + 1, CurrentY];
                c2 = BoardManager.Instance.chessMans[CurrentX + 2, CurrentY];
                if (c == null && c2 == null)
                    r[CurrentX + 2, CurrentY] = true;
            }
        }



        return r;
    }
   }
