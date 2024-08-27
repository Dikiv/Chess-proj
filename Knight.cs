
class Knight : Piece
{
    private Boolean color;
    //tmp num for piece
    private int num;
    //char for piece
    private string piece;

    public Knight(Boolean color, int num){
        this.color = color;
        this.num = num;
        if(color){
            this.piece = "♞";
        }else{
            this.piece = "♘";
        }
    }

    public override List<(int, int)> Attack(int row, int col, Board board)
    {
        var attackList = new List<(int, int)> ();
        
        
        var upRight = board.getPieceOnSquare(row+2,col+1);
        var upLeft = board.getPieceOnSquare(row+2,col-1);
        var leftUp = board.getPieceOnSquare(row+1,col-2);
        var leftDown = board.getPieceOnSquare(row-1,col-2);
        var rightUp = board.getPieceOnSquare(row+1,col+2);
        var rightDown = board.getPieceOnSquare(row-1,col+2);
        var downLeft = board.getPieceOnSquare(row-2,col-1);
        var downRight = board.getPieceOnSquare(row-2,col+1);

        if(upLeft is not null && upLeft.getColor() != this.color){
            attackList.Add((row+2,col-1));
        }
        if(upRight is not null && upRight.getColor() != this.color){
            attackList.Add((row+2,col+1));
        } 
        if(leftUp is not null && leftUp.getColor() != this.color){
            attackList.Add((row+1,col-2));
        } 
        if(leftDown is not null && leftDown.getColor() != this.color){
            attackList.Add((row-1,col-2));
        } 
        if(rightUp is not null && rightUp.getColor() != this.color){
            attackList.Add((row+1,col+2));
        } 
        if(rightDown is not null && rightDown.getColor() != this.color){
            attackList.Add((row-1,col+2));
        } 
        if(downLeft is not null && downLeft.getColor() != this.color){
            attackList.Add((row-2,col-1));
        } 
        if(downRight is not null && downRight.getColor() != this.color){
            attackList.Add((row-2,col+1));
        }
    
        return attackList;
    }

    public override bool getColor()
    {
        return this.color;

    }

    public override List<(int, int)> Move(int row, int col, Board board)
    {
        var moveList = new List<(int,int)>();

        var upRight = board.isSquareTaken(row+2,col+1);
        var upLeft = board.isSquareTaken(row+2,col-1);
        var leftUp = board.isSquareTaken(row+1,col-2);
        var leftDown = board.isSquareTaken(row-1,col-2);
        var rightUp = board.isSquareTaken(row+1,col+2);
        var rightDown = board.isSquareTaken(row-1,col+2);
        var downLeft = board.isSquareTaken(row-2,col-1);
        var downRight = board.isSquareTaken(row-2,col+1);    
    
        if(!upRight){
            moveList.Add((row+2,col+1));
        }
        if(!upLeft){
            moveList.Add((row+2,col-1));
        }
        if(!leftUp){
            moveList.Add((row+1,col-2));
        }
        if(!leftDown){
            moveList.Add((row-1,col-2));
        }
        if(!rightUp){
            moveList.Add((row+1,col+2));
        }
        if(!rightDown){
            moveList.Add((row-1,col+2));
        }
        if(!downLeft){
            moveList.Add((row-2,col-1));
        }
        if(!downRight){
            moveList.Add((row-2,col+2));
        }

        return moveList;

    }

    public override string Show()
    {
        return this.piece;
    }
}