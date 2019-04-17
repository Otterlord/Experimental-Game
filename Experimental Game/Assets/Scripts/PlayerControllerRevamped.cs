using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRevamped : MonoBehaviour
{

    public float maxSpd = 60;
    public float rotSpd;
    public float acceleration;
    public float decceleration;
    public float jumpPower = 80;
    public float stickingPower = 20;
    public float gravity = 20;
    public float minDistance;

    private Vector3 velocity;
    private Vector3 airVelocity = Vector3.zero;

    private Vector3 inputLast;

    public LayerMask mask;

    private Rigidbody rb;

    private bool jumping;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        //bool check = Physics.SphereCast(transform.position, 1, Vector3.down, out hit, 2, mask);
        bool check = Physics.Raycast(ray, out hit, 3, mask);
        Debug.DrawRay(transform.position, -transform.up, Color.red);
        
        //transform.up = Vector3.Lerp(transform.up, hit.normal, rotSpd * Time.deltaTime);
        transform.up = hit.normal;
        //if (check) Physics.gravity = -hit.normal *  40;
        
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (input != Vector3.zero) inputLast = input;
        velocity = Vector3.Lerp(velocity, input * maxSpd, acceleration * Time.deltaTime);
        if (input.magnitude == 0) velocity = Vector3.Lerp(velocity, input * maxSpd, decceleration * Time.deltaTime);

        // TODO: Add smooth rotation in direction of movement


        if (jumping && !check) jumping = false;
        if (check && Input.GetButtonDown("Jump"))
        {
            airVelocity = hit.normal * jumpPower;
            jumping = true;
        }
        if (check && !jumping && hit.distance <= minDistance) airVelocity = transform.rotation * Vector3.zero;
        if (!check || (check && hit.distance > minDistance)) airVelocity += Vector3.down * gravity;

        print(hit.distance);
        rb.velocity = (transform.rotation * velocity) + airVelocity;
        //rb.MovePosition((transform.rotation * velocity) + airVelocity);
        if (check && !jumping && hit.distance <= minDistance) rb.MovePosition(hit.point + (hit.normal)); // snap to ground
    }
}
