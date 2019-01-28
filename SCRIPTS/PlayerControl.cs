using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float MoveSpeed = 10f;
	public float RotateSpeed = 40f;

	private CharacterController controller;

	public Animation anim;

	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		Move();

		if (controller.velocity.magnitude > 0)
		{
			anim.Play("runneutral");
		}
		else
		{
			anim.Play("idle");
		}
	}

	void Move()
	{
		float movementX = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
		float movementZ = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;

		Vector3 forwardMovement = transform.forward * movementZ;
		Vector3 sideMovement = transform.right * movementX;

		controller.Move(forwardMovement + sideMovement);

	}
}
