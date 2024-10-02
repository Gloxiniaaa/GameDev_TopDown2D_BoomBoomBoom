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
}