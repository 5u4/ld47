using Godot;

namespace ld47.Instances.Player
{
    public class ParticlesEmit : Particles2D
    {
        public async void Emit(float sec)
        {
            if (Emitting) return;
            Emitting = true;
            await ToSignal(GetTree().CreateTimer(sec), "timeout");
            Emitting = false;
        }
    }
}
