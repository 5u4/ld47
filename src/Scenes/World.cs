using System;
using Godot;
using ld47.Instances.Player;
using ld47.Utils;

namespace ld47.Scenes
{
    public class World : Node2D
    {
        public Position2D Spawn;

        public override void _Ready()
        {
            base._Ready();
            Spawn = GetNode<Position2D>("Spawn");

            Emitter.Instance.Connect(nameof(Emitter.NewPlayerSignal), this, nameof(InitializePlayer));

            InitializePlayer();
        }

        public void InitializePlayer()
        {
            var player = CreatePlayer();
            AddChild(player);
            player.GlobalPosition = Spawn.GlobalPosition;
        }
        
        public Player CreatePlayer()
        {
            if (!(GD.Load<PackedScene>("res://Instances/Player/Player.tscn").Instance() is Player player))
                throw new Exception();
            return player;
        }
    }
}
