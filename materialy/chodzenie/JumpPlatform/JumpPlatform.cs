using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    Animator anim;
    public GameObject jumpDirection;
    public float jumpForce = 3.0f;
    public AudioClip JumpAudioClip;
    AudioSource aSource;
    void Start()
    {
        anim = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
    }
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        anim.Play("jump");
        aSource.clip = JumpAudioClip;
        aSource.Play();
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().ctrl.Move(new Vector3(0, 0, 0));
            other.GetComponent<Player>().Jump((jumpDirection.transform.position - transform.position) * jumpForce);
        }
        else
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce((jumpDirection.transform.position - transform.position) * jumpForce, ForceMode.Impulse);
        }
    }
}
