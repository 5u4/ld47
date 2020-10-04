using Godot;

namespace ld47.Instances.Player
{
    public class Jump : Node2D
    {
        public Player Player;
        public const float JumpHeight = 240f;
        public const float CoyoteTime = 0.2f;
        public float Coyote = CoyoteTime;
        
        public override void _Ready()
        {
            base._Ready();
            Player = GetNode<Player>("../..");
        }

        // TODO: hold jump higher
        public override void _PhysicsProcess(float delta)
        {
            base._PhysicsProcess(delta);
            Coyote -= delta;
            var onFloor = Player.IsOnFloor();
            if (onFloor) Coyote = CoyoteTime;
            if (!Input.IsActionJustPressed("ui_jump") || Player.ActionLock.IsLocked) return;
            var canJump = onFloor || Coyote > 0;
            if (!canJump) return;
            Player.Velocity.y = -JumpHeight;
        }
    }
}
