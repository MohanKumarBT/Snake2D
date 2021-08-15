using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _snakebody;
    public Transform snakebodyprefab;

    private void Start()
    {
        _snakebody = new List<Transform>();
        _snakebody.Add(this.transform);
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
    private void FixedUpdate()
    {
        for(int i = _snakebody.Count - 1; i>0; i--)
        {
            _snakebody[i].position = _snakebody[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0f
            ); 
    }
    private void grow()
    {
        Transform snakebody = Instantiate(this.snakebodyprefab);
        snakebody.position = _snakebody[_snakebody.Count - 1].position;
        _snakebody.Add(snakebody);

    }
    //private void ResetState()
    //{
    //    for(int i=1; i<_snakebody.Count; i++)
    //    {
    //        Destroy(_snakebody[i].gameObject);
    //    }
    //    _snakebody.Clear();
    //    _snakebody.Add(this.transform);
    //    this.transform.position = Vector3.zero;
    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            grow();
        }//else if (other.tag == "Player")
        //{
        //    ResetState();
        //}
    }
}
