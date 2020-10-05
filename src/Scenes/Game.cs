using System;
using Godot;
using ld47.Environments.CheckPoint;
using ld47.Instances.Player;
using ld47.Utils;

namespace ld47.Scenes
{
    public class Game : Node2D
    {
        public Position2D Spawn;
        public Node2D Corpses;
        public Camera2D Camera;
        public CheckPoint CheckPoint;
        public Node2D Map;
        public Player Player;
        public UI UI;
        public RotationAnimationPlayer RotationAnimationPlayer;
        public bool EnabledSuicide;
        public bool UpsideDown;

        public override void _Ready()
        {
            base._Ready();
            Map = GetNode<Node2D>("Map");
            Spawn = Map.GetNode<Position2D>("Spawn");
            Corpses = GetNode<Node2D>("Corpses");
            Camera = GetNode<Camera2D>("MainCamera");
            UI = GetNode<UI>("CanvasLayer/UI");
            RotationAnimationPlayer = GetNode<RotationAnimationPlayer>("RotationAnimationPlayer");

            Emitter.Instance.Connect(nameof(Emitter.NewPlayerSignal), this, nameof(OnNewPlayer));
            Emitter.Instance.Connect(nameof(Emitter.ActivateCheckPoint), this, nameof(OnActivateCheckPoint));
            Emitter.Instance.Connect(nameof(Emitter.Finish1Game), this, nameof(OnFinish1Game));

            SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            Player = CreatePlayer();
            Map.AddChild(Player);
            Player.GlobalPosition = CheckPoint?.Spawn.GlobalPosition ?? Spawn.GlobalPosition;
            AssignCamera(Player);
            if (EnabledSuicide) Player.Suicide.Enabled = true;
            if (UpsideDown) Player.Rotation = Mathf.Pi;
        }
        
        public Player CreatePlayer()
        {
            if (!(GD.Load<PackedScene>("res://Instances/Player/Player.tscn").Instance() is Player player))
                throw new Exception();
            return player;
        }

        private void OnNewPlayer()
        {
            MovePlayerToCorpses();
            SpawnPlayer();
        }

        private void MovePlayerToCorpses()
        {
            if (Player == null) return;
            var pos = Player.GlobalPosition;
            var parent = Player.GetParent<Node>();
            parent.RemoveChild(Player);
            Corpses.AddChild(Player);
            Player.GlobalPosition = pos;
            Player.Velocity = Vector2.Zero;
        }

        private void AssignCamera(Node2D target)
        {
            var parent = Camera.GetParent();
            parent?.RemoveChild(Camera);
            Camera.Position = Vector2.Zero;
            target.AddChild(Camera);
        }

        private void OnActivateCheckPoint(CheckPoint checkPoint)
        {
            CheckPoint?.Disable();
            CheckPoint = checkPoint;
            CheckPoint.Enable();
        }

        private async void OnFinish1Game()
        {
            var pos = Camera.GlobalPosition;
            Camera.GetParent()?.RemoveChild(Camera);
            Camera.GlobalPosition = pos;
            AddChild(Camera);
            RotationAnimationPlayer.Start(Player);
            Player.ActionLock.Lock();
            Player.Velocity = Vector2.Zero;
            await ToSignal(RotationAnimationPlayer, "animation_finished");
            AssignCamera(Player);
            Player.GlobalPosition += Vector2.Up * 10;
            RotationAnimationPlayer.QueueFree();
            CheckPoint = null;
            
            EnabledSuicide = true;
            UpsideDown = true;
            UI.EnableSuicide();
            
            Player.Rotation = Mathf.Pi; // TODO: Add to animation
            Player.Suicide.Enabled = true;
            
            Player.ActionLock.Unlock();
        }

        private void StopPhysics()
        {
            GetTree().Paused = true;
        }

        private void EnablePhysics()
        {
            GetTree().Paused = false;
        }
    }
}
