using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public bool isAlive = true;
	public bool armed = false;
	public float turret_damage = 5;
	
	public GameObject Spine;
	public GameObject Player;
	public GameObject Laser;
	Animator anim;
	
	public AudioSource speak_aSource;
	public List<AudioClip> turret_shoot_list = new List<AudioClip>();
	public AudioClip activated;
	public AudioClip deploying;
	public AudioClip there_you_are;
	public AudioClip i_see_you;
	public AudioClip anyone_there;
	public AudioClip searching;
	public AudioClip sentry_mode_activated;
	public AudioClip shutting_down;
	
    void Start()
    {
        Player = GameObject.Find("Player Camera");
		anim = GetComponent<Animator>();
		speak_aSource = this.gameObject.AddComponent<AudioSource>();
		speak_aSource.spatialBlend = 1;
    }

    // Update is called once per frame
    void Update()
    {
		if (isAlive)
		{
			SightCheck();
			
			if (armed)
			{
				Quaternion spineRotation = Quaternion.LookRotation(Player.transform.position - Spine.transform.position);
				Spine.transform.rotation = Quaternion.Slerp(Spine.transform.rotation, spineRotation, 5.0f * Time.deltaTime);
			}
			else
			{
				Spine.transform.rotation = Quaternion.Slerp(Spine.transform.rotation, Quaternion.Euler(0, 0, 0), 1.0f * Time.deltaTime);
			}
		}
    }
	
	void SightCheck() 
	{
		RaycastHit hit;
		Debug.DrawRay(Laser.transform.position, (Player.transform.position - Laser.transform.position), Color.green);
		if (Physics.Raycast(Laser.transform.position, (Player.transform.position - Laser.transform.position), out hit, Mathf.Infinity))
		{
			if (hit.transform.name == "Player")
			{
				if (!anim.GetBool("armed"))
				{
					anim.SetBool("armed", true);
					Speak(activated, there_you_are, i_see_you, there_you_are);
				}
			}
			else
			{
				if (anim.GetBool("armed"))
				{
					anim.SetBool("armed", false);
					anim.SetBool("shoot", false);
					Speak(anyone_there, searching, sentry_mode_activated);
				}
			}
		}
	}
	
	public void Speak(params AudioClip[] audio_clip_list)
	{
		int clipIndex = Random.Range(0, audio_clip_list.Length);
		AudioClip clip = audio_clip_list[clipIndex];
		speak_aSource.clip = clip;
		speak_aSource.Play();
	}
	
	public void SetArmedState(int state) 
	{
		if (state == 1)
		{
			armed = true;
		}
		else
		{
			armed = false;
		}
	}
}
