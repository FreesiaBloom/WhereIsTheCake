using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    AudioSource aSource;
    Animator anim;
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void NextLevel()
    {
        Debug.Log("Lecimy do kolejnej planszy!");
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
