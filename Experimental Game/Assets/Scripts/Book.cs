using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public string title;
    public string author;
    public enum CATEGORY {CHILDNONFIC, CHILDFIC, YANONFIC, YAFIC, FIC, NONFIC};
    public enum GENRE {NONFIC, ROMANCE, MYSTERY, ADVENTURE, COMEDY, TRAGEDY};
    public CATEGORY category;

    // References
    private SpriteRenderer sprite;
    private Collider col;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider>();
    }

    public void Hold()
    {
        sprite.enabled = false;
        col.enabled = false;
    }

    public void Drop(Vector3 position)
    {
        sprite.enabled = true;
        col.enabled = true;
        transform.position = position;
    }
}
