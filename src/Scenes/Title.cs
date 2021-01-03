using Godot;

namespace ld47.Scenes
{
	public class Title : Node2D
	{
		public override void _Ready()
		{
			base._Ready();
			GetNode<AnimatedSprite>("AnimatedSprite").Play();
		}

		public override void _Input(InputEvent @event)
		{
			base._Input(@event);
			if (!(@event is InputEventKey)) return;
			GetTree().ChangeScene("res://Scenes/Game.tscn");
		}
	}
}
