using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour

{
    float rotX;
    float rotY;

    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;
    public int jumpFactor = 4;
    public float speed = 3.2f;
    Vector3 movement;
    public CharacterController ctrl;

    float ActionDistance = 3.0f;
    public bool grabbingSomething = false;
    public GameObject hand;
    public GameObject grabbedObject;
	
	public Image Fader;
	public float health = 100.0f;
	public bool isAlive = true;
	public Camera PlayerCamera;
	Rigidbody body;
	
	AudioSource aSource;
	Vector3 old_position;
	public float step_size=1.2f;
	public List<AudioClip> setpAudioClipList = new List<AudioClip>();
	public List<AudioClip> jumpAudioClipList = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        ctrl = GetComponent<CharacterController>();
		body = GetComponent<Rigidbody>();
		aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
	
    void Update()
    {
		if (isAlive) 
		{
			MouseMovement();
			KeyboardMovement();
			Use();
		}
    }

    public void MouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotX += mouseY * mouseSensitivity * Time.deltaTime;
        rotY += mouseX * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
    }

    public void KeyboardMovement()
    {
        if (!ctrl.isGrounded)
        {
            movement.y += Physics.gravity.y * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && ctrl.isGrounded)
        {
            Jump(new Vector3(movement.x, jumpFactor, movement.z));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 8.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 3.2f;
        }
        if (ctrl.isGrounded)
        {
            movement.z = Input.GetAxis("Vertical") * speed * transform.forward.z - Input.GetAxis("Horizontal") * speed * transform.forward.x;
            movement.x = Input.GetAxis("Vertical") * speed * transform.forward.x + Input.GetAxis("Horizontal") * speed * transform.forward.z;
			
			Steps();
        }

        ctrl.Move(movement * Time.deltaTime);
    }

    public void Jump(Vector3 JumpDirection)
    {
        movement = JumpDirection;
    }
	
    public void Use()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * ActionDistance, Color.red);
            if (grabbingSomething == false)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, ActionDistance))
                {
                    if (hit.transform.gameObject.GetComponent<Grippable>())
                    {
                        grabbedObject = hit.transform.gameObject;
                        grabbingSomething = true;
                        grabbedObject.BroadcastMessage("Use", hand, SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
						if (!hit.transform.gameObject.GetComponent<Button_Big>())
						{
							hit.transform.gameObject.BroadcastMessage("Use", null, SendMessageOptions.DontRequireReceiver);
						}
                        
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (grabbingSomething == true)
            {
                grabbedObject.BroadcastMessage("Use", hand, SendMessageOptions.DontRequireReceiver);
                grabbingSomething = false;
                grabbedObject = null;
            }
        }
    }
	
	public void Kill() 
	{
		isAlive = false;
		body.isKinematic = false;
		body.drag = 3;
		body.angularDrag = 3;
		body.useGravity = true;
		GetComponent<SphereCollider>().isTrigger = false;
		Debug.Log("You lose!");
		this.GetComponentInChildren<Gun>().enabled = false;
		Fader.GetComponent<Animator>().Play("fader_black");
	}
	
	public void TakeDamage(float damage) 
	{
		if (isAlive)
		{
			health -= damage;
			Debug.Log("Health: " + health);
			if (health <= 0)
			{
				Kill();
			}
			PlayerCamera.GetComponent<Animator>().Play("camera_hit");
			Fader.GetComponent<Animator>().Play("fader_red");
		}
	}
	
	public void PlaySound(List<AudioClip> lista) 
	{
		aSource.clip = lista[UnityEngine.Random.Range(0, lista.Count)];
		aSource.Play();
	}
	
	public void Steps()
	{
		if (Vector3.Distance(transform.position, old_position) > step_size)
		{
			PlaySound(setpAudioClipList);
			old_position = transform.position;
		}
	}
}
