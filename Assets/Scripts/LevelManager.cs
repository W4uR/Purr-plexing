using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    List<Level> levels;

    [SerializeField]
    Transform levelParent;
    [SerializeField]
    Cell cellPrefab;

    Dictionary<Vector2Int, Cell> _cellsOfCurrentLevel;
    Dictionary<Vector2Int, Cell> CellsOfCurrentLevel
    {
        get
        {
            if(_cellsOfCurrentLevel == null || _cellsOfCurrentLevel.Count == 0)
            {
                _cellsOfCurrentLevel = new Dictionary<Vector2Int, Cell>();
                foreach(var cell in levelParent.GetComponentsInChildren<Cell>())
                {
                    _cellsOfCurrentLevel.Add(new Vector2Int(Mathf.RoundToInt(cell.transform.position.x), Mathf.RoundToInt(cell.transform.position.z)), cell);
                }
            }
            return _cellsOfCurrentLevel;
        }
    }


    private int currentLevelIndex = 0;

    private void Start()
    {
        LoadLevel(currentLevelIndex);
    }

    public void LoadLevel(int levelIndex)
    {
        foreach (Transform cell in levelParent)
        {
            Destroy(cell.gameObject);
        }
        List<(Vector3,FloorMaterial)> levelData = levels[levelIndex].getLevelData();
        foreach (var cellData in levelData)
        {
            var cell = Instantiate(cellPrefab, cellData.Item1, cellPrefab.transform.rotation);
            cell.SetFloorMaterial(cellData.Item2);
            cell.transform.SetParent(levelParent, true);
        }

        foreach(var posCellPair in CellsOfCurrentLevel)
        {
            foreach(Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (doesCellHasNeighbour(posCellPair.Key, direction))
                {
                    posCellPair.Value.hideWall(direction);
                }
            }
        }

        currentLevelIndex = levelIndex;
    }


    private bool doesCellHasNeighbour(Vector2Int cellPosition,Direction direction)
    {
        return CellsOfCurrentLevel.ContainsKey(cellPosition + direction.ToVector2Int());
    }
}
