﻿using UnityEngine;
// Include the namespace required to use Unity UI
using UnityEngine.UI;
using UnityEngine.Playables;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	
	public Text winText;
    Vector3 originalPos;
    public PlayableDirector timeline;
    public AudioSource playerAudio;
    public AudioClip impactSound;
    public AudioClip triumphsound;
    private bool hasPlayedTriumph;
    public AudioClip backgroundAudio;
    public AudioClip bounce;
    public AudioClip pauseSound;
    public AudioClip winSound;

    public GameObject winCanvas;
    public GameObject pauseCanvas;
    public GameObject gameUIcanvas;


    
   
 
 

	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;
	

    void Awake()
    {
        originalPos = gameObject.transform.position;
        winCanvas.SetActive(false);
        gameUIcanvas.SetActive(true);
        pauseCanvas.SetActive(true);
    }

    // At the start of the game..
    void Start ()
	{
        hasPlayedTriumph = false;
        BackgroundAudio();
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		

		// Run the SetCountText function to update the UI (see below)
		

		// Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
		//winText.gameObject.SetActive(false);
        winText.text = "";
	}

	// Each physics step..
	void FixedUpdate ()
	{
        
		/*// Set some local float variables equal to the value of our Horizontal and Vertical Inputs
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		// Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
		// multiplying it by 'speed' - our public player speed that appears in the inspector
		rb.AddForce (movement * speed);*/
	}

	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
		
        if (other.gameObject.tag == "Reset" || other.gameObject.CompareTag("Pick Up"))
        {
            
            rb.velocity=Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            print("raawas");
            gameObject.transform.position = originalPos;
            StopTriumph();
            PlayImpact();
            timeline.Stop();
            Start();

        }
        else if(other.gameObject.tag=="WinTrigger")
        {
            if (hasPlayedTriumph == false)
            {
                playTriumph();
                print("triggerwotrks");
                timeline.Play();
                hasPlayedTriumph = true;
            }
            
        }
        else if(other.gameObject.tag=="Win")
        {
            print("daalbhaat");
            //winText.gameObject.SetActive(true);
            //winText.text = "";
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            winCanvas.SetActive(true);
            playerAudio.Stop();
            playerAudio.PlayOneShot(winSound, 0.5f);
            gameUIcanvas.SetActive(false);
            pauseCanvas.SetActive(false);

        }
        else if(other.gameObject.tag == "bounce")
        {
           if (rb.velocity.y < 0.001)
                {
                    print(Mathf.Abs(rb.velocity.magnitude));
                    float intensity = (Mathf.Abs(rb.velocity.magnitude) * 0.1f);
                    playerAudio.PlayOneShot(bounce,intensity);
                }
          
        }
       
    }

	/*// Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
	void SetCountText()
	{
		// Update the text field of our 'countText' variable
		countText.text = "Count: " + count.ToString ();

		// Check if our 'count' is equal to or exceeded 12
		if (count >= 12) 
		{
			// Set the text value of our 'winText'
			winText.text = "You Win!";
		}
	}*/

    public void MovementLeft()
    {
       print("Left");
        rb.AddForce((Vector3.left * speed));

    }
    public void MovementRight()
    {
        print("Right");
        rb.AddForce(Vector3.right * speed);

    }
    public void MovementUp()
    {
        print("Up");
        rb.AddForce(Vector3.forward * speed);

    }
    public void MovementDown()
    {
        print("Down");
        rb.AddForce(Vector3.back * speed);

    }
    public void MovementJump()
    {
        print("Jump");
        rb.AddForce(Vector3.up * 300);

    }

    public void PlayImpact()
    {
        playerAudio.PlayOneShot(impactSound);
    }

    public void playTriumph()
    {
        playerAudio.PlayOneShot(triumphsound);
    }
    public void StopTriumph()
    {
        playerAudio.Stop();
    }

    public void BackgroundAudio()
    {
        playerAudio.PlayOneShot(backgroundAudio,0.15f);
    }

    public void PlayPauseSound()
    {
        playerAudio.PlayOneShot(pauseSound,0.15f);
    }

}