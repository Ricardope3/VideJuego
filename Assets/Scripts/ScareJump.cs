using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareJump : MonoBehaviour {

    public AudioSource scream;
    public GameObject player;
    public GameObject jumpCam;
    public GameObject flashIMG;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            scream.Play();
            jumpCam.SetActive(true);
            player.SetActive(false);
            flashIMG.SetActive(true);
            StartCoroutine(endJump());
            
        }

    }

    IEnumerator endJump()
    {
        yield return new WaitForSeconds(2.03f);
        player.SetActive(true);
        jumpCam.SetActive(false);
        flashIMG.SetActive(false);
        Destroy(gameObject);
    }
}
