using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Crawler : MonoBehaviour {

    public Animator animator;
    public AudioSource grito;
    public GameObject luz;
    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            this.animator.SetTrigger("Die");
            grito.Play();
            luz.SetActive(true);
            StartCoroutine(esperar());
            

        }
        
    }

    IEnumerator esperar()
    {
        yield return new WaitForSeconds(0.64f);
        SceneManager.LoadScene("DeadScene");
    }
}
