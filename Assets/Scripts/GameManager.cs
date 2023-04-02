using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;

    public void CheckWinState() {
        // Check if player gameobject is still active
        int aliveCount = 0;

        foreach (GameObject player in players) {
            if (player.activeSelf) {
                aliveCount++;
            }
        }

        if (aliveCount <= 1) {
            Debug.Log("Game over! Restarting now!");

            Invoke(nameof(NewRound), 3f);
        }
    }

    private void NewRound() {
        // Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
