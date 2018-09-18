using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {
	public Transform[] path;
	private Transform target;
	int current = 0;

	private float threshold = 0.01f;
	IEnumerator ie;
	Coroutine c;
	float velocidad = 0.1f;
	// Use this for initialization
	void Start () {

		target = path[0];
		ie = verDist();
		c = StartCoroutine(ie);
	}

	// Update is called once per frame
	void Update () {
		transform.LookAt(target);
		transform.Translate(transform.forward*Time.deltaTime*velocidad,Space.World);
	}

	IEnumerator verDist()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.1f);
			float distancia =  Vector3.Distance(transform.position, path[current].position);
			if (distancia < threshold)
			{

				current++;
				current %= path.Length;
				target = path[current];
			}
		}
	}

}		