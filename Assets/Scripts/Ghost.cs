using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour {
	public Transform[] path;
	private Transform target;
    public GameObject jugador;
	int current = 0;
    NavMeshAgent agent;

	private float threshold = 0.01f;
	IEnumerator ie;
	Coroutine c;
	float velocidad = 0.1f;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
		target = path[0];
		//ie = verDist();
		c = StartCoroutine(ie);
        
	}

	// Update is called once per frame
	void Update () {
        agent.speed = 1f;
        //transform.LookAt(target);
        //transform.Translate(transform.forward*Time.deltaTime*velocidad*0.1f,Space.World);
        agent.destination = jugador.transform.position;
        
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                agent.destination = hit.point;
            }
        }
        */

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