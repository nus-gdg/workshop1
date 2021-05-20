using UnityEngine;
using UnityEngine.Tilemaps;

namespace Project.Views.World.Tilemaps
{
    [CreateAssetMenu]
    public class DungeonRuleTile : RuleTile<DungeonRuleTile.Neighbor>
    {
        public enum DungeonTileType
        {
            Ground, Water
        }

        public DungeonTileType tileType;

        public class Neighbor : RuleTile.TilingRule.Neighbor
        {
            // Failsafe in case RuleTile.TilingRule.Neighbor changes
            public const int NotSame = NotThis + 1;
            public const int IsGround = NotSame + 1; 
            public const int IsWater = IsGround + 1;
        }

        public override bool RuleMatch(int neighbor, TileBase tile)
        {
            var customRule = tile as DungeonRuleTile;
            if (customRule)
            {
                switch (neighbor)
                {
                    case Neighbor.IsGround:
                        return customRule.tileType == DungeonTileType.Ground;
                    case Neighbor.IsWater:
                        return customRule.tileType == DungeonTileType.Water;
                    case Neighbor.NotSame:
                        return customRule.tileType != tileType;
                    case Neighbor.NotThis:
                        return false;
                    case Neighbor.This:
                        return true;
                }
            }
            return base.RuleMatch(neighbor, tile);
        }
    }
}
