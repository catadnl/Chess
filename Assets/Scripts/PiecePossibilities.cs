using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PiecePossibilities : Singleton<PiecePossibilities> 
{
	public List<Vector2> Moves (Piece piece)
	{
		List<Vector2> moves = new List<Vector2> ();
		switch (piece.type) {
			case Piece.PieceType.abstr:
				throw new ArgumentException ("Abstract piece should not be moved!");

			// Pion
			case Piece.PieceType.Pawn:
					
				int dir = +1;
				if (piece.color == Piece.PieceColor.Black)
					dir = -1;

				moves.Add (new Vector2 (+0, +1 * dir));
				moves.Add (new Vector2 (+1, +1 * dir));
				moves.Add (new Vector2 (-1, +1 * dir));
				if (piece.IsOnStartingRow ())
					moves.Add (new Vector2 (+0, +2 * dir));
				break;

			// Cal
			case Piece.PieceType.Knight:
				moves.Add (new Vector2 (1, 2));
				moves.Add (new Vector2 (2, 1));
				moves.Add (new Vector2 (-1, -2));
				moves.Add (new Vector2 (-2, -1));
				moves.Add (new Vector2 (1, -2));
				moves.Add (new Vector2 (-2, 1));
				moves.Add (new Vector2 (2, -1));
				moves.Add (new Vector2 (-1, 2));
				break;

			// Nebun
			case Piece.PieceType.Bishop:
				for (var i = 0; i<8; i++){
					moves.Add (new Vector2 (i, i));
					moves.Add (new Vector2 (-i, -i));
					moves.Add (new Vector2 (i, -i));
					moves.Add (new Vector2 (-i, i));
				}
				break;

			// Tura
			case Piece.PieceType.Rook:
				for (var i = 0; i<8; i++){
					moves.Add (new Vector2 (0, i));
					moves.Add (new Vector2 (0, -i));
					moves.Add (new Vector2 (i, 0));
					moves.Add (new Vector2 (-i, 0));
				}
				break;

			// Regina
			case Piece.PieceType.Queen:
				for (var i = 0; i<8; i++){
					moves.Add (new Vector2 (i, i));
					moves.Add (new Vector2 (-i, -i));
					moves.Add (new Vector2 (i, -i));
					moves.Add (new Vector2 (-i, i));
				}
				for (var i = 0; i<8; i++){
					moves.Add (new Vector2 (0, i));
					moves.Add (new Vector2 (0, -i));
					moves.Add (new Vector2 (i, 0));
					moves.Add (new Vector2 (-i, 0));
				}
				break;

			// Rege
			case Piece.PieceType.King:
				moves.Add (new Vector2 (1 ,1));
				moves.Add (new Vector2 (-1 ,-1));
				moves.Add (new Vector2 (1 ,-1));
				moves.Add (new Vector2 (-1 ,1));
				moves.Add (new Vector2 (0 ,1));
				moves.Add (new Vector2 (1 ,0));
				moves.Add (new Vector2 (0,-1));
				moves.Add (new Vector2 (-1 ,0));
				break;
		}

		return moves;
	}

	public List<Vector2>  NotMoves (Piece piece, Vector2 coord){
		List<Vector2> notMoves = new List<Vector2> ();
		switch (piece.type) {
		case Piece.PieceType.Pawn:
			int dir = +1;
			if (piece.color == Piece.PieceColor.Black)
				dir = -1;

			if (piece.IsOnStartingRow () && Math.Abs(coord.y - piece.coord.y) == 1)
			{
				notMoves.Add (new Vector2 (coord.x, coord.y + 1 * dir));

			}

			break;
		case Piece.PieceType.Knight:
			//the Knight may jump
			break;
		case Piece.PieceType.Bishop:
			if(piece.coord.x > coord.x && piece.coord.y < coord.y) 
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x - i, coord.y + i));
			if(piece.coord.x < coord.x && piece.coord.y > coord.y)
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x + i, coord.y - i));
			if(piece.coord.x < coord.x && piece.coord.y < coord.y)
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x + i, coord.y + i));
			if(piece.coord.x > coord.x && piece.coord.y > coord.y)
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x - i, coord.y - i));
			break;
		case Piece.PieceType.Rook:
			if(piece.coord.x == coord.x && piece.coord.y > coord.y) 
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x, coord.y - i));
			if(piece.coord.x == coord.x && piece.coord.y < coord.y) 
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x, coord.y + i));
			if(piece.coord.x > coord.x && piece.coord.y == coord.y) 
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x - i, coord.y));
			if(piece.coord.x < coord.x && piece.coord.y == coord.y) 
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x + i, coord.y));
			break;
		case Piece.PieceType.Queen:
			if(piece.coord.x > coord.x && piece.coord.y < coord.y) 
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x - i, coord.y + i));
			if(piece.coord.x < coord.x && piece.coord.y > coord.y)
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x + i, coord.y - i));
			if(piece.coord.x < coord.x && piece.coord.y < coord.y)
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x + i, coord.y + i));
			if(piece.coord.x > coord.x && piece.coord.y > coord.y)
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x - i, coord.y - i));
			if(piece.coord.x == coord.x && piece.coord.y > coord.y) 
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x, coord.y - i));
			if(piece.coord.x == coord.x && piece.coord.y < coord.y) 
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x, coord.y + i));
			if(piece.coord.x > coord.x && piece.coord.y == coord.y) 
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x - i, coord.y));
			if(piece.coord.x < coord.x && piece.coord.y == coord.y) 
				for (int i = 1; i<8; i++)
					notMoves.Add (new Vector2 (coord.x + i, coord.y));
			break;
		case Piece.PieceType.King:
			//it is not possible for the king to jump
			break;
		}
		return notMoves;
	}

	public List<Vector2> PossibleMoves (Piece piece){
		List<Vector2> possibleMoves = new List<Vector2>();
		List<Vector2> notMoves = new List<Vector2> ();
		List<Vector2> moves = PiecePossibilities.Instance.Moves (piece);
		
		foreach (Vector2 move in moves) {
			Vector2 coord = piece.coord;
			coord += move;
			
			// add a  entry iff the move is inside the board and it is free -- special case for pawn
			if ( (piece.type == Piece.PieceType.Pawn && Board.IsInside (coord) && Board.IsFree (coord) && move.x ==0) 
			    || (piece.type != Piece.PieceType.Pawn && Board.IsInside (coord) && Board.IsFree (coord)) ){
				possibleMoves.Add (coord);
			}
			else if ((piece.type == Piece.PieceType.Pawn && Board.IsInside(coord) && !Board.IsFree(coord) && move.x !=0)
			         || (piece.type != Piece.PieceType.Pawn && Board.IsInside(coord) && !Board.IsFree(coord)) ){
				notMoves.AddRange( PiecePossibilities.Instance.NotMoves (piece, coord) );
				Piece enemy = Board.getPiece(coord);
				if(piece.color != enemy.color && !notMoves.Contains(enemy.coord))
				{	//creating an entry for a pieace that may be destroyed -- special case for pawn
					possibleMoves.Add (coord);
				}
			}
			else if(piece.type == Piece.PieceType.Pawn && move.x == 0 && Board.IsInside (coord) &&!Board.IsFree(coord))
				notMoves.AddRange( PiecePossibilities.Instance.NotMoves (piece, coord) );
		}
		for(int i = possibleMoves.Count - 1; i>=0; i--){
			Vector2 highlight = possibleMoves[i];
			if(notMoves.Contains(highlight))
				possibleMoves.Remove(highlight);
		}
		return possibleMoves;

	}

	public List<Vector2> inChessMoves (Piece piece){

		List<Vector2> inChessMoves = PossibleMoves(piece);
		for (int i = inChessMoves.Count -1; i>= 0; i--){
			Vector2 coord = inChessMoves[i];

			if(Board.IsFree(coord)){
				Vector2 initial = piece.coord;
				piece.coord = coord;
				List<Piece> current = new List<Piece> (PieceManager.instance.currentPieces);
				if(PieceManager.Instance.playerIsInChess(current))
					inChessMoves.Remove(coord);
				piece.coord = initial;
			}
			else if (Board.getPiece(coord).color != piece.color){ //if I destroy a enemy piece
				List<Piece> current = new List<Piece> (PieceManager.instance.currentPieces);
				Piece enemy = Board.getPiece(coord);
				Vector2 initial = piece.coord;
				current.Remove(enemy);
				piece.coord = coord;
				if(PieceManager.Instance.playerIsInChess(current))
					inChessMoves.Remove(coord);
				piece.coord = initial;
				current.Add(enemy);
			}

		}
		foreach (Piece p in PieceManager.Instance.currentPieces){
			Vector3  pos = Board.Clamp (Board.CoordToWorld(p.coord));
			pos.z = -5;
			p.transform.position = pos;
		}
		return inChessMoves;
	}
	
}
/* arrives at the opposite side of the table
 * starting new game after one finishes
 * rotation of the table in slow motion
 * less hard-coding
 * try to reduce going over the list of pieces so many times - more dynamical functions 
 * (ex ^ : reseting z in inChessMoves)
 */