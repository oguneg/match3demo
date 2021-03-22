using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour
{
    public int gridSize;
    [SerializeField] private Cell cellPrefab;
    private Cell[,] cells;
    private const int comboCount = 3;
    private List<Cell> contiguity = new List<Cell>();

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        cells = new Cell[gridSize, gridSize];
        CreateGrid();
        SetCellNeighbours();
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize; x++)
            for (int y = 0; y < gridSize; y++)
            {
                CreateCell(x, y);
            }
    }

    private void SetCellNeighbours()
    {
        for (int x = 0; x < gridSize; x++)
            for (int y = 0; y < gridSize; y++)
            {
                var cell = cells[x, y];
                if (y > 0)
                {
                    cell.AddNeighbour(Direction.Down, cells[x, y - 1]);
                }
                if (x > 0)
                {
                    cell.AddNeighbour(Direction.Left, cells[x - 1, y]);
                }
                if (y < gridSize - 1)
                {
                    cell.AddNeighbour(Direction.Up, cells[x, y + 1]);

                }
                if (x < gridSize - 1)
                {
                    cell.AddNeighbour(Direction.Right, cells[x + 1, y]);
                }
            }
    }

    private void CreateCell(int x, int y)
    {
        var cell = Instantiate(cellPrefab, transform);
        cell.gameObject.name = $"cell {x},{y}";
        cell.Init(x, y);
        cell.OnClick += OnCellClicked;
        cells[x, y] = cell;
    }

    private void OnCellClicked(Cell cell)
    {
        if (!cell.IsSelected)
        {
            cell.Select();
            CheckCells(cell);
        }
    }

    private void CheckCells(Cell cell)
    {
        contiguity.Clear();
        CheckCellContiguity(cell);
        if (contiguity.Count >= comboCount)
        {
            foreach (var element in contiguity)
            {
                element.Deselect();
            }
            contiguity.Clear();
        }
    }

    private void CheckCellContiguity(Cell cell)
    {
        var isCellAdded = contiguity.Any(o => o == cell);
        if (!isCellAdded)
        {
            var activeNeighbours = cell.ActiveNeighbours();
            contiguity.Add(cell);
            foreach (var element in activeNeighbours)
            {
                CheckCellContiguity(element);
            }
        }
    }
}
