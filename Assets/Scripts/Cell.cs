using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    public UnityAction<Cell> OnClick;
    private Dictionary<Direction, Cell> neighbours = new Dictionary<Direction, Cell>();
    public Dictionary<Direction, Cell> Neighbours => neighbours;

    [SerializeField] private SpriteRenderer tick;
    public bool IsSelected { get; private set; }

    public void AddNeighbour(Direction direction, Cell neighbour)
    {
        neighbours.Add(direction, neighbour);
    }

    public void Init(int x, int y)
    {
        SetPosition(x, y);
    }

    public List<Cell> ActiveNeighbours()
    {
        List<Cell> activeNeighbours = new List<Cell>();
        foreach(var element in neighbours)
        {
            if(element.Value.IsSelected)
            {
                activeNeighbours.Add(element.Value);
            }
        }
        return activeNeighbours;
    }

    private void SetPosition(int x, int y)
    {
        transform.position = new Vector3(x, y, 0);
    }

    public void Select()
    {
        IsSelected = true;
        UpdateSelectionVisibility();
    }

    public void Deselect()
    {
        IsSelected = false;
        UpdateSelectionVisibility();
    }

    private void UpdateSelectionVisibility()
    {
        tick.gameObject.SetActive(IsSelected);
    }


    private void OnMouseDown()
    {
        OnClick?.Invoke(this);
    }
}
