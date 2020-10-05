using Godot;

namespace ld47.Scenes
{
    public class Instructions : HBoxContainer
    {
        public float CountDown;
        public Color Color;
        public bool Falling;

        public override void _Ready()
        {
            base._Ready();
            Color = Modulate;
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
            CountDown = Falling ? Mathf.Max(CountDown - delta, -2) : Mathf.Min(CountDown + delta, 1);
            if (CountDown < -1.5f) Falling = false;
            Color.a = Mathf.Clamp(CountDown, 0, 1);
            Modulate = Color;
        }

        public override void _Input(InputEvent @event)
        {
            base._Input(@event);
            Falling = @event is InputEventKey;
        }
    }
}
