using Godot;
using ld47.Utils;

namespace ld47.Scenes
{
    public class UI : Control
    {
        public int DeathCount = 0;

        public override void _Ready()
        {
            base._Ready();
            Emitter.Instance.Connect(nameof(Emitter.DeathSignal), this, nameof(OnDeath));
        }

        public void EnableSuicide()
        {
            GetNode<Control>("Instructions/Suicide").Show();
        }
        
        public void OnDeath()
        {
            DeathCount++;
            var label = GetNode<Label>("DeathCount");
            label.Text = DeathCount.ToString();
            GetNode<AnimationPlayer>("AnimationPlayer").Play("Shake");
        }
    }
}
