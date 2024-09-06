
using System.Data;
using System.Text.RegularExpressions;

var x = new Board(8,false);
//x.movePiece((1,0),(2,0));
//printallmoves(x);
//x.printBoard(false);
whiteTurn(x);

void printallmoves(Board b){
    foreach(Square s in b.getBoard()){
            if(s.GetPiece() is not null){

                var m = s.GetPiece().Move(s.getCoordinate().Item1,s.getCoordinate().Item2,x);
                Console.WriteLine(s.GetPiece().getColor() + " " + s.Show() + "  (" + s.getCoordinate() + ") can move: ");
                foreach(var p in m){
                    Console.WriteLine(p);
                }
                Console.WriteLine("-----------------------------------");
            }
        }
}

void printmylegalmoves(Board b, bool color){
    foreach(Square s in b.getBoard()){
            if(s.GetPiece() is not null && s.GetPiece().getColor() == color){
                var m = s.GetPiece().Move(s.getCoordinate().Item1,s.getCoordinate().Item2,x);
                Console.WriteLine(s.GetPiece().getColor() + " " + s.Show() + "  (" + s.getCoordinate() + ") can move: ");
                foreach(var p in m){
                    Console.WriteLine(p);
                }
                Console.WriteLine("-----------------------------------");
            }
    }
}



//whiteTurn(x);

void whiteTurn(Board board){
    bool side = true;
    /*
    if(isCheckmated(board, true)){
        Console.WriteLine("------ BLACK WINS ------");
        //return board;
    }
    */
    printmylegalmoves(x, true);
    //Console.WriteLine("__________________________________");
    var mymoves = getmylegalmoves(x,side);

    while(true){
        
        var pieceToMove = parseInput(ReadPlayerInput());
        Console.WriteLine(pieceToMove);
        var placeToMove = parseInput(ReadPlayerInput());
        Console.WriteLine(placeToMove);
        if((pieceToMove.Item1 > -1 || pieceToMove.Item2 > -1) && 
            (placeToMove.Item1 > -1 || placeToMove.Item2 > -1) &&
            (placeToMove.Item1 < 9 || placeToMove.Item2 < 9) && 
            (pieceToMove.Item1 < 9 || pieceToMove.Item2 < 9) ){
            board.movePiece(pieceToMove,placeToMove);
            board.printBoard(side);
        }
        
    }

    //return board;
} 

void blackTurn(Board board){
    bool side = false;
    
    if(isCheckmated(board, side)){
        Console.WriteLine("------ WHITE WINS ------");
    }

    board.printBoard(true);
    
    //var moves = getLegalMoves(board, side, isKingChecked(board, side));
    
    //scanner 
    while(true){
        
        string input = Console.ReadLine();
        string[] splits = Regex.Split(input, @"(?<=\d)");
        foreach (string s in splits){
            Console.WriteLine(s);
        }

        /*
        Convert.ToInt32(input.Split(','));
        
        if(moves.ContainsKey(input)){
           moves.TryGetValue(input, out side);
        }
        */
    }
    
    
}

bool isCheckmated(Board board, bool side){
    if(getLegalMoves(board, side, isKingChecked(board,side)).Values.Count == 0){
        return true;
    }
    return false;
}

Dictionary<(int,int),List<(int,int)>> getLegalMoves(Board board, bool side, bool isChecked){

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
        }
    }
    return legalMoves;
}

bool isKingChecked(Board board, bool side){

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

String ReadPlayerInput(){
    
    string input = Console.ReadLine();
    Console.WriteLine(input);
    return input;
}

Dictionary<Square,List<(int,int)>> getmylegalmoves(Board b, bool color){
    var myMoves = new Dictionary<Square,List<(int,int)>>();
    foreach(Square s in b.getBoard()){
            if(s.GetPiece() is not null && s.GetPiece().getColor() == color){
                var m = s.GetPiece().Move(s.getCoordinate().Item1,s.getCoordinate().Item2,x);
                
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


(int,int) parseInput(string text){
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
                Console.WriteLine("Piece: " + cc[0]);
                
            }else{
                Console.WriteLine("Position: " + cc[0]);
                col = Convert.ToInt32(cc[0].ToString().ToCharArray()[0])-97;
                Console.WriteLine(col);

                row = Convert.ToInt32(cc[0].ToString().ToCharArray()[1]-49);
                Console.WriteLine(row);

            }   
         }

         m = m.NextMatch();
      }
      return (row,col);
}