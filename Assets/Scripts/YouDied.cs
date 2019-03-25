using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDied : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(irAMenu());
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    IEnumerator irAMenu()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Menu");
    }
}
