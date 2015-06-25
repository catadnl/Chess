using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class Piece : MonoBehaviour 
{
	public enum PieceType { abstr, Pawn, Knight, Bishop, Queen, King, Rook };
	public enum PieceColor { abstr, White, Black };

	SpriteRenderer sprRen;

	public PieceType type;
	public PieceColor color;

	public void SetSpriteAndName ()
	{
		name = color + " "  + type;

		if (sprRen == null)
			sprRen = GetComponent <SpriteRenderer> ();

		if (color == PieceColor.abstr || type == PieceType.abstr)
			sprRen.sprite = SpriteReferences.Instance.abstractSprites [0];

		if (color == PieceColor.White)
			switch (type) {
				case PieceType.Pawn:
					sprRen.sprite = SpriteReferences.Instance.whiteSprites [0];
					break;
				case PieceType.King:
					sprRen.sprite = SpriteReferences.Instance.whiteSprites [1];
					break;
				case PieceType.Queen:
					sprRen.sprite = SpriteReferences.Instance.whiteSprites [2];
					break;
				case PieceType.Knight:
					sprRen.sprite = SpriteReferences.Instance.whiteSprites [3];
					break;
				case PieceType.Bishop:
					sprRen.sprite = SpriteReferences.Instance.whiteSprites [4];
					break;
				case PieceType.Rook:
					sprRen.sprite = SpriteReferences.Instance.whiteSprites [5];
					break;
		}
		else if (color == PieceColor.Black)
			switch (type) {
				case PieceType.Pawn:
					sprRen.sprite = SpriteReferences.Instance.blackSprites [0];
					break;
				case PieceType.King:
					sprRen.sprite = SpriteReferences.Instance.blackSprites [1];
					break;
				case PieceType.Queen:
					sprRen.sprite = SpriteReferences.Instance.blackSprites [2];
					break;
				case PieceType.Knight:
					sprRen.sprite = SpriteReferences.Instance.blackSprites [3];
					break;
				case PieceType.Bishop:
					sprRen.sprite = SpriteReferences.Instance.blackSprites [4];
					break;
				case PieceType.Rook:
					sprRen.sprite = SpriteReferences.Instance.blackSprites [5];
					break;
		}

	}

	public Vector2 coord
	{
		get
		{
			return Board.WorldToCoord (transform.position);
		}

		set 
		{
			transform.position = Board.CoordToWorld (value);
		}
	}

	void Start ()
	{
		PieceManager.Instance.currentPieces.Add (this);
	}

	void Update ()
	{
		SetSpriteAndName ();
		stayStill ();
	}

	void OnMouseDown ()
	{
		PieceManager.Instance.PickUp (this);
	}

	public bool IsOnStartingRow ()
	{
		return (color == PieceColor.White && coord.y == 1) ||
			   (color == PieceColor.Black && coord.y == 6);
	}

	public void stayStill(){
		transform.rotation = Quaternion.identity;
	}

	public void Die () 
	{
		PieceManager.Instance.currentPieces.Remove (this);
	}

}
