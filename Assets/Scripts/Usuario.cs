﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Usuario : MonoBehaviour {
	public GameObject	ground;
    public GameObject chest;
    public GameObject chestOpen;
    public GameObject chestLampara;
    public AudioClip coin;
    public GameObject light;
	private bool walking=false;
	private Vector3 spawn;
    IEnumerator ie;
    Coroutine c;
    float start = 0;
    float stop = 0;
    bool lampara = false;
    public Camera cam;
    public GameObject loading;
    public Slider slider;
    public Slider vida;
    AudioSource source;
    public AudioClip clipsitos;
    Rigidbody rb;
    // Use this for initialization
    void Start () {
		spawn = transform.position;
        ie = verCofre();
        vida.value = 0;

        source = GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody>();
        source.clip = clipsitos;
        source.Play();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            light.GetComponent<Light>().spotAngle = 10;
        }

        vida.value += 0.05f;
        if (light.active)
        {
            light.GetComponent<Light>().intensity -= 0.001f;
        }
		if (walking) {
            //transform.position = transform.position + Camera.main.transform.forward * Time.deltaTime*1.5f;
            transform.Translate(cam.transform.forward * Time.deltaTime * 1.5f, Space.World);
            if(!source.isPlaying)
            {
                source.Play();
            }          
		}
        if (walking == false)
        {
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            source.Stop();
        }

		if (transform.position.y < -10) {
			transform.position = spawn;
		}
        
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f,.5f,0));
		RaycastHit hit;

		if(Physics.Raycast(ray,out hit)){
			if(hit.collider.name.Contains("Principal")){
                source.Stop();  
				walking=false;
			}

            if (hit.collider.name == "chest_close")
            {
                stop = Time.time;
                walking = false;
                lampara = false;
                //c = StartCoroutine(ie);
                
            }
            else if (hit.collider.name == "lampara_chest")
            {
                stop = Time.time;
                walking = false;
                lampara = true;
                //c = StartCoroutine(ie);

            }
            else {
                
				walking=true;
                stop = 0;
			}
		}
        if (stop == 0)
        {
            start = Time.time;
            loading.SetActive(false);
        }
        if (stop >0)
        {
            loading.SetActive(true);
            slider.value = stop - start; 
            if (stop - start > 5)
            {
                print(stop  -  start);
                stop = 0;
                if (!lampara)
                {
                    Instantiate<GameObject>(chestOpen, chest.transform.position, chest.transform.rotation);
                    Destroy(GameObject.Find("chest_close"));
                    vida.value -= 50;
                    source.clip = coin;
                    StartCoroutine(sonarCoin());
                 
                }
                if (lampara)
                {
                    light.SetActive(true);
                }
                
                
                
            }
        }
	}

    IEnumerator verCofre()
    {
        
            yield return new WaitForSeconds(6);
            print("hola jajajaj");
        

    }

    IEnumerator sonarCoin()
    {
        yield return new WaitForSeconds(1.776f);
        source.clip = clipsitos;

    }
}
