using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternArmyRTS : MonoBehaviour {
	public Camera cam;
	public UnityEngine.AI.NavMeshAgent nav;
	public Vector3 Destination;
	public float PathRemain;
	public List<GameObject> selectedObjects;
	//public GameObject[] Array;
	public float Size;
	public int i;
	public int H;
	public int V;
	Vector3 MouseStartingPosition;

	void Start () {
		cam = Camera.main;
	}

	void Move (Vector3 Goal, int i )
	{
		nav = selectedObjects [i].GetComponent<UnityEngine.AI.NavMeshAgent> ();
		nav.SetDestination (Goal);
	}

	public void Pattern (Vector3 Pos)
	{
		Move (Pos, 0); // Di chuyen phan tu dau tien
		while (i < selectedObjects.Count) {
			if (i >= Size) { // xuong dong
				Vector3 PosTemp;
				V++;
				PosTemp.x = Pos.x;
				PosTemp.y = Pos.y;
				PosTemp.z = Pos.z + V*2;
				Move (PosTemp, i);
				Size += Mathf.Sqrt ((float)selectedObjects.Count);

				H = 1;
				i++;
			} else {
				while (i < Size) {
					Vector3 PosTemp1;
					PosTemp1.x = Pos.x + H*2;
					PosTemp1.z = Pos.z + V*2;
					PosTemp1.y = Pos.y;
					Move (PosTemp1, i);
					H++;
					i++;
				}
			}
		}
	}
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				Destination = hit.point;
				i = 1;
				H = 1;
				V = 0;
				Size = Mathf.Sqrt ((float)selectedObjects.Count);
				Pattern (Destination);
			}
		}

		if (Input.GetMouseButtonDown (0)) 
			MouseStartingPosition = Input.mousePosition;
		

		if (Input.GetMouseButtonUp (0)) {
			selectedObjects.Clear ();
			foreach( var selectableObject in FindObjectsOfType<SelectableObject>() )
			{
				if( IsWithinSelectionBounds( selectableObject.gameObject ) )
				{
					Debug.Log(selectableObject.gameObject.name);
					selectedObjects.Add( selectableObject.gameObject );
				}
			}
		}

	}

	public bool IsWithinSelectionBounds( GameObject gameObject )
	{
		if( !UnitSelecting.isSelecting )
			return false;
		var camera = Camera.main;
		var viewportBounds = UnitSelectingLib.GetViewportBounds( camera, MouseStartingPosition, Input.mousePosition );
		return viewportBounds.Contains( camera.WorldToViewportPoint( gameObject.transform.position ) );
	}
}