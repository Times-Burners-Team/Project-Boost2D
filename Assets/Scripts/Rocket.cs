using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		    //Rotate();
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
				StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

     private void StartSuccessSequence()
     {
        state = State.Transcending;
        mainEngineParticles.Stop();
        audioSource.Stop();
        audioSource.PlayOneShot(death);
		LoadNextScene ();
     }

    private void StartDeathSequence()
    {
        state = State.Dying;
        mainEngineParticles.Stop();
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        //Invoke("LoadLastLevel", 2f);
    }

	private void LoadNextScene()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextScenceIndex = currentSceneIndex + 1;
		if (nextScenceIndex == SceneManager.sceneCountInBuildSettings)
		{
			nextScenceIndex = 0; // loop back to start
		}
		SceneManager.LoadScene(nextScenceIndex);
	}

    private void Thrust()
	{
        float rotationThisFrame = rcsThrust * Time.deltaTime;
		if (Control[0].clickedIs == true && Control[1].clickedIs == false)    
        {
            ApplyThrust();
            RotateLeft();
        }
        else if (Control[1].clickedIs == true && Control[0].clickedIs == false)
        {
            ApplyThrust();
            RotateRight();
        }
        else if (Control[0].clickedIs == true && Control[1].clickedIs == true)
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


    private void RotateLeft()
    {
        rigidBody.freezeRotation = true;
        float rotateThisFrame = rcsThrust * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotateThisFrame);
    }

    private void RotateRight()
    {
        rigidBody.freezeRotation = true;
        float rotateThisFrame = rcsThrust * Time.deltaTime;
        transform.Rotate(-Vector3.forward * rotateThisFrame);
    }

}