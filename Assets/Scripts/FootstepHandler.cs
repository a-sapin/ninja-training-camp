using UnityEngine;
using UnityEngine.Tilemaps;

public enum GroundType
{
    Grass,
    Stone,
    Wood,
    Dirt,
    Null // is NULL necessary?
}

public class FootstepHandler : MonoBehaviour
{
    [SerializeField] GroundTypeList grassTiles;
    [SerializeField] GroundTypeList stoneTiles;
    [SerializeField] GroundTypeList woodTiles;
    [SerializeField] GroundTypeList dirtTiles;

    /// <summary>
    /// EXPENSIVE, USE WITH CAUTION
    /// </summary>
    public GroundType DetermineGroundType(TileBase currentGround)
    {
        if (IsGrass(currentGround))
        {
            return GroundType.Grass;
        }
        else if (IsStone(currentGround))
        {
            return GroundType.Stone;
        }
        else if (IsWood(currentGround))
        {
            return GroundType.Wood;
        }
        else if (IsDirt(currentGround))
        {
            return GroundType.Dirt;
        }
        else
        {
            return GroundType.Null;
        }
    }

    /// <summary>
    /// EXPENSIVE, USE WITH CAUTION
    /// </summary>
    public bool IsGrass(TileBase currentGround)
    {
        return grassTiles.Contains(currentGround);
    }

    /// <summary>
    /// EXPENSIVE, USE WITH CAUTION
    /// </summary>
    public bool IsStone(TileBase currentGround)
    {
        return stoneTiles.Contains(currentGround);
    }

    /// <summary>
    /// EXPENSIVE, USE WITH CAUTION
    /// </summary>
    public bool IsWood(TileBase currentGround)
    {
        return woodTiles.Contains(currentGround);
    }

    /// <summary>
    /// EXPENSIVE, USE WITH CAUTION
    /// </summary>
    public bool IsDirt(TileBase currentGround)
    {
        return dirtTiles.Contains(currentGround);
    }
}
