using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CurrentStar : MonoBehaviour 
{
    [SerializeField]
	Sprite Star;

	[SerializeField]
	Sprite EmptyStar;

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    
    public int coinsValue = 0;
    public void CurrentStarValue()
        {
            gameObject.GetComponent<Rocket>().coinsInt = coinsValue;

            if(coinsValue <= 500)
            {
               Star1.GetComponent<Image>().sprite = Star;
			   Star1.GetComponent<Button>().interactable = false;
               Star2.GetComponent<Image>().sprite = EmptyStar;
			   Star2.GetComponent<Button>().interactable = false;
               Star3.GetComponent<Image>().sprite = EmptyStar;
			   Star3.GetComponent<Button>().interactable = false;
            }
            else if(coinsValue <= 1000 && coinsValue > 500)
            {
               Star1.GetComponent<Image>().sprite = Star;
			   Star1.GetComponent<Button>().interactable = false;
               Star2.GetComponent<Image>().sprite = Star;
			   Star2.GetComponent<Button>().interactable = false;
               Star3.GetComponent<Image>().sprite = EmptyStar;
			   Star3.GetComponent<Button>().interactable = false;
            }
            else if(coinsValue <= 1500 && coinsValue > 1000)
            {
               Star1.GetComponent<Image>().sprite = Star;
			   Star1.GetComponent<Button>().interactable = false;
               Star2.GetComponent<Image>().sprite = Star;
			   Star2.GetComponent<Button>().interactable = false;
               Star3.GetComponent<Image>().sprite = Star;
			   Star3.GetComponent<Button>().interactable = false;
            }
        }
}