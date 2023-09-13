using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
    public int enemyInX;
    public int enemyInY;
    public int enemyInZ;
    public List<int> enemyTypesInZ;
    public float movingSpeed;
}

public class LevelManager
{
    public static LevelInfo GetLevelInfo(int level)
    {
        LevelInfo info = new LevelInfo();

        switch (level)
        {
            case 1:
                info.enemyInX = 2;
                info.enemyInY = 2;
                info.enemyInZ = 2;
                info.movingSpeed = 1f;
                info.enemyTypesInZ = new List<int>() { 0, 1 };
                break;
            case 2:
                info.enemyInX = 3;
                info.enemyInY = 2;
                info.enemyInZ = 2;
                info.movingSpeed = 1.5f;
                info.enemyTypesInZ = new List<int>() { 0, 1 };
                break;
            case 3:
                info.enemyInX = 4;
                info.enemyInY = 2;
                info.enemyInZ = 3;
                info.movingSpeed = 2f;
                info.enemyTypesInZ = new List<int>() { 0, 1, 2 };
                break;
            default:
                info.enemyInX = 5;
                info.enemyInY = 5;
                info.enemyInZ = 5;
                info.movingSpeed = 3f;
                info.enemyTypesInZ = new List<int>() { 0, 0, 1, 1, 2 };
                break;
        }

        return info;
    }
}
