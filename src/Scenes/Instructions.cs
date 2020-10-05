using Godot;
using ld47.Instances.Player;

namespace ld47.Scenes
{
    public class Instructions : HBoxContainer
    {
        public float CountDown;
        public Color Color;
        public bool Fading;

        public override void _Ready()
        {
            base._Ready();
            Color = Modulate;
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
            UpdateFade();
            CountDown = Fading ? Mathf.Max(CountDown - delta, -2) : Mathf.Min(CountDown + delta, 1);
            if (CountDown < -1.5) Fading = false; 
            Color.a = Mathf.Clamp(CountDown, 0, 1);
            Modulate = Color;
        }

        public void UpdateFade()
        {
            if (Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right") ||
                Input.IsActionPressed("ui_jump") || Input.IsActionPressed("ui_suicide")) Fading = true;
        }
    }
}
