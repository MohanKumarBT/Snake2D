using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    private Vector2 direction = Vector2.up;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentprefab;
    public int initialSize = 3;
    private float maxX, maxY, minX, minY;
    public BoxCollider2D wallArea;
    private bool l = false, r = false, u = true, d = false;
    public GameObject gameOverUI;
    public GameObject pause_score;
    public ScoreController scoreController;
    private bool canDie = true;
    [SerializeField] private bool isShield = false;

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
        Movement();
        if (isShield)
            canDie = false;
    }
   
    private void FixedUpdate()
    {
        Wallwrap();

        for (int i = _segments.Count - 1; i>0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
            ); 
    }
    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!d)
            {
                u = true;
                l = false;
                r = false;
                direction = Vector2.up;
                gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (!u)
            {
                d = true;
                l = false;
                r = false;
                direction = Vector2.down;
                gameObject.transform.localEulerAngles = new Vector3(0, 0, 180);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (!r)
            {
                u = false;
                d = false;
                l = true;
                direction = Vector2.left;
                gameObject.transform.localEulerAngles = new Vector3(0, 0, 90);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (!l)
            {
                r = true;
                u = false;
                d = false;
                direction = Vector2.right;
                gameObject.transform.localEulerAngles = new Vector3(0, 0, -90);
            }
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
    private void grow()
    {
        Transform segment = Instantiate(this.segmentprefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);

    }
    private void reduce()
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
            scoreController.Score1Increment(100);
        }
        else if (other.tag == "Body" && canDie)
        {
            GameOver();
        }
        else if (other.tag == "Bfood")
        {
            //if(_segments.Count > 4)
            reduce();
            scoreController.Score1Decrement(50);
        }
        else if (other.CompareTag("scoreboost"))
        {
            scoreController.Score1Double(scoreController.WhatIsScore1());
        }
        else if (other.CompareTag("shield"))
        {
            isShield = true;
            Invoke("ShieldOver", 10f);
        }
    }
    private void ShieldOver()
    {
        isShield = false;
        canDie = true;
    }
    void GameOver()
    {
        Time.timeScale = 0;
        gameOverUI.gameObject.SetActive(true);
        pause_score.gameObject.SetActive(false);
    }
}
