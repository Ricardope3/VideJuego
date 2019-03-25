﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour {
    

    public GameObject jugador;
	int current = 0;
    NavMeshAgent agent;
    public Transform spawn;
    bool vamonos;

	// Use this for initialization
	void Start () {
        //spawn = gameObject.transform;
        agent = GetComponent<NavMeshAgent>();
		//target = path[0];
		//ie = verDist();
		// = StartCoroutine(ie);
        
	}

	// Update is called once per frame
	void Update () {
        agent.speed = 0.5f;
        vamonos = GameObject.Find("Player").GetComponent<Usuario>().saquese;
        //transform.LookAt(target);
        //transform.Translate(transform.forward*Time.deltaTime*velocidad*0.1f,Space.World);
        if (!vamonos)
        {
                agent.destination = jugador.transform.position;

        }
        if (vamonos)
        {
            agent.destination = spawn.position;
            StartCoroutine(esperar());
        }
        
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
    IEnumerator esperar()
    {
        yield return new WaitForSeconds(6);
        gameObject.transform.position = spawn.position;
    }



}		