using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "New Ground Type")]
public class GroundTypeList : ScriptableObject
{
    [SerializeField] TileBase[] tiles;

    /// <summary>
    /// Checks if a tile is in this list.
    /// </summary>
    /// <param name="theTile">The tile we want to know is part of this list</param>
    /// <returns>True or False</returns>
    public bool Contains(TileBase theTile)
    {
        if (theTile is null)
            return false;

        foreach (TileBase currentTileInArray in tiles)
        {
            if (theTile.Equals(currentTileInArray))
                return true;
        }
        return false;
    }
}
