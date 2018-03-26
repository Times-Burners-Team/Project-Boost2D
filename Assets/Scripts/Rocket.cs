using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour 
{

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip win;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;

    public Click[] Control;

    Vector3 pos;

	public GameObject WinMenuUI;
	public GameObject LoseMenuUI;



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
    

	 public void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
			case "Friendly":
                rigidBody.freezeRotation = false;
                break;
            case "Respawn":
                rigidBody.freezeRotation = false;
                print("dasdas");
                break;
	    	case "Finish":
                StartSuccessSequence ();
                NextLevel ();
                rigidBody.freezeRotation = false;
                break;
            default:
                StartDeathSequence();
                rigidBody.freezeRotation = false;
                break;
        }
    }

	public void NextLevel()
    {
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (LevelManager.countUnlockedLevel == currentSceneIndex) 
        {
			LevelManager.countUnlockedLevel += 1;
		}
	}


    private void StartSuccessSequence()
    {
        if(state != State.Dying)
        {
            state = State.Transcending;
            mainEngineParticles.Stop();
            audioSource.Stop();
            successParticles.Play();
            audioSource.PlayOneShot(win);
            WinMenuUI.SetActive (true);
            Time.timeScale = 0.5f;
        }
     }

    private void StartDeathSequence()
    {
        if(state != State.Transcending)
        {
            state = State.Dying;
            mainEngineParticles.Stop();
            audioSource.Stop();
            audioSource.PlayOneShot(death);
            deathParticles.Play();
            LoseMenuUI.SetActive (true);
            Time.timeScale = 0.5f;
        } 
    }

	public void ToMainMenu()
    {
		SceneManager.LoadScene(0);
		Time.timeScale = 1f;
	}

	public void prevLevel()
    {
		LoadPrevScene();
	}

	public void LoadCurrentLevel()
    {
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex);
		Time.timeScale = 1f;

	}

	public void LoadPrevScene()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int prevScenceIndex = currentSceneIndex - 1;
		if (prevScenceIndex == SceneManager.sceneCountInBuildSettings)
		{
			prevScenceIndex = 0; // loop back to start
		}
		SceneManager.LoadScene(prevScenceIndex);
		Time.timeScale = 1f;
	}

	public void LoadNextScene()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextScenceIndex = currentSceneIndex + 1;
		if (nextScenceIndex == SceneManager.sceneCountInBuildSettings)
		{
			nextScenceIndex = 0; // loop back to start
		}
		SceneManager.LoadScene(nextScenceIndex);
		Time.timeScale = 1f;
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