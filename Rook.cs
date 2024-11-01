
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class Rook : Piece
{
    //False = Black
    //True = White
    private Boolean color;
    //tmp num for piece
    private int num;
    //char for piece
    private string piece;

    //List of current attack targets
    private List<(int,int)> attacks;

    //bool for checking if rook has moved in case of castling
    private bool moved;

    public Rook(bool color, int num){
        this.color = color;
        this.num = num;
        if(color){
            this.piece  = "♜";
        }else{
            this.piece = "♖";
        }
        
        this.attacks = new List<(int,int)>();
    }
    //Takes Current position and returns squares that can be attacked.
    public override List<(int,int)> Attack(int row, int col, Board board)
    {   
        Move(row,col,board);
        return this.attacks;
    }

    public override bool getColor()
    {
        return this.color;
    }

    //Takes Current position and returns squares that can be moved to.
    public override List<(int,int)> Move(int row, int col, Board board)
    {
        var moveList = new List<(int,int)>();
        this.attacks.Clear();
        var left = col-1;
        var right = col+1;
        var up = row+1;
        var down = row-1;

        while(true){

            if(!board.isSquareTaken(row,left)){
                moveList.Add((row,left));
                left--;
            }else{
                var tmp = board.getPieceOnSquare(row, left);

                if(tmp is not null && tmp.getColor() != this.color)
                {
                    this.attacks.Add((row,left));
                }
            }

            if(!board.isSquareTaken(row,right)){
                moveList.Add((row,right));
                right++;
            }else{
                var tmp = board.getPieceOnSquare(row, right);
                if(tmp is not null && tmp.getColor() != this.color)
                {
                    this.attacks.Add((row,right));
                }
            }

            if(!board.isSquareTaken(up,col)){
                moveList.Add((up,col));
                up++;
            
            }else{
                var tmp = board.getPieceOnSquare(up, col);

                if(tmp is not null && tmp.getColor() != this.color)
                {
                    this.attacks.Add((up,col));
                }
            }
            
            if(!board.isSquareTaken(down,col)){
                moveList.Add((down,col));
                down--;
            }else{
                var tmp = board.getPieceOnSquare(down, col);
                
                if(tmp is not null && tmp.getColor() != this.color)
                {
                    this.attacks.Add((down,col));
                }
            }
            

            if(board.isSquareTaken(down,col) && 
                board.isSquareTaken(up,col) && 
                board.isSquareTaken(row,right) && 
                board.isSquareTaken(row,left)){
                break;
            }

        }
        
        return moveList;
    }
    public (int,int) CastleMove(int row, int col, Board board){
        
        if(col == 0){
            if(board.getPieceOnSquare(row,col+4) is King){
                return (row,col+3);
            }
            return (row,col+2);
        }

        if(board.getPieceOnSquare(row,col-4) is King){
            return (row,col-3);
        }
    
        return (row, col-2);
    }

    public override string Show()
    {
        return this.piece;
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