using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishops : ChessMan {
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        bool returnR = false;

        ChessMan c;
        int i, j;

        if (!BoardManager.checkForChecking)
        {
            // Top Left
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i--;
                j++;
                if (i < 0 || j >= 8)
                    break;

                c = BoardManager.Instance.chessMans[i, j];
                if (c == null)
                    r[i, j] = true;
                else
                {
                    if (BoardManager.checkForKing && c.GetType() == typeof(King))
                    {
                        r[i, j] = true;
                    }
                    else if (c.isWhite != isWhite)
                    {
                        r[i, j] = true;
                        break;
                    }
                    else if (c.isWhite == isWhite)
                    {
                        r[i, j] = false;
                        break;
                    }
                }
            }

            // Top Right
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i++;
                j++;
                if (i >= 8 || j >= 8)
                    break;

                c = BoardManager.Instance.chessMans[i, j];
                if (c == null)
                    r[i, j] = true;
                else
                {
                    if (BoardManager.checkForKing && c.GetType() == typeof(King))
                    {
                        r[i, j] = true;
                    }
                    else if (c.isWhite != isWhite)
                    {
                        r[i, j] = true;
                        break;
                    }
                    else if (c.isWhite == isWhite)
                    {
                        r[i, j] = false;
                        break;
                    }
                }
            }

            // Down Left
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i--;
                j--;
                if (i < 0 || j < 0)
                    break;

                c = BoardManager.Instance.chessMans[i, j];
                if (c == null)
                    r[i, j] = true;
                else
                {
                    if (BoardManager.checkForKing && c.GetType() == typeof(King))
                    {
                        r[i, j] = true;
                    }
                    else if (c.isWhite != isWhite)
                    {
                        r[i, j] = true;
                        break;
                    }
                    else if (c.isWhite == isWhite)
                    {
                        r[i, j] = false;
                        break;
                    }
                }
            }

            // Down Right
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i++;
                j--;
                if (i >= 8 || j < 0)
                    break;

                c = BoardManager.Instance.chessMans[i, j];
                if (c == null)
                    r[i, j] = true;
                else
                {
                    if (BoardManager.checkForKing && c.GetType() == typeof(King))
                    {
                        r[i, j] = true;
                    }
                    else if (c.isWhite != isWhite)
                    {
                        r[i, j] = true;
                        break;
                    }
                    else if (c.isWhite == isWhite)
                    {
                        r[i, j] = false;
                        break;
                    }
                }
            }

            return r;
        }
        else
        {
            // Top Left
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i--;
                j++;
                if (i < 0 || j >= 8)
                    break;

                c = BoardManager.Instance.chessMans[i, j];
                if (c == null)
                    r[i, j] = true;
                else
                {
                    if (isWhite != c.isWhite)
                        r[i, j] = true;
                    else if (BoardManager.checkForKing)
                    {
                        r[i, j] = true;
                    }
                    if (BoardManager.Instance.chessMans[i, j].GetType() == typeof(King))
                    {
                        r[i, j] = false;
                        returnR = true;
                    }
                    break;
                }
            if (!returnR)
            {
                for (int d = CurrentX; d >= 0; d--)
                {
                    for (int l = CurrentY; l < 8; l++)
                    {
                        r[d, l] = false;
                    }
                }
            }
        }

        if (!returnR)
        {
            // Top Right
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i++;
                j++;
                if (i >= 8 || j >= 8)
                    break;

                c = BoardManager.Instance.chessMans[i, j];
                if (c == null)
                    r[i, j] = true;
                else
                {
                    if (isWhite != c.isWhite)
                        r[i, j] = true;
                    else if (BoardManager.checkForKing)
                    {
                        r[i, j] = true;
                    }
                    if (BoardManager.Instance.chessMans[i, j].GetType() == typeof(King))
                    {
                        r[i, j] = false;
                        returnR = true;
                    }
                    break;
                }
            }
            if (!returnR)
            {
                for (int d = CurrentX; d < 8; d++)
                {
                    for (int l = CurrentY; l < 8; l++)
                    {
                        r[d, l] = false;
                    }
                }
            }
        }

        if (!returnR)
        {
            // Down Left
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i--;
                j--;
                if (i < 0 || j < 0)
                    break;

                c = BoardManager.Instance.chessMans[i, j];
                if (c == null)
                    r[i, j] = true;
                else
                {
                    if (isWhite != c.isWhite)
                        r[i, j] = true;
                    else if (BoardManager.checkForKing)
                    {
                        r[i, j] = true;
                    }
                    if (BoardManager.Instance.chessMans[i, j].GetType() == typeof(King))
                    {
                        r[i, j] = false;
                        returnR = true;
                    }
                    break;
                }
            }
            if (!returnR)
            {
                for (int d = CurrentX; d >= 0; d--)
                {
                    for (int l = CurrentY; l >= 0; l--)
                    {
                        r[d, l] = false;
                    }
                }
            }
        }

        if (!returnR)
        {
            // Down Right
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i++;
                j--;
                if (i >= 8 || j < 0)
                    break;

                c = BoardManager.Instance.chessMans[i, j];
                if (c == null)
                    r[i, j] = true;
                else
                {
                    if (isWhite != c.isWhite)
                        r[i, j] = true;
                    else if (BoardManager.checkForKing)
                    {
                        r[i, j] = true;
                    }
                    if (BoardManager.Instance.chessMans[i, j].GetType() == typeof(King))
                    {
                        r[i, j] = false;
                        returnR = true;
                    }
                    break;
                }
            }
            if (!returnR)
            {
                for (int d = CurrentX; d >= 0; d++)
                {
                    for (int l = CurrentY; l < 8; l--)
                    {
                        r[d, l] = false;
                    }
                }
            }
        }
        return r;
    }
    }
}
