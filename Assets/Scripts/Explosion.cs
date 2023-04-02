using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSpriteRenderer start;
    public AnimatedSpriteRenderer middle;
    public AnimatedSpriteRenderer end;

    public void SetActiveRenderer(AnimatedSpriteRenderer renderer) {
        start.enabled = renderer == start;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end;
    }

    public void SetDirection(Vector2 direction) {
        // Rotate the explosion sprites based on direction
        float angle = Mathf.Atan2(direction.y, direction.x);
        // Rotate the Z axis (Vector3.forward)
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void DestroyAfter(float seconds) {
        Destroy(gameObject, seconds);
    }
}
