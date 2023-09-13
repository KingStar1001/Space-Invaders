using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceInvaders;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public Transform enemyContainer;
    public List<GameObject> enemyPrefabs;
    public Transform UFOContainer;
    public GameObject UFOPrefab;
    public int initialStep = 10;
    public float movingSpeed = 1f;
    public float nextFireDuration = 0.6f;
    public float UFOAppearDuration = 3f;
    public float UFOMovingSpeed = 10f;
    private float delta = 0f;
    private float UFODelta = 0f;
    private int UFOIndex = 0;
    private List<EnemyController> enemies = new List<EnemyController>();
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            if (delta >= nextFireDuration)
            {
                if (enemies.Count > 0)
                {
                    nextFireDuration = Random.Range(6f / (float)enemies.Count, 12f / (float)enemies.Count);
                    delta = 0f;
                    Fire();
                }
                else
                {
                    nextFireDuration = 6f;
                }
            }
            delta += Time.deltaTime;

            if (UFODelta >= UFOAppearDuration)
            {
                GenerateUFO();
                UFODelta = 0f;
            }
            UFODelta += Time.deltaTime;
        }
    }

    public void CreateEnemies(LevelInfo lvlInfo)
    {
        movingSpeed = lvlInfo.movingSpeed;
        Utils.DestroyChildren(enemyContainer);
        Utils.DestroyChildren(UFOContainer);
        enemies.Clear();
        delta = 0f;
        UFODelta = 0f;

        for (int z = 0; z < lvlInfo.enemyInZ; z++)
        {
            for (int x = 0; x < lvlInfo.enemyInX; x++)
            {
                for (int y = 0; y < lvlInfo.enemyInY; y++)
                {
                    float xOffset = x * 0.9f - (lvlInfo.enemyInX - 1) * 0.9f / 2f;
                    float yOffset = y * 0.75f - (lvlInfo.enemyInY - 1) * 0.75f / 2f;
                    float zOffset = z * 2.6f;
                    GameObject enemyObj = Instantiate(enemyPrefabs[lvlInfo.enemyTypesInZ[z]], enemyContainer);
                    enemyObj.transform.localPosition = new Vector3(xOffset, yOffset, zOffset);
                    enemyObj.name = x + "_" + y + "_" + z;

                    EnemyController controller = enemyObj.GetComponent<EnemyController>();
                    controller.InitEnemy(initialStep + z);
                    enemies.Add(controller);
                }
            }
        }
    }

    public void Fire()
    {
        List<EnemyController> frontEnemies = new List<EnemyController>();
        foreach (EnemyController enemy in enemies)
        {
            if (enemy.IsFrontEnemy())
            {
                frontEnemies.Add(enemy);
            }
        }

        if (frontEnemies.Count > 0)
        {
            frontEnemies[Random.Range(0, frontEnemies.Count)].Fire();
        }
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        movingSpeed += 0.1f;
        enemies.Remove(enemy);
        if (enemies.Count == 0)
        {
            GameManager.instance.NextLevel();
        }
    }

    public void GenerateUFO()
    {
        Vector3 startPosition = Vector3.one;
        Vector3 velocity = Vector3.one;
        Vector3 from = new Vector3(-4f, Random.Range(-1.4f, 1.4f), Random.Range(-1f, 1f));
        Vector3 to = new Vector3(4f, Random.Range(-1.4f, 1.4f), Random.Range(-1f, 1f));
        if (Random.Range(0, 2) == 0)
        {
            velocity = (to - from) / 8f;
            startPosition = from - velocity * 9f;
        }
        else
        {
            velocity = (from - to) / 8f;
            startPosition = to - velocity * 9f;
        }

        GameObject UFOObject = Instantiate(UFOPrefab, UFOContainer);
        UFOObject.name = "UFO_" + UFOIndex;
        UFOIndex++;
        UFOObject.transform.localPosition = startPosition;
        Rigidbody UFORb = UFOObject.GetComponent<Rigidbody>();
        UFORb.velocity = velocity * UFOMovingSpeed;
    }
}
