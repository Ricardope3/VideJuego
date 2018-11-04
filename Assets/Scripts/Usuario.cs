﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Usuario : MonoBehaviour {
	public GameObject	ground;
    public GameObject chest;
    public GameObject chestOpen;
    public GameObject chestLampara;
    public AudioClip coin;
    public GameObject light;
    private Light lightcomponent;
	public bool walking=false;
	private Vector3 spawn;
    IEnumerator ie;
    Coroutine c;
    float start = 0;
    float stop = 0;
    bool lampara = false;
    bool lamparaArma = false;
    public Camera cam;
    public bool saquese=false;
    public GameObject loadingCofres;
    public GameObject linternaPanel;
    public Slider cofreSlider;
    public Slider linternaSlider;
    public Slider vida;
    AudioSource source;
    public AudioClip clipsitos;
    Rigidbody rb;
    float velocity = 1.5f;
    // Use this for initialization
    void Start () {
		spawn = transform.position;
        //ie = verCofre();
        vida.value = 0;
        lightcomponent = light.GetComponent<Light>();
        source = GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody>();
        source.clip = clipsitos;
        source.Play();
    }

    // Update is called once per frame
    void Update() {
        linternaSlider.value = lightcomponent.intensity;
        if (lightcomponent.intensity < 1)
        {
            linternaPanel.SetActive(false);
            light.SetActive(false);
        }
        if (lamparaArma)
        {
            linternaSlider.maxValue = 40;
        
        }
        if (!lamparaArma)
        {
            linternaSlider.maxValue = 6;
        }
        if (!light.active)
        {
            linternaPanel.SetActive(false);
        }
        
        if (light.active)
        {
            linternaPanel.SetActive(true);
        }
        if (light.active && Input.GetKeyDown(KeyCode.A))
        {

            lamparaArma = !lamparaArma;
            if (lamparaArma)
            {
                lightcomponent.intensity = lightcomponent.intensity + lightcomponent.intensity * 6;
            }
            else
            {
                lightcomponent.intensity = lightcomponent.intensity - lightcomponent.intensity *0.9f ;
            }
            
        }


        vida.value += 0.02f;
        if (light.active)
        {
            if (lamparaArma)
            {
                lightcomponent.spotAngle = 10;
                lightcomponent.range = 20;
               
                lightcomponent.intensity -=0.02f;
               
                
            }
            else
            {
                lightcomponent.spotAngle = 40;
                lightcomponent.range = 9;
                    
                lightcomponent.intensity -= 0.001f;
                 
            }
        }
        if (walking) {
            velocity = 1.5f;
            //transform.position = transform.position + Camera.main.transform.forward * Time.deltaTime*1.5f;
            transform.Translate(cam.transform.forward * Time.deltaTime * velocity, Space.World);
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        if (!walking)
        {
           rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            if (!source.clip.Equals(coin))
            {
                source.Stop();
            }
            velocity = 0;
        }

        if (transform.position.y < -10 || vida.value >99) {
            SceneManager.LoadScene("DeadScene");
        }
        
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f,.5f,0));
		RaycastHit hit;

		if(Physics.Raycast(ray,out hit)){
			if(hit.collider.name.Contains("Principal")){
                if (!source.clip.Equals(coin))
                {

                source.Stop();  
                }
				walking=false;
			}

            else if (hit.collider.name == "chest_close")
            {
                stop = Time.time;
                walking = false;
                lampara = false;
                //c = StartCoroutine(ie);
                
            }
            else if(hit.collider.name == "Ghost")
            {
                if (lamparaArma && light.active &&lightcomponent.intensity>0)
                {
                    saquese = true;
                    StartCoroutine(esperarSaquese());
                }
                
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
            loadingCofres.SetActive(false);
        }
        if (stop >0)
        {
            loadingCofres.SetActive(true);
            cofreSlider.value = stop - start; 
            if (stop - start > 5)
            {
                
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
                    //Destroy(GameObject.Find("lampara_chest"));
                    Instantiate<GameObject>(chestOpen, chestLampara.transform.position, chestLampara.transform.rotation);
                    light.SetActive(true);
                    lamparaArma = false;
                    lightcomponent.intensity = 5.5f;
                    source.clip = coin;
                    StartCoroutine(sonarCoin());

                }



            }
        }
	}



    IEnumerator sonarCoin()
    {
        yield return new WaitForSeconds(2f);
        source.clip = clipsitos;

    }

    IEnumerator esperarSaquese()
    {
        yield return new WaitForSeconds(10);
        saquese = false;
    }
}
