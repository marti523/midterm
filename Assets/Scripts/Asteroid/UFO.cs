using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public static float ufoSpeed;
    public float rotateSpeed;
    public AudioSource explosion;

    private Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        ufoSpeed = 1;
        explosion = GetComponent<AudioSource>();
        GetComponent<AudioSource>().playOnAwake = false;
      
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(0,rotateSpeed*Time.deltaTime,0); //rotate UFO constantly

    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(-1.0f, 0.0f, 0.0f);
        rb.velocity = movement * ufoSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
        
        if(other.gameObject.CompareTag("Player"))
        {                      
                explosion.Play();
                GetComponentInChildren<Renderer>().enabled = false;
                Destroy(gameObject, explosion.clip.length);
            
        }
    }
}
