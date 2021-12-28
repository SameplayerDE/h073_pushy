using System;
using Microsoft.Xna.Framework;

namespace h073_pu_iso
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
    
    public static class DirectionExtensions
    {
        public static float ToRotation(this Direction direction)
        {
            return direction switch
            {
                Direction.Up => 0.0f,
                Direction.Right => MathHelper.ToRadians(90f),
                Direction.Down => MathHelper.ToRadians(180f),
                Direction.Left => MathHelper.ToRadians(-90f),
                _ => 0.0f
            };
        }
    }
}