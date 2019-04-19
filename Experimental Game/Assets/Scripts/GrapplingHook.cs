using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    // Physics tweaks
    public float maxDistance;
    public float travelSpeed;

    public LayerMask grappleStuff;

    // Other stuff
    private Vector3 target;
    private bool going = false;

    // References
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, maxDistance, grappleStuff);
            target = hit.point;
            player.enabled = false; // stop player from doing thing
            going = true;
        }
        if (going)
        {
            if (transform.position == target)
            {
                player.enabled = true;
                going = false;
                return;
            }

            transform.Translate((target - transform.position).normalized * travelSpeed * Time.deltaTime);
        }
    }
}
