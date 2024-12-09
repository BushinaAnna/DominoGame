using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;



namespace Domino
{
    public class Game
    {
        public List<Tile> Board { get; set; } = new List<Tile>();
        public static bool isPlayerTurn { get; set; } = true;  // (true - ход игрока, false - ход компьютера)
        public Player humanPlayer { get; private set; }
        public Player aiPlayer { get; private set; }
        public List<Tile> TileBank { get; private set; } = new List<Tile>(); 

        public Game()
        {
            humanPlayer = new Player();
            aiPlayer = new Player();
            DealTiles();
        }
        public void DealTiles()
        {
            List<Tile> allTiles = GenerateAllPossibleTiles();

            allTiles = allTiles.OrderBy(x => Guid.NewGuid()).ToList();

            humanPlayer.Tiles = allTiles.Take(7).ToList();
            aiPlayer.Tiles = allTiles.Skip(7).Take(7).ToList();

            TileBank = allTiles.Skip(14).ToList();
        }
        public Tile MakeComputerMove(List<Tile> board)
        {
            int bestScore = int.MinValue;
            Tile bestMove = null;

            foreach (var tile in aiPlayer.Tiles)
            {
                int index = IndexPlayTile(tile, out bool vertical, out bool flip, out bool Double);
                if (index != -30)
                {
                    List<Tile> newBoard = new List<Tile>(board);
                    newBoard = PossibilityMove(newBoard, tile);

                    List<Tile> newAiTiles = new List<Tile>(aiPlayer.Tiles);
                    newAiTiles.Remove(tile);

                    List<Tile> UnknownHumanTiles = GetUnknownTiles(newBoard, aiPlayer.Tiles);                  
                    int score = Minimax(newBoard, newAiTiles, UnknownHumanTiles, depth: 3, alpha: int.MinValue, beta: int.MaxValue, maximizingPlayer: false);

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = tile;
                    }
                }
            }

            if (bestMove != null)
            {
                Debug.WriteLine($"Компьютер сделал ход: {bestMove?.Side1}|{bestMove?.Side2}");

                PossibilityMove(board, bestMove);
                aiPlayer.Tiles.Remove(bestMove); 
                return bestMove;
            }

            bool result = DrawTile(0, TileBank);
            if (result)
            {
                return new Tile(7, 7); // Условный индикатор успешного взятия фищки из банка
            }

