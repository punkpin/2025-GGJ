using UnityEngine;

public class BaseBubble : MonoBehaviour
{
    private Rigidbody2D rb;
    new private CircleCollider2D collider2D;
    public Color circleColor = Color.white; // Default color
    public float moveSpeed = 40.0f; // Default move speed

    protected void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        collider2D = gameObject.AddComponent<CircleCollider2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.mass = 1;
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        collider2D.sharedMaterial = Resources.Load<PhysicsMaterial2D>("BouncyMaterial");
        collider2D.radius = 0.5f;
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