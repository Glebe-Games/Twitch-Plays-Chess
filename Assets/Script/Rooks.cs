using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooks : ChessMan {
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        bool returnR = false;

        ChessMan c;
        int i;

        if (!BoardManager.checkForChecking)
        {

            //Right
            i = CurrentX;
            while (true)
            {
                i++;
                if (i >= 8)
                    break;

                c = BoardManager.Instance.chessMans[i, CurrentY];
                if (c == null)
                    r[i, CurrentY] = true;
                else
                {
                    if (BoardManager.checkForKing && c.GetType() == typeof(King))
                    {
                        r[i, CurrentY] = true;
                    }
                    else if (c.isWhite != isWhite)
                    {
                        r[i, CurrentY] = true;
                        break;
                    }
                    else if(c.isWhite == isWhite)
                    {
                        r[i, CurrentY] = false;
                        break;
                    }
                }
            }
            //Left
            i = CurrentX;
            while (true)
            {
                i--;
                if (i < 0)
                    break;

                c = BoardManager.Instance.chessMans[i, CurrentY];
                if (c == null)
                    r[i, CurrentY] = true;
                else
                {
                    if (BoardManager.checkForKing && c.GetType() == typeof(King))
                    {
                        r[i, CurrentY] = true;
                    }
                    else if (c.isWhite != isWhite)
                    {
                        r[i, CurrentY] = true;
                        break;
                    }
                    else if (c.isWhite == isWhite)
                    {
                        r[i, CurrentY] = false;
                        break;
                    }
                }
            }

            //Up
            i = CurrentY;
            while (true)
            {
                i++;
                if (i >= 8)
                    break;

                c = BoardManager.Instance.chessMans[CurrentX, i];
                if (c == null)
                    r[CurrentX, i] = true;
                else
                {
                    if (BoardManager.checkForKing && c.GetType() == typeof(King))
                    {
                        r[CurrentX, i] = true;
                    }
                    else if (c.isWhite != isWhite)
                    {
                        r[CurrentX, i] = true;
                        break;
                    }
                    else if (c.isWhite == isWhite)
                    {
                        r[CurrentX, i] = false;
                        break;
                    }
                }
            }

            //Down
            i = CurrentY;
            while (true)
            {
                i--;
                if (i < 0)
                    break;

                c = BoardManager.Instance.chessMans[CurrentX, i];
                if (c == null)
                    r[CurrentX, i] = true;
                else
                {
                    if (BoardManager.checkForKing && c.GetType() == typeof(King))
                    {
                        r[CurrentX, i] = true;
                    }
                    else if (c.isWhite != isWhite)
                    {
                        r[CurrentX, i] = true;
                        break;
                    }
                    else if (c.isWhite == isWhite)
                    {
                        r[CurrentX, i] = false;
                        break;
                    }
                }
            }
            return r;
        }
        else
        {
            //Right
            i = CurrentX;
            while (true)
            {
                i++;
                if (i >= 8)
                    break;

                c = BoardManager.Instance.chessMans[i, CurrentY];
                if (c == null)
                    r[i, CurrentY] = true;
                else
                {
                    if (c.isWhite != isWhite)
                        r[i, CurrentY] = true;
                    else if (BoardManager.checkForKing)
                    {
                        r[i, CurrentY] = true;
                    }
                    if (BoardManager.Instance.chessMans[i, CurrentY].GetType() == typeof(King))
                    {
                        returnR = true;
                        break;
                    }
                    break;
                }
            }
            if (!returnR)
            {
                for (int d = CurrentX; d < 8; d++)
                {
                    r[d, CurrentY] = false;
                }
            }

            if (!returnR)
            {
                //Left
                i = CurrentX;
                while (true)
                {
                    i--;
                    if (i < 0)
                        break;

                    c = BoardManager.Instance.chessMans[i, CurrentY];
                    if (c == null)
                        r[i, CurrentY] = true;
                    else
                    {
                        if (c.isWhite != isWhite)
                            r[i, CurrentY] = true;
                        else if (BoardManager.checkForKing)
                        {
                            r[i, CurrentY] = true;
                        }
                        if (BoardManager.Instance.chessMans[i, CurrentY].GetType() == typeof(King))
                        {
                            returnR = true;
                            break;
                        }
                        break;
                    }
                }
                if (!returnR)
                {
                    for (int d = CurrentX; d >= 0; d--)
                    {
                        r[d, CurrentY] = false;
                    }
                }
            }

            if (!returnR)
            {
                //Up
                i = CurrentY;
                while (true)
                {
                    i++;
                    if (i >= 8)
                        break;

                    c = BoardManager.Instance.chessMans[CurrentX, i];
                    if (c == null)
                        r[CurrentX, i] = true;
                    else
                    {
                        if (c.isWhite != isWhite)
                            r[CurrentX, i] = true;
                        else if (BoardManager.checkForKing)
                        {
                            r[CurrentX, i] = true;
                        }
                        if (BoardManager.Instance.chessMans[CurrentX, i].GetType() == typeof(King))
                        {
                            returnR = true;
                            break;
                        }
                        break;
                    }
                }
                if (!returnR)
                {
                    for (int d = CurrentY; d < 8; d++)
                    {
                        r[CurrentX, d] = false;
                    }
                }
            }

            if (!returnR)
            {
                //Down
                i = CurrentY;
                while (true)
                {
                    i--;
                    if (i < 0)
                        break;

                    c = BoardManager.Instance.chessMans[CurrentX, i];
                    if (c == null)
                        r[CurrentX, i] = true;
                    else
                    {
                        if (c.isWhite != isWhite)
                            r[CurrentX, i] = true;
                        else if (BoardManager.checkForKing)
                        {
                            r[CurrentX, i] = true;
                        }
                        if (BoardManager.Instance.chessMans[CurrentX, i].GetType() == typeof(King))
                        {
                            returnR = true;
                            break;
                        }
                        break;
                    }
                }
                if (!returnR)
                {
                    for (int d = CurrentX; d >= 0; d--)
                    {
                        r[CurrentX, d] = false;
                    }
                }
            }
            return r;
        }
    }
}
