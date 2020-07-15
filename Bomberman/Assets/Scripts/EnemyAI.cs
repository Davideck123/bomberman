﻿using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;

    public Tilemap tilemap;

    public Animator animator;

    public Rigidbody2D rb;

    public new Collider2D collider;

    public GameObject[] wallChecks = new GameObject[4]; // bottom, top, right, left
    int[,] directions = new int[,] { { 0, -1 }, { 0, 1 }, { 1, 0}, { -1, 0} }; // bottom, top, right, left

    bool directionChosen = false;

    int index;

    Vector2 movement;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 10);
    }
    void Update()
    {
        if (!directionChosen)
        {
            getDirection();
        }
        if (obstacleThere(wallChecks[index])) {
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
        while (obstacleThere(wallChecks[index]))
        {
            index = rnd.Next(4);
        }
        movement.x = directions[index, 0];
        movement.y = directions[index, 1];
        movement.Normalize();
        directionChosen = true;
    }

    bool obstacleThere(GameObject check)
    {
        Vector3 worldPos = check.transform.position;
        Vector3Int cell = tilemap.WorldToCell(worldPos);
        if (tilemap.GetTile(cell) != null)
        {
            return true;
        }

        try
        {
            Vector3 bombPos = GameObject.FindGameObjectWithTag("Bomb").transform.position;
            Vector3Int bombCell = tilemap.WorldToCell(bombPos);
            if (cell == bombCell)
            {
                return true;
            }
            Vector3 gatePos = GameObject.FindGameObjectWithTag("Gate").transform.position;
            Vector3Int gateCell = tilemap.WorldToCell(gatePos);
            if (cell == gateCell)
            {
                return true;
            }
        }
        catch (System.NullReferenceException)
        {
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            col.gameObject.GetComponent<PlayerMovement>().animator.SetBool("Death", true);
            col.gameObject.GetComponent<PlayerMovement>().moveSpeed = 0f;
            for (int i = 0; i < 3; i++)
            {
                col.gameObject.GetComponent<PlayerMovement>().colliders[i].enabled = false;
            }
            Destroy(col.gameObject, 2f);
        }
    }
}
