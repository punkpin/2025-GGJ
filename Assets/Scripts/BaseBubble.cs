using UnityEngine;

public class BaseBubble : MonoBehaviour
{
    private Rigidbody2D rb;
    public Color circleColor = Color.white; // Default color
    public float moveSpeed = 5.0f; // Default move speed

    protected void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.mass = 1; // Set the mass of the bubble
        rb.gravityScale = 0; // Assuming no gravity effect
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        GetComponent<Collider2D>().sharedMaterial = Resources.Load<PhysicsMaterial2D>("BouncyMaterial");
        GetComponent<SpriteRenderer>().color = circleColor;
    }

    public void Move(Vector2 moveDirection)
    {
        moveDirection.Normalize();
        rb.velocity = moveDirection * moveSpeed;

        Vector3 newPosition = transform.position;
        transform.position = newPosition;
    }
}