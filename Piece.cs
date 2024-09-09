
using System.Collections;

public abstract class Piece{
    
    public abstract List<(int, int)> Move(int row, int col, Board board);
    public abstract List<(int, int)> Attack(int row, int col, Board board);
    public abstract string Show();
    public abstract bool getColor();
}

