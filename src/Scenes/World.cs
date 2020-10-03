using System;
using Godot;
using ld47.Environments.Corpse;
using ld47.Instances.Player;
using ld47.Utils;

namespace ld47.Scenes
{
    public class World : Node2D
    {
        public Position2D Spawn;
        public Node2D Corpses;

        public override void _Ready()
        {
            base._Ready();
            Spawn = GetNode<Position2D>("Spawn");
            Corpses = GetNode<Node2D>("Corpses");

            Emitter.Instance.Connect(nameof(Emitter.HitSignal), this, nameof(OnHit));

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

        public void RemovePlayer(Player player)
        {
            player.CollisionShape2D.Disabled = true;
            player.Hide();
            player.QueueFree();
        }

        public Corpse CreateCorpse()
        {
            if (!(GD.Load<PackedScene>("res://Environments/Corpse/Corpse.tscn").Instance() is Corpse corpse))
                throw new Exception();
            return corpse;
        }
        
        private void OnHit(Player player)
        {
            var corpse = CreateCorpse();
            CallDeferred(nameof(OnHitReady), player, corpse);
        }

        private void OnHitReady(Player player, Corpse corpse)
        {
            Corpses.AddChild(corpse);
            corpse.GlobalPosition = player.GlobalPosition;
            corpse.AnimatedSprite.FlipH = player.AnimatedSprite.FlipH;
            CallDeferred(nameof(RemovePlayer), player);
        }
    }
}
