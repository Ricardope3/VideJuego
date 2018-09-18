using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour {
	public Nodo[] vecinos;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(transform.position, .6f);
		if(vecinos!=null && vecinos.Length > 0)
		{
			for(int i = 0; i<vecinos.Length; i++)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawLine(transform.position, vecinos[i].transform.position);
			}
		}
	}
}