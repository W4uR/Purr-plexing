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
        //Szobadarabok inicializ�l�sa
        foreach (var position in layoutMap.cellBounds.allPositionsWithin)
        {
            // Ha az ellen�rz�tt pozici�n nincs mez�, akkor ugrunk a k�vetkez�re
            if (layoutMap.HasTile(position) == false) continue;

            // Mez� l�trehoz�s a vil�gban
            LayoutTile layoutTile = (LayoutTile)layoutMap.GetTile(position);
            Vector3 worldPosition = new Vector3(position.x, 0f, position.y);
            var cell = Instantiate(cellPrefab, worldPosition, cellPrefab.transform.rotation);
            cell.transform.SetParent(parent);

            // Megfel� padl�t�pus be�ll�t�sa
            cell.SetFloorMaterial(layoutTile.floorMaterial);

            //  Logikai t�rol�s < Koordin�ta - Mez� > p�ros
            cells.Add(worldPosition.ToVector2Int(), cell);

            // Debug - 
            cell.name = worldPosition.ToVector2Int().ToString();
        }

        //Falak elrejt�se
        foreach (var posCellPair in cells)
        {
            // Mind a n�gy ir�ny ellen�rz�se ( �szak, kelet, d�l �s nyugat)
            foreach (AbsoluteDirection direction in Enum.GetValues(typeof(AbsoluteDirection)))
            {
                // Ha van szomsz�d az adott ir�nyba
                if (cells.ContainsKey(posCellPair.Key + direction.ToVector2Int()))
                {
                    // Rejts�k el a falat
                    posCellPair.Value.HideWall(direction);
                }
            }
        }
    }


    void GenerateObjects()
    {
        foreach (var position in objectMap.cellBounds.allPositionsWithin)
        {
            // Ha a vizsg�lt pozitci�ban nincs mez�, akkor ugrunk tov�bb
            if (objectMap.HasTile(position) == false) continue;

            ObjectTile objectTile = (ObjectTile)objectMap.GetTile(position);
            Vector3 worldPosition = new Vector3(position.x, 0f, position.y);
            foreach (GameObject o in objectTile.objects)
            {
                var newObject = Instantiate(o, cells[worldPosition.ToVector2Int()].transform);
                // Ha a l�trehozott objektum egy macska, akkor n�velj�k a macsk�k sz�m�t
                if(newObject.CompareTag("Cat"))
                {
                    catsOnLevel++;
                    print("A cat has been spawned on " + worldPosition.ToVector2Int());
                }
                // Kezd� pozitci� elt�rol�sa
                else if (newObject.CompareTag("Spawn"))
                {
                    spawnPoint = newObject.transform.position;
                }
            }
        }
    }
}
