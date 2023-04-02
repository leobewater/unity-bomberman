using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmount = 1; // max bomb a player can drops
    private int bombsRemaining = 0; // how many bombs remaining a player can place

    private void OnEnable() {
        bombsRemaining = bombAmount;
    }

    private void Update() {
        // Drop bomb
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey)) {
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb() {
        // Use player's position for bomb
        Vector2 position = transform.position;
        // Round the number with the grid
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;

        // Suspend this function until after the bombFuseTime is done
        yield return new WaitForSeconds(bombFuseTime);

        Destroy(bomb);
    }
}