            return null;
        }
        public bool DrawTile(int play, List<Tile> tileBank)
        {
            if (tileBank.Count > 0)
            {
                if (play == 0)
                {
                    aiPlayer.Tiles.Add(tileBank.First());
                }
                else
                {
                    humanPlayer.Tiles.Add(tileBank.First());
                }
                tileBank.RemoveAt(0);
                return true; 
            }
            return false;
        }
        public int IndexPlayTile(Tile tile, out bool vertical, out bool flip, out bool Double)
        {
            if (Board.Count == 0)
            {
                vertical = tile.IsDouble;
                Double = tile.IsDouble;
                flip = false;
                
                return 0;
            }
            else
            {
                Tile firstTile = Board.First();
                Tile lastTile = Board.Last();

                if (tile.Side1 == lastTile.Side2)
                {
                    vertical = tile.IsDouble;
                    Double = tile.IsDouble;
                    flip = false;

                    return Board.Count;
                }
                else if (tile.Side2 == lastTile.Side2)
                {
                    vertical = tile.IsDouble;
                    Double = tile.IsDouble;
                    flip = true;

                    return Board.Count;
                }
                else if (tile.Side2 == firstTile.Side1)
                {
                    vertical = tile.IsDouble;
                    Double = tile.IsDouble;
                    flip = false;

                    return 0;
                }
                else if (tile.Side1 == firstTile.Side1)
                {
                    vertical = tile.IsDouble;
                    Double = tile.IsDouble;
                    flip = true;

                    return 0;
                }

                for (int i = 1; i < Board.Count - 1; i++)
                {
                    if (Board[i].IsDouble)
                    {
                        if (!Board[i + 1].IsVertical && !Board[i - 1].IsVertical)
                        {
                            if (tile.Side2 == Board[i].Side2)
                            {
                                vertical = true;
                                Double = false;
                                flip = true;

                                return i + 1;
                            }
                            if (tile.Side1 == Board[i].Side2)
                            {
                                vertical = true;
                                Double = false;
                                flip = false;

                                return i + 1;
                            }
                            if (tile.Side1 == Board[i].Side1)
                            {
                                vertical = true;
                                Double = false;
                                flip = true;

                                return i;
                            }
                            if (tile.Side2 == Board[i].Side1)
                            {
                                vertical = true;
                                Double = false;
                                flip = false;

                                return i;
                            }

                        }
                        for (int j = i + 1; j < Board.Count; j++)
                        {
                            if (!Board[j].IsVertical)
                            {
                                break;
                            }

                            if (Board[j].IsVertical && !Board[j + 1].IsVertical)
                            {

                                if (tile.Side2 == Board[j].Side2)
                                {
                                    vertical = true;
                                    Double = false;
                                    flip = true;

                                    return j + 1;
                                }
                                if (tile.Side1 == Board[j].Side2)
                                {
                                    vertical = true;
                                    Double = false;
                                    flip = false;

                                    return j + 1;
                                }

                            }
                        }
                        for (int j = i - 1; j > 0; j--)
                        {
                            if (!Board[j].IsVertical)
                            {
                                break;
                            }
                            else if (!Board[j - 1].IsVertical)
                            {
                                if (tile.Side1 == Board[j].Side1)
                                {
                                    vertical = true;
                                    Double = false;
                                    flip = true;

                                    return j;
                                }
                                if (tile.Side2 == Board[j].Side1)
                                {
                                    vertical = true;
                                    Double = false;
                                    flip = false;

                                    return j;
                                }
                            }
                        }
                    }
                }
            }
            vertical = tile.IsDouble;
            Double = tile.IsDouble;
            flip = false;

            return -30;
        }
        private int Minimax(List<Tile> board, List<Tile> aiTiles, List<Tile> playerTiles, int depth, int alpha, int beta, bool maximizingPlayer)
        {
            if (depth == 0 || IsGameOver() != null)
            {
                return EvaluateBoard(board, aiTiles, playerTiles);
            }

            if (maximizingPlayer)
            {
                int maxEval = int.MinValue;

                foreach (var tile in aiTiles)
                {
                    int index = IndexPlayTile(tile, out bool vertical, out bool flip, out bool Double);
                    if (index != -30)
                    {
                        List<Tile> newBoard = new List<Tile>(board);
                        newBoard.Insert(index, tile);

                        List<Tile> newAiTiles = new List<Tile>(aiTiles);
                        newAiTiles.Remove(tile);

                        int eval = Minimax(newBoard, newAiTiles, playerTiles, depth - 1, alpha, beta, false);
                        maxEval = Math.Max(maxEval, eval);
                        alpha = Math.Max(alpha, eval);

                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;

                foreach (var tile in playerTiles)
                {
                    int index = IndexPlayTile(tile, out bool vertical, out bool flip, out bool Double);
                    if (index != -30)
                    {
                        List<Tile> newBoard = new List<Tile>(board);
                        newBoard.Insert(index, tile);

                        List<Tile> newPlayerTiles = new List<Tile>(playerTiles);
                        newPlayerTiles.Remove(tile);

                        int eval = Minimax(newBoard, aiTiles, newPlayerTiles, depth - 1, alpha, beta, true);
                        minEval = Math.Min(minEval, eval);
                        beta = Math.Min(beta, eval);

                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
                return minEval;
            }
        }
        public string IsGameOver()
        {
            if (aiPlayer.Tiles.Count == 0)
            {
                return "Компьютер победил.";
            }
            else if (humanPlayer.Tiles.Count == 0)
            {
                return "Вы победили!";
            }
            else if (TileBank.Count == 0 && !CanAnyPlayerMove())
            {
                int aiScore = CalculateScore(aiPlayer.Tiles);
                int playerScore = CalculateScore(humanPlayer.Tiles);

                string winner = aiScore < playerScore ? "Вы победили!" :
                                aiScore > playerScore ? "Компьютер победил!" :
                                "Ничья!";

                return winner;
            }
            return null;
        }
        private int EvaluateBoard(List<Tile> board, List<Tile> aiTiles, List<Tile> playerTiles)
        {
            return playerTiles.Count - aiTiles.Count;
        }
        private List<Tile> GetUnknownTiles(List<Tile> board, List<Tile> aiTiles)
        {
            List<Tile> allTiles = GenerateAllPossibleTiles();
            List<Tile> knownTiles = board.Concat(aiTiles).ToList();
            List<Tile> UnknownTiles = allTiles.Except(knownTiles).ToList();

            int handSize = humanPlayer.Tiles.Count;
            List<Tile> UnknownHumanTiles = UnknownTiles.Take(handSize).ToList();

            return UnknownHumanTiles;
        }
        private List<Tile> GenerateAllPossibleTiles()
        {
            var allTiles = new List<Tile>();
            for (int i = 0; i <= 6; i++)
            {
                for (int j = i; j <= 6; j++)
                {
                    allTiles.Add(new Tile(i, j));
                }
            }
            return allTiles;
        }
        private int CalculateScore(List<Tile> tiles)
        {
            return tiles.Sum(tile => tile.Side1 + tile.Side2);
        }
        private bool CanAnyPlayerMove()
        {
            if (isPlayerTurn)
            {
                foreach (var tile in humanPlayer.Tiles)
                {
                    if (IndexPlayTile(tile, out bool vertical, out bool flip, out bool Double) != -30) return true;
                }
            }

            else
            {
                foreach (var tile in aiPlayer.Tiles)
                {
                    if (IndexPlayTile(tile, out bool vertical, out bool flip, out bool Double) != -30) return true;
                }
            }

            return false;
        }        
        private List<Tile> PossibilityMove(List<Tile> board, Tile tile)
        {
            int index = IndexPlayTile(tile, out bool vertical, out bool flip, out bool Double);
            if (index != -30)
            {
                if (flip)
                {
                    tile.Flip(tile);
                }
                if (vertical)
                {
                    tile.IsVertical = true;
                }                
                if(!Double)
                {
                    tile.IsDouble = false;
                }

                board.Insert(index, tile);
                return board;
            }
            else
            {
                return board;
            }
        }
    }

}
