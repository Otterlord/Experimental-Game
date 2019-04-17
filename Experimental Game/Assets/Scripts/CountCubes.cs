using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCubes : MonoBehaviour
{
    private int count = 0;
    public int threshold = 5;

    public Animator door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube")) count++;
        if (count == threshold)
        {
            door.SetBool("open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube")) count--;
        if (count < threshold) door.SetBool("open", false);
    }
}
