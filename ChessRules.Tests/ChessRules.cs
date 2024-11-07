using System;
using Xunit;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using Moq;
using System.IO;
using System.Security.Permissions;
using ChessApp;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ChessApp.Chess;


namespace ChessRules.Tests{

public class ChessRules 
{
    [TestFixture]
    public class consoleTests
    {   
        StringBuilder _ConsoleOutput;
        Mock<TextReader> _ConsoleInput;

        [SetUp]            
        public void Setup(){
            _ConsoleOutput = new StringBuilder();
            var consoleOutputWriter = new StringWriter(_ConsoleOutput);
            _ConsoleInput = new Mock<TextReader>();
            Console.SetOut(consoleOutputWriter);
            Console.SetIn(_ConsoleInput.Object);
        }

        private string[] RunMainAndGetConsoleOutput(){
            ChessApp.Chess.Main(default);
            return _ConsoleOutput.ToString().Split("\r\n");
        }

        private MockSequence SetupUserResponse(params string[] userResponse){
            var sequence = new MockSequence();
            foreach (var response in userResponse){
                _ConsoleInput.InSequence(sequence).Setup(x => x.ReadLine()).Returns(response);
            }
            return sequence;
        }
        
        [Test]
        public void TestTwoMovesA2toA3ThenA7ToA5(){
            SetupUserResponse("a3","a4");
            var expectedOutput = "x";
            
            var outputLines = RunMainAndGetConsoleOutput();

            //Chess.Main(default);

            NUnit.Framework.Assert.Equals(expectedOutput, outputLines[0]);
        }
    }


    public class unitTests
    {   


    [Fact]
    public void BlackPawnCreatedAtG7()
    {
        var board = new Board(8,false,false);
        var pawn = new Pawn(false,1);
        board.getBoard()[6,6].placePiece(pawn);
        Xunit.Assert.Equal(pawn,board.getPieceOnSquare(6,6));
    }

    [Fact]
    public void BlackPawnAtG7CanMoveToG6AndG5()
    {
        var board = new Board(8,false,false);
        var pawn = new Pawn(false,1);
        board.getBoard()[6,6].placePiece(pawn);
        var expectedposList = new List<(int, int)>
        {
            (5, 6),
            (4, 6)
        };
        
        Xunit.Assert.Equal(expectedposList,board.getPieceOnSquare(6,6).Move(6,6,board));
    }

    [Fact]
    public void BlackPawnAtG6CanOnlyMoveToG5()
    {
        var board = new Board(8,false,false);
        var pawn = new Pawn(false,1);
        board.getBoard()[5,6].placePiece(pawn);
        var expectedposList = new List<(int, int)>
        {
            (4, 6)
        };
        
        Xunit.Assert.Equal(expectedposList,board.getPieceOnSquare(5,6).Move(5,6,board));
    }

    [Fact]
    public void WhitePawnAtG3CanOnlyMoveToG4()
    {
        var board = new Board(8,true,false);
        var pawn = new Pawn(true,1);
        board.getBoard()[2,6].placePiece(pawn);
        var expectedposList = new List<(int, int)>
        {
            (3, 6)
        };
        
        Xunit.Assert.Equal(expectedposList,board.getPieceOnSquare(2,6).Move(2,6,board));
    }

    [Fact]
    public void WhitePawnAtG2CanMoveToG3AndG4()
    {
        var board = new Board(8,true,false);
        var pawn = new Pawn(true,1);
        board.getBoard()[1,6].placePiece(pawn);
        var expectedposList = new List<(int, int)>
        {
            (2, 6),
            (3, 6)
        };
                
        Xunit.Assert.Equal(expectedposList,board.getPieceOnSquare(1,6).Move(1,6,board));
    }

    [Fact]
    public void WhitePawnAtG2CanAttackBlackPieceAtF3()
    {
        var board = new Board(8,true,false);
        var wpawn = new Pawn(true,1);
        var bpawn = new Pawn(false,1);
        board.getBoard()[2,5].placePiece(bpawn);
        board.getBoard()[1,6].placePiece(wpawn);
        var expectedattackList = new List<(int, int)>
        {
            (2, 5)
        };
        
                
        Xunit.Assert.Equal(expectedattackList,board.getPieceOnSquare(1,6).Attack(1,6,board));
    }


