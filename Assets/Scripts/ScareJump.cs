using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ScareJump : MonoBehaviour {

    public AudioSource scream;
    public AudioSource insanitySource;
    public GameObject player;
    public GameObject jumpCam;
    public GameObject flashIMG;
    public PostProcessingBehaviour ppb;
        void Start()
    {
        ppb = GameObject.Find("Main Camera").GetComponent<PostProcessingBehaviour>();

        print(ppb.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            scream.Play();
            
            jumpCam.SetActive(true);
            //player.SetActive(false);
            flashIMG.SetActive(true);
            ppb.enabled = true;
            //print(ppb.gameObject.name);
            insanitySource.Play();
            StartCoroutine(Sanity());
            StartCoroutine(endJump());


        }

    }

    IEnumerator endJump()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(scream);
        print("helo");
        jumpCam.SetActive(false);
        flashIMG.SetActive(false);
        
        
    }
    IEnumerator Sanity()
    {
        yield return new WaitForSeconds(15);
        // player.SetActive(true);
        ppb.enabled = false;
        Destroy(gameObject);
    }
}
