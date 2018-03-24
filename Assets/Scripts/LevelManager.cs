﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public static int countUnlockedLevel = 1;
	int x = countUnlockedLevel;

	[SerializeField]
	Sprite unlockedIcon;

	[SerializeField]
	Sprite lockedIcon;


	void Start () {
		for (int i = 0; i < transform.childCount; i++) 
		{
			#region RenameButtonsAndChangeText
					int numLvl = i+1;
					transform.GetChild(i).gameObject.name = numLvl.ToString();
				    transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text = numLvl.ToString();
			#endregion

			if (i < countUnlockedLevel) 
			{
				#region FirstStateBtn
					transform.GetChild(i).GetComponent<Image>().sprite = unlockedIcon;
					transform.GetChild(i).GetComponent<Button>().interactable = true;
				#endregion
			}
			else
			{
				#region SndStateBtn
					transform.GetChild(i).GetComponent<Image>().sprite = lockedIcon;
					transform.GetChild(i).GetComponent<Button>().interactable = false;
				#endregion
		}

			#region nextLevel
			int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
			if (countUnlockedLevel == currentSceneIndex) {
				
			}
			#endregion
	}
}
}
