using Godot;
using ld47.Environments.CheckPoint;
using ld47.Instances.Player;

namespace ld47.Utils
{
    public class Emitter : Node
    {
        private Emitter()
        {
        }

        public static Emitter Instance { get; } = new Emitter();

        [Signal] public delegate void NewPlayerSignal();
        [Signal] public delegate void ActivateCheckPoint(CheckPoint checkPoint);
        [Signal] public delegate void Finish1Game();
        [Signal] public delegate void DeathSignal();
    }
}
