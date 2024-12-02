using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float walkSpeed = 12f;    
    public float runSpeed = 18f;     
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;

    private bool isGrounded;
    private bool isRunning;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Kiểm tra xem nhân vật có đang chạm đất hay không
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        
        isRunning = Input.GetKey(KeyCode.LeftShift);

        
        float speed = isRunning ? runSpeed : walkSpeed;

        
        Vector3 move = transform.right * x + transform.forward * z;

        
        controller.Move(move * speed * Time.deltaTime);

       
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Tính toán lực rơi (gravity)
        velocity.y += gravity * Time.deltaTime;

        // Áp dụng lực rơi cho nhân vật
        controller.Move(velocity * Time.deltaTime);
    }
}
