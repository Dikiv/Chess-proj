using System;
using Xunit;


namespace ChessRules.Tests;

public class ChessRules
{
    [Fact]
    public void BlackPawnCreatedAtG7()
    {
        var board = new Board(8,false);
        var pawn = new Pawn(false,1);
        board.getBoard()[6,6].placePiece(pawn);
        Assert.Equal(pawn,board.getPieceOnSquare(6,6));
    }

    [Fact]
    public void BlackPawnAtG7CanMoveToG6AndG5()
    {
        var board = new Board(8,false);
        var pawn = new Pawn(false,1);
        board.getBoard()[6,6].placePiece(pawn);
        var expectedposList = new List<(int,int)>();
        expectedposList.Add((5,6));
        expectedposList.Add((4,6));
        
        Assert.Equal(expectedposList,board.getPieceOnSquare(6,6).Move(6,6,board));
    }

    [Fact]
    public void BlackPawnAtG6CanOnlyMoveToG5()
    {
        var board = new Board(8,false);
        var pawn = new Pawn(false,1);
        board.getBoard()[5,6].placePiece(pawn);
        var expectedposList = new List<(int,int)>();
        expectedposList.Add((4,6));
        
        Assert.Equal(expectedposList,board.getPieceOnSquare(5,6).Move(5,6,board));
    }

    [Fact]
    public void WhitePawnAtG3CanOnlyMoveToG4()
    {
        var board = new Board(8,true);
        var pawn = new Pawn(true,1);
        board.getBoard()[2,6].placePiece(pawn);
        var expectedposList = new List<(int,int)>();
        expectedposList.Add((3,6));
        
        Assert.Equal(expectedposList,board.getPieceOnSquare(2,6).Move(2,6,board));
    }

    [Fact]
    public void WhitePawnAtG2CanMoveToG3AndG4()
    {
        var board = new Board(8,true);
        var pawn = new Pawn(true,1);
        board.getBoard()[1,6].placePiece(pawn);
        var expectedposList = new List<(int,int)>();
        expectedposList.Add((2,6));
        expectedposList.Add((3,6));
                
        Assert.Equal(expectedposList,board.getPieceOnSquare(1,6).Move(1,6,board));
    }

    [Fact]
    public void WhitePawnAtG2CanAttackBlackPieceAtF3()
    {
        var board = new Board(8,true);
        var wpawn = new Pawn(true,1);
        var bpawn = new Pawn(false,1);
        board.getBoard()[2,5].placePiece(bpawn);
        board.getBoard()[1,6].placePiece(wpawn);
        var expectedattackList = new List<(int,int)>();
        expectedattackList.Add((2,5));
        
                
        Assert.Equal(expectedattackList,board.getPieceOnSquare(1,6).Attack(1,6,board));
    }



}