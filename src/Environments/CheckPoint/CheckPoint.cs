using Godot;
using ld47.Instances.Player;
using ld47.Utils;

namespace ld47.Environments.CheckPoint
{
    public class CheckPoint : Area2D
    {
        public Position2D Spawn;
        public AnimatedSprite AnimatedSprite;
        
        public override void _Ready()
        {
            base._Ready();
            Spawn = GetNode<Position2D>("Spawn");
            AnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            AnimatedSprite.Play("Inactivated");
            Connect("body_entered", this, nameof(OnBodyEntered));
        }

        public void Enable()
        {
            AnimatedSprite.Play("Activated");
        }

        public void Disable()
        {
            AnimatedSprite.Play("Inactivated");
        }
        
        private void OnBodyEntered(Node body)
        {
            if (!(body is Player player) || player.Die.Enabled) return;
            Emitter.Instance.EmitSignal(nameof(Emitter.ActivateCheckPoint), this);
        }
    }
}
