using UnityEngine;
using System.Collections;

public class Board : Singleton<Board>
{
	const int DIMENSION = 8;

	public  static Piece.PieceColor currentColor = Piece.PieceColor.White;


	public Transform originRef;
	public Transform incrementRef;
	static Vector2 origin;
	static Vector2 increment;

	void Awake ()
	{
		origin = originRef.position;
		increment = (Vector2)incrementRef.position - origin;
	}
	public void rotate(){
		if(currentColor == Piece.PieceColor.Black)
			currentColor = Piece.PieceColor.White;
		else
			currentColor = Piece.PieceColor.Black;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z +180));
		foreach(Piece piece in PieceManager.Instance.currentPieces){
			piece.stayStill();
		}
		origin = originRef.position;
		increment = (Vector2)incrementRef.position - origin;
		
	}
	
	void OnMouseDown ()
	{
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		PieceManager.Instance.Drop (mousePosition);
	}

	public static Vector2 WorldToCoord (Vector2 world)
	{
		Vector2 coord = world;

		coord -= origin;
		coord.x = Mathf.Round (coord.x / increment.x);
		coord.y = Mathf.Round (coord.y / increment.y);

		return coord;
	}

	public static Vector2 CoordToWorld (Vector2 coord)
	{
		Vector2 world = new Vector2 (coord.x * increment.x,
		                             coord.y * increment.y);
		world += origin;
		return world;
	}

	/**
	 * Returns the center of the cell
	 * indicated by the position argument
	 */
	public static Vector2 Clamp (Vector2 position)
	{
		// We strip the noise
		Vector2 result = WorldToCoord (position);
		// We return the center of the cell
		result = CoordToWorld (result);
		return result;
	}

	public static bool IsInside (Vector2 coord)
	{
		// Coordinates are zero indexed
		return (coord.x >= 0 && coord.x < DIMENSION) &&
			   (coord.y >= 0 && coord.y < DIMENSION);			   
	}

	public static bool IsFree (Vector2 coord)
	{
		foreach (Piece piece in PieceManager.Instance.currentPieces)
			if (piece.coord == coord)
				return false;
		return true;
	}

	public static Piece getPiece (Vector2 coord){
		foreach (Piece piece in PieceManager.Instance.currentPieces)
			if (piece.coord == coord)
				return piece;
		return null;
	}
	
}
