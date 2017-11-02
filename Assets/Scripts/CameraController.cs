using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public enum CameraModes
	{
		FirstPerson,
		Ghost,
		TopDown
	}
	// Use this for initialization
	public float 		moveSpeed = 5;
	public float 		rotationSpeedX = 5;
	public float 		rotationSpeedY = 5;
	public float minimumY = -60f;
	public float maximumY = 60f;

	public GameObject 	cameraObject;
	public CameraModes	cameraMode;
	private Rigidbody 	myRB;
	private float rotationY = 0;

	void Start () 
	{
		myRB = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float vertical = Input.GetAxisRaw ("Vertical");
		float horizontal = Input.GetAxisRaw ("Horizontal");


		float rotationX = transform.rotation.eulerAngles.y + (Input.GetAxis("Mouse X") * rotationSpeedX) ;

		//transform.Rotate (Quaternion.Euler (0, rotationX, 0));

		switch (cameraMode)
		{
		case CameraModes.FirstPerson :
			{
				rotationY += (Input.GetAxis("Mouse Y") * rotationSpeedY) ;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				cameraObject.transform.localEulerAngles = new Vector3(-rotationY,0 , 0);
				transform.localEulerAngles = new Vector3(0, rotationX, 0);
				myRB.MovePosition (transform.position + (transform.forward * moveSpeed * vertical * Time.deltaTime) + (transform.right * moveSpeed * horizontal * Time.deltaTime));

				break;
			}
		case CameraModes.Ghost :
			{
				float elevation = Input.GetAxisRaw ("Elevation");



				myRB.useGravity = false;
				rotationY += (Input.GetAxis("Mouse Y") * rotationSpeedY) ;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				cameraObject.transform.localEulerAngles = new Vector3(0, 0, 0);
				transform.localEulerAngles = new Vector3(-rotationY, rotationX ,0); 
				myRB.MovePosition (transform.position + (transform.forward * moveSpeed * vertical * Time.deltaTime) + (transform.right * moveSpeed * horizontal * Time.deltaTime) 
					+ (Vector3.up * moveSpeed * elevation * Time.deltaTime));

				break;
			}
			default :
			break;
		}
		//float rotationY = transform.rotation.eulerAngles.x;

	}
}
