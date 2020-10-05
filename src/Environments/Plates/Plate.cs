using Godot;
using ld47.Instances.Player;

namespace ld47.Environments.Plates
{
    public class Plate : Area2D
    {
        public Label Label;
        
        public override void _Ready()
        {
            base._Ready();
            Label = GetNode<Label>("Label");
            Connect("body_entered", this, nameof(OnBodyEntered));
            Connect("body_exited", this, nameof(OnBodyExited));
        }

        private void OnBodyEntered(Node body)
        {
            if (!(body is Player player) || player.Die.Enabled) return;
            Label.Show();
        }
        
        private void OnBodyExited(Node body)
        {
            if (!(body is Player player) || player.Die.Enabled) return;
            Label.Hide();
        }
    }
}
