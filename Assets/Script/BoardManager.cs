using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    public static BoardManager Instance { set; get; }

    public ChessMan[,] chessMans { set; get; }
    private ChessMan selectedChessMan;

    private const float tileSize = 1.0f;
    private const float tileOffset = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    private Material previousMat;
    public Material selectedMatWhite;
    public Material selectedMatBlack;
    public Material checkMat;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    public int[] EnPassantMove { set; get; }

    private Quaternion whiteOrientation = Quaternion.Euler(0, 90, 0);
    private Quaternion blackOrientation = Quaternion.Euler(0, -90, 0);
    public static bool whiteKingAndRookMoved;
    public static bool blackKingAndRookMoved;

    public bool isWhiteTurn = true;
    private bool[,] allowedMoves = new bool[8, 8];

    public static bool justForCheck = false;
    public static bool checkForKing = false;
    private bool check = false;
    private int[] pieceChecing = new int[2];
    public List<int> piecesThatCanMoveWhenCheckCoordinatesX;
    public List<int> piecesThatCanMoveWhenCheckCoordinatesY;
    public List<int> movesForPiecesThatCanMoveWhenCheckCoordinatesX;
    public List<int> movesForPiecesThatCanMoveWhenCheckCoordinatesY;
    private bool[,] chessManCanMove = new bool[8, 8];
    private bool alreadyIdentifiedPieceThatCanMove = false;
    private bool thereIsAnOption = true;
    public static bool checkForChecking = false;
    public int kingPositionX;
    public int kingPositionY;


    private bool AI_selecting = false;
    private int positions = 0;
    private bool WhiteLeftRookMoved;
    private bool WhiteRightRookMoved;
    private bool BlackLeftRookMoved;
    private bool BlackRightRookMoved;
    private bool BlackKingMoved;
    private bool WhiteKingMoved;

    private int WhiteKingN = 1;
    private int BlackPawnN = 8;
    private int BlackKnightN = 2;
    private int BlackBishopN = 2;
    private int BlackRookN = 2;
    private int BlackQueenN = 1;
    private int BlackKingN = 1;
    private int[] PossibleMovesX;
    private int[] PossibleMovesY;
    private int[] BlackPiecesX = new int[16];
    private int[] BlackPiecesY = new int[16];
    private int[] WhitePiecesX = new int[16];
    private int[] WhitePiecesY = new int[16];
    private bool BlackCastling;

    private int Turn;
    private int[,] map;
    private int[,] tempMap = new int[8, 8];
    private int[,] tempMap2 = new int[8, 8];
    private int SelectedX;
    private int SelectedY;
    private int[,] PawnPlace = new int[,] { {0,50,10,5,0,5,5,0},{0,50,10,5,0,-5,1,0}, {0,50,20,10,0,-10,10,0},{0,50,30,25,20,0,-20,0 },
     { 0,50,30,25,20,0,-20,0}, { 0,50,20,10,0,-10,10,0},{ 0,50,10,5,0,-5,10,0}, { 0,50,10,5,0,5,5,0} };
    private int[,] KnightPlace = new int[,] { {-50,-40,-30,-30,-30,-30,-40,-50},{-40,-20,0,5,0,5,-20,-40}, 
        {-30,0,10,15,15,10,0,-30},{-30,0,15,20,20,15,5,-30 },{ 0,50,30,25,20,0,-20,0}, { 0,50,20,10,0,-10,10,0},
        { 0,50,10,5,0,-5,10,0}, { 0,50,10,5,0,5,5,0} };
    private int[,] BishopPlace = new int[,] { {-20,-10,-10,-10,-10,-10,-10,-20},{-10,0,0,5,0,10,5,-10}, {-10,0,5,5,10,10,0,-10},
      {-10,0,10,10,10,10,0,-10 },{ -10,0,10,10,10,10,0,-10},{-10,0,5,5,10,10,0,-10},{-10,0,0,5,0,10,5,-10}, {-20,-10,-10,-10,-10,-10,-10,-20}};
    private int[,] RookPlace = new int[,] { {0,5,-5,-5,-5,-5,-5,0},{0,10,0,0,0,0,0,0}, {0,10,0,0,0,0,0,0},
      {0,10,0,0,0,0,0,5},{ 0,10,0,0,0,0,0,5},{0,10,0,0,0,0,0,0},{0,10,0,0,0,0,0,0},{0,5,-5,-5,-5,-5,-5,0}};
    private int[,] QueenPlace = new int[,] { {-20,-10,-10,-5,0,-10,-10,-20},{-10,0,0,0,0,5,0,-10}, {-5,0,5,5,5,5,0,-5},
      {-5,0,5,5,5,5,0,-5},{ -5,0,5,5,5,5,0,-5},{-10,0,5,5,5,5,0,-10},{-10,0,0,0,0,0,0,-10},{-20,-10,-10,-5,0,-10,-10,-20}};
    private int[,] KingPlace = new int[,] { {-30,-30,-30,-30,-20,-10,20,40},{-40,-40,-40,-40,-30,-20,20,60},{-40,-40,-40,-40,-30,-20,0,20},
      {-50,-50,-50,-50,-40,-20,0,0},{-50,-50,-50,-50,-40,-20,0,0},{-40,-40,-40,-40,-30,-20,0,20},
      {-40,-40,-40,-40,-30,-20,20,60},{-30,-30,-30,-30,-20,-10,20,40}};

    private const int PawnValue = 100;
    private const int KnightValue = 300;
    private const int BishopValue = 300;
    private const int RookValue = 500;
    private const int QueenValue = 900;
    private const int KingValue = 90000;
    private int TotalBoardValue = 0;

    public GameObject checkMateScreen;
    public GameObject errorScreen;

    public int modes;
    // Use this for initialization
    void OnEnable()
    {
        Instance = this;
        if(activeChessman != null){
        foreach (GameObject go in activeChessman)
           Destroy(go);
        }

        isWhiteTurn = true;
        if(BoardHighlights.Instance != null){
            BoardHighlights.Instance.HideHighlights();
        }
        spawnAllChessman();
        /*System.Array.Reverse(PawnPlace);
        System.Array.Reverse(BishopPlace);
        System.Array.Reverse(RookPlace);
        System.Array.Reverse(KnightPlace);
        System.Array.Reverse(KingPlace);
        System.Array.Reverse(QueenPlace);*/
    }

    // Update is called once per frame
    void Update()
    {
        modes = modePicker.whichMode;
        for (int i = 0; i < 8; i++)
        {
            for (int k = 0; k < 8; k++)
            {
                chessManCanMove[i, k] = true;
            }
        }
        if (!ScriptForInGameMenue.pause && !isWhiteTurn && modes == 1)
        {
            LoadAI();
        }
        else if(modePicker.white && !isWhiteTurn && modes == 3)
        {
            if (TwitchChat.selectedX >= 0 && TwitchChat.selectedY >= 0 && TwitchChat.selectedMoveX >= 0 && TwitchChat.selectedMoveY >= 0)
            {
                if (selectedChessMan == null)
                {
                    // Select the chessman
                    SelectChessMan(TwitchChat.selectedX, TwitchChat.selectedY, map);
                }
                else
                {
                    // Move the chessman
                    MoveChessMan(TwitchChat.selectedMoveX, TwitchChat.selectedMoveY);
                }
            }
        }
        else if (!modePicker.white && isWhiteTurn && modes == 3)
        {
            if (TwitchChat.selectedX >= 0 && TwitchChat.selectedY >= 0 && TwitchChat.selectedMoveX >= 0 && TwitchChat.selectedMoveY >= 0)
            {
                if (selectedChessMan == null)
                {
                    // Select the chessman
                    SelectChessMan(TwitchChat.selectedX, TwitchChat.selectedY, map);
                }
                else
                {
                    // Move the chessman
                    MoveChessMan(TwitchChat.selectedMoveX, TwitchChat.selectedMoveY);
                }
            }
        }
        else if (isWhiteTurn && modes == 4)
        {
            if (TwitchChat.selectedX >= 0 && TwitchChat.selectedY >= 0 && TwitchChat.selectedMoveX >= 0 && TwitchChat.selectedMoveY >= 0)
            {
                if (selectedChessMan == null)
                {
                    // Select the chessman
                    SelectChessMan(TwitchChat.selectedX, TwitchChat.selectedY, map);
                }
                else
                {
                    // Move the chessman
                    MoveChessMan(TwitchChat.selectedMoveX, TwitchChat.selectedMoveY);
                }
            }
        }
        else if (!isWhiteTurn && modes == 4)
        {
            LoadAI();
        }
        else
        {
            updateSelection();
            if (Input.GetMouseButtonDown(0))
            {
                if (selectionX >= 0 && selectionY >= 0)
                {
                    if (selectedChessMan == null)
                    {
                        // Select the chessman
                        SelectChessMan(selectionX, selectionY, map);
                    }
                    else
                    {
                        // Move the chessman
                        MoveChessMan(selectionX, selectionY);
                    }
                }
            }
        }

    }
    private void Check()
    {
        for (int k = 0; k < 8; k++)
        {
            for (int b = 0; b < 8; b++)
            {
                if (map[k, b] != 0)
                {
                    for (int s = 0; s < 8; s++)
                    {
                        for (int r = 0; r < 8; r++)
                        {
                            if (chessMans[s, r] != null && chessMans[s, r].isWhite != chessMans[k, b].isWhite)
                            {
                                if (!justForCheck)
                                {
                                    checkForKing = true;
                                    if (chessMans[k, b].PossibleMove()[s, r])
                                    {
                                        if (chessMans[s, r].GetType() == typeof(King))
                                        {
                                            kingPositionX = s;
                                            kingPositionY = r;
                                            pieceChecing[0] = k;
                                            pieceChecing[1] = b;
                                            check = true;
                                        }
                                    }
                                }
                                else if(chessMans[k,b].isWhite != isWhiteTurn)
                                {
                                    checkForKing = true;
                                    if (chessMans[k, b].PossibleMove()[s, r])
                                    {
                                        if (chessMans[s, r].GetType() == typeof(King))
                                        {
                                            kingPositionX = s;
                                            kingPositionY = r;
                                            pieceChecing[0] = k;
                                            pieceChecing[1] = b;
                                            check = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        checkForKing = false;
    }
    private void AfterCheck()
    {
        movesForPiecesThatCanMoveWhenCheckCoordinatesX.Clear();
        movesForPiecesThatCanMoveWhenCheckCoordinatesY.Clear();
        thereIsAnOption = false;
        for (int k = 0; k < 8; k++)
        {
            for (int b = 0; b < 8; b++)
            {
                if (chessMans[k, b] != null)
                {
                    if (chessMans[k, b].isWhite == isWhiteTurn && chessMans[k,b].GetType() != typeof(King))
                    {

                        for (int s = 0; s < 8; s++)
                        {
                            for (int r = 0; r < 8; r++)
                            {
                                checkForChecking = false;
                                if (chessMans[k, b].PossibleMove()[s, r])
                                {
                                    if (chessMans[k, b].isWhite != chessMans[pieceChecing[0], pieceChecing[1]].isWhite)
                                    {
                                        checkForChecking = true;
                                        if (chessMans[pieceChecing[0], pieceChecing[1]].PossibleMove()[s, r])
                                        {
                                            piecesThatCanMoveWhenCheckCoordinatesX.Add(k);
                                            piecesThatCanMoveWhenCheckCoordinatesY.Add(b);
                                            movesForPiecesThatCanMoveWhenCheckCoordinatesX.Add(s);
                                            movesForPiecesThatCanMoveWhenCheckCoordinatesY.Add(r);
                                        }
                                    }
                                    if (s == pieceChecing[0] && r == pieceChecing[1])
                                    {
                                        piecesThatCanMoveWhenCheckCoordinatesX.Add(k);
                                        piecesThatCanMoveWhenCheckCoordinatesY.Add(b);
                                        movesForPiecesThatCanMoveWhenCheckCoordinatesX.Add(s);
                                        movesForPiecesThatCanMoveWhenCheckCoordinatesY.Add(r);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        for (int i = 0; i < 8; i++)
        {
            for (int k = 0; k < 8; k++)
            {
                chessManCanMove[i, k] = false;
            }
        }
        for (int i = 0; i < 8; i++)
        {
            for (int k = 0; k < 8; k++)
            {
                if (map[i, k] != 0 && chessMans[i, k].GetType() != typeof(King))
                {
                    if (chessMans[i, k].isWhite == isWhiteTurn)
                    {
                        for (int f = 0; f < piecesThatCanMoveWhenCheckCoordinatesX.Count; f++)
                        {
                            if (i == piecesThatCanMoveWhenCheckCoordinatesX[f] && k == piecesThatCanMoveWhenCheckCoordinatesY[f])
                            {
                                thereIsAnOption = true;
                                chessManCanMove[i, k] = true;
                                alreadyIdentifiedPieceThatCanMove = true;
                            }
                        }
                    }
                }
            }
        }
        piecesThatCanMoveWhenCheckCoordinatesX.Clear();
        piecesThatCanMoveWhenCheckCoordinatesY.Clear();
        return;
    }

    private bool SelectChessManAI(int x, int y, int[,] MapArray)
    {
        if (chessMans[x, y] == null)
            return false;
        if (!CheckCorrectColor(x, y, MapArray))
            return false;
        if (chessMans[x, y].GetType() != typeof(King))
        {
            if (!chessManCanMove[x, y])
                return false;
        }
        check = false;
        checkForKing = false;
        bool hasAtleastOneMove = false;
        selectedChessMan = chessMans[x, y];
        SelectedX = x;
        SelectedY = y;
        allowedMoves = new bool[8, 8];
        allowedMoves = GetPossibleMoves(x, y, MapArray);
        //if (selectedChessMan.GetType()== typeof(King))
        //{
        //    for (int k = 0; k < 8; k++)
        //    {
        //        for (int b = 0; b < 8; b++)
        //        {
        //            if (allowedMoves[k, b])
        //            {
        //                for (int l = 0; l < 8; l++)
        //                {
        //                    for (int g = 0; g < 8; g++)
        //                    {
        //                        if (chessMans[l, g] != null)
        //                        {
        //                            if (chessMans[l, g].isWhite != chessMans[x, y].isWhite)
        //                            {

        //                                for (int d = 0; d < 8; d++)
        //                                {
        //                                    for (int m = 0; m < 8; m++)
        //                                    {
        //                                        checkForKing = true;
        //                                        if (chessMans[l, g].PossibleMove()[d, m] == allowedMoves[k, b] && chessMans[l, g].PossibleMove()[d, m] && allowedMoves[k, b])
        //                                        {
        //                                            if (d == k && m == b)
        //                                            {
        //                                                allowedMoves[k, b] = false;
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //checkForKing = false;
        //if (alreadyIdentifiedPieceThatCanMove && selectedChessMan.GetType() != typeof(King))
        //{
        //    for (int k = 0; k < 8; k++)
        //    {
        //        for (int b = 0; b < 8; b++)
        //        {
        //            allowedMoves[k, b] = false;
        //        }
        //    }
        //    for (int k = 0; k < movesForPiecesThatCanMoveWhenCheckCoordinatesX.Count; k++)
        //    {
        //        allowedMoves[movesForPiecesThatCanMoveWhenCheckCoordinatesX[k], movesForPiecesThatCanMoveWhenCheckCoordinatesY[k]] = true;
        //        if (selectedChessMan.PossibleMove()[movesForPiecesThatCanMoveWhenCheckCoordinatesX[k], movesForPiecesThatCanMoveWhenCheckCoordinatesY[k]] == true)
        //            allowedMoves[movesForPiecesThatCanMoveWhenCheckCoordinatesX[k], movesForPiecesThatCanMoveWhenCheckCoordinatesY[k]] = true;
        //        else
        //        {
        //            allowedMoves[movesForPiecesThatCanMoveWhenCheckCoordinatesX[k], movesForPiecesThatCanMoveWhenCheckCoordinatesY[k]] = false;
        //        }
        //    }

        //}

        //int save = 0;
        //if (!AI_selecting)
        //{
        //    if (selectedChessMan.GetType() != typeof(King))
        //    {
        //        DestroyImmediate(selectedChessMan.gameObject);
        //        for (int i = 0; i < 8; i++)
        //        {
        //            for (int j = 0; j < 8; j++)
        //            {
        //                if (allowedMoves[i, j])
        //                {
        //                    justForCheck = true;
        //                    if (chessMans[i, j] != null)
        //                    {
        //                        save = map[i, j];
        //                        DestroyImmediate(chessMans[i, j].gameObject);
        //                    }
        //                    spawnChessman(map[selectionX, selectionY], i, j);
        //                    Check();
        //                    DestroyImmediate(chessMans[i, j].gameObject);
        //                    justForCheck = false;
        //                    if (save != 0)
        //                        spawnChessman(save, i, j);
        //                    if (check)
        //                    {
        //                        allowedMoves[i, j] = false;
        //                        check = false;
        //                    }
        //                    save = 0;
        //                }
        //            }
        //        }

        //        spawnChessman(map[x, y], x, y);
        //        selectedChessMan = chessMans[SelectedX, SelectedY];
        //    }
        //}
        int NumberPossibleMoves = 0;
        for (int a = 0; a < allowedMoves.GetLength(0); a++)
        {
            for (int l = 0; l < allowedMoves.GetLength(1); l++)
            {
                if (allowedMoves[a, l])
                    NumberPossibleMoves++;
            }
        }
        PossibleMovesX = new int[NumberPossibleMoves];
        PossibleMovesY = new int[NumberPossibleMoves];
        int count = 0;
        for (int v = 0; v < allowedMoves.GetLength(0); v++)
        {
            for (int q = 0; q < allowedMoves.GetLength(1); q++)
            {
                if (allowedMoves[v, q])
                {
                    PossibleMovesX[count] = v;
                    PossibleMovesY[count] = q;
                    count++;
                }
            }
        }
        if (NumberPossibleMoves > 0)
        {
            hasAtleastOneMove = true;
        }
        //Check();
        //if (!thereIsAnOption && !hasAtleastOneMove && check)
        //{
        //    EndGame();
        //
        /*else */if (!hasAtleastOneMove)
            return false;

        return true;
    }

    private bool SelectChessMan(int x, int y, int[,] MapArray)
    {
        if (chessMans[x,y] == null)
            return false;
        if (!CheckCorrectColor(x, y, MapArray))
            return false;
        if (chessMans[x,y].GetType() != typeof(King))
        {
            if (!chessManCanMove[x, y])
                return false;
        }
        check = false;
        checkForKing = false;
        bool hasAtleastOneMove = false;
        selectedChessMan = chessMans[x, y];
        SelectedX = x;
        SelectedY = y;
        allowedMoves = new bool[8, 8];
        allowedMoves = GetPossibleMoves(x, y, MapArray);
        if (selectedChessMan.GetType() == typeof(King))
        {
            for (int k = 0; k < 8; k++)
            {
                for (int b = 0; b < 8; b++)
                {
                    if (allowedMoves[k, b])
                    {
                        for (int l = 0; l < 8; l++)
                        {
                            for (int g = 0; g < 8; g++)
                            {
                                if (chessMans[l,g] != null)
                                {
                                    if (chessMans[l, g].isWhite != chessMans[x,y].isWhite)
                                    {

                                        for (int d = 0; d < 8; d++)
                                        {
                                            for (int m = 0; m < 8; m++)
                                            {
                                                checkForKing = true;
                                                if (chessMans[l, g].PossibleMove()[d, m] == allowedMoves[k, b] && chessMans[l, g].PossibleMove()[d, m] && allowedMoves[k, b])
                                                {
                                                    if (d == k && m == b)
                                                    {
                                                        allowedMoves[k, b] = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        checkForKing = false;
        if (alreadyIdentifiedPieceThatCanMove && selectedChessMan.GetType() != typeof(King))
        {
            
            for (int k = 0; k < 8; k++)
            {
                for (int b = 0; b < 8; b++)
                {
                    allowedMoves[k, b] = false;
                }
            }
            for (int k = 0; k < movesForPiecesThatCanMoveWhenCheckCoordinatesX.Count; k++)
            {
                allowedMoves[movesForPiecesThatCanMoveWhenCheckCoordinatesX[k], movesForPiecesThatCanMoveWhenCheckCoordinatesY[k]] = true;
                if (selectedChessMan.PossibleMove()[movesForPiecesThatCanMoveWhenCheckCoordinatesX[k], movesForPiecesThatCanMoveWhenCheckCoordinatesY[k]] == true)
                    allowedMoves[movesForPiecesThatCanMoveWhenCheckCoordinatesX[k], movesForPiecesThatCanMoveWhenCheckCoordinatesY[k]] = true;
                else
                {
                    allowedMoves[movesForPiecesThatCanMoveWhenCheckCoordinatesX[k], movesForPiecesThatCanMoveWhenCheckCoordinatesY[k]] = false;
                }
            }

        }

        int save = 0;
        if (!AI_selecting)
        {
            if (selectedChessMan.GetType() != typeof(King))
            {
                DestroyImmediate(selectedChessMan.gameObject);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (allowedMoves[i, j])
                        {
                            justForCheck = true;
                            if (chessMans[i, j] != null)
                            {
                                save = map[i, j];
                                DestroyImmediate(chessMans[i, j].gameObject);
                            }
                            spawnChessman(map[selectionX, selectionY], i, j);
                            Check();
                            DestroyImmediate(chessMans[i, j].gameObject);
                            justForCheck = false;
                            if (save != 0)
                                spawnChessman(save, i, j);
                            if (check)
                            {
                                allowedMoves[i, j] = false;
                                check = false;
                            }
                            save = 0;
                        }
                    }
                }

                spawnChessman(map[x, y], x, y);
                selectedChessMan = chessMans[SelectedX, SelectedY];
            }
        }
        int NumberPossibleMoves = 0;
        for (int a = 0; a < allowedMoves.GetLength(0); a++)
        {
            for (int l = 0; l < allowedMoves.GetLength(1); l++)
            {
                if (allowedMoves[a, l])
                    NumberPossibleMoves++;
            }
        }
        PossibleMovesX = new int[NumberPossibleMoves];
        PossibleMovesY = new int[NumberPossibleMoves];
        int count = 0;
        for (int v = 0; v < allowedMoves.GetLength(0); v++)
        {
            for (int q = 0; q < allowedMoves.GetLength(1); q++)
            {
                if (allowedMoves[v, q])
                {
                    PossibleMovesX[count] = v;
                    PossibleMovesY[count] = q;
                    count++;
                }
            }
        }
        if (NumberPossibleMoves > 0)
        {
            hasAtleastOneMove = true;
        }
        Check();
        if (!thereIsAnOption && !hasAtleastOneMove && check)
        {
            EndGame();
        }



        if (!AI_selecting || modes != 3)
        {
            if (!check)
            {
                previousMat = selectedChessMan.GetComponent<MeshRenderer>().material;
                if (isWhiteTurn)
                {
                    selectedMatWhite.mainTexture = previousMat.mainTexture;
                    selectedChessMan.GetComponent<MeshRenderer>().material = selectedMatWhite;
                }
                else
                {
                    selectedMatBlack.mainTexture = previousMat.mainTexture;
                    selectedChessMan.GetComponent<MeshRenderer>().material = selectedMatBlack;
                }
            }
            BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
        }
        if (!hasAtleastOneMove)
            return false;

        return true;
    }
    private void MoveChessMan(int x, int y)
    {
        int PieceType = 0;
        if (allowedMoves[x, y])
        {
            alreadyIdentifiedPieceThatCanMove = false;
            check = false;
            PieceType = map[SelectedX, SelectedY];
            if (PieceType == 12)
                WhiteKingMoved = true;
            if (PieceType == 6)
                BlackKingMoved = true;
            if (SelectedX == 0 && PieceType == 2)
                WhiteLeftRookMoved = true;
            if (SelectedX == 7 && PieceType == 2)
                WhiteRightRookMoved = true;
            if (SelectedX == 0 && PieceType == 8)
                BlackLeftRookMoved = true;
            if (SelectedX == 7 && PieceType == 8)
                BlackRightRookMoved = true;
            if (PieceType == 12 && SelectedX - x == 2)
            {
                map[0, 0] = 0;
                map[3, 0] = 2;
            }
            if (PieceType == 12 && SelectedX - x == -2)
            {
                map[7, 0] = 0;
                map[5, 0] = 2;
            }
            if (PieceType == 6 && SelectedX - x == 2)
            {
                map[0, 7] = 0;
                map[3, 7] = 8;
                BlackCastling = true;
            }
            if (PieceType == 6 && SelectedX - x == -2)
            {
                map[7, 7] = 0;
                map[5, 7] = 8;
                BlackCastling = true;
            }
            map[SelectedX, SelectedY] = 0;
            map[x, y] = PieceType;

            if(!AI_selecting)
                selectedChessMan.GetComponent<MeshRenderer>().material = previousMat;
            DrawMap();
            check = false;
            Check();
            selectedChessMan = null;
            isWhiteTurn = !isWhiteTurn;
            if (check)
            {
                AfterCheck();
                if (isWhiteTurn)
                {
                    selectedMatBlack.mainTexture = previousMat.mainTexture;
                    chessMans[kingPositionX,kingPositionY].GetComponent<MeshRenderer>().material = checkMat;
                }
                else
                {
                    selectedMatWhite.mainTexture = previousMat.mainTexture;
                    chessMans[kingPositionX, kingPositionY].GetComponent<MeshRenderer>().material = checkMat;
                }
                SelectChessMan(kingPositionX, kingPositionY, map);
                selectedChessMan = null;
            }
            checkForChecking = false;
        }
        BoardHighlights.Instance.HideHighlights();
        if (!check && selectedChessMan != null)
        {
            selectedChessMan.GetComponent<MeshRenderer>().material = previousMat;
        }
        selectedChessMan = null;
        SelectedX = -1;
        SelectedY = -1;



    }

    private void updateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("chessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    private void spawnChessman(int index, int x, int y)
    {
        if (index <= 5)
        {
            GameObject go = Instantiate(chessmanPrefabs[index], getTileCenter(x, y), whiteOrientation) as GameObject;
            if (justForCheck)
            {
                go.GetComponent<MeshRenderer>().enabled = false;
            }
            go.transform.SetParent(transform);
            chessMans[x, y] = go.GetComponent<ChessMan>();
            chessMans[x, y].SetPosition(x, y);
            activeChessman.Add(go);
        }
        else if (index >= 6)
        {
            GameObject go = Instantiate(chessmanPrefabs[index], getTileCenter(x, y), blackOrientation) as GameObject;
            if (justForCheck)
            {
                go.GetComponent<MeshRenderer>().enabled = false;
            }
            go.transform.SetParent(transform);
            chessMans[x, y] = go.GetComponent<ChessMan>();
            chessMans[x, y].SetPosition(x, y);
            activeChessman.Add(go);
        }

    }

    private void spawnAllChessman()
    {
        //Turn = 0;
        activeChessman = new List<GameObject>();
        chessMans = new ChessMan[8, 8];
        EnPassantMove = new int[2] { -1, -1 };
        map = new int[,] { { 2, 5, 0, 0, 0, 0, 11, 8 }, { 4, 5, 0, 0, 0, 0, 11, 10 }, { 3, 5, 0, 0, 0, 0, 11, 9 },{1,5,0,0,0,0,11,7},
        {12,5,0,0,0,0,11,6},{3,5,0,0,0,0,11,9 },{4,5,0,0,0,0,11,10},{2,5,0,0,0,0,11,8} };
        WhiteLeftRookMoved = false;
        WhiteRightRookMoved = false;
        BlackLeftRookMoved = false;
        BlackRightRookMoved = false;
        BlackKingMoved = false;
        WhiteKingMoved = false;
        BlackCastling = false;
        DrawMap();
    }
    private void DrawMap()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (chessMans[x, y] != null)
                {
                    DestroyImmediate(chessMans[x, y].gameObject);
                }
                if (map[x, y] != 0)
                {
                    if (y == 0 && map[x, y] == 11)
                    {
                        map[x, y] = 7;
                    }
                    if (y == 7 && map[x, y] == 5)
                    {
                        map[x, y] = 1;
                    }
                    spawnChessman(map[x, y], x, y);
                }
            }
        }
        CheckPiecesLocation(map);

        //System.Array.Copy(map, tempMap, map.Length);
    }
    private int CalculateBoardValue(int[,] Calculator)
    {
        int Score = 0;
        WhiteKingN = 0;
        BlackKingN = 0;
        BlackQueenN = 0;
        BlackBishopN = 0;
        BlackKnightN = 0;
        BlackRookN = 0;
        BlackPawnN = 0;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (Calculator[x, y] != 0)
                {
                    switch (Calculator[x, y])
                    {
                        case 1:
                            Score -= QueenValue;
                            break;
                        case 2:
                            Score -= RookValue;
                            break;
                        case 3:
                            Score -= BishopValue;
                            break;
                        case 4:
                            Score -= KnightValue;
                            break;
                        case 5:
                            Score -= PawnValue;
                            break;
                        case 6:
                            BlackKingN++;
                            Score += KingValue;
                     Score += KingPlace[x, y];
                            break;
                        case 7:
                            BlackQueenN++;
                            Score += QueenValue;
                            Score += QueenPlace[x, y];
                            break;
                        case 8:
                            BlackRookN++;
                            Score += RookValue;
                           Score += RookPlace[x, y];
                            break;
                        case 9:
                            BlackBishopN++;
                            Score += BishopValue;
                            Score += BishopPlace[x, y];
                            break;
                        case 10:
                            BlackKnightN++;
                            Score += KnightValue;
                            Score += KnightPlace[x, y];
                            break;
                        case 11:
                            BlackPawnN++;
                            Score += PawnValue;
                            Score += PawnPlace[x, y];
                            break;
                        case 12:
                            WhiteKingN++;
                            Score -= KingValue;
                            break;
                    }
                }
            }
        }

        return Score;
    }
    //private int PawnEvaluation(int[,] Board)
    //{
    //    int PawnScore = 0;
    //    /* bool[] DoublePawn = new bool[8];
    //     int RowY = 7;
    //     int RowX = 7;*/
    //    PawnScore = PawnValue * BlackPawnN;
    //    /* for (RowX = 0; RowX < 8; RowX++)
    //     {
    //         for (RowY = 0; RowY < 8; RowY++)
    //         {
    //             if (DoublePawn[7 - RowX])
    //             {
    //                 PawnScore -= 7;
    //             }
    //             if(Board[7 - RowX,7 - RowY] == 11)
    //             {
    //                 DoublePawn[7 - RowX] = true;
    //                 if (RowX != 0 && RowX != 7) {
    //                     if (Board[7 - RowX-1, 7- RowY] <= 5 && Board[7-RowX - 1,7-RowY + 1] <= 5 && Board[7-RowX,7-RowY+1] <= 5 && Board[7-RowX-1,7-RowY-1] <= 5
    //                        && Board[7-RowX + 1,7- RowY +1] <= 5 && Board[7-RowX+1,7-RowY] <=5 && Board[7-RowX+1,7-RowY-1] <= 5 && Board[7-RowX,7-RowY-1] <= 5)
    //                     {
    //                         PawnScore -= 2;
    //                     }
    //                         }
    //                 else if (RowX == 0)
    //                 {
    //                     if (Board[7-RowX - 1,7- RowY] <= 5 && Board[7-RowX - 1, 7-RowY + 1] <= 5 && Board[7-RowX,7- RowY + 1] <= 5
    //                         && Board[7-RowX - 1, 7-RowY - 1] <= 5 && Board[7-RowX, 7-RowY - 1] <= 5)
    //                     {
    //                         PawnScore -= 2;
    //                     }
    //                 }
    //                 else if (RowX == 7)
    //                 {
    //                     if (  Board[7-RowX, 7-RowY + 1] <= 5  && Board[7-RowX + 1, 7-RowY + 1] <= 5 && Board[7-RowX + 1, 7-RowY] <= 5 
    //                         && Board[7-RowX + 1, 7-RowY - 1] <= 5 && Board[7-RowX, 7-RowY - 1] <= 5)
    //                     {
    //                         PawnScore -= 2;
    //                     }
    //                 }
    //                 PawnScore += RowY;
    //                 for (int t = 0; t < 8; t++)
    //                 {
    //                     if(Board[7- RowX, t] == 5)
    //                     {
    //                         PawnScore -= RowY;
    //                         break;
    //                     }
    //                 }
    //                 if (Board[7-RowX,7-RowY-1] >= 1 && Board[7 - RowX, 7 - RowY - 1] <= 5 || Board[7 - RowX, 7 - RowY - 1] == 12)
    //                 {
    //                     PawnScore -= RowY;
    //                 }
    //                 if(RowY != 1)
    //                 {
    //                     PawnScore += 3 + (RowY - 1) * 5;
    //                 }
    //             }
    //         }
    //     }*/
    //    return PawnScore;
    //}
    //private int BishopEvaluation(int[,] Board)
    //{
    //    int BishopScore = 0;
    //    BishopScore = BishopValue * BlackBishopN;
    //    /*if (BlackBishopN == 2)
    //        BishopScore += 100;
    //    for (int x = 0; x < 8; x++)
    //    {
    //        for (int y = 0; y < 8; y++)
    //        {
    //            if (Board[x,y] == 9)
    //            {
    //                if (x != 0 && y!= 0) {
    //                    if (Board[x - 1, y - 1] == 11 || Board[x - 1, y - 1] == 5)
    //                    {
    //                        BishopScore -= 7;
    //                    }
    //                }
    //                if (x != 0 && y != 7)
    //                {
    //                    if (Board[x - 1, y + 1] == 11 || Board[x - 1, y + 1] == 5)
    //                    {
    //                        BishopScore -= 7;
    //                    }
    //                }
    //                if (x != 7 && y != 0)
    //                {
    //                    if (Board[x + 1, y - 1] == 11 || Board[x + 1, y - 1] == 5)
    //                    {
    //                        BishopScore -= 7;
    //                    }
    //                }
    //                if (x != 7 && y != 7)
    //                {
    //                    if (Board[x + 1, y + 1] == 11 || Board[x + 1, y + 1] == 5)
    //                    {
    //                        BishopScore -= 7;
    //                    }
    //                }
    //            }
    //        }
    //    }*/
    //    return BishopScore;
    //}
    //private int RookEvaluation(int[,] Board)
    //{
    //    int RookScore = 0;
    //    /*int KingX = 0;
    //    int KingY = 0;
    //    bool[] DoubleRook = new bool[8];
    //    bool substract = false;
    //    bool add = false;*/
    //    RookScore = RookValue * BlackRookN;
    //    /*for (int v = 0; v < 8; v++)
    //    {
    //        for (int u = 0; u < 8; u++)
    //        {
    //            if (Board[v,u] == 12)
    //            {
    //                KingX = v;
    //                KingY = u;
    //            }
    //        }
    //    }
    //    for (int x = 0; x < 8; x++)
    //    {
    //        for (int y = 0; y < 8; y++)
    //        {
    //            if (Board[x,y] == 8)
    //            {
    //                if (DoubleRook[x])
    //                {
    //                    RookScore += 15;
    //                    for (int t = 0; t < 8; t++)
    //                    {
    //                        if (Board[x,t] == 11)
    //                        {
    //                            substract = true;
    //                        }
    //                        if (Board[x, t] == 5)
    //                        {
    //                            add = true;
    //                        }
    //                    }
    //                }
    //                if (y == 0)
    //                    RookScore += 20;
    //                if (x > KingX)
    //                    RookScore += 20 + ((KingX - x) * -1) * -5;
    //                else
    //                    RookScore += 20 + (KingX - x) * -5;
    //                if (y > KingY)
    //                    RookScore += 20 + ((KingY - y) * -1) * -5;
    //                else
    //                    RookScore += 20 + (KingY - y) * -5;
    //            }
    //        }
    //        if (!substract)
    //            RookScore += 10;
    //        else
    //            substract = false;
    //        if (add)
    //        {
    //            RookScore += 3;
    //            add = false;
    //        }
    //    }*/
    //    return RookScore;
    //}
    //private int KnightEvaluation(int[,] Board)
    //{
    //    int KnightScore = 0;
    //    KnightScore = KnightValue * BlackKnightN;
    //    /*for (int x = 0; x < 8; x++)
    //    {
    //        for (int y = 0; y < 8; y++)
    //        {
    //            if(Board[x,y] == 10)
    //            {
    //                if (x <= 3)
    //                {
    //                    KnightScore += 7 - (3 - x) * 7;
    //                }
    //                if (x >= 4)
    //                {
    //                    KnightScore += 7 - (x - 4) * 7;
    //                }
    //                if (y <= 3)
    //                {
    //                    KnightScore += 7 - (3 - y) * 7;
    //                }
    //                if (y >= 4)
    //                {
    //                    KnightScore += 7 - (y - 4) * 7;
    //                }
    //            }
    //        }
    //    }*/
    //    return KnightScore;
    //}
    //private int QueenEvaluation(int[,] Board)
    //{
    //    int QueenScore = 0;
    //    QueenScore = QueenValue * BlackQueenN;

    //    return QueenScore;
    //}
    //private int KingEvaluation(int[,] Board)
    //{
    //    int KingScore = 0;
    //    KingScore = KingValue * BlackKingN;
    //    /*int KingX = 0;
    //    int KingY = 0;
    //    int EnemyNumber = 0;
    //    int AlliesNumber = 0;
    //    for (int v = 0; v < 8; v++)
    //    {
    //        for (int b = 0; b < 8; b++)
    //        {
    //            if(Board[v,b] == 6)
    //            {
    //                KingX = v;
    //                KingY = b;
    //            }
    //        }
    //    }
    //    for (int x = 0; x < 8; x++)
    //    {
    //        for (int y = 0; y < 8; y++)
    //        {
    //            if (x >= KingX - 2 && x <= KingX + 2 && y >= KingY - 2 && y <= KingY + 2)
    //            {
    //                if (Board[x, y] == 1)
    //                {
    //                    EnemyNumber += 3;
    //                }
    //                else if (Board[x, y] >= 2 && Board[x, y] <= 5)
    //                    EnemyNumber++;
    //                else if (Board[x, y] >= 6 && Board[x, y] <= 11)
    //                    AlliesNumber++;
    //            }
    //        }
    //    }
    //    KingScore += (AlliesNumber - EnemyNumber) * 5;
    //    if (!BlackCastling)
    //        KingScore -= 20;*/
    //    return KingScore;
    //}
    private void CheckPiecesLocation(int[,] mapArray)
    {
        int countWhite = 0;
        int countBlack = 0;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (mapArray[x, y] >= 1 && mapArray[x, y] <= 5 || mapArray[x, y] == 12)
                {
                    countWhite++;
                }
                if (mapArray[x, y] >= 6 && mapArray[x, y] <= 11)
                {
                    countBlack++;
                }
            }
        }
        System.Array.Resize(ref WhitePiecesX, countWhite);
        System.Array.Resize(ref WhitePiecesY, countWhite);
        System.Array.Resize(ref BlackPiecesX, countBlack);
        System.Array.Resize(ref BlackPiecesY, countBlack);
        countWhite = 0;
        countBlack = 0;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (mapArray[x, y] >= 1 && mapArray[x, y] <= 5 || mapArray[x, y] == 12)
                {
                    WhitePiecesX[countWhite] = x;
                    WhitePiecesY[countWhite] = y;
                    countWhite++;
                }
                if (mapArray[x, y] >= 6 && mapArray[x, y] <= 11)
                {
                    BlackPiecesX[countBlack] = x;
                    BlackPiecesY[countBlack] = y;
                    countBlack++;
                }
            }
        }
    }
    private Vector3 getTileCenter(int x, int z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (tileSize * x) + tileOffset;
        origin.z += (tileSize * z) + tileOffset;
        return origin;
    }

    private int EvaluateMove(long[] Evaluate)
    {
        int EqualMovesNumber = 0;
        long TopValue = -1000;
        for (int p = 0; p < Evaluate.Length; p++)
        {
            if (Evaluate[p] > TopValue )
            {
                TopValue = Evaluate[p];
            }
        }

        for (int k = 0; k < Evaluate.Length; k++)
        {
            if (Evaluate[k] == TopValue)
            {
                EqualMovesNumber++;
            }
       }
        int[] RandomMoves = new int[EqualMovesNumber];
        int RandomCounting = 0;
        for (int g = 0; g < Evaluate.Length; g++)
        {
            if (Evaluate[g] == TopValue)
            {
                RandomMoves[RandomCounting] = g;
                RandomCounting++;
            }
        }
        int ChoosingRandom = Random.Range(0, RandomMoves.Length);
        if (RandomMoves.Length == 0)
            return 0;
        return RandomMoves[ChoosingRandom];
    }
    private void EndGame()
    {
        //Debug.Log(positions);
        //positions = 0;
        Debug.Log("Check Mate");
        if (isWhiteTurn)
            Debug.Log("White team wins");
        else
            Debug.Log("Black team wins");

        checkMateScreen.SetActive(true);

        //foreach (GameObject go in activeChessman)
        //    Destroy(go);

        //isWhiteTurn = true;
        //BoardHighlights.Instance.HideHighlights();
        //Start();
    }
    private bool[,] GetPossibleMoves(int x, int y, int[,] Board)
    {
        bool[,] LegalMoves = new bool[8, 8];
        if (Board[x, y] == 1 || Board[x, y] == 7)
            LegalMoves = QueenMoves(x, y, Board);
        else if (Board[x, y] == 2 || Board[x, y] == 8)
            LegalMoves = RookMoves(x, y, Board);
        else if (Board[x, y] == 3 || Board[x, y] == 9)
            LegalMoves = BishopMoves(x, y, Board);
        else if (Board[x, y] == 4 || Board[x, y] == 10)
            LegalMoves = KnightLegalMoves(x, y, Board);
        else if (Board[x, y] == 5 || Board[x, y] == 11)
            LegalMoves = PawnMoves(x, y, Board);
        else if (Board[x, y] == 6 || Board[x, y] == 12)
            LegalMoves = KingMoves(x, y, Board);
        return LegalMoves;
    }
    private bool[,] QueenMoves(int x, int y, int[,] Board)
    {
        bool[,] LegalMoves = new bool[8, 8];
        int TempX = x;
        int TempY = y;
        bool[,] LegalMoves1 = BishopMoves(x, y, Board);
        bool[,] LegalMoves2 = RookMoves(x, y, Board);
        for (int i = 0; i < 8; i++)
        {
            for (int u = 0; u < 8; u++)
            {
                if (LegalMoves1[i, u])
                    LegalMoves[i, u] = LegalMoves1[i, u];
                if (LegalMoves2[i, u])
                    LegalMoves[i, u] = LegalMoves2[i, u];
            }
        }
        return LegalMoves;
    }
    private bool[,] RookMoves(int x, int y, int[,] Board)
    {
        bool[,] LegalMoves = new bool[8, 8];
        int TempX;
        int TempY;
        //Right
        TempX = x;
        while (true)
        {
            TempX++;
            if (TempX >= 8)
                break;
            if (Board[TempX, y] == 0)
                LegalMoves[TempX, y] = true;
            else
            {
                if (!CheckCorrectColor(TempX, y, Board))
                    LegalMoves[TempX, y] = true;
                break;
            }
        }

        //Left
        TempX = x;
        while (true)
        {
            TempX--;
            if (TempX < 0)
                break;
            if (Board[TempX, y] == 0)
                LegalMoves[TempX, y] = true;
            else
            {
                if (!CheckCorrectColor(TempX, y, Board))
                    LegalMoves[TempX, y] = true;
                break;
            }
        }

        //Up
        TempY = y;
        while (true)
        {
            TempY++;
            if (TempY >= 8)
                break;
            if (Board[x, TempY] == 0)
                LegalMoves[x, TempY] = true;
            else
            {
                if (!CheckCorrectColor(x, TempY, Board))
                    LegalMoves[x, TempY] = true;
                break;
            }
        }

        //Down
        TempY = y;
        while (true)
        {
            TempY--;
            if (TempY < 0)
                break;
            if (Board[x, TempY] == 0)
                LegalMoves[x, TempY] = true;
            else
            {
                if (!CheckCorrectColor(x, TempY, Board))
                    LegalMoves[x, TempY] = true;
                break;
            }
        }
        return LegalMoves;
    }
    private bool[,] BishopMoves(int x, int y, int[,] Board)
    {
        bool[,] LegalMoves = new bool[8, 8];
        int TempX;
        int TempY;
        // Top Left
        TempX = x;
        TempY = y;
        while (true)
        {
            TempX--;
            TempY++;
            if (TempX < 0 || TempY >= 8)
                break;

            if (Board[TempX, TempY] == 0)
                LegalMoves[TempX, TempY] = true;
            else
            {
                if (!CheckCorrectColor(TempX, TempY, Board))
                    LegalMoves[TempX, TempY] = true;
                //else if (checkForKing)
                //{
                //    LegalMoves[TempX, TempY] = true;
                //}
                break;
            }
        }

        // Top Right
        TempX = x;
        TempY = y;
        while (true)
        {
            TempX++;
            TempY++;
            if (TempX >= 8 || TempY >= 8)
                break;

            if (Board[TempX, TempY] == 0)
                LegalMoves[TempX, TempY] = true;
            else
            {
                if (!CheckCorrectColor(TempX, TempY, Board))
                    LegalMoves[TempX, TempY] = true;
                break;
            }
        }

        // Down Left
        TempX = x;
        TempY = y;
        while (true)
        {
            TempX--;
            TempY--;
            if (TempX < 0 || TempY < 0)
                break;

            if (Board[TempX, TempY] == 0)
                LegalMoves[TempX, TempY] = true;
            else
            {
                if (!CheckCorrectColor(TempX, TempY, Board))
                    LegalMoves[TempX, TempY] = true;
                break;
            }
        }

        // Down Right
        TempX = x;
        TempY = y;
        while (true)
        {
            TempX++;
            TempY--;
            if (TempX >= 8 || TempY < 0)
                break;

            if (Board[TempX, TempY] == 0)
                LegalMoves[TempX, TempY] = true;
            else
            {
                if (!CheckCorrectColor(TempX, TempY, Board))
                    LegalMoves[TempX, TempY] = true;
                break;
            }
        }
        return LegalMoves;
    }
    private bool[,] KnightLegalMoves(int x, int y, int[,] Board)
    {
        bool[,] LegalMoves = new bool[8, 8];
        //UpLeft
        KnightMove(x - 1, y + 2, ref LegalMoves, Board);

        //UpRight
        KnightMove(x + 1, y + 2, ref LegalMoves, Board);

        //RightUp
        KnightMove(x + 2, y + 1, ref LegalMoves, Board);

        //RightDown
        KnightMove(x + 2, y - 1, ref LegalMoves, Board);

        //DownLeft
        KnightMove(x - 1, y - 2, ref LegalMoves, Board);

        //DownRight
        KnightMove(x + 1, y - 2, ref LegalMoves, Board);

        //LeftUp
        KnightMove(x - 2, y + 1, ref LegalMoves, Board);

        //LeftDown
        KnightMove(x - 2, y - 1, ref LegalMoves, Board);

        return LegalMoves;
    }
    private void KnightMove(int x, int y, ref bool[,] Moves, int[,] Board)
    {
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            if (Board[x, y] == 0)
                Moves[x, y] = true;
            else if (!CheckCorrectColor(x, y, Board))
            {
                Moves[x, y] = true;
            }
        }
    }
    private bool[,] PawnMoves(int x, int y, int[,] Board)
    {
        bool[,] LegalMoves = new bool[8, 8];
        // White team move
        if (isWhiteTurn)
        {
            //Diagonal Left
            if (x != 0 && y != 7)
            {
                if (Board[x - 1, y + 1] >= 6 && Board[x - 1, y + 1] <= 11)
                    LegalMoves[x - 1, y + 1] = true;
            }
            //Diagonal Right
            if (x != 7 && y != 7)
            {
                if (Board[x + 1, y + 1] >= 6 && Board[x + 1, y + 1] <= 11)
                    LegalMoves[x + 1, y + 1] = true;
            }

            //Middle
            if (y != 7)
            {
                if (Board[x, y + 1] == 0)
                    LegalMoves[x, y + 1] = true;
            }
            //Middle on first move
            if (y == 1)
            {
                if (Board[x, y + 1] == 0 && Board[x, y + 2] == 0)
                    LegalMoves[x, y + 2] = true;
            }
        }
        else
        {
            //Diagonal Left
            if (x != 0 && y != 0)
            {
                if (Board[x - 1, y - 1] != 0 && Board[x - 1, y - 1] >= 1 && Board[x - 1, y - 1] <= 5 || Board[x - 1, y - 1] == 12)
                    LegalMoves[x - 1, y - 1] = true;
            }

            //Diagonal Right
            if (x != 7 && y != 0)
            {
                if (Board[x + 1, y - 1] != 0 && Board[x + 1, y - 1] >= 1 && Board[x + 1, y - 1] <= 5 || Board[x + 1, y - 1] == 12)
                    LegalMoves[x + 1, y - 1] = true;
            }

            //Middle
            if (y != 0)
            {
                if (Board[x, y - 1] == 0)
                    LegalMoves[x, y - 1] = true;
            }

            //Middle on first move
            if (y == 6)
            {
                if (Board[x, y - 1] == 0 && Board[x, y - 2] == 0)
                    LegalMoves[x, y - 2] = true;
            }
        }
        return LegalMoves;
    }
    private bool[,] KingMoves(int x, int y, int[,] Board)
    {
        bool[,] LegalMoves = new bool[8, 8];
        int TempX, TempY;

        // Top Side
        TempX = x - 1;
        TempY = y + 1;
        if (y != 7)
        {
            for (int k = 0; k < 3; k++)
            {
                if (TempX >= 0 && TempX < 8)
                {
                    if (Board[TempX, TempY] == 0)
                        LegalMoves[TempX, TempY] = true;
                    else if (!CheckCorrectColor(TempX, TempY, Board))
                        LegalMoves[TempX, TempY] = true;
                }
                TempX++;
            }
        }

        // Down Side
        TempX = x - 1;
        TempY = y - 1;
        if (y != 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (TempX >= 0 && TempX < 8)
                {
                    if (Board[TempX, TempY] == 0)
                        LegalMoves[TempX, TempY] = true;
                    else if (!CheckCorrectColor(TempX, TempY, Board))
                        LegalMoves[TempX, TempY] = true;
                }
                TempX++;
            }
        }


        //Middle Left
        if (x != 0)
        {
            if (Board[x - 1, y] == 0)
                LegalMoves[x - 1, y] = true;
            else if (!CheckCorrectColor(x - 1, y, Board))
                LegalMoves[x - 1, y] = true;
        }

        //Middle Right
        if (x != 7)
        {
            if (Board[x + 1, y] == 0)
                LegalMoves[x + 1, y] = true;
            else if (!CheckCorrectColor(x + 1, y, Board))
                LegalMoves[x + 1, y] = true;
        }
        //Casteling
        if (!isWhiteTurn && !BlackKingMoved)
        {
            if (!BlackLeftRookMoved)
            {
                if (Board[x - 1, y] == 0 && Board[x - 2, y] == 0 && Board[x - 3, y] == 0)
                {
                    LegalMoves[x - 2, y] = true;
                }
            }
            if (!BlackRightRookMoved)
            {
                if (Board[x + 1, y] == 0 && Board[x + 2, y] == 0)
                {
                    LegalMoves[x + 2, y] = true;
                }
            }
        }
        if (isWhiteTurn && !WhiteKingMoved)
        {
            if (!WhiteLeftRookMoved && Board[x - 1, y] == 0 && Board[x - 2, y] == 0 && Board[x - 3, y] == 0)
            {
                LegalMoves[x - 2, y] = true;
            }
            if (!WhiteRightRookMoved && Board[x + 1, y] == 0 && Board[x + 2, y] == 0)
            {
                LegalMoves[x + 2, y] = true;
            }
        }
        return LegalMoves;
    }
    private bool CheckCorrectColor(int x, int y, int[,] Board)
    {
        if ((!isWhiteTurn && ((Board[x, y] >= 1 && Board[x, y] <= 5) || Board[x, y] == 12)) || (isWhiteTurn && Board[x, y] >= 6 && Board[x, y] <= 11))
        {
            return false;
        }
        return true;
    }
    private void LoadAI()
    {
        try{
            AI_selecting = true;
            if (Turn == 0)
            {
                OpeningDatabase();
            }
            else
                LoadAlgorithm();
            Turn++;
            AI_selecting = false;
        } catch {
            errorScreen.SetActive(true);
        }
    }
    private void LoadAlgorithm()
    {
        long[] AllMovesValue;
        int[] AllMovesX;
        int[] AllMovesY;
        long[] BestMovesValue = new long[BlackPiecesX.Length];
        int[] BestMovesX = new int[BlackPiecesX.Length];
        int[] BestMovesY = new int[BlackPiecesX.Length];
        int SortingNumber = 0;
        for (int j = 0; j < BlackPiecesX.Length; j++)
        {
            BestMovesValue[j] = -100000;
            if (SelectChessManAI(BlackPiecesX[j], BlackPiecesY[j], map))
            {
                Check();
                if (check)
                {
                    AfterCheck();
                    SaveTheKing();
                    return;
                    /*if (map[BlackPiecesX[j], BlackPiecesY[j]] == 6)
                    {
                        for (int g = 0; g < PossibleMovesX.Length; g++)
                        {
                            tempMap[PossibleMovesX[g], PossibleMovesY[g]] = tempMap[BlackPiecesX[j], BlackPiecesY[j]];
                            tempMap[BlackPiecesX[j], BlackPiecesY[j]] = 0;
                            SelectChessManAI(PossibleMovesX[g], PossibleMovesY[g], tempMap);
                            check = false;
                            Check();
                            if (PossibleMovesX.Length > 0)
                            {
                                if (!check)
                                {
                                    MoveChessMan(PossibleMovesX[g], PossibleMovesY[g]);
                                    return;
                                }
                            }
                        }
                    }*/
                }
                else if (map[BlackPiecesX[j], BlackPiecesY[j]] != 12)
                {
                    AllMovesValue = new long[PossibleMovesX.Length];
                    AllMovesX = new int[PossibleMovesX.Length];
                    AllMovesY = new int[PossibleMovesX.Length];
                    for (int h = 0; h < PossibleMovesX.Length; h++)
                    {
                        if (tempMap[PossibleMovesX[h], PossibleMovesY[h]] == 12)
                        {
                            SelectChessManAI(BlackPiecesX[j], BlackPiecesY[j], map);
                            MoveChessMan(PossibleMovesX[h], PossibleMovesY[h]);
                            return;
                        }
                        if (PossibleMovesY[h] == 0 && tempMap[BlackPiecesX[j], BlackPiecesY[j]] == 11)
                        {
                            tempMap[BlackPiecesX[j], BlackPiecesY[j]] = 7;
                        }
                        else
                        {
                            tempMap[PossibleMovesX[h], PossibleMovesY[h]] = tempMap[BlackPiecesX[j], BlackPiecesY[j]];
                        }
                        tempMap[SelectedX, SelectedY] = 0;
                        SelectChessManAI(PossibleMovesX[h], PossibleMovesY[h], tempMap);
                        AllMovesValue[h] = NextBranch() + 1;
                        SelectChessMan(BlackPiecesX[j], BlackPiecesY[j], map);
                        AllMovesX[h] = PossibleMovesX[h];
                        AllMovesY[h] = PossibleMovesY[h];
                        System.Array.Copy(map, tempMap, map.Length);
                        positions++;
                    }
                    SortingNumber = EvaluateMove(AllMovesValue);
                    BestMovesValue[j] = AllMovesValue[SortingNumber];
                    BestMovesX[j] = AllMovesX[SortingNumber];
                    BestMovesY[j] = AllMovesY[SortingNumber];
                }
            }
        }
        SortingNumber = EvaluateMove(BestMovesValue);
        SelectChessManAI(BlackPiecesX[SortingNumber], BlackPiecesY[SortingNumber], map);
        MoveChessMan(BestMovesX[SortingNumber], BestMovesY[SortingNumber]);
    }
    private int NextBranch()
    {
        System.Array.Copy(tempMap, tempMap2, tempMap.Length);
        isWhiteTurn = true;
        int TopValue = 10000;
        int OptimumValue = -10000;
        int TempOptimumValue = 0;

        int TempValue = 0;
        CheckPiecesLocation(tempMap);
        for (int j = 0; j < WhitePiecesX.Length; j++)
        {
            if (SelectChessManAI(WhitePiecesX[j], WhitePiecesY[j], tempMap))
            {
                for (int h = 0; h < PossibleMovesX.Length; h++)
                {
                    if (PossibleMovesY[h] == 7 && tempMap2[WhitePiecesX[j], WhitePiecesY[j]] == 5)
                    {
                        tempMap2[PossibleMovesX[h], PossibleMovesY[h]] = 1;
                    }
                    else
                    {
                        tempMap2[PossibleMovesX[h], PossibleMovesY[h]] = tempMap2[WhitePiecesX[j], WhitePiecesY[j]];
                    }
                    tempMap2[WhitePiecesX[j], WhitePiecesY[j]] = 0;
                   TempValue = CalculateBoardValue(tempMap2) - TotalBoardValue;
                    if (TempValue < TopValue)
                    {
                        TopValue = TempValue;
                    }
                    System.Array.Copy(tempMap, tempMap2, tempMap.Length);
                    positions++;
                }
            }
        }
        CheckPiecesLocation(tempMap);
        for (int j = 0; j < WhitePiecesX.Length; j++)
        {
            if (SelectChessManAI(WhitePiecesX[j], WhitePiecesY[j], tempMap))
            {
                for (int h = 0; h < PossibleMovesX.Length; h++)
                {
                    if (PossibleMovesY[h] == 7 && tempMap2[WhitePiecesX[j], WhitePiecesY[j]] == 5)
                    {
                        tempMap2[PossibleMovesX[h], PossibleMovesY[h]] = 1;
                    }
                    else
                    {
                        tempMap2[PossibleMovesX[h], PossibleMovesY[h]] = tempMap2[WhitePiecesX[j], WhitePiecesY[j]];
                    }
                    tempMap2[WhitePiecesX[j], WhitePiecesY[j]] = 0;
                    TempValue = CalculateBoardValue(tempMap2) - TotalBoardValue;
                    if (TempValue == TopValue)
                    {
                        TempOptimumValue = NextBranch2();
                    }
                    if (TempOptimumValue > OptimumValue)
                    {
                        OptimumValue = TempOptimumValue;
                    }
                    System.Array.Copy(tempMap, tempMap2, tempMap.Length);
                }
            }
        }
        CheckPiecesLocation(map);
        isWhiteTurn = false;
        return OptimumValue;
    }
    private int NextBranch2()
    {
        int[,] tempMap3 = new int[8, 8];
        System.Array.Copy(tempMap2, tempMap3, tempMap2.Length);
        int TopValue = -10000;
        int TempValue = 0;
        isWhiteTurn = false;
        bool checkMate = false;
        CheckPiecesLocation(tempMap2);
        for (int j = 0; j < BlackPiecesX.Length; j++)
        {
            if (SelectChessManAI(BlackPiecesX[j], BlackPiecesY[j], tempMap2))
            {
                for (int h = 0; h < PossibleMovesX.Length; h++)
                {
                    if (tempMap3[PossibleMovesX[h], PossibleMovesY[h]] == 12)
                        checkMate = true;
                    if (PossibleMovesY[h] == 0 && tempMap3[BlackPiecesX[j], BlackPiecesY[j]] == 11)
                    {
                        tempMap3[PossibleMovesX[h], PossibleMovesY[h]] = 7;
                    }
                    else
                    {
                    tempMap3[PossibleMovesX[h], PossibleMovesY[h]] = tempMap3[BlackPiecesX[j], BlackPiecesY[j]];
                    }
                    tempMap2[BlackPiecesX[j], BlackPiecesY[j]] = 0;
                    TempValue = CalculateBoardValue(tempMap3) - TotalBoardValue;
                    if (checkMate)
                    {
                        TempValue += KingValue;
                        checkMate = false;
                    }
                    if (TempValue > TopValue)
                    {
                        TopValue = TempValue;
                    }
                    positions++;
                    System.Array.Copy(tempMap2, tempMap3, tempMap2.Length);
                }
            }
        }
        CheckPiecesLocation(tempMap);
        isWhiteTurn = true;
        return TopValue;
    }
    private void OpeningDatabase()
    {
        if (map[4, 3] == 5)
        {
            SelectChessManAI(2, 6, map);
            MoveChessMan(2, 4);
        }
        else if (map[3,3] == 5)
        {
            SelectChessManAI(3, 6, map);
            MoveChessMan(3, 4);
        }
        else
        {
            SelectChessManAI(1, 7, map);
            MoveChessMan(2, 5);
       }
    }
    //private bool SelectChessManAi(int x, int y, int[,] MapArray)
    //{
    //    if (MapArray[x, y] == 0)
    //        return false;
    //    if (!CheckCorrectColor(x, y, MapArray))
    //        return false;
    //    bool hasAtleastOneMove = false;
    //    SelectedX = x;
    //    SelectedY = y;
    //    allowedMoves = new bool[8, 8];
    //    allowedMoves = GetPossibleMoves(x, y, MapArray);


    //    int NumberPossibleMoves = 0;
    //    for (int a = 0; a < allowedMoves.GetLength(0); a++)
    //    {
    //        for (int l = 0; l < allowedMoves.GetLength(1); l++)
    //        {
    //            if (allowedMoves[a, l])
    //                NumberPossibleMoves++;
    //        }
    //    }
    //    PossibleMovesX = new int[NumberPossibleMoves];
    //    PossibleMovesY = new int[NumberPossibleMoves];
    //    int count = 0;
    //    for (int v = 0; v < allowedMoves.GetLength(0); v++)
    //    {
    //        for (int q = 0; q < allowedMoves.GetLength(1); q++)
    //        {
    //            if (allowedMoves[v, q])
    //            {
    //                PossibleMovesX[count] = v;
    //                PossibleMovesY[count] = q;
    //                count++;
    //            }
    //        }
    //    }
    //    if (NumberPossibleMoves > 0)
    //    {
    //        hasAtleastOneMove = true;
    //    }
    //    else if (!hasAtleastOneMove)
    //        return false;
    //    return true;
    //}
    private void SaveTheKing()
    {
        for (int s = 0; s < 8; s++)
        {
            for (int r = 0; r < 8; r++)
            {
                if (chessMans[s, r] != null && !chessMans[s,r].isWhite)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        for (int b = 0; b < 8; b++)
                        {
                            allowedMoves[k, b] = false;
                        }
                    }
                    if (alreadyIdentifiedPieceThatCanMove && chessMans[s,r].GetType() != typeof(King))
                    {
                        for (int k = 0; k < movesForPiecesThatCanMoveWhenCheckCoordinatesX.Count; k++)
                        {
                            allowedMoves[movesForPiecesThatCanMoveWhenCheckCoordinatesX[k], movesForPiecesThatCanMoveWhenCheckCoordinatesY[k]] = true;
                            if (chessMans[s, r].PossibleMove()[movesForPiecesThatCanMoveWhenCheckCoordinatesX[k], movesForPiecesThatCanMoveWhenCheckCoordinatesY[k]] == true)
                            {
                                allowedMoves[movesForPiecesThatCanMoveWhenCheckCoordinatesX[k], movesForPiecesThatCanMoveWhenCheckCoordinatesY[k]] = true;
                            }
                            else
                            {
                                allowedMoves[movesForPiecesThatCanMoveWhenCheckCoordinatesX[k], movesForPiecesThatCanMoveWhenCheckCoordinatesY[k]] = false;
                            }
                        }

                    }
                    for (int k = 0; k < 8; k++)
                    {
                        for (int b = 0; b < 8; b++)
                        {
                            if(allowedMoves[k, b]) {
                                SelectChessMan(s, r, map);
                                MoveChessMan(k, b);
                                return;
                            }
                        }
                    }
                    if (chessMans[s,r].GetType() == typeof(King))
                    {
                        allowedMoves = GetPossibleMoves(s,r,map);
                        for (int k = 0; k < 8; k++)
                        {
                            for (int b = 0; b < 8; b++)
                            {
                                if (allowedMoves[k,b])
                                {
                                    for (int l = 0; l < 8; l++)
                                    {
                                        for (int g = 0; g < 8; g++)
                                        {
                                            if (chessMans[l, g] != null)
                                            {
                                                if (chessMans[l, g].isWhite != chessMans[s, r].isWhite)
                                                {
                                                    for (int d = 0; d < 8; d++)
                                                    {
                                                        for (int m = 0; m < 8; m++)
                                                        {
                                                            checkForKing = true;
                                                            if (chessMans[l, g].PossibleMove()[d, m] == allowedMoves[k, b] && chessMans[l, g].PossibleMove()[d, m] && allowedMoves[k, b])
                                                            {
                                                                if (d == k && m == b)
                                                                {
                                                                    allowedMoves[k, b] = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    for (int k = 0; k < 8; k++)
                    {
                        for (int b = 0; b < 8; b++)
                        {
                            if (allowedMoves[k, b])
                            {
                                SelectChessMan(s, r, map);
                                MoveChessMan(k, b);
                                return;
                            }
                        }
                    }
                    checkForKing = false;
                }
            }
        }
        
    }
    
    private bool GetMovesAI(int x, int y, int[,] MapArray)
    {
        allowedMoves = new bool[8, 8];
        allowedMoves = GetPossibleMoves(x, y, MapArray);
        int NumberPossibleMoves = 0;
        for (int a = 0; a < allowedMoves.GetLength(0); a++)
        {
            for (int l = 0; l < allowedMoves.GetLength(1); l++)
            {
                if (allowedMoves[a, l])
                {
                    NumberPossibleMoves++;
                }
            }
        }
        PossibleMovesX = new int[NumberPossibleMoves];
        PossibleMovesY = new int[NumberPossibleMoves];
        int count = 0;
        for (int v = 0; v < allowedMoves.GetLength(0); v++)
        {
            for (int q = 0; q < allowedMoves.GetLength(1); q++)
            {
                if (allowedMoves[v, q])
                {
                    PossibleMovesX[count] = v;
                    PossibleMovesY[count] = q;
                    count++;
                }
            }
        }
        if (NumberPossibleMoves > 0)
            return true;
        else
            return false;
    }
}


