using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed;
    public float Speed { get { return speed; } }

    GameManager manager;
    Camera mainCam;
    Rigidbody body;

    bool sideBoundaryPassed = false;
    bool topBoundaryPassed = false;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //check ball has hit side boundary
        if (transform.position.x <= mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x ||
            transform.position.x >= mainCam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x)
        {
            /*bool ensures changing of direction only happens once in case
             the ball has remained outside the boundary for more than one frame*/
            if (!sideBoundaryPassed)
            {
                Vector3 direction = new Vector3(-body.velocity.x, body.velocity.y);
                direction.Normalize();
                body.velocity = direction * speed;
            }

            sideBoundaryPassed = true;
        }

        //ball is back inside game screen
        if (transform.position.x > mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x &&
            transform.position.x < mainCam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x)
        {
            sideBoundaryPassed = false;
        }

        //check ball has hit upper boundary
        if (transform.position.y >= mainCam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y)
        {
            if (!topBoundaryPassed)
            {
                Vector3 direction = new Vector3(body.velocity.x, -body.velocity.y);
                direction.Normalize();
                body.velocity = direction * speed;
            }

            topBoundaryPassed = true;
        }

        //ball is back inside game screen
        if (transform.position.y >= mainCam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y)
        {
            topBoundaryPassed = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 direction = transform.position - collision.transform.position;
        direction.Normalize();
        body.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Death")
        {
            manager.RestartGame();
        }
    }
}
