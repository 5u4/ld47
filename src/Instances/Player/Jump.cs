using Godot;

namespace ld47.Instances.Player
{
    public class Jump : Node2D
    {
        public Player Player;
        public const float JumpHeight = 240f;
        
        public override void _Ready()
        {
            base._Ready();
            Player = GetNode<Player>("../..");
        }

        // TODO: Coyote time + hold jump higher
        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            if (!Input.IsActionJustPressed("ui_jump") || !Player.IsOnFloor() || Player.ActionLock.IsLocked) return;
            Player.Velocity.y = -JumpHeight;
        }
    }
}