    [Fact]
    public void RookAtD4Moves()
    {
        var board = new Board(8,true,false);
        var wrook = new Rook(true,1);
        board.getBoard()[3,3].placePiece(wrook);
        var expectedmoveList = new List<(int,int)>();
        
        for(int i = 0; i < 8; i++){
            if(i!=3){
            expectedmoveList.Add((3,i));
            expectedmoveList.Add((i,3));
            }
        }
        expectedmoveList.Sort();

        var actual  = board.getPieceOnSquare(3,3).Move(3,3,board);
        if(actual.Count > 0){
            actual.Sort();
        }
        
        Xunit.Assert.Equal(expectedmoveList,actual);
    }

    public void WhiteRookAtD4AttacksD1andA4andD8()
    {
        var board = new Board(8,true,false);
        var wrook = new Rook(true,1);
        board.getBoard()[3,3].placePiece(wrook);
        var expectedattackList = new List<(int, int)>
        {
            (3, 0),
            (7, 3),
            (2, 3)
        };

        expectedattackList.Sort();
        
        var bpawn = new Pawn(false,1);
        var brook = new Rook(false,1);
        var bbishop = new Bishop(false,1);
        var wpawn = new Pawn(true,1);

        board.getBoard()[3,0].placePiece(bpawn);
        board.getBoard()[7,3].placePiece(brook);
        board.getBoard()[3,6].placePiece(wpawn);
        board.getBoard()[2,3].placePiece(bbishop);
            
        var actual  = board.getPieceOnSquare(3,3).Attack(3,3,board);

        if(actual.Count > 0){
            actual.Sort();
        }
        
        Xunit.Assert.Equal(expectedattackList,actual);
    }

    [Fact]
    public void BishopAtE5Moves()
    {
        var board = new Board(8,true,false);
        var wbishop = new Bishop(true,1);
        board.getBoard()[4,4].placePiece(wbishop);
        var expectedmoveList = new List<(int,int)>();
        
        var positive = 4;
        var negative = 4;
        
        while(true){
            negative--;
            positive++;
            
            if(positive < 8){    
                expectedmoveList.Add((positive,positive));
            }
            
            if(negative > -1){
                expectedmoveList.Add((negative,negative));    
            }
            
            if(negative > -1 && positive < 8){
                expectedmoveList.Add((positive,negative));
                expectedmoveList.Add((negative,positive));
            }
            
            if(negative <= 0 && positive >= 7){
                break;
            }
        }
        
        expectedmoveList.Sort();
        expectedmoveList.Remove((4,4));

        var actual  = board.getPieceOnSquare(4,4).Move(4,4,board);
        if(actual.Count > 0){
            actual.Sort();
        }
        
        Xunit.Assert.Equal(expectedmoveList,actual);
    }

    [Fact]
    public void BishopAtD4Moves()
    {
        var board = new Board(8,true,false);
        var wbishop = new Bishop(true,1);
        board.getBoard()[3,3].placePiece(wbishop);
        var expectedmoveList = new List<(int,int)>();
        
        var positive = 3;
        var negative = 3;
        
        while(true){
            negative--;
            positive++;

            if(positive < 8){
                expectedmoveList.Add((positive,positive));
            }
                      
            if(negative > -1){
                expectedmoveList.Add((negative,negative));    
            }
            
            if(negative > -1 && positive < 8){
                expectedmoveList.Add((positive,negative));
                expectedmoveList.Add((negative,positive));
            }
                        
            
            if(negative <= 0 && positive >= 7){
                break;
            }
        }
        
        expectedmoveList.Sort();
        expectedmoveList.Remove((3,3));
        
        var actual  = board.getPieceOnSquare(3,3).Move(3,3,board);
        if(actual.Count > 0){
            actual.Sort();
        }
        
        Xunit.Assert.Equal(expectedmoveList,actual);
    }

    [Fact]
    public void BishopAtD4Attacks()
    {
        var board = new Board(8,true,false);
        var wbishop = new Bishop(true,1);
        var bpawn = new Pawn(false,1);
        
        board.getBoard()[3,3].placePiece(wbishop);
        var expectedAttackList = new List<(int,int)>(){
            (1,1),
            (4,4),
            (5,1),
            (1,5)
        };
        
        board.getBoard()[1,1].placePiece(bpawn);
        board.getBoard()[4,4].placePiece(bpawn);
        board.getBoard()[5,1].placePiece(bpawn);
        board.getBoard()[1,5].placePiece(bpawn);
        
        expectedAttackList.Sort();
        
        var actual  = board.getPieceOnSquare(3,3).Attack(3,3,board);
        if(actual.Count > 0){
            actual.Sort();
        }
        
        Xunit.Assert.Equal(expectedAttackList,actual);
    }

