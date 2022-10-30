class Solution
{

    /*
     * Complete the simplifiedChessEngine function below.
     */

    static string simplifiedChessEngine(char[][] whites, char[][] blacks, int moves)
    {
        List<Piece> blackPieces = new();
        List<Piece> whitePieces = new();

        //placing on the table
        for (int i = 0; i < blacks.Length; i++)
        {
            if (blacks[i].Length == 3)
            {
                char piece = blacks[i][0];
                int col = int.Parse(ColToColNum(blacks[i][1]).ToString());
                int row = int.Parse(blacks[i][2].ToString());
                blackPieces.Add(new Piece() { X = col, Y = row, PType = ParsePieceType(piece), ID = i });
            }
        }

        for (int i = 0; i < whites.Length; i++)
        {
            if (whites[i].Length == 3)
            {
                char piece = whites[i][0];
                int col = int.Parse(ColToColNum(whites[i][1]).ToString());
                int row = int.Parse(whites[i][2].ToString());
                whitePieces.Add(new Piece() { X = col, Y = row, PType = ParsePieceType(piece), ID = i});
            }
        }

        //Looping implementation per moves had some issues with performance on the test platform, thus running this as multiple loops
        //still unsure if "moves" means total moves, as in white moves, black moves, 
        List<Tuple<List<Piece>, List<Piece>>> Boards1 = new();
        List<Tuple<List<Piece>, List<Piece>>> Boards2 = new();
        List<Tuple<List<Piece>, List<Piece>>> Boards3 = new();
        List<Tuple<List<Piece>, List<Piece>>> Boards4 = new();
        List<Tuple<List<Piece>, List<Piece>>> Boards5 = new();
        List<Tuple<List<Piece>, List<Piece>>> Boards6 = new();
        List<Piece> newWhitesList = new();
        List<Piece> newBlacksList = new();
        Piece newPiece = new();
        Piece whiteQueen = new();
        Piece blackQueen = blackPieces.First(x => x.PType == Piece.PieceType.Queen);

        if (moves > 0)
        {
            foreach (var piece in whitePieces)
            {
                newWhitesList = new();
                newWhitesList.AddRange(whitePieces.Where(x => x.ID != piece.ID));
                piece.ReCalculatePossibleMoves(whitePieces, blackPieces);

                foreach (var move in piece.PossibleMoves)
                {
                    if (move[1] == blackQueen.Y && move[0] == blackQueen.X)
                    {
                        return "YES";
                    }
                    newPiece = new() { PType = piece.PType, ID = piece.ID, X = move[0], Y = move[1] };
                    newBlacksList = new();
                    newBlacksList.AddRange(blackPieces);
                    newBlacksList.RemoveAll(x => x.X == newPiece.X && x.Y == newPiece.Y);
                    newWhitesList.RemoveAll(x => x.ID == piece.ID);
                    newWhitesList.Add(newPiece);
                    Boards1.Add(new Tuple<List<Piece>, List<Piece>>(newWhitesList, newBlacksList));
                }
            }
        }
        if (moves > 1)
        {
            foreach (var newBoard in Boards1)
            {
                foreach (var piece in newBoard.Item2)
                {
                    whiteQueen = newBoard.Item1.First(x => x.PType == Piece.PieceType.Queen);
                    newBlacksList = new();
                    newBlacksList.AddRange(newBoard.Item2.Where(x => x.ID != piece.ID));
                    piece.ReCalculatePossibleMoves(newBoard.Item2, newBoard.Item1);

                    foreach (var move in piece.PossibleMoves)
                    {
                        if (move[1] == whiteQueen.Y && move[0] == whiteQueen.X)
                        {
                            continue;
                        }
                        newPiece = new() { PType = piece.PType, ID = piece.ID, X = move[0], Y = move[1] };
                        newWhitesList = new();
                        newWhitesList.AddRange(newBoard.Item1);
                        newWhitesList.RemoveAll(x => x.X == newPiece.X && x.Y == newPiece.Y);
                        newBlacksList.RemoveAll(x => x.ID == piece.ID);
                        newBlacksList.Add(newPiece);
                        Boards2.Add(new Tuple<List<Piece>, List<Piece>>(newWhitesList, newBlacksList));
                    }
                }
            }
        }

        if (moves > 2)
        {
            foreach (var newBoard in Boards2)
            {
                foreach (var piece in newBoard.Item1)
                {
                    blackQueen = newBoard.Item2.First(x => x.PType == Piece.PieceType.Queen);
                    newWhitesList = new();
                    newWhitesList.AddRange(newBoard.Item1.Where(x => x.ID != piece.ID));
                    piece.ReCalculatePossibleMoves(newBoard.Item1, newBoard.Item2);

                    foreach (var move in piece.PossibleMoves)
                    {
                        if (move[1] == blackQueen.Y && move[0] == blackQueen.X)
                        {
                            return "YES";
                        }
                        newPiece = new() { PType = piece.PType, ID = piece.ID, X = move[0], Y = move[1] };
                        newBlacksList = new();
                        newBlacksList.AddRange(newBoard.Item2);
                        newBlacksList.RemoveAll(x => x.X == newPiece.X && x.Y == newPiece.Y);
                        newWhitesList.RemoveAll(x => x.ID == piece.ID);
                        newWhitesList.Add(newPiece);
                        Boards3.Add(new Tuple<List<Piece>, List<Piece>>(newWhitesList, newBlacksList));
                    }
                }
            }
        }

        if (moves > 3)
        {
            foreach (var newBoard in Boards3)
            {
                foreach (var piece in newBoard.Item2)
                {
                    whiteQueen = newBoard.Item1.First(x => x.PType == Piece.PieceType.Queen);
                    newBlacksList = new();
                    newBlacksList.AddRange(newBoard.Item2.Where(x => x.ID != piece.ID));
                    piece.ReCalculatePossibleMoves(newBoard.Item2, newBoard.Item1);

                    foreach (var move in piece.PossibleMoves)
                    {
                        if (move[1] == whiteQueen.Y && move[0] == whiteQueen.X)
                        {
                            continue;
                        }
                        newPiece = new() { PType = piece.PType, ID = piece.ID, X = move[0], Y = move[1] };
                        newWhitesList = new();
                        newWhitesList.AddRange(newBoard.Item1);
                        newWhitesList.RemoveAll(x => x.X == newPiece.X && x.Y == newPiece.Y);
                        newBlacksList.RemoveAll(x => x.ID == piece.ID);
                        newBlacksList.Add(newPiece);
                        Boards4.Add(new Tuple<List<Piece>, List<Piece>>(newWhitesList, newBlacksList));
                    }
                }
            }
        }

        if (moves > 4)
        {
            foreach (var newBoard in Boards4)
            {
                foreach (var piece in newBoard.Item1)
                {
                    blackQueen = newBoard.Item2.First(x => x.PType == Piece.PieceType.Queen);
                    newWhitesList = new();
                    newWhitesList.AddRange(newBoard.Item1.Where(x => x.ID != piece.ID));
                    piece.ReCalculatePossibleMoves(newBoard.Item1, newBoard.Item2);

                    foreach (var move in piece.PossibleMoves)
                    {
                        if (move[1] == blackQueen.Y && move[0] == blackQueen.X)
                        {
                            return "YES";
                        }
                        newPiece = new() { PType = piece.PType, ID = piece.ID, X = move[0], Y = move[1] };
                        newBlacksList = new();
                        newBlacksList.AddRange(newBoard.Item2);
                        newBlacksList.RemoveAll(x => x.X == newPiece.X && x.Y == newPiece.Y);
                        newWhitesList.RemoveAll(x => x.ID == piece.ID);
                        newWhitesList.Add(newPiece);
                        Boards5.Add(new Tuple<List<Piece>, List<Piece>>(newWhitesList, newBlacksList));
                    }
                }
            }
        }
    
        //sixth loop is meaningless as its black move
        return "NO";
    }

    private static Piece.PieceType ParsePieceType(char piece)
    {
        return piece switch
        {
            'Q' => Piece.PieceType.Queen,
            'N' => Piece.PieceType.Knight,
            'B' => Piece.PieceType.Bishop,
            'R' => Piece.PieceType.Rook,
            _ => Piece.PieceType.Queen,
        };
    }

    static char ColToColNum(char col)
    {
        return col switch
        {
            'A' => '1',
            'B' => '2',
            'C' => '3',
            'D' => '4',
            _ => '0',
        };
    }

    
        
    static void Main()
    {
        //char[][] whites = new char[2][];
        //whites[0] = new char[3] { 'N', 'B', '2' };
        //whites[1] = new char[3] { 'Q', 'B', '1' };

        //char[][] blacks = new char[1][];
        //blacks[0] = new char[3] { 'Q', 'A', '4' };
        char[][] whites = new char[4][];
        char[][] blacks = new char[5][];


        whites[0] = new char[3] { 'N', 'B', '1' };
        whites[1] = new char[3] { 'N', 'C', '2' };
        whites[2] = new char[3] { 'B', 'C', '4' };
        whites[3] = new char[3] { 'Q', 'A', '4' };
        blacks[0] = new char[3] { 'N', 'B', '2' };
        blacks[1] = new char[3] { 'N', 'A', '3' };
        blacks[2] = new char[3] { 'B', 'D', '3' };
        blacks[3] = new char[3] { 'R', 'C', '3' };
        blacks[4] = new char[3] { 'Q', 'C', '1' };
        string result = simplifiedChessEngine(whites, blacks, 4);

    }
}
public class Piece
{
    public int ID { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public PieceType PType { get; set; }
    public List<int[]> PossibleMoves { get; set; } = new List<int[]>();

    internal List<int[]> CalculatePossibleMoves(List<Piece> allyPieces, List<Piece> foePieces)
    {
        List<int[]> result = new();
        switch (PType)
        {
            case PieceType.Queen:
                {
                    result.AddRange(CalculatePossibleMovesBishop(allyPieces, foePieces));
                    result.AddRange(CalculatePossibleMovesRook(allyPieces, foePieces));
                }
                break;
            case PieceType.Knight:
                {
                    result.AddRange(CalculatePossibleMovesKnight(allyPieces));
                }
                break;
            case PieceType.Bishop:
                {
                    result.AddRange(CalculatePossibleMovesBishop(allyPieces, foePieces));
                }
                break;
            case PieceType.Rook:
                {
                    result.AddRange(CalculatePossibleMovesRook(allyPieces, foePieces));
                }
                break;
        }
        return result;
    }

    private List<int[]> CalculatePossibleMovesRook(List<Piece> allyPieces, List<Piece> foePieces)
    {
        List<int[]> result = new();
        bool rightBlocked = false;
        bool leftBlocked = false;
        bool upBlocked = false;
        bool downBlocked = false;

        for (int i = 1; i < 4; i++)
        {
            //right
            if (!rightBlocked && IsCoordinateInsideLimit(X + i, Y))
            {
                if (allyPieces.Any(p => p.X == this.X + i && p.Y == this.Y))
                {
                    rightBlocked = true;
                }
                else
                {
                    if (foePieces.Any(p => p.X == this.X + i && p.Y == this.Y))
                    {
                        rightBlocked = true;
                    }
                    result.Add(new int[] { this.X + i, this.Y });
                }
            }
            else
            {
                rightBlocked = true;
            }

            //left
            if (!leftBlocked && IsCoordinateInsideLimit(X - i, Y))
            {
                if (allyPieces.Any(p => p.X == this.X - i && p.Y == this.Y))
                {
                    leftBlocked = true;
                }
                else
                {
                    if (foePieces.Any(p => p.X == this.X - i && p.Y == this.Y))
                    {
                        leftBlocked = true;
                    }
                    result.Add(new int[] { this.X - i, this.Y});
                }
            }
            else
            {
                leftBlocked = true;
            }

            //up
            if (!upBlocked && IsCoordinateInsideLimit(X, Y + i))
            {
                if (allyPieces.Any(p => p.X == this.X && p.Y == this.Y + i))
                {
                    upBlocked = true;
                }
                else
                {
                    if (foePieces.Any(p => p.X == this.X && p.Y == this.Y + i))
                    {
                        upBlocked = true;
                    }
                    result.Add(new int[] { this.X,  this.Y + i });
                }
            }
            else
            {
                upBlocked = true;
            }

            //up
            if (!downBlocked && IsCoordinateInsideLimit(X, Y - i))
            {
                if (allyPieces.Any(p => p.X == this.X && p.Y == this.Y - i))
                {
                    downBlocked = true;
                }
                else
                {
                    if (foePieces.Any(p => p.X == this.X && p.Y == this.Y - i))
                    {
                        downBlocked = true;
                    }
                    result.Add(new int[] { this.X, this.Y - i });
                }
            }
            else
            {
                downBlocked = true;
            }

        }
        return result;
    }

    private List<int[]> CalculatePossibleMovesBishop(List<Piece> allyPieces, List<Piece> foePieces)
    {
        List<int[]> result = new();
        for (int i = 1; i < 4; i++)
        {
            bool topRightBlocked = false;
            bool topLeftBlocked = false;
            bool bottomRightBlocked = false;
            bool bottomLeftBlocked = false;

            //topright
            if (!topRightBlocked && IsCoordinateInsideLimit(X + i, Y + i))
            {
                if (allyPieces.Any(p => p.X == this.X + i && p.Y == this.Y + i))
                {
                    topRightBlocked = true;
                }
                else
                {
                    if (foePieces.Any(p => p.X == this.X + i && p.Y == this.Y + i))
                    {
                        topRightBlocked = true;
                    }
                    result.Add(new int[] { this.X + 1, this.Y + 1 });
                }
            }
            else
            {
                topRightBlocked = true;
            }

            //topleft
            if (!topLeftBlocked && IsCoordinateInsideLimit(X - i, Y + i))
            {
                if (allyPieces.Any(p => p.X == this.X - i && p.Y == this.Y + i))
                {
                    topLeftBlocked = true;
                }
                else
                {
                    if (foePieces.Any(p => p.X == this.X - i && p.Y == this.Y + i))
                    {
                        topLeftBlocked = true;
                    }
                    result.Add(new int[] { this.X - i,  this.Y + i });
                }
            }
            else
            {
                topLeftBlocked = true;
            }

            //bottomRight
            if (!bottomRightBlocked && IsCoordinateInsideLimit(X + i, Y - i))
            {
                if (allyPieces.Any(p => p.X == this.X + i && p.Y == this.Y - i))
                {
                    bottomRightBlocked = true;
                }
                else
                {
                    if (foePieces.Any(p => p.X == this.X + i && p.Y == this.Y - i))
                    {
                        bottomRightBlocked = true;
                    }
                    result.Add(new int[] { this.X + i,  this.Y - i });
                }
            }
            else
            {
                bottomRightBlocked = true;
            }

            //bottomRight
            if (!bottomLeftBlocked && IsCoordinateInsideLimit(X - i, Y - i))
            {
                if (allyPieces.Any(p => p.X == this.X - i && p.Y == this.Y - i))
                {
                    bottomLeftBlocked = true;
                }
                else
                {
                    if (foePieces.Any(p => p.X == this.X - i && p.Y == this.Y - i))
                    {
                        bottomLeftBlocked = true;
                    }
                    result.Add(new int[] { this.X - i,  this.Y - i });
                }
            }
            else
            {
                bottomLeftBlocked = true;
            }

        }
        return result;
    }

    private List<int[]> CalculatePossibleMovesKnight(List<Piece> allyPieces)
    {
        List <int[]> result = new();
        //1
        if (!allyPieces.Any(x => x.Y == Y + 1 && x.X == X + 2) && IsCoordinateInsideLimit(Y + 1, X + 2))
        {
            result.Add(new int[] { this.X + 2, this.Y + 1 });
        }
        //2
        if (!allyPieces.Any(x => x.Y == Y + 2 && x.X == X + 1) && IsCoordinateInsideLimit(Y + 2, X + 1))
        {
            result.Add(new int[] { this.X + 1, this.Y + 2  });
        }
        //3
        if (!allyPieces.Any(x => x.Y == Y - 1 && x.X == X + 2) && IsCoordinateInsideLimit(Y - 1, X + 2))
        {
            result.Add(new int[] { this.X + 2, this.Y - 1  });
        }
        //4
        if (!allyPieces.Any(x => x.Y == Y - 2 && x.X == X + 1) && IsCoordinateInsideLimit(Y - 2, X + 1))
        {
            result.Add(new int[] { this.X + 1, this.Y - 2 });
        }
        //5
        if (!allyPieces.Any(x => x.Y == Y - 2 && x.X == X - 1) && IsCoordinateInsideLimit(Y - 2, X - 1))
        {
            result.Add(new int[] { this.X - 1, this.Y - 2 });
        }
        //6
        if (!allyPieces.Any(x => x.Y == Y - 1 && x.X == X - 2) && IsCoordinateInsideLimit(Y - 1, X - 2))
        {
            result.Add(new int[] { this.X - 2, this.Y - 1 });
        }
        //7
        if (!allyPieces.Any(x => x.Y == Y + 1 && x.X == X - 2) && IsCoordinateInsideLimit(Y + 1, X - 2))
        {
            result.Add(new int[] { this.X - 2, this.Y + 1 });
        }
        //8
        if (!allyPieces.Any(x => x.Y == Y + 2 && x.X == X - 1) && IsCoordinateInsideLimit(Y + 2, X - 1))
        {
            result.Add(new int[] { this.X - 1, this.Y + 2  });
        }

        return result;
    }

    private static bool IsCoordinateInsideLimit(int x, int y)
    {
        return x > 0 && y > 0 && x < 5 && y < 5;
    }

    internal void ReCalculatePossibleMoves(List<Piece> allyPieces, List<Piece> foePieces)
    {
        PossibleMoves = new List<int[]>();
        PossibleMoves.AddRange(CalculatePossibleMoves(allyPieces, foePieces));
    }

    public enum PieceType
    {
        Queen,
        Knight,
        Bishop,
        Rook
    }

}
