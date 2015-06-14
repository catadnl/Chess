using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PieceManager : Singleton<PieceManager> 
{
	const float PIECE_Z = -5;
	const float HIGHLIGHT_Z = -1;
	public Piece heldPiece = null;

	public GameObject highlightPrefab;
	public GameObject selectedPrefab;
	public List<GameObject> activeHighlights;

	[HideInInspector] public List <Piece> currentPieces;

	void Awake ()
	{
		currentPieces = new List<Piece> ();
	}
	
	public void PickUp (Piece piece)
	{
		print ("Picking" + piece.type +" " + piece.color);
		if(!hasLost()){
		// If the player already holds a piece (held piece is not null)
		// this call is ignored
		if (heldPiece == null && Board.currentColor == piece.color) {
			heldPiece = piece;
			Vector3 highlightPosition = Board.CoordToWorld (heldPiece.coord);
			highlightPosition.z = HIGHLIGHT_Z;
			
			GameObject highlight = Instantiate (selectedPrefab,
			                                    highlightPosition,
			                                    Quaternion.identity) as GameObject;
				activeHighlights.Add(highlight);
			ShowHighlights (); 
		}

		//if I'm picking an enemy piece to destroy
		Vector2 pieceCoord = Board.CoordToWorld(piece.coord);
		bool ok = false;
		foreach (GameObject highlight in activeHighlights)
			if ( Board.WorldToCoord(highlight.transform.position) == piece.coord && piece.color != heldPiece.color){
				ok = true;
				piece.Die();
				Destroy(piece.gameObject);		
			}
		if(ok){	//if I destroyed an enemy
			ok = false;
			Drop(pieceCoord);
			}}			else
			print ("LOST");
		
	}

	void ShowHighlights ()
	{

		List<Vector2> possibleMovess = new List<Vector2>();
		possibleMovess = PiecePossibilities.Instance.inChessMoves(heldPiece);


		foreach(Vector2 coord in possibleMovess){
			Vector3 highlightPosition = Board.CoordToWorld (coord);
			highlightPosition.z = HIGHLIGHT_Z;
			
			GameObject highlight = Instantiate (highlightPrefab,
			                                    highlightPosition,
			                                    Quaternion.identity) as GameObject;
			activeHighlights.Add (highlight);
		}
	}

	public void Drop (Vector2 position)
	{

		if (heldPiece != null) {
			Vector3  pos = Board.Clamp (position);
			pos.z = PIECE_Z;

			// Move the piece iff the position is highlighted and it is a valid move
			foreach (GameObject highlight in activeHighlights)
				if ((Vector2)highlight.transform.position == (Vector2)pos)
				{
					heldPiece.transform.position = pos;
					Board.Instance.rotate ();
				}
			print ("Dropping" + heldPiece.type +" " + heldPiece.color);
			// Releasing the piece
			heldPiece = null;

			HideHighlights ();
		}

		// If theere is no piece being held
		// the call is ignored


	}

	void HideHighlights ()
	{
		foreach (GameObject highlight in activeHighlights)
			Destroy (highlight);
		activeHighlights.Clear ();
	}
	
	public bool playerIsInChess (List<Piece> pieceList){
		Piece King = null;
		foreach(Piece piece in pieceList)
		if(piece.color == Board.currentColor && piece.type == Piece.PieceType.King){
			King = piece;
			break;
		}
		foreach(Piece piece in pieceList)
			if(piece.color != King.color){
				List<Vector2> possibleAttacks = PiecePossibilities.Instance.PossibleMoves(piece);
				if(possibleAttacks.Contains(King.coord))
					return true;	
			}
		return false;
	}
	public bool hasLost(){
		foreach(Piece piece in currentPieces)
			if(piece.color == Board.currentColor){
				List<Vector2> possibleMovess = PiecePossibilities.Instance.inChessMoves(piece);
				if(possibleMovess.Count != 0)
					return false;
			}
		return true;
	}
}