    [Fact]
    public void BishopAtD4DoesNotAttackAlliedPiece()
    {
        var board = new Board(8,true,false);
        var wbishop = new Bishop(true,1);
        var wbishop1 = new Bishop(true,1);
        var bpawn = new Pawn(false,1);
        
        board.getBoard()[3,3].placePiece(wbishop);
        
        board.getBoard()[5,5].placePiece(bpawn);
        board.getBoard()[4,4].placePiece(wbishop1);

        var actual  = board.getPieceOnSquare(3,3).Attack(3,3,board);
                
        Xunit.Assert.Empty(actual);
    }

    [Fact]
    public void KnightMovePattern()
    {
        var board = new Board(8,true,false);
        var wknight = new Knight(true,1);
        var wbishop = new Bishop(true,1);
        
        board.getBoard()[3,3].placePiece(wknight);
        board.getBoard()[5,2].placePiece(wbishop);
        board.getBoard()[4,5].placePiece(wbishop);

        var expectedmoveList = new List<(int,int)>(){
            (1,4),
            (1,2),
            (2,1),
            (2,5),
            (5,4),
            (4,1)
        };

        var actual  = board.getPieceOnSquare(3,3).Move(3,3,board);

        expectedmoveList.Sort();
        actual.Sort();    
                
        Xunit.Assert.Equal(expectedmoveList,actual);
    }

    [Fact]
    public void KnightD4Attacks2()
    {
        var board = new Board(8,true,false);
        var wknight = new Knight(true,1);
        var wbishop = new Bishop(true,1);
        var bpawn =  new Pawn(false, 1);
        
        board.getBoard()[3,3].placePiece(wknight);
        board.getBoard()[5,2].placePiece(wbishop);
        board.getBoard()[4,5].placePiece(wbishop);
        board.getBoard()[1,4].placePiece(bpawn);
        board.getBoard()[2,5].placePiece(bpawn);
        
        var expectedAttackList = new List<(int,int)>(){
            (1,4),
            (2,5)
        };

        var actual  = board.getPieceOnSquare(3,3).Attack(3,3,board);

        expectedAttackList.Sort();
        actual.Sort();    
                
        Xunit.Assert.Equal(expectedAttackList,actual);
    }

    [Fact]
    public void KingMovesB2()
    {
        var board = new Board(8,true,false);
        var wking = new King(true,1);
        
        
        board.getBoard()[1,1].placePiece(wking);
       
        
        var expectedMovesList = new List<(int,int)>(){
            (2,0),
            (1,0),
            (0,1),
            (0,0),
            (0,2),
            (2,2),
            (2,1),
            (1,2)
        };

        var actual = board.getPieceOnSquare(1,1).Move(1,1,board);

        expectedMovesList.Sort();
        actual.Sort();    
                
        Xunit.Assert.Equal(expectedMovesList,actual);
    }
    
    [Fact]
    public void KingMovesA1()
    {
        var board = new Board(8,true,false);
        var wking = new King(true,1);
        
        
        board.getBoard()[0,0].placePiece(wking);
       
        var expectedMovesList = new List<(int,int)>(){
            (1,1),
            (1,0),
            (0,1)
        };

        var actual = board.getPieceOnSquare(0, 0).Move(0,0, board);

        expectedMovesList.Sort();
        actual.Sort();    
                
        Xunit.Assert.Equal(expectedMovesList,actual);
    }
    [Fact]
    public void PawnA1()
    {
        var board = new Board(8,true,false);
        var wpawn = new Pawn(true,1);
        
        board.getBoard()[0,0].placePiece(wpawn);
       
        var expectedMovesList = new List<(int,int)>(){
            (1,0)
        };

        var actual = board.getPieceOnSquare(0, 0).Move(0,0,board);

        expectedMovesList.Sort();
        actual.Sort();    
                
        Xunit.Assert.Equal(expectedMovesList,actual);
    }

    [Fact]
    public void KingA1OnlyLegalMoveIsA2()
    {
        var board = new Board(8,true,false);
        var wking = new King(true,1);
        var brook = new Rook(false,1);

        board.getBoard()[0,0].placePiece(wking);
        board.getBoard()[7,1].placePiece(brook);

        var expectedMovesList = new List<(int,int)>(){
            (1,0)
        };

        var actual = board.getPieceOnSquare(0, 0).Move(0,0,board);

        expectedMovesList.Sort();
        actual.Sort();    
                
        Xunit.Assert.Equal(expectedMovesList,actual);
    }

