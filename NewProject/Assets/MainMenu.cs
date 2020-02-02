using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenuGroup;
	public GameObject MainMenuGroup;
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void ShowOptions()
	{
		MainMenuGroup.SetActive(false);
		OptionsMenuGroup.SetActive(true);
	}
	
	public void HideOptions()
	{
		MainMenuGroup.SetActive(true);
		OptionsMenuGroup.SetActive(false);
	}
	
	public void SetVolume(Slider slider)
	{
		AudioListener.volume = slider.value;
		Globals.volume = slider.value;
	}
	
	public void Play()
	{
		SceneManager.LoadScene("Level_1");
	}
	
	public void Exit()
	{
		Application.Quit();
	}
}
