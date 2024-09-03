
using System.Text.RegularExpressions;

var x = new Board(8,false);
x.movePiece((1,0),(2,0));
// printallmoves(x);
x.printBoard(false);


void printallmoves(Board x){
    foreach(Square s in x.getBoard()){
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



//whiteTurn(x);

void whiteTurn(Board board){
    
    if(isCheckmated(board, true)){
        Console.WriteLine("------ BLACK WINS ------");
        //return board;
    }
    
    board.printBoard(false);

    //scanner 
    while(true){
        
        string input = Console.ReadLine();

        string[] splits = Regex.Split(input, @"(?<=\d)|,");
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

    //return board;
} 

void blackTurn(Board board){
    bool side = false;

    if(isCheckmated(board, side)){
        Console.WriteLine("------ WHITE WINS ------");
    }

    board.printBoard(true);
    
    var moves = getLegalMoves(board, side, isKingChecked(board, side));
    
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


