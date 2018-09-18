using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usuario : MonoBehaviour {
	public GameObject	ground;
	private bool walking=false;
	private Vector3 spawn;
	// Use this for initialization
	void Start () {
		spawn = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (walking) {
			transform.position = transform.position + Camera.main.transform.forward * Time.deltaTime*.5f;
		}	

		if (transform.position.y < -10) {
			transform.position = spawn;
		}

		Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f,.5f,0));
		RaycastHit hit;

		if(Physics.Raycast(ray,out hit)){
			if(hit.collider.name.Contains("Principal")){
				walking=false;
			
			}else{
				walking=true;
			}
		}
	}
}
