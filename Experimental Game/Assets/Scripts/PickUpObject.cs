using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public Transform hand;
    public float distance;
    public float offset;
    public LayerMask grabbable;

    public float minHandHeight;
    public float maxHandHeight;

    private GameObject heldObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && heldObject == null)
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward, out hit, distance, grabbable);
            if (hit.collider == null) return;
            heldObject = hit.collider.gameObject;
            
            heldObject.GetComponent<Rigidbody>().useGravity = false;
            heldObject.transform.rotation = Quaternion.Euler(Vector3.zero); 
            heldObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
        else if (Input.GetKeyDown(KeyCode.E) && heldObject != null && heldObject.GetComponent<Rigidbody>() != null)
        {
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            heldObject.GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z);
            Destroy(heldObject.GetComponent<FixedJoint>());
            heldObject = null;
        }
        if (heldObject != null && heldObject.GetComponent<Rigidbody>() != null)
        {
            //heldObject.GetComponent<Rigidbody>().MovePosition(Camera.main.ScreenToWorldPoint(Vector2.zero) + offset * Camera.main.transform.forward);
            heldObject.transform.position = Camera.main.ScreenToWorldPoint(Vector2.zero) + offset * Camera.main.transform.forward;
            heldObject.transform.rotation = Quaternion.Euler(heldObject.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, heldObject.transform.eulerAngles.z);
        }
            
    }
}
