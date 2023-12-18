using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;

    public List<(Vector3,FloorMaterial)> getLevelData()
    {
        List<(Vector3, FloorMaterial)> levelData = new List<(Vector3, FloorMaterial)>();
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position) == false) continue;
            
            CustomTile tileInfo = (CustomTile)tilemap.GetTile(position);
            Vector3 worldPosition = new Vector3(position.x, 0f, position.y);
            levelData.Add((worldPosition, tileInfo.floorMaterial));
        }
        return levelData;  
    }
}
