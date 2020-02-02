using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMennu : MonoBehaviour
{
    public GameObject Menu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
			Menu.SetActive(!Menu.activeSelf);
			if (Menu.activeSelf)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			else
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
    }
	
	public void Resume()
	{
		Menu.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	public void Exit()
	{
		SceneManager.LoadScene("Menu");
	}
}
