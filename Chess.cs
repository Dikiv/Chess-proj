
using System.Data;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ChessRules.Tests")]
namespace ChessApp;

    class Chess{
    
    public static void Main(string[] args){
        var x = new Board(8,true,true);
        //x.populateStandard(true);
        playerTurn(x,true);
    }

    private static void printallmoves(Board b){
        foreach(Square s in b.getBoard()){
                if(s.GetPiece() is not null){

                var m = s.GetPiece().Move(s.getCoordinate().Item1,s.getCoordinate().Item2,b);
                Console.WriteLine(s.GetPiece().getColor() + " " + s.Show() + "  (" + s.getCoordinate() + ") can move: ");
                foreach(var p in m){
                    Console.WriteLine(p);
                }
                Console.WriteLine("-----------------------------------");
            }
        }
    }


    private static void printmylegalmoves(Board b, bool color){
        foreach(Square s in b.getBoard()){
                if(s.GetPiece() is not null && s.GetPiece().getColor() == color){
                    var m = s.GetPiece().Move(s.getCoordinate().Item1,s.getCoordinate().Item2,b);
                    Console.WriteLine(s.GetPiece().getColor() + " " + s.Show() + "  (" + s.getCoordinate() + ") can move: ");
                    foreach(var p in m){
                        Console.WriteLine(p);
                    }
                    Console.WriteLine("-----------------------------------");
                }
        }
    }


    private static void playerTurn(Board board, bool side){
        
        var tmpBoard = board;
        while(true){
            
            var pieceToMove = parseInput(ReadPlayerInput());
            //Console.WriteLine(pieceToMove);
            var placeToMove = parseInput(ReadPlayerInput());
            //Console.WriteLine(placeToMove);
            if((pieceToMove.Item1 > -1 || pieceToMove.Item2 > -1) && 
                (placeToMove.Item1 > -1 || placeToMove.Item2 > -1) &&
                (placeToMove.Item1 < 8 || placeToMove.Item2 < 8) && 
                (pieceToMove.Item1 < 8 || pieceToMove.Item2 < 8) ){

                tmpBoard = PlayMove(board,pieceToMove,placeToMove,side);

                //board.movePiece(pieceToMove,placeToMove,side);

            }
            Console.WriteLine();

            if(board.getBoard()[placeToMove.Item1,placeToMove.Item2].GetPiece() != null && 
                board.getBoard()[pieceToMove.Item1,pieceToMove.Item2].GetPiece() == null &&
                !tmpBoard.Equals(board)){
                //Console.WriteLine("SWAP");
                break;
            }        
        }
        board.printBoard(!side);
        playerTurn(board, !side);

        //return board;
    } 




    private static bool isCheckmated(Board board, bool side){
        if(getLegalMoves(board, side, isKingChecked(board,side)).Values.Count == 0){
            return true;
        }
        return false;
    }


    private static Dictionary<(int,int),List<(int,int)>> getLegalMoves(Board board, bool side, bool isChecked){

        var legalMoves = new Dictionary<(int,int), List<(int,int)>>();
        var tmpBoard = board;

        foreach(Square s in tmpBoard.getBoard()){
            var piece = s.GetPiece();

            if(piece is not null && piece.getColor() == side){

                var tmpmoveList = piece.Move(s.getCoordinate().Item1,s.getCoordinate().Item2,board);

                foreach(var mov in tmpmoveList){
                    var tmpmove = tmpBoard;
                    if(mov.Item1 < tmpBoard.getDim() && mov.Item1 > -1 && mov.Item2 < tmpBoard.getDim() && mov.Item2 > -1){

                    Console.WriteLine(mov);
                    //tmpmove.movePiece(s.getCoordinate(), piece, mov);
                    
                    if(!isKingChecked(tmpmove,side)){
                        if(legalMoves.ContainsKey(s.getCoordinate())){
                            legalMoves.Keys.Where(x=> x.Equals(s.getCoordinate())).ToList().Add(mov);
                        }else{
                            legalMoves.Add(s.getCoordinate(),new List<(int, int)>());
                        }
                    }
                    }
                }

                //castle move
                if(piece is King king){
                    var castle = king.CastleMoves(s.getCoordinate().Item1,s.getCoordinate().Item2,board);
                    foreach(var c in castle){
                    legalMoves.Keys.Where(x=> x.Equals(s.getCoordinate())).ToList().Add(c);
                    }
                }

            }
        }
        return legalMoves;
    }


    private static bool isKingChecked(Board board, bool side){

        var kinglocation = (-1,-1);

        foreach(Square s in board.getBoard()){
            if(s.GetPiece() is not null && s.GetPiece().getColor()==side && s.GetPiece().GetType() == typeof(King)){
                kinglocation = s.getCoordinate();
            }
        }
        foreach(Square s in board.getBoard()){
            if(s.GetPiece() is not null && s.GetPiece().getColor()!= side && s.GetPiece().Attack(s.getCoordinate().Item1,s.getCoordinate().Item2,board).Contains(kinglocation)){
                return true;
            } 
        }
        return false;    
    }


    private static String ReadPlayerInput(){
        
        string input = string.Empty;
        while(string.IsNullOrEmpty(input)){
            input = Console.ReadLine();
        }
        //Console.WriteLine(input);
        return input;
    }


    private static Dictionary<Square,List<(int,int)>> getmylegalmoves(Board b, bool color){
        var myMoves = new Dictionary<Square,List<(int,int)>>();
        foreach(Square s in b.getBoard()){
                if(s.GetPiece() is not null && s.GetPiece().getColor() == color){
                    var m = s.GetPiece().Move(s.getCoordinate().Item1,s.getCoordinate().Item2,b);
                    
                    if(!myMoves.ContainsKey(s)){
                        myMoves.Add(s,[]);
                    }
                    var pieceMoves = new List<(int,int)>();

                    //Console.WriteLine(s.GetPiece().getColor() + " " + s.Show() + "  (" + s.getCoordinate() + ") can move: ");
                    foreach(var p in m){
                        //Console.WriteLine(p);
                        myMoves.TryGetValue(s, out pieceMoves);
                        pieceMoves.Add(p);

                        
                    }
                    foreach(var e in pieceMoves){
                        //Console.WriteLine(e);
                    }
                    //Console.WriteLine("-----------------------------------");
                }
        }
        return myMoves;
    }



    private static (int,int) parseInput(string text){
            // Instantiate the regular expression object.
                //OG Pattern = @"([QKBNPR]?)([x]?)([abcdefgh12345678]{2})([+]?)";
        var pattern = @"([QKBNPR]?)([abcdefgh12345678]{2})";
        Regex r = new Regex(pattern);

        var row = -1;
        var col = -1;

        Match m = r.Match(text);

        while (m.Success)
        {

            for (int i = 1; i <= 2; i++)
            {
                Group g = m.Groups[i];
                CaptureCollection cc = g.Captures;
                    
                if(i%2 == 1){
                    //Console.WriteLine("Piece: " + cc[0]);
                    
                }else{
                    //Console.WriteLine("Position: " + cc[0]);
                    col = Convert.ToInt32(cc[0].ToString().ToCharArray()[0])-97;
                    //Console.WriteLine(col);

                    row = Convert.ToInt32(cc[0].ToString().ToCharArray()[1]-49);
                    //Console.WriteLine(row);

                }   
            }

            m = m.NextMatch();
        }
        return (row,col);
    }

    private static Board PlayMove(Board b, (int,int) pieceToMove, (int,int) placeToMove, bool side){
        var tmpBoard = b;
        //check if move results in check
        try{
        tmpBoard.movePiece(pieceToMove,placeToMove,side);
        var myKingLocation = tmpBoard.getKing(side);
        var myKing = tmpBoard.getPieceOnSquare(myKingLocation.Item1,myKingLocation.Item2) as King;   
        if(myKing.getAttackedSquares(b).Contains(myKingLocation)){
            Console.WriteLine("Illegal move, Piece is Pinned");
            return b;
        }

        //check if move is a castle
        tmpBoard = b;
        var piece = tmpBoard.getPieceOnSquare(pieceToMove.Item1,pieceToMove.Item2);
        if(piece is King king && piece.getColor() == side && 
            king.CastleMoves(pieceToMove.Item1,pieceToMove.Item2,tmpBoard).Contains(placeToMove)){
        
                //move king
                tmpBoard.movePiece(pieceToMove,placeToMove,side);
                //get diff in moved king
                var colDiff = pieceToMove.Item2 - placeToMove.Item2;
                //move rook
                if(colDiff == 2){
                    tmpBoard.movePiece((pieceToMove.Item1,0),(pieceToMove.Item1,3),side);
                }else{
                    tmpBoard.movePiece((pieceToMove.Item1,7),(pieceToMove.Item1,5),side);
                }
                
        }
        }catch(Exception e){
            Console.WriteLine(""+e.Message);
        }

        return tmpBoard;
    }

    }