    [Fact]
    public void KingCastlingRight()
    {
        //Start
        var board = new Board(8,true,false);
        var wking = new King(true,1);
        var wrook = new Rook(true,1);
        board.getBoard()[0,4].placePiece(wking);
        board.getBoard()[0,7].placePiece(wrook);

        //Expected
        var expectedRookpos = (0,5);
        var expectedKingpos = (0,6);


        //Execute    
        var actualKing = board.getPieceOnSquare(0,4) as King;
        var castleKingPos = actualKing?.CastleMoves(0,4,board).First();

        var actualRook = board.getPieceOnSquare(0,7) as Rook;
        var castleRookPos = actualRook?.CastleMove(0,7,board);

        //Assert
        Xunit.Assert.Equal(expectedKingpos,castleKingPos);
        Xunit.Assert.Equal(expectedRookpos,castleRookPos);
    }

    [Fact]
    public void KingCastlingLeft()
    {
        //Start
        var board = new Board(8,true,false);
        var wking = new King(true,1);
        var wrook = new Rook(true,1);
        board.getBoard()[0,4].placePiece(wking);
        board.getBoard()[0,0].placePiece(wrook);

        //Expected
        var expectedRookpos = (0,3);
        var expectedKingpos = (0,2);


        //Execute    
        var actualKing = board.getPieceOnSquare(0,4) as King;
        var castleKingPos = actualKing?.CastleMoves(0,4,board).First();

        var actualRook = board.getPieceOnSquare(0,0) as Rook;
        var castleRookPos = actualRook?.CastleMove(0,0,board);

        //Assert
        Xunit.Assert.Equal(expectedKingpos,castleKingPos);
        Xunit.Assert.Equal(expectedRookpos,castleRookPos);
    }

        [Fact]
    public void KingCastlingLeftisBlocked()
    {
        //Start
        var board = new Board(8,true,false);
        var wking = new King(true,1);
        var wrook = new Rook(true,1);
        var wknight = new Knight(true,1);

        board.getBoard()[0,4].placePiece(wking);
        board.getBoard()[0,0].placePiece(wrook);
        board.getBoard()[0,1].placePiece(wknight);


        //Execute    
        var actualKing = board.getPieceOnSquare(0,4) as King;
        var castleKingPos = actualKing?.CastleMoves(0,4,board);

        var actualRook = board.getPieceOnSquare(0,0) as Rook;
        var castleRookPos = actualRook?.CastleMove(0,0,board);

        //Assert
        Xunit.Assert.Empty(castleKingPos);
        Xunit.Assert.Equal(castleRookPos,(0,3));
    }


    [Fact]
    public void KnightA1Moves()
    {
        var board = new Board(8,true,false);
        var bknight = new Knight(false,1);

        board.getBoard()[0,0].placePiece(bknight);

        var expectedMovesList = new List<(int,int)>(){
            (1,2),
            (2,1)
        };

        var actual = board.getPieceOnSquare(0, 0).Move(0,0,board);

        expectedMovesList.Sort();
        actual.Sort();    
                
        Xunit.Assert.Equal(expectedMovesList,actual);
    }
    
    [Fact]
    public void KingCastlingLeftPathIsAttacked()
    {
        //Start
        var board = new Board(8,true,false);
        var wking = new King(true,1);
        var wrook = new Rook(true,1);
        var brook = new Rook(false,1);

        board.getBoard()[0,4].placePiece(wking);
        board.getBoard()[0,0].placePiece(wrook);
        board.getBoard()[7,2].placePiece(brook);

        //Execute    
        var actualKing = board.getPieceOnSquare(0,4) as King;
        var castleKingPos = actualKing?.CastleMoves(0,4,board);

        //Assert
        Xunit.Assert.Empty(castleKingPos);
    }


    [Fact]
    public void pinnedRook()
    {
        //Start
        var board = new Board(8,true,false);
        var wking = new King(true,1);
        var wrook = new Rook(true,1);
        var brook = new Rook(false,1);

        board.getBoard()[0,4].placePiece(wking);
        board.getBoard()[0,0].placePiece(wrook);
        board.getBoard()[7,2].placePiece(brook);

        //Execute    
        var actualKing = board.getPieceOnSquare(0,4) as King;
        var castleKingPos = actualKing?.CastleMoves(0,4,board);

        //Assert
        Xunit.Assert.Empty(castleKingPos);
    }
  }

  
    }
}