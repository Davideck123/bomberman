using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelCreator : MonoBehaviour
{
    public Tilemap tilemap; // x from -9 to 11, y from -9 to 1

    public Tile destructibleTile;

    public GameObject enemyPrefab;

    public GameObject gatePrefab;

    public int numberOfTiles = 50;
    public int numberOfEnemies = 8;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();
        createDestructibleTiles(rnd);
        spawnEnemies(rnd);
    }

    void createDestructibleTiles(System.Random rnd)
    {
        int tiles = 0;
        tilemap.SetTile(new Vector3Int(-7, 1, 0), destructibleTile);
        tilemap.SetTile(new Vector3Int(-9, -1, 0), destructibleTile);
        tiles = 2;
        bool gateAdded = false;
        while (tiles < numberOfTiles)
        {
            int x = rnd.Next(-9, 12);
            int y = rnd.Next(-9, 2);
            if ((x != -9 && x != -8) || (y != 1 && y != 0)) // make space for the player
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(pos) == null)
                {
                    if (!gateAdded)
                    {
                        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(pos);
                        Instantiate(gatePrefab, cellCenterPos, Quaternion.identity);
                        gateAdded = true;
                    }
                    tilemap.SetTile(pos, destructibleTile);
                    tiles++;
                }
            }            
        }
    }

    void spawnEnemies(System.Random rnd)
    {
        int enemies = 0;
        while (enemies < numberOfEnemies)
        {
            int x = rnd.Next(-9, 12);
            int y = rnd.Next(-9, 2);
            if ((x != -9 && x != -8) || (y != 1 && y != 0))
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(pos) == null)
                {
                    Vector3 cellCenterPos = tilemap.GetCellCenterWorld(pos);
                    GameObject enemy = Instantiate(enemyPrefab, cellCenterPos, Quaternion.identity);
                    enemy.GetComponent<EnemyAI>().tilemap = tilemap;
                    enemies++;
                }
            }
        }
    }

}
