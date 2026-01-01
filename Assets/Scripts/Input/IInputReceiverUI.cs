namespace Input
{
    public interface IInputReceiverUI : IInputReceiver
    {
        public int UILayer { get; }
        public int UIOrder { get; }
    }
}