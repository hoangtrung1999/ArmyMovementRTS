using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectingLib : MonoBehaviour {
	static Texture2D _whiteTexture;
	public static Texture2D WhiteTexture
	{
		get
		{
			if( _whiteTexture == null )
			{
				_whiteTexture = new Texture2D( 1, 1 );
				_whiteTexture.SetPixel( 0, 0, Color.white );
				_whiteTexture.Apply();
			}

			return _whiteTexture;
		}
	}

	public static void DrawBox( Rect rect, Color color )
	{
		GUI.color = color;
		GUI.DrawTexture( rect, WhiteTexture );
		GUI.color = Color.white;
	}

	public static void DrawBoxBorder ( Rect rect , float thickness, Color color)
	{
		// Ve tung canh cua hinh chu nhat
		// thickness: Do dai cua canh hinh chu nhat
		// Top
		UnitSelectingLib.DrawBox( new Rect( rect.xMin, rect.yMin, rect.width, thickness ), color );
		// Left
		UnitSelectingLib.DrawBox( new Rect( rect.xMin, rect.yMin, thickness, rect.height ), color );
		// Right
		UnitSelectingLib.DrawBox( new Rect( rect.xMax - thickness, rect.yMin, thickness, rect.height ), color);
		// Bottom
		UnitSelectingLib.DrawBox( new Rect( rect.xMin, rect.yMax - thickness, rect.width, thickness ), color );

	}

	public static Rect GetScreenRect (Vector3 ScreenPosition1, Vector3 ScreenPosition2)
	{
		ScreenPosition1.y = Screen.height - ScreenPosition1.y;
		ScreenPosition2.y = Screen.height - ScreenPosition2.y;
		Vector3 TopLeft = Vector3.Min (ScreenPosition1, ScreenPosition2);
		Vector3 BottomRight = Vector3.Max (ScreenPosition1, ScreenPosition2);
		return Rect.MinMaxRect (TopLeft.x, TopLeft.y, BottomRight.x, BottomRight.y);
	}

	public static Bounds GetViewportBounds( Camera camera, Vector3 screenPosition1, Vector3 screenPosition2 )
	{
		
		var v1 = Camera.main.ScreenToViewportPoint( screenPosition1 );
		var v2 = Camera.main.ScreenToViewportPoint( screenPosition2 );
		var min = Vector3.Min( v1, v2 );
		var max = Vector3.Max( v1, v2 );
		min.z = camera.nearClipPlane;
		max.z = camera.farClipPlane;
		var bounds = new Bounds();
		bounds.SetMinMax( min, max );
		return bounds;
	}
}
