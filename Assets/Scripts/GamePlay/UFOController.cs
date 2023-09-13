using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    public float maxDistance = 40;
    public int score = 30;
    public GameObject ufoObject;
    public GameObject explosion;
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

    public void Pause()
    {
        savedVelocity = rb.velocity;
        rb.velocity = Vector3.zero;
    }

    void Resume()
    {
        rb.velocity = savedVelocity;
    }

    public void Claimed()
    {
        GameManager.instance.AddScore(score);
        ufoObject.SetActive(false);
        StopCoroutine("DestroyUFO");
        StartCoroutine("DestroyUFO");
    }

    IEnumerator DestroyUFO()
    {
        explosion.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameObject.Destroy(gameObject);
    }
}
