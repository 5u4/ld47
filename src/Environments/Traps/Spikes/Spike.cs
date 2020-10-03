using Godot;
using ld47.Instances.Player;
using ld47.Utils;

namespace ld47.Environments.Traps.Spikes
{
    public class Spike : StaticBody2D
    {
        public Area2D HitBox;
        public const float DeadJumpSpeed = -800f;

        public override void _Ready()
        {
            base._Ready();
            HitBox = GetNode<Area2D>("HitBox");
            HitBox.Connect("body_entered", this, nameof(OnBodyEnter));
        }

        private void OnBodyEnter(Node body)
        {
            if (!(body is Player player)) return;
            Emitter.Instance.EmitSignal(nameof(Emitter.HitSignal), player);
        }
    }
}
