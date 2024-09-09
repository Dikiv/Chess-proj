
public class Pawn : Piece
{
    //False = Black
    //True = White
    private Boolean color;
    //tmp num for piece
    private int num;
    //char for piece
    private string piece;

    public Pawn(bool color, int num){
        this.color = color;
        this.num = num;
        if(color){
            this.piece = "♟";
        }else{
            this.piece = "♙";
        }
    }

    //Takes Current position and returns squares that can be attacked.
    public override List<(int,int)> Attack(int row, int col, Board board)
    {   
        var attackList = new List<(int,int)>();
        
        if(color == true){
            var attackLeft = board.getPieceOnSquare(row+1,col-1);
            if(attackLeft is not null && attackLeft.getColor() == false){
                attackList.Add((row+1,col-1));
            }

            var attackRight = board.getPieceOnSquare(row+1,col+1);
            if(attackRight is not null && attackRight.getColor() == false){
                attackList.Add((row+1,col+1));
            }

            //To Be made
            var enpeasentRight = board.getPieceOnSquare(row,col+1);
            
            var enpeasentLeft = board.getPieceOnSquare(row,col-1);



        }else{
            var attackLeft = board.getPieceOnSquare(row-1,col-1);
            if(attackLeft is not null && attackLeft.getColor() == true){
                attackList.Add((row-1,col-1));
            }

            var attackRight = board.getPieceOnSquare(row+1,col+1);
            if(attackRight is not null && attackRight.getColor() == true){
                attackList.Add((row-1,col+1));
            }
        }
        return attackList;
    }
    //Takes Current position and returns squares that can be moved to.
    public override List<(int,int)> Move(int row, int col, Board board)
    {
        var moveList = new List<(int,int)>();

        if(color == true){
            if(!board.isSquareTaken(row+1,col)){
                moveList.Add((row+1,col));
            }
            if((!board.isSquareTaken(row+2,col))&& row == 1){
                moveList.Add((row+2,col));
            }

        }else{
            if(!board.isSquareTaken(row-1,col)){
                moveList.Add((row-1,col));
            }
            if((!board.isSquareTaken(row-2,col))&& row == 6){
                moveList.Add((row-2,col));
            }
        }

        return moveList;
    }

    public override string Show()
    {
        return this.piece;
    }

    public override bool getColor()
    {
        return this.color;
    }
}