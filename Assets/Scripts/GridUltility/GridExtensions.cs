using UnityEngine;

public static class GridExtensions
{
    public static Vector2 MapToGrid(Grid grid, Vector2 worldPos)
    {
        Vector3Int cell = grid.WorldToCell(worldPos);
        return grid.GetCellCenterWorld(cell);
    }

    public static Vector3 MapToGrid(Grid grid, Vector3 worldPos)
    {
        Vector3Int cell = grid.WorldToCell(worldPos);
        return grid.GetCellCenterWorld(cell);
    }

    public static Vector2 GetRandomDirection()
    {
        int i = Random.Range(0, 4);
        if (i == 0)
        {
            return Vector2.up;
        }
        else if (i == 1)
        {
            return Vector2.right;
        }
        else if (i == 2)
        {
            return Vector2.down;
        }
        else
        {
            return Vector2.left;
        }
    }
}