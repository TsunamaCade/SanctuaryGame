using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    private Vector3 move;

    [SerializeField] private float speed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight;
    

    private Vector3 velocity;
    [SerializeField] private bool isGrounded;

    void Update()
    {
        //Character Movement
        if(cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -20f;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = (transform.right * x + transform.forward * z);

        cc.Move(move * speed * Time.deltaTime);

        //Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);

        if(Input.GetButtonDown("Sprint"))
        {
            speed = runSpeed;
        }
        if(Input.GetButtonUp("Sprint"))
        {
            speed = walkSpeed;
        }
    }
}