using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FootstepHandler : MonoBehaviour
{
    [SerializeField] GroundTypeList grassTiles;
    [SerializeField] GroundTypeList stoneTiles;
    [SerializeField] GroundTypeList woodTiles;
    [SerializeField] GroundTypeList dirtTiles;

    public bool IsGrass(TileBase currentGround)
    {
        return grassTiles.Contains(currentGround);
    }

    public bool IsStone(TileBase currentGround)
    {
        return stoneTiles.Contains(currentGround);
    }

    public bool IsWood(TileBase currentGround)
    {
        return woodTiles.Contains(currentGround);
    }

    public bool IsDirt(TileBase currentGround)
    {
        return dirtTiles.Contains(currentGround);
    }
}
