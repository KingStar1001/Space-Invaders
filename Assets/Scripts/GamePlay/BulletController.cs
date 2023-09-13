using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float maxDistance = 30;
    public bool isPlayer;

    Vector3 initPos;

    private Vector3 savedVelocity;
    private Rigidbody rb; // Reference to the Rigidbody component

    // Use this for initialization
    void Start()
    {
        initPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            if (rb.velocity == Vector3.zero)
            {
                Resume();
            }
            // difference in all coordinate
            float diffX = Math.Abs(initPos.x - transform.position.x);
            float diffY = Math.Abs(initPos.y - transform.position.y);
            float diffZ = Math.Abs(initPos.z - transform.position.z);

            // destroy if it's too far away
            if (diffX >= maxDistance || diffY >= maxDistance || diffZ >= maxDistance)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (rb.velocity != Vector3.zero)
            {
                Pause();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isPlayer)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyController>().KillEnemy();
                other.enabled = false;
                GameObject.Destroy(gameObject);
            }
            if (other.CompareTag("UFO"))
            {
                other.GetComponent<UFOController>().Claimed();
                other.enabled = false;
                GameObject.Destroy(gameObject);
            }
        }
        else
        {
            if (other.CompareTag("PlayerShip"))
            {
                other.transform.parent.GetComponent<PlayerShip>().DestroyShip();
                GameObject.Destroy(gameObject);
            }
        }
    }

    public void Pause()
    {
        savedVelocity = rb.velocity;
        rb.velocity = Vector3.zero;
    }

    void Resume()
    {
        rb.velocity = savedVelocity;
    }
}
