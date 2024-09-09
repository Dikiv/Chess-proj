
public class Queen : Piece
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

    public Queen(Boolean color,int num){
        this.color = color;
        this.num = num;
        if(color){
            this.piece = "♛";
        }else{
            this.piece = "♕";
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

        var left = col--;
        var right = col++;
        var up = row++;
        var down = row--;

        attacks.Clear();

        while(true){

        //Diagonal
        if(!board.isSquareTaken(upLeft.Item1,upRight.Item2)){
            moveList.Add(upLeft);
            upLeft = (upLeft.Item1++,upLeft.Item2--);
        }else{
            var tmp = board.getPieceOnSquare(upLeft.Item1,upLeft.Item2);
            if(tmp is not null && tmp.getColor() != this.color){
                attacks.Add(upLeft);
            }
        }
        if(!board.isSquareTaken(upRight.Item1,upLeft.Item2)){
            moveList.Add(upRight);
            upRight = (upRight.Item1++,upRight.Item2++);
        }else{
            var tmp = board.getPieceOnSquare(downRight.Item1,downRight.Item2);
            if(tmp is not null && tmp.getColor()!= this.color){
                attacks.Add(downRight);
            }
        }
        if(!board.isSquareTaken(downLeft.Item1,downLeft.Item2)){
            moveList.Add(downLeft);
            downLeft = (downLeft.Item1--,downLeft.Item2--);
        }else{
            var tmp = board.getPieceOnSquare(downRight.Item1,downRight.Item2);
            if(tmp is not null && tmp.getColor()!= this.color){
                attacks.Add(downRight);
            }
        }
        if(!board.isSquareTaken(downRight.Item1 ,downRight.Item2)){
            moveList.Add(downRight);
            downRight = (downRight.Item1--,downRight.Item2++);
        }else{
            var tmp = board.getPieceOnSquare(downRight.Item1,downRight.Item2);
            if(tmp is not null && tmp.getColor()!= this.color){
                attacks.Add(downRight);
            }
        }

        //Horizontal & Vertical
        if(!board.isSquareTaken(row,left)){
            moveList.Add((row,left));
            left--;
        }else{
            var tmp = board.getPieceOnSquare(row, left);

            if(tmp is not null && tmp.getColor() != this.color){
                this.attacks.Add((row,left));
            }
        }

        if(!board.isSquareTaken(row,right)){
            moveList.Add((row,right));
            right++;
        }else{
            var tmp = board.getPieceOnSquare(row, right);
            if(tmp is not null && tmp.getColor() != this.color){
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


        if(board.isSquareTaken(upLeft.Item1 ,upLeft.Item2) && 
            board.isSquareTaken(upRight.Item1 ,upRight.Item2) &&
            board.isSquareTaken(downRight.Item1 ,downRight.Item2) &&
            board.isSquareTaken(downLeft.Item1 ,downLeft.Item2) &&
            board.isSquareTaken(down,col) && 
            board.isSquareTaken(up,col) && 
            board.isSquareTaken(row,right) && 
            board.isSquareTaken(row,left)){
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