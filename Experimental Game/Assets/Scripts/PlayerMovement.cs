using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float gravity;
    public float minGravity;
    public float jumpForce;
    public LayerMask ground;
    private Vector3 velocity;
    private Vector3 verticalVelocity;

    private Vector3 input;

    public float speed;
    public float airControlAmount;
    public float airSpeed;
    public float airDrag;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // raycast
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -Vector3.up);
        bool check = Physics.Raycast(ray, out hit, 3, ground);

        //transform.up = hit.normal;
        
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = input.normalized;
        if (input != velocity.normalized) print("yeeha");
        else print("nope");

        if (controller.isGrounded)
        {
            velocity = input * speed;

            if (Input.GetButtonDown("Jump")) verticalVelocity = jumpForce * Vector3.up;
            else verticalVelocity = minGravity * Vector3.down;
        }
        else
        {
            if (input != velocity.normalized) velocity += input * airControlAmount * Time.deltaTime;
            else velocity = input * airSpeed;
            velocity *= airDrag;
            verticalVelocity += gravity * Vector3.down * Time.deltaTime;
        }
            

        Vector3 relative = ((Quaternion.FromToRotation(Vector3.up, hit.normal) * velocity) + verticalVelocity) * Time.deltaTime;
        relative = transform.TransformDirection(relative);
        controller.Move(relative);
    }
}
