using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float maxX = 5.0f; // Maximum X-axis position
    public float minX = -5.0f; // Minimum X-axis position
    public float maxY = 3.0f; // Maximum Y-axis position
    public float minY = -3.0f; // Minimum Y-axis position

    [Header("Bullet Option")]
    public float bulletSpeed = 10;
    public GameObject bulletPrefab;
    public GameObject gun;
    public GameObject shipObj;
    public GameObject explosion;
    public bool isDestroyed = false;

    private void Update()
    {
        if (GameManager.instance.isPlaying && !GameManager.instance.isGameOver && !isDestroyed)
        {
            // Get input from the keyboard
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate the new position
            Vector3 newPosition = transform.position + new Vector3(horizontalInput * moveSpeed * Time.deltaTime, verticalInput * moveSpeed * Time.deltaTime, 0);

            // Clamp the position within the specified boundaries
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            // Move the player
            transform.position = newPosition;

            if (Input.GetButtonDown("Fire1"))
            {
                GameObject newBullet = Instantiate(bulletPrefab, GameManager.instance.bulletContainer);
                newBullet.transform.position = gun.transform.position;
                Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
                bulletRb.velocity = gun.transform.forward * bulletSpeed;
            }
        }
    }

    public void InitShip()
    {
        transform.position = new Vector3(0f, -1f, 0f);
        shipObj.SetActive(true);
        explosion.SetActive(false);
        isDestroyed = false;
    }

    public void DestroyShip()
    {
        shipObj.SetActive(false);
        explosion.SetActive(true);
        ParticleSystem particle = explosion.GetComponent<ParticleSystem>();
        particle.Play();
        isDestroyed = true;
        GameManager.instance.ReduceLife();

        if (GameManager.instance.life > 0)
        {
            StopCoroutine("Revoke");
            StartCoroutine("Revoke");
        }
    }

    public void GameOver()
    {
        shipObj.SetActive(false);
        explosion.SetActive(true);
        ParticleSystem particle = explosion.GetComponent<ParticleSystem>();
        particle.Play();
    }

    IEnumerator Revoke()
    {
        yield return new WaitForSeconds(1f);
        transform.position = new Vector3(0f, -1f, 0f);
        shipObj.SetActive(true);
        explosion.SetActive(false);
        isDestroyed = false;
    }
}
