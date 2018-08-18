using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessMan {

	public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        ChessMan c, c2;
        int[] e = BoardManager.Instance.EnPassantMove;

        // White team move
        if (isWhite)
        {
            if (!BoardManager.checkForChecking)
            {
                //Diagonal Left
                if (CurrentX != 0 && CurrentY != 7 && !BoardManager.checkForKing)
                {
                    if (e[0] == CurrentX - 1 && e[1] == CurrentY + 1)
                        r[CurrentX - 1, CurrentY + 1] = true;

                    c = BoardManager.Instance.chessMans[CurrentX - 1, CurrentY + 1];
                    if (c != null && !c.isWhite)
                        r[CurrentX - 1, CurrentY + 1] = true;
                }

                //Diagonal Right
                if (CurrentX != 7 && CurrentY != 7 && !BoardManager.checkForKing)
                {
                    if (e[0] == CurrentX + 1 && e[1] == CurrentY + 1)
                        r[CurrentX + 1, CurrentY + 1] = true;

                    c = BoardManager.Instance.chessMans[CurrentX + 1, CurrentY + 1];
                    if (c != null && !c.isWhite)
                        r[CurrentX + 1, CurrentY + 1] = true;
                }

                //Middle
                if (CurrentY != 7 && !BoardManager.checkForKing)
                {
                    c = BoardManager.Instance.chessMans[CurrentX, CurrentY + 1];
                    if (c == null)
                        r[CurrentX, CurrentY + 1] = true;
                }
                //Middle on first move
                if (CurrentY == 1 && !BoardManager.checkForKing)
                {
                    c = BoardManager.Instance.chessMans[CurrentX, CurrentY + 1];
                    c2 = BoardManager.Instance.chessMans[CurrentX, CurrentY + 2];
                    if (c == null && c2 == null)
                        r[CurrentX, CurrentY + 2] = true;
                }
            }
            if (BoardManager.checkForKing || BoardManager.checkForChecking)
            {
                if (CurrentX != 0 && CurrentY != 7)
                {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
                if (CurrentX != 7 && CurrentY != 7)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }
        }
        else
        {
            if (!BoardManager.checkForChecking)
            {
                //Diagonal Left
                if (CurrentX != 0 && CurrentY != 0 && !BoardManager.checkForKing)
                {
                    if (e[0] == CurrentX - 1 && e[1] == CurrentY - 1)
                        r[CurrentX - 1, CurrentY - 1] = true;

                    c = BoardManager.Instance.chessMans[CurrentX - 1, CurrentY - 1];
                    if (c != null && c.isWhite)
                        r[CurrentX - 1, CurrentY - 1] = true;
                }

                //Diagonal Right
                if (CurrentX != 7 && CurrentY != 0 && !BoardManager.checkForKing)
                {
                    if (e[0] == CurrentX + 1 && e[1] == CurrentY - 1)
                        r[CurrentX + 1, CurrentY - 1] = true;

                    c = BoardManager.Instance.chessMans[CurrentX + 1, CurrentY - 1];
                    if (c != null && c.isWhite)
                        r[CurrentX + 1, CurrentY - 1] = true;
                }

                //Middle
                if (CurrentY != 0 && !BoardManager.checkForKing)
                {
                    c = BoardManager.Instance.chessMans[CurrentX, CurrentY - 1];
                    if (c == null)
                        r[CurrentX, CurrentY - 1] = true;
                }

                //Middle on first move
                if (CurrentY == 6 && !BoardManager.checkForKing)
                {
                    c = BoardManager.Instance.chessMans[CurrentX, CurrentY - 1];
                    c2 = BoardManager.Instance.chessMans[CurrentX, CurrentY - 2];
                    if (c == null && c2 == null)
                        r[CurrentX, CurrentY - 2] = true;
                }
            }
            if (BoardManager.checkForKing||BoardManager.checkForChecking)
            {
                if (CurrentX != 0 && CurrentY != 0)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }
                if (CurrentX != 7 && CurrentY != 0)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }
            }
        }
        return r;
    }
}
