using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    public Tilemap tilemap;

    public GameObject bombPrefab;

    public GameObject player;

    Vector3Int lastBombCell;
    bool bombCollision = true;
    public bool bombSpawned = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPos = player.transform.position;
        worldPos.y = worldPos.y + 0.45f;
        Vector3Int cell = tilemap.WorldToCell(worldPos);

        if (Input.GetButtonDown("Fire1") && !bombSpawned)
        {
            spawnBomb(cell);
        }
        updateBombCollision(cell);
    }

    void spawnBomb(Vector3Int cell)
    {
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

        Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
        lastBombCell = cell;
        bombCollision = false;
        bombSpawned = true;
    }

    void updateBombCollision(Vector3Int cell)
    {
        if (!bombCollision && lastBombCell == cell)
        {
            Physics2D.IgnoreLayerCollision(8, 9);
        }
        else if (!bombCollision && lastBombCell != cell)
        {
            Physics2D.IgnoreLayerCollision(8, 9, false);
            bombCollision = true;
        }
    }
}
