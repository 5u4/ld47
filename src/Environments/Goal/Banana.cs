using Godot;
using ld47.Instances.Player;

namespace ld47.Environments.Goal
{
    public class Banana : Area2D
    {
        public override void _Ready()
        {
            base._Ready();
            GetNode<AnimatedSprite>("AnimatedSprite").Play();
            Connect("body_entered", this, nameof(OnBodyEntered));
        }

        private void OnBodyEntered(Node body)
        {
            if (!(body is Player player) || player.Die.Enabled) return;
            GetTree().ChangeScene("res://Scenes/Credits.tscn");
        }
    }
}
