using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    AudioSource aSource;
    Animator anim;
	public string nextLevelName;
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
    public void Close()
    {
        if (anim.GetBool("isOpen") == true )
        {
            anim.SetBool("isOpen", false);
            aSource.Play();
        }
    }
}
