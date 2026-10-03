namespace Magna_TestApplication.Models
{
    public enum SequencePhase
    {
        Idle,           // Waiting for a command
        Acknowledged,   // Ack sent, waiting for PLC to clear
        Complete        // Finished, ready for next cycle
    }

    public class SequenceState
    {
        public string Name { get; set; }
        public SequencePhase Phase { get; set; } = SequencePhase.Idle;
        public int LastCommand { get; set; } = 0;
        public int LastAck { get; set; } = 0;

        public override string ToString() =>
            $"{Name}: Phase={Phase}, LastCmd={LastCommand}, LastAck={LastAck}";
    }
}