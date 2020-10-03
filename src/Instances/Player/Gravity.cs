using Godot;

namespace ld47.Instances.Player
{
    public class Gravity : Node2D
    {
        public Player Player;
        public const float GravityScale = 500;
        public const float FallingSpeedReduction = 0.2f;

        public override void _Ready()
        {
            base._Ready();
            Player = GetNode<Player>("../..");
        }

        public override void _PhysicsProcess(float delta)
        {
            Player.Velocity.y += delta * (Player.Velocity.y < 0
                ? GravityScale
                : GravityScale * (1 - FallingSpeedReduction));
        }
    }
}
