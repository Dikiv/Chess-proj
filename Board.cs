
class Board{
    private Square[,] positions;
    private int dim;

    public Board(int dim, bool side){
        //Sets Board Size
        this.positions = new Square[dim,dim];
        this.dim = dim;

        initBoard();

        populateStandard(side);

        printBoard(side);
        
    }

    //Horizontal Line
    private void drawLine(){
        for(int j = 0;j<dim; j++){   
                Console.Write("--");
            }
            Console.WriteLine('-');
    }

    public void printBoard(bool side){
        drawLine();
        if(side){
            for (int i = 0; i < dim; i++){
            //Console.Write('|');
            for(int j = 0;j<dim; j++){   
                Console.Write('|');
                Console.Write(positions[i,j].Show());
            }
            Console.Write('|');
            Console.WriteLine(" ");
            drawLine();
            }
        }else{
            for (int i = dim-1; i > -1; i--){
            //Console.Write('|');
            for(int j = dim-1;j>-1; j--){   
                Console.Write('|');
                Console.Write(positions[i,j].Show());
            }
            Console.Write('|');
            Console.WriteLine(" ");
            drawLine();
            }
        }
        
    }

    public bool isSquareTaken(int row, int col){

        if( 
            row < 0 ||
            col < 0 ||
            row > dim-1 || 
            col > dim -1 ||
            positions[row,col].GetPiece() is not null ){
            return true;
        }
        return false;
    }

    public Piece? getPieceOnSquare(int row, int col){
        if( row > 0 &&
            col > 0 &&
            row < dim-1 && 
            col < dim -1 ){
        return positions[row,col].GetPiece();
        }
        return null;
    }

    
    /**
    Populates the board in standard
    False indicates Black to be at the bottom, and white to be at the top.
    True indicates White to be at the bottom, and black to be at the top.
    **/
    private void populateStandard(bool side){

        //create Pawns
        //Top 
        for(int i = 0; i < 8; i++){
            positions[1,i].placePiece(new Pawn(true,1));
        }
        
        //Bottom
        for(int i = 0; i < 8; i++){
            positions[6,i].placePiece(new Pawn(false,1));
        }

        //create Rooks
        //white
        positions[0,0].placePiece(new Rook(true,5));
        positions[0,7].placePiece(new Rook(true,5));
        //black
        positions[7,0].placePiece(new Rook(false,5));
        positions[7,7].placePiece(new Rook(false,5));


        //create Knights
        //white
        positions[0,1].placePiece(new Knight(true,3));
        positions[0,6].placePiece(new Knight(true,3));
        //black
        positions[7,1].placePiece(new Knight(false,3));
        positions[7,6].placePiece(new Knight(false,3));

        //create Bishops
        //white
        positions[0,2].placePiece(new Bishop(true,3));
        positions[0,5].placePiece(new Bishop(true,3));
        //black
        positions[7,2].placePiece(new Bishop(false,3));
        positions[7,5].placePiece(new Bishop(false,3));

        //create Queens
        //White 
        positions[0,4].placePiece(new Queen(true,9));
        //Black
        positions[7,4].placePiece(new Queen(false,9));
        
        //create Kings
        //White
        positions[0,3].placePiece(new King(true,100));
        //Black
        positions[7,3].placePiece(new King(false,100));
        
    }

    private void initBoard(){
       
        //sets Checkered pattern
            for(int i=0; i<dim;i++){
                for (int j=0; j<dim;j++){
                    if(((j+i)%2)==0){
                        this.positions[i,j] = new Square(false,(i,j));
                    }else{
                        this.positions[i,j] = new Square(true,(i,j));
                    }
                }
            }
        /*
        if(!side){
            
        }else{
            for(int i=dim-1; i>-1;i--){
                for (int j=dim-1; j>-1;j--){
                    if(((j+i)%2)==0){
                        this.positions[i,j] = new Square(side,(i,j));
                    }else{
                        this.positions[i,j] = new Square(!side,(i,j));
                    }
                }
            }
        }
        */
    }

    public Square[,] getBoard(){
        return this.positions;
    }

    public Board movePiece((int,int)location, Piece p, (int,int) dest){
        var tmp = this.positions;
        if(dest.Item1 < dim && dest.Item1 > -1 && dest.Item1 < dim && dest.Item2 > -1){
        tmp[location.Item1,location.Item2].removePiece();
        tmp[dest.Item1,dest.Item2].placePiece(p);
        this.positions = tmp;
        }
        return this;
    }

    public int getDim(){
        return this.dim;
    }


    
}