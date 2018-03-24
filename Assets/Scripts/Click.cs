using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour 
{

	public bool clickedIs = false;

	void OnMouseDown()
	{
		clickedIs = true;
	}

	void OnMouseUp()
	{
		clickedIs = false;	
	}

}
