using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    private bool gameEnded = false;
    private Vector3 respawn;
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
        Debug.Log(respawn);
        FindObjectOfType<Player_Movement>().transform.position = respawn;
    }

    public void SetRespawn(Vector3 pos)
    {
        respawn = pos;
    }
}
