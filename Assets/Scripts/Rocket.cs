﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour 
{

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip win;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] Sprite Star;
	[SerializeField] Sprite EmptyStar;

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;


    public Click[] Control;

    
    Vector3 pos;


	public GameObject WinMenuUI;
	public GameObject LoseMenuUI;
    public GameObject Settings;
    public GameObject ControlFly;
   

    public int coinsInt = 0;
    

    public Text coinsText;    
    public float mainThrust;
	public float rcsThrust;
   

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
        coinsText.text = coinsInt.ToString();

		if (state == State.Alive)
		{
            Thrust();
		} 
    }
    
    

    public void OnTriggerEnter2D(Collider2D pickUp)
    {   
       int scorernd = new System.Random().Next(350,500); 
        switch(pickUp.gameObject.tag)
        {
            case "Coins":
                Destroy(pickUp.gameObject);
                coinsInt += scorernd;
            break;
        }
    }
    public void CurrentStarValue()
        {
            if(coinsInt <= 500)
            {
               Star1.GetComponent<Image>().sprite = Star;
			   Star1.GetComponent<Button>().interactable = false;
               Star2.GetComponent<Image>().sprite = EmptyStar;
			   Star2.GetComponent<Button>().interactable = false;
               Star3.GetComponent<Image>().sprite = EmptyStar;
			   Star3.GetComponent<Button>().interactable = false;
            }
            else if(coinsInt <= 1000 && coinsInt > 500)
            {
               Star1.GetComponent<Image>().sprite = Star;
			   Star1.GetComponent<Button>().interactable = false;
               Star2.GetComponent<Image>().sprite = Star;
			   Star2.GetComponent<Button>().interactable = false;
               Star3.GetComponent<Image>().sprite = EmptyStar;
			   Star3.GetComponent<Button>().interactable = false;
            }
            else if(coinsInt <= 1500 && coinsInt > 1000)
            {
               Star1.GetComponent<Image>().sprite = Star;
			   Star1.GetComponent<Button>().interactable = false;
               Star2.GetComponent<Image>().sprite = Star;
			   Star2.GetComponent<Button>().interactable = false;
               Star3.GetComponent<Image>().sprite = Star;
			   Star3.GetComponent<Button>().interactable = false;
            }
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
                break;
	    	case "Finish":
                StartSuccessSequence();
                NextLevel();
                CurrentStarValue();
                rigidBody.freezeRotation = false;
                break;
            default:
                StartDeathSequence();
                rigidBody.freezeRotation = false;
                break;
        }
    }

    public void StartSuccessSequence()
    {
        if(state != State.Dying)
        {
            state = State.Transcending;
            mainEngineParticles.Stop();
            successParticles.Play();
            audioSource.Stop();
            audioSource.PlayOneShot(win);
            WinMenuUI.SetActive (true);
            Time.timeScale = 0.5f;
            Settings.SetActive (false);
            ControlFly.SetActive (false);
        }
       
     }

    public void StartDeathSequence()
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
            Settings.SetActive (false);
            ControlFly.SetActive (false);
        }  
    }

	public void ToMainMenu()
    {
		SceneManager.LoadScene(0);
		Time.timeScale = 1f;
	}
    public void NextLevel()
    {
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (LevelManager.countUnlockedLevel == currentSceneIndex) 
        {
			LevelManager.countUnlockedLevel += 1;
		}
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

    public void Thrust()
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

    
	
	public void ApplyThrust()
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


    public void RotateLeft()
    {
        rigidBody.freezeRotation = true;
        float rotateThisFrame = rcsThrust * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotateThisFrame);
    }

    public void RotateRight()
    {
        rigidBody.freezeRotation = true;
        float rotateThisFrame = rcsThrust * Time.deltaTime;
        transform.Rotate(-Vector3.forward * rotateThisFrame);
    }

}