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
    float velocity = 2.5f;
    string destruirEsteCofre ="";
    int llaveInt = 0;
    private bool puertaAbierta = false;
    public AudioSource llaveaudiosource;
    Vector3 velocidad;

    public GameObject aluzarpuerta;
    public GameObject puertaDestruir;

    // Use this for initialization
    void Start () {
        llaveInt = Random.Range(0, 2);
        print(llaveInt);
        walking = true;
		spawn = transform.position;
        //ie = verCofre();
        vida.value = 0;
        lightcomponent = light.GetComponent<Light>();
        source = GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody>();
        source.clip = clipsitos;
        velocity = 1.5f;
        source.Play();
    }

    // Update is called once per frame
    void Update() {
        velocidad = rb.velocity;
        
        //print("velocidad" + velocidad.magnitude);
        
        if (velocidad.magnitude>2)
        {
            print("bajar");
            rb.velocity = Vector3.zero;
            transform.Translate(cam.transform.forward * Time.deltaTime * velocity, Space.World);
            rb.angularVelocity = Vector3.zero;
        }
        print(puertaAbierta);
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
        if (light.active && Input.GetMouseButtonDown(0))
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

        if (transform.position.y < -2 || vida.value >99) {
            SceneManager.LoadScene("DeadScene");
        }
        
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f,.5f,0));
		RaycastHit hit;
        
		if(Physics.Raycast(ray,out hit)){
            if (hit.distance <= 4)
            {
                if (hit.collider.name.Contains("Principal"))
                {
                    if (!source.clip.Equals(coin))
                    {
                        source.Stop();
                        walking = false;
                    }
                        
                    
                    
                    stop = 0;
                }

                else if (hit.collider.name.Contains("chest_close"))
                {
                    stop = Time.time;
                    walking = false;
                    lampara = false;


                }
                else if (hit.collider.name == "Ghost")
                {
                    if (lamparaArma && light.active && lightcomponent.intensity > 0)
                    {
                        saquese = true;
                        StartCoroutine(esperarSaquese());
                    }

                }
                else if (hit.collider.name.Contains("lampara_chest"))
                {
                    stop = Time.time;
                    walking = false;
                    lampara = true;


                }

                else
                {

                    walking = true;
                    stop = 0;
                }
            }
            else
            {
                walking = true;
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
            if (stop - start > 3)
            {
                source.clip = coin;
                source.Play();
                StartCoroutine(sonarCoin());
                destruirEsteCofre = hit.collider.name;
                char enteroChar = destruirEsteCofre[destruirEsteCofre.Length - 2];
                int enteroInt;
                if (enteroChar == 's')
                {
                    enteroInt = 0;
                }
                else
                {
                    enteroInt = (int)System.Char.GetNumericValue(enteroChar);
                }
                
                print(enteroInt);
                GameObject destruirEsteCofreObject = GameObject.Find(destruirEsteCofre);

                stop = 0;
                if (!lampara)
                {
                    if (enteroInt == llaveInt)
                    {
                        puertaAbierta = true;
                        llaveaudiosource.Play();
                        Destroy(puertaDestruir);
                        aluzarpuerta.SetActive(true);
                    }
                    Instantiate<GameObject>(chestOpen, destruirEsteCofreObject.transform.position, destruirEsteCofreObject.transform.rotation);
                    Destroy(destruirEsteCofreObject);
                    vida.value -= 50;
                    
                 
                }
                if (lampara)
                {
                    //Destroy(GameObject.Find("lampara_chest"));
                    Instantiate<GameObject>(chestOpen, destruirEsteCofreObject.transform.position, destruirEsteCofreObject.transform.rotation);
                    Destroy(GameObject.Find(destruirEsteCofre));
                    light.SetActive(true);
                    lamparaArma = false;
                    lightcomponent.intensity = 5.5f;
                    

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
        yield return new WaitForSeconds(7);
        saquese = false;
    }
}
