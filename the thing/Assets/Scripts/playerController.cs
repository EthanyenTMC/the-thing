using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller;
    private Rigidbody rb;

    private Vector3 movement;
    [SerializeField] private float walkSpeed, sprintSpeed, speed;
    private bool grounded;
    private Vector3 playerVelocity, direction, vertical;

    [SerializeField] private Transform camera;
    private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        controller = gameObject.GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!controller.isGrounded){
            Debug.Log("graviting");
            vertical += new Vector3(0, worldController.getGravity(),0);
            //controller.Move(new Vector3(0,worldController.getGravity(),0));
        }
        playerVelocity.x = Input.GetAxis("Horizontal") * speed;
        playerVelocity.z = Input.GetAxis("Vertical") * speed;  

        if(Input.GetKey(KeyCode.LeftShift)){
            speed = sprintSpeed;
        }else{
            speed = walkSpeed;
        }
        if(Input.GetKey(KeyCode.Space) && controller.isGrounded){
            //playerVelocity.y += 0.1f;
            vertical += new Vector3(0, 1,0);
        }


        direction = new Vector3(playerVelocity.x, 0 ,playerVelocity.z).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        

        movement = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;
        if(direction.magnitude >= 0.1f){
            controller.transform.rotation = Quaternion.Euler(0f,targetAngle,0f);
            controller.Move(movement*speed*Time.deltaTime + vertical);
        }
        
        /*if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            controller.transform.rotation = Quaternion.Euler(0f,targetAngle,0f);

            movement = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;
            controller.Move(movement*speed*Time.deltaTime);
        }*/

        

    }
}
