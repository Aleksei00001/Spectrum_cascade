using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameState;

    [SerializeField] private Player player;

    [SerializeField] private Level level;

    [SerializeField] private PlayerManager playerManager;

    public void CloseAll()
    {
        for (int i = 0; i < gameState.Length; i++)
        {
            gameState[i].SetActive(false);
        }
    }

    public void OpenMenu()
    {
        gameState[1].SetActive(true);
    }

    public void OpenScore()
    {
        gameState[0].SetActive(true);
    }

    public void SetStartGame()
    {
        CloseAll();
        OpenScore();
        player.SetActive(true);
        player.SetZero();
        level.SetZero();
        Particle[] destroyParticle = FindObjectsOfType<Particle>();
        for(int i = 0; i < destroyParticle.Length; i++)
        {
            Destroy(destroyParticle[i].gameObject);
        }
        Meteorite[] destroyMeteorite = FindObjectsOfType<Meteorite>();
        for (int i = 0; i < destroyMeteorite.Length; i++)
        {
            Destroy(destroyMeteorite[i].gameObject);
        }
        Projectile[] destroyProjectile = FindObjectsOfType<Projectile>();
        for (int i = 0; i < destroyProjectile.Length; i++)
        {
            Destroy(destroyProjectile[i].gameObject);
        }
    }

    public void SetStopGame()
    {
        CloseAll();
        OpenScore();
        OpenMenu();
        player.SetActive(false);
        playerManager.StartCoroutineSetupRoutine();
    }
}
