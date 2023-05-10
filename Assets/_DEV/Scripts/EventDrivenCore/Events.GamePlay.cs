namespace LastHand
{
    public partial class Events
    {
        public static class GamePlay
        {
            public static Event OnMinimapCollider = new Event(nameof(OnMinimapCollider));
            public static Event OnPuzzleWin = new Event(nameof(OnPuzzleWin));

        }
    }
}