using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	[SerializeField] float mainThrust = 100f;
	[SerializeField] float rcsThrust = 100f;
	[SerializeField] AudioClip mainEngine;
	// [SerializeField] ParticleSystem mainEngineParticles;
	
	AudioSource audioSource;
	Rigidbody2D rigidBody;

	enum State { Alive, Dying, Transcending };
    State state = State.Alive;

	// Use this for initialization
	void Start () 
	{
		rigidBody = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (state == State.Alive)
		{
		Thrust();
		Rotate();
		}
		
	}

	 void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
			case "Friendly":
            // do nothing
                break;
            case "Respawn":
			print("dasdas");
                break;
            case "Finish":
                //StartSuccessSequence();
                break;
            default:
                //StartDeathSequence();
                break;
        }
    }


	private void Thrust()
	{

		if (Input.GetKey(KeyCode.Space))
		{
			rigidBody.AddRelativeForce(Vector3.up * mainThrust);
			if (!audioSource.isPlaying)
        	{
            	audioSource.PlayOneShot(mainEngine);
         	}
			// mainEngineParticles.Play();
		}
		else
        {
            audioSource.Stop();
			// mainEngineParticles.Stop();
        }
	}
	

	private void Rotate()
	{
		rigidBody.freezeRotation = true;    // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
		rigidBody.freezeRotation = false;
	}


}
