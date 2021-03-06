using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassBurner : MonoBehaviour
{
    public BoxCollider2D gridarea;
    private void Start()
    {

        StartCoroutine(changePosition());
    }

    private void RandomizePosition()
    {
        Bounds bounds = this.gridarea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    IEnumerator changePosition()
    {
        RandomizePosition();
        yield return new WaitForSeconds(Random.Range(8f, 12f));

        StartCoroutine(changePosition());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
        }
    }
}
