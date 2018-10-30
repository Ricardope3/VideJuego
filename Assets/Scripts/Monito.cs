using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monito : MonoBehaviour {
    float h;
    Animator animator;
    bool walking = true;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("Walking", walking);
    }
	
	// Update is called once per frame
	void Update () {
        this.walking = GameObject.Find("GvrMain").GetComponent<Usuario>().walking;

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            h = 4 * Input.GetAxis("Mouse X");
            transform.Rotate(0, h, 0);
        }

        
        
            animator.SetBool("Walking", this.walking);
        
        
	}
}
