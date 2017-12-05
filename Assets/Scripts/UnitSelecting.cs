using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelecting : MonoBehaviour {
	public static bool isSelecting = false;
	Vector3 MouseStartingPosition;

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) {
			isSelecting = true;
			MouseStartingPosition = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp (0)) {
			isSelecting = false;
		}
	}
		
	void OnGUI()
	{
		if (isSelecting) {
			Rect rect = UnitSelectingLib.GetScreenRect (MouseStartingPosition, Input.mousePosition);
			//Draw the "inside" box
			UnitSelectingLib.DrawBox (rect, new Color (0.8f, 0.8f, 0.95f, 0.25f));
			// Draw the "border" box
			UnitSelectingLib.DrawBoxBorder (rect, 2, new Color (0.8f, 0.8f, 0.95f));
		}
	}



}
