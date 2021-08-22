using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> PowerUps = new List<GameObject>();
    [SerializeField] private float PowerUpTime;
    [SerializeField] private float destroyPowerUpTime;
    [SerializeField] private float ResetPowerUpTime;
    [SerializeField] private float ResetDestroyPowerUpTime;
    [SerializeField] private BoxCollider2D GridArea;
    private GameObject P1, P2;
    private bool isSpawn = false;
    private bool isFirstPowerUp = false;
    private bool isSecondPowerUp = false;

    private void Update()
    {
        PowerUpTime -= Time.deltaTime;
        if (PowerUpTime <= 0)
        {
            isSpawn = true;
            SpawnPowerUp();
            PowerUpTime = ResetPowerUpTime;
        }

        if (isSpawn)
        {
            destroyPowerUpTime -= Time.deltaTime;
            if (destroyPowerUpTime <= 0)
            {
                if (isFirstPowerUp)
                {
                    Destroy(P1);
                    PowerUpTime = ResetPowerUpTime;
                }
                else if (isSecondPowerUp)
                {
                    Destroy(P2);
                    PowerUpTime = ResetPowerUpTime;
                }
                destroyPowerUpTime = ResetDestroyPowerUpTime;
            }
        }

        if (!isSpawn)
        {
            destroyPowerUpTime = ResetDestroyPowerUpTime;
        }
    }

    private void SpawnPowerUp()
    {
        float g = Random.Range(1, PowerUps.Count + 1);
        float x = PosX();
        float y = PosY();
        switch (g)
        {
            case 1:
                if (isSpawn)
                {
                    P1 = Instantiate(PowerUps[0], new Vector2(x, y), transform.rotation);
                    isFirstPowerUp = true;
                    Invoke(nameof(SpawnOver), ResetDestroyPowerUpTime + 0.2f);
                }
                break;

            case 2:
                if (isSpawn)
                {
                    P2 = Instantiate(PowerUps[1], new Vector2(x, y), transform.rotation);
                    isSecondPowerUp = true;
                    Invoke(nameof(SpawnOver), ResetDestroyPowerUpTime + 0.2f);
                }
                break;
        }
    }

    private float PosX()
    {
        Bounds bounds = this.GridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        return x;
    }

    private float PosY()
    {
        Bounds bounds = this.GridArea.bounds;

        float y = Random.Range(bounds.min.y, bounds.max.y);
        return y;
    }

    private void SpawnOver()
    {
        isSpawn = false;
    }
}