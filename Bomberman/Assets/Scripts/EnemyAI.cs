using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;

    public Tilemap tilemap;

    public Animator animator;

    public Rigidbody2D rb;

    public GameObject[] wallChecks = new GameObject[4]; // bottom, top, right, left
    int[,] directions = new int[,] { { 0, -1 }, { 0, 1 }, { 1, 0}, { -1, 0} }; // bottom, top, right, left

    bool directionChosen = false;

    int index;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        if (!directionChosen)
        {
            getDirection();
        }
        if (wallThere(wallChecks[index])) {
            movement.x = 0;
            movement.y = 0;
            directionChosen = false;
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void getDirection()
    {
        System.Random rnd = new System.Random();
        index = rnd.Next(4);
        while (wallThere(wallChecks[index]))
        {
            index = rnd.Next(4);
        }
        movement.x = directions[index, 0];
        movement.y = directions[index, 1];
        movement.Normalize();
        directionChosen = true;
    }

    bool wallThere(GameObject check)
    {
        Vector3 worldPos = check.transform.position;
        Vector3Int cell = tilemap.WorldToCell(worldPos);
        if (tilemap.GetTile(cell) == null)
        {
            return false;
        }
        return true;
    }
}
