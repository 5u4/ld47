using Godot;
using System.Linq;
using ld47.Instances.Player;

namespace ld47.Environments.Traps.Doors
{
    public class Button : Area2D
    {
        [Export] public NodePath DoorPath;
        [Export] public bool Reversed;
        public AnimatedSprite AnimatedSprite;
        public Door Door;

        public override void _Ready()
        {
            base._Ready();
            Door = GetNode<Door>(DoorPath);
            if (Reversed) Door.TurnOn(); else Door.TurnOff();
            AnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            AnimatedSprite.Play("TurnOff");
            Connect("body_entered", this, nameof(OnBodyEntered));
            Connect("body_exited", this, nameof(OnBodyExited));
        }

        private void TurnOn()
        {
            AnimatedSprite.Play("TurnOn");
            if (Reversed) Door.TurnOff(); else Door.TurnOn();
        }

        private void TurnOff()
        {
            AnimatedSprite.Play("TurnOff");
            if (Reversed) Door.TurnOn(); else Door.TurnOff();
        }

        private void OnBodyEntered(Node body)
        {
            if (!(body is Player)) return;
            TurnOn();
        }

        private void OnBodyExited(Node body)
        {
            if (!(body is Player player) 
                || GetOverlappingBodies().OfType<Player>().Any(p => p != player)) return;
            TurnOff();
        }
    }
}
