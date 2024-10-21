
public class Bishop : Piece
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

    public Bishop(Boolean color,int num){
        this.color = color;
        this.num = num;
        if(color){
            this.piece = "♝";
        }else{
            this.piece = "♗";
        }
        this.attacks = new List<(int,int)>();
    }
    public override List<(int, int)> Attack(int row, int col, Board board)
    {
        Move(row, col, board);
        return this.attacks;
    }
    public override bool getColor()
    {
        return this.color;
    }
    public override List<(int, int)> Move(int row, int col, Board board)
    {
        var moveList = new List<(int,int)>();

        var upLeft = (row+1,col-1);
        var upRight = (row+1,col+1);
        var downLeft = (row-1,col-1);
        var downRight = (row-1,col+1);

        var upLeftStop  = false;
        var upRightStop = false;
        var downLeftStop = false;
        var downRightStop = false;
        
        attacks.Clear();

        while(true){
        
        if(!board.isSquareTaken(upRight.Item1,upRight.Item2)){
            moveList.Add(upRight);
            upRight = (upRight.Item1+1,upRight.Item2+1);
        }else{
            var tmp = board.getPieceOnSquare(upRight.Item1,upRight.Item2);
            upRightStop = true;
            if(tmp is not null && tmp.getColor() != this.color && !attacks.Contains(upRight)){
                attacks.Add(upRight);
            }
        }
        
        
        if(!board.isSquareTaken(upLeft.Item1,upLeft.Item2)){
            moveList.Add(upLeft);
            upLeft = (upLeft.Item1+1,upLeft.Item2-1);
        }else{
            var tmp = board.getPieceOnSquare(upLeft.Item1,upLeft.Item2);
            upLeftStop = true;
            if(tmp is not null && tmp.getColor()!= this.color && !attacks.Contains(upLeft)){
                attacks.Add(upLeft);
            }
        }

        if(!board.isSquareTaken(downLeft.Item1,downLeft.Item2)){
            moveList.Add(downLeft);
            downLeft = (downLeft.Item1-1,downLeft.Item2-1);
        }else{
            var tmp = board.getPieceOnSquare(downLeft.Item1,downLeft.Item2);
            downLeftStop = true;
            if(tmp is not null && tmp.getColor()!= this.color && !attacks.Contains(downLeft)){
                attacks.Add(downLeft);
            }
        }
        
        if(!board.isSquareTaken(downRight.Item1 ,downRight.Item2)){
            moveList.Add(downRight);
            downRight = (downRight.Item1-1,downRight.Item2+1);
        }else{
            var tmp = board.getPieceOnSquare(downRight.Item1,downRight.Item2);
            downRightStop = true;
            if(tmp is not null && tmp.getColor()!= this.color && !attacks.Contains(downRight)){
                attacks.Add(downRight);
            }
        }

        
            
        if(downLeftStop &&
            downRightStop &&
            upRightStop &&
            upLeftStop){
                break;
            }
        }
        

        return moveList;
    }

    public override string Show()
    {
        return this.piece;
    }
}