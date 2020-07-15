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

    void Update()
    {
        ///place bomb, one at a time

        Vector3 worldPos = player.transform.position;
        worldPos.y = worldPos.y + 0.45f;
        Vector3Int cell = tilemap.WorldToCell(worldPos);

        if (Input.GetButtonDown("Fire1") && !bombSpawned)
        {
            SpawnBomb(cell);
        }
        UpdateBombCollision(cell);
    }

    void SpawnBomb(Vector3Int cell)
    {
        ///create bomb object at the cell location
        
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

        Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
        lastBombCell = cell;
        bombCollision = false;
        bombSpawned = true;
    }

    void UpdateBombCollision(Vector3Int cell)
    {
        ///layer 8: player, layer 9: bomb
        ///bomb doesn't collide with player until player leaves 
        ///the cell of the bomb

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
