using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController cc;

    public float gravity;
    public float minGravity;
    public float jumpForce;
    public LayerMask ground;
    private Vector3 velocity;
    private Vector3 verticalVelocity;
    public Vector3 offset;

    private Vector3 input;
    private Vector3 previousNormal = Vector3.up;

    public float speed;
    public float airControlAmount;
    public float airDrag;
    public float coyoteTime = 0.5f;

    public Vector3 ceilingRayOffset;
    public float ceilingDist;

    private float rememberTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Reset()
    {
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // raycast
        RaycastHit hit;
        RaycastHit ceilingInfo;
        Ray ray = new Ray(transform.position + offset, Vector3.down);
        Debug.DrawLine(transform.position + offset, transform.position + offset + Vector3.down * 2);
        bool check = Physics.Raycast(ray, out hit, 2, ground);
        bool closeToCeiling = Physics.Raycast(transform.position + ceilingRayOffset, Vector3.up, out ceilingInfo, ceilingDist, ground);


        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = Vector3.ClampMagnitude(input, 1);

        if (check)
        {
            velocity = input * speed;
            rememberTime = 0;
        }
        else
        {
            velocity += input * airControlAmount * Time.deltaTime;
            //else velocity = input * airSpeed;
            velocity *= airDrag;
            verticalVelocity += gravity * Vector3.down * Time.deltaTime;
            print("new verticalVelocity: " + verticalVelocity);
            rememberTime += Time.deltaTime;
        }
        //if (closeToCeiling && verticalVelocity.normalized == Vector3.up) verticalVelocity = Vector3.zero; // bump head on ceiling

        if (Input.GetButtonDown("Jump") && rememberTime <= coyoteTime)
        {
            verticalVelocity = jumpForce * Vector3.up;
            rememberTime = 1;
        }
        //else if (verticalVelocity.y < 0 && check) verticalVelocity = -hit.normal * minGravity;

        Vector3 relative = ((Quaternion.FromToRotation(Vector3.up, hit.normal) * velocity));
        relative = transform.TransformDirection(relative);

        cc.Move((relative + verticalVelocity) * Time.deltaTime);
        GetComponent<Rigidbody>().velocity = relative + (verticalVelocity * Time.deltaTime);
    }
}
