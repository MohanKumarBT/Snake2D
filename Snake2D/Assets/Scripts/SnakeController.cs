using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentprefab;
    public int initialSize = 3;
    private float maxX, maxY, minX, minY;
    public BoxCollider2D wallArea;

    private void Start()
    {
        ResetState();
        Bounds bounds = this.wallArea.bounds;
        maxX = bounds.max.x;
        maxY = bounds.max.y;
        minX = bounds.min.x;
        minY = bounds.min.y;

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
    }
    private void Wallwrap()
    {
        Vector3 newPosition = transform.position;

        if (newPosition.x > maxX)
        {
            newPosition.x = -newPosition.x + 1f;
        }
        else if (newPosition.x <= minX)
        {
            newPosition.x = -newPosition.x - 1f;
        }

        if (newPosition.y >= maxY)
        {
            newPosition.y = -newPosition.y + 1f;
        }
        else if (newPosition.y <= minY)
        {
            newPosition.y = -newPosition.y - 1f;
        }

        transform.position = newPosition;
    }
    private void FixedUpdate()
    {
        Wallwrap();
        for (int i = _segments.Count - 1; i>0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0f
            ); 
    }
    private void grow()
    {
        Transform segment = Instantiate(this.segmentprefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);

    }
    private void shrink()
    {
        Transform segment = _segments[_segments.Count - 1].transform; 
        _segments.Remove(segment);
        Destroy(segment.gameObject);

    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        this.transform.position = Vector3.zero;
        _segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentprefab));
        }
            //this.transform.position = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            grow();
        }
        else if (other.tag == "Body")
        {
            ResetState();
        }
        else if (other.tag == "Bfood")
        {
            if(_segments.Count > 3)
            shrink();
        }
    }
}
