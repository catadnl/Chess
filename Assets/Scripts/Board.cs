using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : Singleton<Board>
{
	const int DIMENSION = 8;
	const float ROTATION_DURATION = .1f;

	public  static Piece.PieceColor currentColor = Piece.PieceColor.White;


	public Transform originRef;
	public Transform incrementRef;
	static Vector2 origin;
	static Vector2 increment;

	float rot
	{
		get { return transform.eulerAngles.z; }
		set { transform.rotation = Quaternion.Euler (new Vector3 (0, 0, value)); }
	}

	void Awake ()
	{
		origin = originRef.position;
		increment = (Vector2)incrementRef.position - origin;

		startTime = -1; // don't rotate it on startup
	}

	float startTime;
	float startRot;
	bool rotateBackwards;
	public void rotate(){
		if(currentColor == Piece.PieceColor.Black)
			currentColor = Piece.PieceColor.White;
		else
			currentColor = Piece.PieceColor.Black;

		rotateBackwards = !rotateBackwards;
		startTime = Time.time;
		startRot = rot;
	}

	void Update ()
	{
		if (startTime != -1) {
			float perEplapsed = (Time.time - startTime) / ROTATION_DURATION;
			if (perEplapsed < 1) 
				rot = startRot + perEplapsed * (rotateBackwards ? 180 : -180);

			else {
				rot = startRot + 180;
				origin = originRef.position;
				increment = (Vector2)incrementRef.position - origin;
			}
		}
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

	public static bool IsFree (Vector2 coord, List<Piece> current)
	{
		foreach (Piece piece in current)
			if (piece.coord == coord)
				return false;
		return true;
	}

	public static Piece getPiece (Vector2 coord, List<Piece> current){
		foreach (Piece piece in current)
			if (piece.coord == coord)
				return piece;
		return null;
	}
	
}
