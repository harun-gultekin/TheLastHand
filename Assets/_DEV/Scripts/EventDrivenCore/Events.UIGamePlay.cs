namespace LastHand
{
    public partial class Events
    {
        public static class UIGamePlay
        {
            public static Event OnPuzzleClose = new Event(nameof(OnPuzzleClose));
            public static Event OnCraneClose = new Event(nameof(OnCraneClose));
        }
    }
}