using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody rb;

    private CapsuleCollider col;
    [SerializeField]private LayerMask groundLayers;
    private float speed;
    [SerializeField] private Transform camera;
    [SerializeField]private float walkSpeed, sprintSpeed, jumpSpeed, turnSmoothTime;
    Vector3 movement, direction;
    private float turnSmoothVelocity;
    // Start is called before the first frame update    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

    }

    void Update(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        if(Input.GetKey(KeyCode.Space) && IsGrounded()){
            rb.AddForce(Vector3.up*jumpSpeed, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            speed = sprintSpeed;
        }else{
            speed = walkSpeed;
        }
        if(direction.magnitude >= 0.1f){
            rb.AddForce((movement.normalized*speed)- new Vector3(rb.velocity.x, 0f, rb.velocity.z), ForceMode.Force);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            rb.rotation = Quaternion.Euler(0f,angle,0f);

            movement = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;
        }
        //rb.AddForce((direction.normalized*speed) - new Vector3(rb.velocity.x, 0f, rb.velocity.z), ForceMode.Force);
    }

    private bool IsGrounded(){
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,col.bounds.min.y, col.bounds.center.z), col.radius *0.9f, groundLayers);
    }
}
