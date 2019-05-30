using System.Collections.Generic;
using UnityEngine;

public class PickUpBook : MonoBehaviour
{
    public float distance;
    public LayerMask book;

    private List<Book> books = new List<Book>();
    private int currentIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward, out hit, distance, book);
            if (hit.collider == null) return;
            GameObject obj = hit.collider.gameObject;

            if (books.Count < 5)
            {
                Book b = obj.GetComponent<Book>();
                books.Add(obj.GetComponent<Book>());
                b.Hold();
            }
            else
            {
                print("Can't hold more than 5 books!");
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (books.Count <= 0) return;
            if (currentIndex >= 0 && currentIndex < books.Count)
            {
                books[currentIndex].Drop(transform.position);
                books.RemoveAt(currentIndex);
                currentIndex--;
            }
        }

    }
}
