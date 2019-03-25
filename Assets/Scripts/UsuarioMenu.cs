using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UsuarioMenu : MonoBehaviour {
    float stop = 0;
    float start = 0;
    public GameObject loading;
    public Slider slider;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name == "ESCAPE")
            {
                stop = Time.time;
                if (stop - start > 3)
                {
                    SceneManager.LoadScene("Salir1");
                }
            }

            else if (hit.collider.name == "SURVIVE")
            {
                stop = Time.time;
                if (stop - start > 3)
                {
                    SceneManager.LoadScene("Supervivencia1");
                }
            }
            else if (hit.collider.name == "ESCAPE (EASY)")
            {
                stop = Time.time;
                if (stop - start > 3)
                {
                    SceneManager.LoadScene("SalirFacil");
                }
            }
            else
            {
                stop = 0;
               
            }

        }
        if (stop == 0)
        {
            start = Time.time;
            loading.SetActive(false);
        }
        if (stop > 0)
        {
            loading.SetActive(true);
            slider.value = stop - start;
        }
    }
}
