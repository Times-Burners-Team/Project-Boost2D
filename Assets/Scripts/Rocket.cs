using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour 
{

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    //[SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;

    public Click[] Control;

    //public GameObject fuelProgressBar;

    public float mainThrust;
	public float rcsThrust;
    //public float fuelSize;
    //public float fuelUsage;
    //private float currentFuel; 

	AudioSource audioSource;
	Rigidbody2D rigidBody;

	enum State { Alive, Dying, Transcending };
    State state = State.Alive;

	// Use this for initialization
	void Start () 
	{
		rigidBody = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
        //currentFuel = fuelSize;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (state == State.Alive)
		{
            Thrust();
		    Rotate();
		} 
    //  else if (currentFuel <= 0)
    //  {
    //      mainEngineParticles.Stop();
    //      audioSource.Stop();
    //   }

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
                //successParticles.Play();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    // private void StartSuccessSequence()
    // {
    //     state = State.Transcending;
    //     mainEngineParticles.Stop();
    //     audioSource.Stop();
    //     audioSource.PlayOneShot(death);
    // }

    private void StartDeathSequence()
    {
        state = State.Dying;
        mainEngineParticles.Stop();
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        //Invoke("LoadLastLevel", 2f);
    }

private void Thrust()
	{

		if (Control[0].clickedIs == true)    // can thrust while rotating
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
	}
	
	private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        //currentFuel -= fuelUsage * Time.deltaTime;
        //print(currentFuel);
        //fuelProgressBar.transform.localScale = new Vector2(currentFuel / fuelSize,1);
        
        if (!audioSource.isPlaying) 
        {
            audioSource.PlayOneShot(mainEngine);
        }
        
        mainEngineParticles.Play();
    }


	private void Rotate()
	{
		rigidBody.freezeRotation = true;    // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Control[1].clickedIs == true)
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }    
        
        if (Control[2].clickedIs == true)
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
		
        rigidBody.freezeRotation = false;
    }

}