using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {
    public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	private bool grounded = false;

	//ROTATION
	public float speedH = 2.0f;
	private float xRotV;
	private float yaw = 0.0f;
	private float x;
	public float smthD = 0.1f;

	private Animator anim;




	void Awake () {
	    GetComponent<Rigidbody>().freezeRotation = true;
	    GetComponent<Rigidbody>().useGravity = false;
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate () {
	    if (grounded) {
	        // Calculate how fast we should be moving
	        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			if (Input.GetButton ("Vertical") && Input.GetAxis ("Vertical") > 0) {
				anim.SetBool ("MoveForward", true);
			} else
				anim.SetBool ("MoveForward", false);
	        targetVelocity = transform.TransformDirection(targetVelocity);
	        targetVelocity *= speed;

	        // Apply a force that attempts to reach our target velocity
	        Vector3 velocity = GetComponent<Rigidbody>().velocity;
	        Vector3 velocityChange = (targetVelocity - velocity);
	        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
	        velocityChange.y = 0;
	        GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

	        // Jump
	        if (canJump && Input.GetButton("Jump")) {
	            GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
	        }
				
	    }
		if (Input.GetMouseButton(0)) {
			anim.SetBool ("Fire", true);

		} else
			anim.SetBool ("Fire", false);

		if (Input.GetMouseButtonDown(1)) {
			anim.SetBool ("Aim", !anim.GetBool("Aim"));

		}

	    // We apply gravity manually for more tuning control
	    GetComponent<Rigidbody>().AddForce(new Vector3 (0, -gravity * GetComponent<Rigidbody>().mass, 0));

	    grounded = false;


	}

	void OnCollisionStay () {
	    grounded = true;
	}

	float CalculateJumpVerticalSpeed () {
	    // From the jump height and gravity we deduce the upwards speed
	    // for the character to reach at the apex.
	    return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

	void Update (){
		yaw += speedH * Input.GetAxis("Mouse X");
		x = Mathf.SmoothDamp(x, yaw, ref xRotV, smthD);
		transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);



	}
}
