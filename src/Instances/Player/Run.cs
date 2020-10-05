using Godot;

namespace ld47.Instances.Player
{
    public class Run : Node2D
    {
        public Player Player;
        public const float Speed = 100;
        public const float Acceleration = 0.5f;
        public const float Friction = 0.2f;
        
        public override void _Ready()
        {
            base._Ready();
            Player = GetNode<Player>("../..");
        }
        
        public override void _PhysicsProcess(float delta)
        {
            if (Player.ActionLock.IsLocked) return;
            base._PhysicsProcess(delta);
            var direction = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            Player.Velocity.x = Mathf.Abs(direction) > 0
                ? Mathf.Lerp(Player.Velocity.x, direction * Speed, Acceleration)
                : Mathf.Lerp(Player.Velocity.x, 0, Friction);
        }
    }
}
