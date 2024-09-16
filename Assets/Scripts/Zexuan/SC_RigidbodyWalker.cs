using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(SphereCollider))]

public class SC_RigidbodyWalker : MonoBehaviour
{
    private Animator animator;
    public float speed = 15.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    public Camera playerCamera;
    public float rotationSpeed = 2.0f;
    bool grounded = false;
    Rigidbody r;
    Vector2 rotation = Vector2.zero;
    float maxVelocityChange = 10.0f;

    void Awake()
    {
        r = GetComponent<Rigidbody>();
        r.freezeRotation = true;
        r.useGravity = false;
        r.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rotation.y = transform.eulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        animator = GetComponent<Animator>();
    }

    void Update()
    {

        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }

        float rotationAmount = horizontalInput * rotationSpeed;
        Quaternion currentRotation = transform.rotation;
        Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
        transform.rotation = currentRotation * deltaRotation;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            animator.SetFloat("Speed", r.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            //Direction of movement
            Vector3 forwardDir = transform.forward;
            
            //Target velocity, only moving forward
            Vector3 targetVelocity = forwardDir * Mathf.Max(0, Input.GetAxis("Vertical")) * speed;
            
            Vector3 velocity = r.velocity;
            velocity.y = 0;
           
            Vector3 velocityChange = targetVelocity - velocity;
            velocityChange.y = 0; 
            
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

            r.AddForce(velocityChange, ForceMode.VelocityChange);
            animator.SetFloat("Speed", velocityChange.magnitude);

            if (Input.GetButton("Jump") && canJump)
            {
                r.AddForce(transform.up * jumpHeight, ForceMode.VelocityChange);
            }
        }

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }


}
