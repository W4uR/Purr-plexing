using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    [SerializeField]
    Tilemap layout_map;
    [SerializeField]
    Tilemap object_map;

    [SerializeField]
    Cell cellPrefab;

    private Dictionary<Vector2Int, Cell> cells;

    public Cell GetCell(Vector3 worldPosition)
    {
        if(cells == null ||cells.Count == 0)
        {
            Debug.LogError("Call level.initialize() before accessing cells.");
            return null;
        }
        else
        {
            return cells[worldPosition.ToVector2Int()];
        }

    }

    public void initialize(Transform parent = null)
    {
        cells = new Dictionary<Vector2Int, Cell>();
        generateLayout(parent);
        generateObjects();
    }

    void generateLayout(Transform parent)
    {
        //Instantiating the cells
        foreach (var position in layout_map.cellBounds.allPositionsWithin)
        {
            if (layout_map.HasTile(position) == false) continue;
            LayoutTile layoutTile = (LayoutTile)layout_map.GetTile(position);
            Vector3 worldPosition = new Vector3(position.x, 0f, position.y);
            var cell = Instantiate(cellPrefab, worldPosition, cellPrefab.transform.rotation);
            cell.transform.SetParent(parent);
            cell.SetFloorMaterial(layoutTile.floorMaterial);
            cells.Add(worldPosition.ToVector2Int(), cell);
        }

        //Removing walls
        foreach (var posCellPair in cells)
        {
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (cells.ContainsKey(posCellPair.Key + direction.ToVector2Int()))
                {
                    posCellPair.Value.hideWall(direction);
                }
            }
        }
    }
    void generateObjects()
    {
        foreach (var position in object_map.cellBounds.allPositionsWithin)
        {
            if (object_map.HasTile(position) == false) continue;
            ObjectTile objectTile = (ObjectTile)object_map.GetTile(position);
            Vector3 worldPosition = new Vector3(position.x, 0f, position.y);
            foreach (GameObject o in objectTile.objects)
            {
                Instantiate(o, cells[worldPosition.ToVector2Int()].transform);
            }
        }
    }
}
