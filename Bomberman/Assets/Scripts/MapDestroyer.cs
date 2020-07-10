using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    public Tilemap tilemap;

    public Tile destructibleTile;
    public Tile solidTile;

    public GameObject explosionPrefab;

    public void Explosion(Vector2 pos)
    {
        Vector3Int baseCell = tilemap.WorldToCell(pos);

        ExplodeCell(baseCell);
        ExplodeCell(baseCell + new Vector3Int(1, 0, 0));
        ExplodeCell(baseCell + new Vector3Int(-1, 0, 0));
        ExplodeCell(baseCell + new Vector3Int(0, 1, 0));
        ExplodeCell(baseCell + new Vector3Int(0, -1, 0));
    }

    void ExplodeCell(Vector3Int cell)
    {
        Tile tile =  tilemap.GetTile<Tile>(cell);

        if (tile == solidTile)
        {
            return;
        }
        else if (tile == destructibleTile)
        {
            tilemap.SetTile(cell, null);
        }

        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, pos, Quaternion.identity);
    }
}
