using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int step = 10;
    public float direction = 1;
    public float bulletSpeed = 50f;
    public GameObject bulletPrefab;
    public GameObject gun;
    public GameObject shipObj;
    public GameObject explotion;
    public int score = 10;
    public bool isDestroyed = false;

    public void InitEnemy(int _step)
    {
        step = _step;
        isDestroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlaying && !GameManager.instance.isGameOver && !isDestroyed)
        {
            Vector3 newPosition = transform.position + new Vector3(direction * EnemyManager.instance.movingSpeed * Time.deltaTime, 0f, 0f);
            transform.position = newPosition;
            if (newPosition.x < -4 && direction < 0 || newPosition.x > 4 && direction > 0)
            {
                ChangeDirection();
            }
        }
    }

    public void ChangeDirection()
    {
        step--;
        direction = direction * -1f;
        transform.position = transform.position - new Vector3(0, 0f, 2.6f);
        if (step == 0)
        {
            GameManager.instance.GameOver();
        }
    }

    public bool IsFrontEnemy()
    {
        Ray ray = new Ray(transform.position, transform.forward * -1f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 20f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                return false;
            }
        }

        return true;
    }

    public void Fire()
    {
        GameObject newBullet = Instantiate(bulletPrefab, GameManager.instance.bulletContainer);
        newBullet.transform.position = gun.transform.position;
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
        bulletRb.velocity = gun.transform.forward * bulletSpeed;
    }

    public void KillEnemy()
    {
        GameManager.instance.AddScore(score);
        EnemyManager.instance.RemoveEnemy(this);
        shipObj.SetActive(false);
        isDestroyed = true;
        StopCoroutine("DestroyShip");
        StartCoroutine("DestroyShip");
    }

    IEnumerator DestroyShip()
    {
        explotion.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameObject.Destroy(gameObject);
    }
}
