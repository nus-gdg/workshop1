using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Data.Tilemap
{
    [CreateAssetMenu]
    public class DungeonRuleTile : RuleTile<DungeonRuleTile.Neighbor>
    {
        public enum DungeonTileType
        {
            Ground, Water, Wall
        }

        public DungeonTileType tileType;

        public class Neighbor : RuleTile.TilingRule.Neighbor
        {
            public const int IsGround = 3;
            public const int IsWater = 4;
            public const int IsWall = 5;
        }

        public override bool RuleMatch(int neighbor, TileBase tile)
        {
            var customRule = tile as DungeonRuleTile;
            if (customRule)
            {
                switch (neighbor)
                {
                    case Neighbor.IsGround:
                        return customRule && customRule.tileType == DungeonTileType.Ground;
                    case Neighbor.IsWater:
                        return customRule && customRule.tileType == DungeonTileType.Water;
                    case Neighbor.IsWall:
                        return customRule && customRule.tileType == DungeonTileType.Wall;
                    case Neighbor.NotThis:
                        return customRule && customRule.tileType != this.tileType;
                }
            }
            return base.RuleMatch(neighbor, tile);
        }
    }
}
