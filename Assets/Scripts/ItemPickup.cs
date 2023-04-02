using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Dropdown
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }

    public ItemType type;

    private void OnItemPickup(GameObject player) {
        switch (type) {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;
            case ItemType.BlastRadius:
                player.GetComponent<BombController>().explosionRadius++;
                break;
            case ItemType.SpeedIncrease:
                player.GetComponent<MovementController>().speed++;
                break;
        }

        // Destory/hide the item after picked up
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Check if player collides with this item
        if (other.CompareTag("Player")) {
            OnItemPickup(other.gameObject);
        }
    }
}
