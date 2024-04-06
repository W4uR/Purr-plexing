using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    [SerializeField]
    Cell cellPrefab;

    [Header("Tilemap layers")]
    [SerializeField]
    Tilemap layoutMap;
    [SerializeField]
    Tilemap objectMap;
    [Header("Level Event Handler")]
    [SerializeField]
    GameObject levelEventHandler;


    public int CatsOnLevel { get; private set; }
    public Dictionary<Vector2Int, Cell> Cells { get; private set; }

    public Cell GetCell(Vector3 worldPosition)
    {
        if(Cells == null || Cells.Count == 0)
        {
            Debug.LogError("Call level.initialize() before accessing cells.");
            return null;
        }
        else
        {
            if (Cells.TryGetValue(worldPosition.ToVector2Int(), out Cell cell))
                return cell;
            return null;
        }

    }

    public void Initialize(Transform parent)
    {
        ResetLevel(parent);
        GenerateLayout(parent);
        GenerateObjects(parent);
        Instantiate(levelEventHandler, parent);
    }

    private void ResetLevel(Transform parent)
    {
        Cells = new Dictionary<Vector2Int, Cell>();
        foreach (Transform cell in parent)
        {
            Destroy(cell.gameObject);
        }
        CatsOnLevel = 0;
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

            // Megfel� l�p�shangok be�ll�t�sa
            cell.SetFloorMaterial(layoutTile.stepSounds);

            //  Logikai t�rol�s < Koordin�ta - Mez� > p�ros
            Cells.Add(worldPosition.ToVector2Int(), cell);

            // Debug - 
            cell.name = worldPosition.ToVector2Int().ToString();
        }

        //Falak elrejt�se
        foreach (var posCellPair in Cells)
        {
            // Mind a n�gy ir�ny ellen�rz�se ( �szak, kelet, d�l �s nyugat)
            foreach (AbsoluteDirection direction in Enum.GetValues(typeof(AbsoluteDirection)))
            {
                // Ha van szomsz�d az adott ir�nyba
                if (Cells.ContainsKey(posCellPair.Key + direction.ToVector2Int()))
                {
                    // Rejts�k el a falat
                    posCellPair.Value.HideWall(direction);
                }
            }
        }
    }


    void GenerateObjects(Transform parent)
    {
        foreach (var position in objectMap.cellBounds.allPositionsWithin)
        {
            // Ha a vizsg�lt pozitci�ban nincs mez�, akkor ugrunk tov�bb
            if (objectMap.HasTile(position) == false) continue;

            ObjectTile objectTile = (ObjectTile)objectMap.GetTile(position);
            Vector3 worldPosition = new Vector3(position.x, 0f, position.y);
            foreach (GameObject o in objectTile.objects)
            {
                var newObject = Instantiate(o, worldPosition + o.transform.position, o.transform.rotation, parent);
                // Ha a l�trehozott objektum egy macska, akkor n�velj�k a macsk�k sz�m�t
                if (newObject.CompareTag("Cat"))
                {
                    CatsOnLevel++;
                    print("A cat has been spawned on " + worldPosition.ToVector2Int());
                }
                else if (newObject.CompareTag("Spawn"))
                {
                    Debug.Log("Spawn point: " + worldPosition);
                    Debug.Log("Player position: " + Player.GetInstance().transform.position);
                    Player.TeleportTo(worldPosition);
                }
            }
        }
    }
}
