using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternArmyRTS : MonoBehaviour {
	public Camera cam;
	public UnityEngine.AI.NavMeshAgent nav;
	public bool isComing = false;
	public Vector3 PresentPosition;
	public Vector3 Destination;
	public float PathRemain;
	public GameObject[] Array;
	public float Size;
	public int i;
	public int H;
	public int V;

	void Start () {
		cam = Camera.main;
		//nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}

	void Move (Vector3 Goal, int i )
	{
		Debug.Log (i);
		Debug.Log (Goal);
		Debug.Log ("Space");
		nav = Array [i].GetComponent<UnityEngine.AI.NavMeshAgent> ();
		nav.SetDestination (Goal);
	}

	public void Pattern (Vector3 Pos)
	{
		//Vector3 Pos0 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		Move (Pos, 0);
		//Debug.Log (Pos);
		//Array [0].transform.position = Pos;
		while (i <= Array.Length - 1) {
			if (i >= Size) { // xuong dong
				Vector3 PosTemp;
				V++;
				PosTemp.x = Pos.x;
				PosTemp.y = Pos.y;
				PosTemp.z = Pos.z + V*2;
				Move (PosTemp, i);
				//Array [i].transform.position = PosTemp;
				Size += Mathf.Sqrt ((float)Array.Length);

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
		//PlayerNavigation.isCome = false;
	}
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				Destination = hit.point;
				//Destination = new Vector3 ( 0 , 0 , 0);
				Debug.Log ("gg");
				i = 1;
				H = 1;
				V = 0;
				Size = Mathf.Sqrt ((float)Array.Length);
				Pattern (Destination);
			}
		}
	}
}