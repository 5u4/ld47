using Godot;
using ld47.Instances.Player;
using ld47.Utils;

namespace ld47.Scenes
{
    public class Game1Ending : Area2D
    {
        public AnimatedSprite AnimatedSprite;
        public bool Disabled;
        
        public override void _Ready()
        {
            base._Ready();
            AnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
            AnimatedSprite.Play();
            Connect("body_entered", this, nameof(OnBodyEntered));
            Emitter.Instance.Connect(nameof(Emitter.Finish2Game), this, nameof(OnFinish2Game));
        }

        private void OnBodyEntered(Node body)
        {
            if (!(body is Player) || Disabled) return;
            Emitter.Instance.EmitSignal(nameof(Emitter.Finish1Game));
            ((Game) GetTree().CurrentScene).Spawn.GlobalPosition = GlobalPosition;
            Hide();
            Disabled = true;
            GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
        }

        private void OnFinish2Game()
        {
            Show();
            Disabled = false;
        }
    }
}
