using UnityEngine;

public class GroundObject : MonoBehaviour
{
    public bool isWall = false;
    public Vector2 position;

    public int team;
    public Color unOccupiedColor;
    public Color wallColor = Color.gray;
    public Bounds initialBounds;
    public SpriteRenderer squareRenderer;
    private Color currentColor;
    private Collider2D squareCollider;
    private Rigidbody2D rb;

    void Start()
    {
        squareRenderer = GetComponent<SpriteRenderer>();
        squareCollider = GetComponent<Collider2D>();
        squareCollider.sharedMaterial = Resources.Load<PhysicsMaterial2D>("BouncyMaterial");
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = isWall ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
        rb.gravityScale = 0; // No gravity effect
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        currentColor = isWall ? wallColor : unOccupiedColor;
        squareCollider.enabled = true;
        squareCollider.isTrigger = !isWall;
    }

    void Update()
    {
        if (currentColor != squareRenderer.color)
        {
            squareRenderer.color = currentColor;
        }
    }

    public void SetWall(bool wall, Color inputColor)
    {
        isWall = wall;
        wallColor = inputColor;
    }

    public void SetTeam(int inputTeam, Color inputColor)
    {
        team = inputTeam;
        currentColor = inputColor;
    }
}