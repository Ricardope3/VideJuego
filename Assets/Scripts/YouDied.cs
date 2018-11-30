using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDied : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(irAMenu());
	}
    IEnumerator irAMenu()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Menu");
    }
}
