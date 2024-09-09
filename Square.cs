
using System.Security.Cryptography.X509Certificates;

public class Square{
    
    //True = white
    //False = Black
    private bool color;
    //Piece
    private Piece? P;

    private (int,int) coordinate;


    public Square(Boolean color, (int,int) coordinate)
    {
        this.color = color;
        this.coordinate = coordinate;
    }

    public void placePiece(Piece p){
        
        this.P = p;

    }

    public void removePiece(){
         if(P is not null){
            this.P = null;
        }
    }

    public string Show(){
        if(P is not null){
            return P.Show();
        }
        if(color){
            
            return "◼";
        }
        return "◻";
    }

    public Piece? GetPiece(){
        return this.P;
    }

    public (int,int) getCoordinate(){
        return this.coordinate;
    }    

}