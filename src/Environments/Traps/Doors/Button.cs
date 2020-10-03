using Godot;
using ld47.Instances.Player;

namespace ld47.Environments.Traps.Doors
{
    public class Button : Area2D
    {
        public AnimatedSprite AnimatedSprite;
        [Export] public NodePath DoorPath;
        public Door Door;

        public override void _Ready()
        {
            base._Ready();
            Door = GetNode<Door>(DoorPath);
            AnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            Connect("body_entered", this, nameof(OnBodyEntered));
            Connect("body_exited", this, nameof(OnBodyExited));
        }

        private void TurnOn()
        {
            AnimatedSprite.Play("TurnOn");
            Door.TurnOn();
        }

        private void TurnOff()
        {
            AnimatedSprite.Play("TurnOff");
            Door.TurnOff();
        }

        private void OnBodyEntered(Node body)
        {
            if (!(body is Player)) return;
            TurnOn();
        }

        private void OnBodyExited(Node body)
        {
            if (!(body is Player)) return;
            TurnOff();
        }
    }
}
