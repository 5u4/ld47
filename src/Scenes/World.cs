using System;
using Godot;
using ld47.Environments.CheckPoint;
using ld47.Instances.Player;
using ld47.Utils;

namespace ld47.Scenes
{
    public class World : Node2D
    {
        public Position2D Spawn;
        public Node2D Corpses;
        public Camera2D Camera;
        public CheckPoint CheckPoint;

        public override void _Ready()
        {
            base._Ready();
            Spawn = GetNode<Position2D>("Spawn");
            Corpses = GetNode<Node2D>("Corpses");
            Camera = GetNode<Camera2D>("Camera2D");

            Emitter.Instance.Connect(nameof(Emitter.NewPlayerSignal), this, nameof(OnNewPlayer));
            Emitter.Instance.Connect(nameof(Emitter.ActivateCheckPoint), this, nameof(OnActivateCheckPoint));

            SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            var player = CreatePlayer();
            AddChild(player);
            player.GlobalPosition = CheckPoint?.Spawn.GlobalPosition ?? Spawn.GlobalPosition;
            AssignCamera(player);
        }
        
        public Player CreatePlayer()
        {
            if (!(GD.Load<PackedScene>("res://Instances/Player/Player.tscn").Instance() is Player player))
                throw new Exception();
            return player;
        }

        private void OnNewPlayer()
        {
            SpawnPlayer();
        }

        private void AssignCamera(Node2D target)
        {
            var parent = Camera.GetParent();
            parent?.RemoveChild(Camera);
            target.AddChild(Camera);
        }

        private void OnActivateCheckPoint(CheckPoint checkPoint)
        {
            CheckPoint?.Disable();
            CheckPoint = checkPoint;
            CheckPoint.Enable();
        }
    }
}
