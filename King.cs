
using System.Collections;
using System.Collections.Generic;

public class King : Piece
{
    //False = Black
    //True = White
    private Boolean color;
    //tmp num for piece
    private int num;
    //char for piece
    private string piece;

    //bool for checking if king has moved in case of castling
    private bool moved = false;


    public King(bool color, int num){
        this.color = color;
        this.num = num;
        if(color){
            this.piece = "♚";
        }else{
            this.piece = "♔";
        }
    }

    //Takes Current position and returns squares that can be attacked.
    public override List<(int,int)> Attack(int row, int col, Board board)
    {   
        var attackList = new List<(int,int)>();
        
        var left = (row,col-1);
        var right = (row,col+1);
        var up = (row+1,col);
        var down = (row-1, col); 
        var upRight = (row+1,col+1);
        var downRight = (row-1,col+1);
        var upLeft = (row+1,col-1);
        var downLeft = (row-1,col-1);

        if(board.getPieceOnSquare(left.Item1, left.Item2) is not null && 
            board.getPieceOnSquare(left.Item1,left.Item2).getColor() != this.color){
            attackList.Add(left);
        }
        if(board.getPieceOnSquare(right.Item1, right.Item2) is not null && 
            board.getPieceOnSquare(right.Item1,right.Item2).getColor() != this.color){
            attackList.Add(right);
        }
        if(board.getPieceOnSquare(down.Item1, down.Item2) is not null && 
            board.getPieceOnSquare(down.Item1,down.Item2).getColor() != this.color){
            attackList.Add(down);
        }
        if(board.getPieceOnSquare(up.Item1, up.Item2) is not null && 
            board.getPieceOnSquare(up.Item1,up.Item2).getColor() != this.color){
            attackList.Add(up);
        }
        if(board.getPieceOnSquare(upLeft.Item1, upLeft.Item2) is not null && 
            board.getPieceOnSquare(upLeft.Item1,upLeft.Item2).getColor() != this.color){
            attackList.Add(upLeft);
        }
        if(board.getPieceOnSquare(upRight.Item1, upRight.Item2) is not null && 
            board.getPieceOnSquare(upRight.Item1,upRight.Item2).getColor() != this.color){
            attackList.Add(upRight);
        }
        if(board.getPieceOnSquare(downLeft.Item1, downLeft.Item2) is not null && 
            board.getPieceOnSquare(downLeft.Item1,downLeft.Item2).getColor() != this.color){
            attackList.Add(downLeft);
        }
        if(board.getPieceOnSquare(downRight.Item1, downRight.Item2) is not null && 
            board.getPieceOnSquare(downRight.Item1,downRight.Item2).getColor() != this.color){
            attackList.Add(downRight);
        }
        

        return attackList;
    }
    //Takes Current position and returns squares that can be moved to.
    public override List<(int,int)> Move(int row, int col, Board board)
    {
        var moveList = new List<(int,int)>();

        var left = (row,col-1);
        var right = (row,col+1);
        var up = (row+1,col);
        var down = (row-1, col); 
        var upRight = (row+1,col+1);
        var downRight = (row-1,col+1);
        var upLeft = (row+1,col-1);
        var downLeft = (row-1,col-1);

        if(col > 0 && !board.isSquareTaken(left.Item1, left.Item2)){
                moveList.Add(left);
        }
        if(col < 7 && !board.isSquareTaken(right.Item1, right.Item2)){
                moveList.Add(right);
        }
        if(row > 0 && !board.isSquareTaken(down.Item1, down.Item2)){
                moveList.Add(down);   
        }
        if(row < 7 && !board.isSquareTaken(up.Item1, up.Item2)){
                moveList.Add(up);
        }
        if(col < 7 && row < 7 && !board.isSquareTaken(upRight.Item1, upRight.Item2)){
                moveList.Add(upRight);  
        }
        if(col > 0 && row > 0 && !board.isSquareTaken(downLeft.Item1, downLeft.Item2)){
                moveList.Add(downLeft);
        }
        if(col < 7 && row > 0 && !board.isSquareTaken(downRight.Item1, downRight.Item2)){
                moveList.Add(downRight);
        }
        if(col > 0 && row < 7 && !board.isSquareTaken(upLeft.Item1, upLeft.Item2)){
                moveList.Add(upLeft);
        }

        

        var attackedSquares = getAttackedSquares(board);
        
        foreach(var attacked in attackedSquares){
            if(moveList.Contains(attacked)){
                moveList.Remove(attacked);
            }
        }

        return moveList; 
    }

    public List<(int,int)> CastleMoves(int row, int col, Board board){

        var CastlesList = new List<(int,int)>();
        
        if(!moved){
            //check empty spaces between king + rook
            for(int i = 1; i < col ; i++){
                if(board.isSquareTaken(row,i)){
                    break;
                }else if(i == col-1){
                    var rok = board.getPieceOnSquare(row,0);
                    if(rok is Rook && !rok.getHasMoved()){
                        CastlesList.Add((row,col-2));
                    }
                }
            }
            for(int i = col+1; i < 7 ; i++){
                if(board.isSquareTaken(row,i)){
                    break;
                }else if(i == 6){
                    var rok = board.getPieceOnSquare(row,7);
                    if(rok is Rook && !rok.getHasMoved()){
                        CastlesList.Add((row,col+2));
                    }
                }
            }
        }

        return CastlesList;
    }

    public override string Show()
    {
        return this.piece;
    }

    public override bool getColor()
    {
        return this.color;
    }

    public void hasMoved(){
        this.moved = true;
    }

    public HashSet<(int,int)> getAttackedSquares(Board board){
        var attackedPositions = new HashSet<(int,int)>(); 

        for(int i = 0; i < 8; i++){
            for(int j = 0; j < 8; j++){
                var piece = board.getPieceOnSquare(i, j);
                if(piece is not null && piece.getColor() != this.color){
                    Console.WriteLine("attackSquares");
                    foreach(var x in piece.Move(i,j,board)){
                        attackedPositions.Add(x);
                    }
                    
                }

            }
        }


        return attackedPositions; 
    }

    public override bool getHasMoved()
    {
        return this.moved;
    }

    public override void setMoved()
    {
        this.moved = true;
    }
}