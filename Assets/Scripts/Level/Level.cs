using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    [SerializeField]
    Tilemap layoutMap;
    [SerializeField]
    Tilemap objectMap;

    [SerializeField]
    Cell cellPrefab;

    private Dictionary<Vector2Int, Cell> cells;
    public int catsOnLevel { get; private set; }
    public Vector3 spawnPoint { get; private set; }


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

    public void Initialize(Transform parent = null)
    {
        cells = new Dictionary<Vector2Int, Cell>();
        catsOnLevel = 0;
        GenerateLayout(parent);
        GenerateObjects();
    }

    void GenerateLayout(Transform parent)
    {
        //Szobadarabok inicializálása
        foreach (var position in layoutMap.cellBounds.allPositionsWithin)
        {
            // Ha az ellenörzött pozición nincs mezõ, akkor ugrunk a következõre
            if (layoutMap.HasTile(position) == false) continue;

            // Mezõ létrehozás a világban
            LayoutTile layoutTile = (LayoutTile)layoutMap.GetTile(position);
            Vector3 worldPosition = new Vector3(position.x, 0f, position.y);
            var cell = Instantiate(cellPrefab, worldPosition, cellPrefab.transform.rotation);
            cell.transform.SetParent(parent);

            // Megfelõ padlótípus beállítása
            cell.SetFloorMaterial(layoutTile.floorMaterial);

            //  Logikai tárolás < Koordináta - Mezõ > páros
            cells.Add(worldPosition.ToVector2Int(), cell);

            // Debug - 
            cell.name = worldPosition.ToVector2Int().ToString();
        }

        //Falak elrejtése
        foreach (var posCellPair in cells)
        {
            // Mind a négy irány ellenörzése ( észak, kelet, dél és nyugat)
            foreach (AbsoluteDirection direction in Enum.GetValues(typeof(AbsoluteDirection)))
            {
                // Ha van szomszéd az adott irányba
                if (cells.ContainsKey(posCellPair.Key + direction.ToVector2Int()))
                {
                    // Rejtsük el a falat
                    posCellPair.Value.HideWall(direction);
                }
            }
        }
    }


    void GenerateObjects()
    {
        foreach (var position in objectMap.cellBounds.allPositionsWithin)
        {
            // Ha a vizsgált pozitcióban nincs mezõ, akkor ugrunk tovább
            if (objectMap.HasTile(position) == false) continue;

            ObjectTile objectTile = (ObjectTile)objectMap.GetTile(position);
            Vector3 worldPosition = new Vector3(position.x, 0f, position.y);
            foreach (GameObject o in objectTile.objects)
            {
                var newObject = Instantiate(o, cells[worldPosition.ToVector2Int()].transform);
                // Ha a létrehozott objektum egy macska, akkor növeljük a macskák számát
                if(newObject.CompareTag("Cat"))
                {
                    catsOnLevel++;
                    print("A cat has been spawned on " + worldPosition.ToVector2Int());
                }
                // Kezdõ pozitció eltárolása
                else if (newObject.CompareTag("Spawn"))
                {
                    spawnPoint = newObject.transform.position;
                }
            }
        }
    }
}
