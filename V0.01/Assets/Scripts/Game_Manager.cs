using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    private bool gameEnded = false;
    private static Vector3 respawn;
    public void GameOver()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetRespawn(Vector3 pos)
    {
        respawn = pos;
        Debug.Log("set");
    }

    public Vector3 GetRespawn()
    {
        return respawn;
    }
}
