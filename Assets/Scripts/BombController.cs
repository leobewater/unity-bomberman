using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public KeyCode inputKey = KeyCode.Space;
    public GameObject bombPrefab;
    public float bombFuseTime = 3f;
    public int bombAmount = 1; // max bomb a player can drops
    private int bombsRemaining = 0; // how many bombs remaining a player can place

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

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

        // Bomb may get pushed by player, get the latest position
        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        // Bomb exploding
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        // Set Explosion animation
        explosion.SetActiveRenderer(explosion.start);
        // Destroy explosion after explosion duration
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombsRemaining++;
    }

    private void Explode(Vector2 position, Vector2 direction, int length) {
        // Recursive function to show explosion in multiple tiles base on the "length"
        // Base case
        if (length <= 0) {
            return;
        }

        position += direction;

        // Checking if overlap tile has collider based on the defined layer
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask)) {
            // Hit the edge and ignore the explosion on those tiles
            return;
        }

        // Bomb exploding
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        // Set Explosion animation
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        // Destroy explosion after explosion duration
        //Destroy(explosion.gameObject, explosionDuration);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }

    private void OnTriggerExit2D(Collider2D other) {
        // Set bomb "is Trigger" in Collider when player walks away the bomb after dropped
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) {
            other.isTrigger = false;
        }
    }
}
