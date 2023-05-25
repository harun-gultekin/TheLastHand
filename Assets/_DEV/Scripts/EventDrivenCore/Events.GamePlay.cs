namespace LastHand
{
    public partial class Events
    {
        public static class GamePlay
        {
            public static Event OnMinimapCollider = new Event(nameof(OnMinimapCollider));
            public static Event OnPuzzleWin = new Event(nameof(OnPuzzleWin));
            public static Event OnSteamDischarged = new Event(nameof(OnSteamDischarged));
            public static Event OnDoorControlled = new Event(nameof(OnDoorControlled));
            public static Event OnSystemControlled = new Event(nameof(OnSystemControlled));
            public static Event OnVentilationOpened = new Event(nameof(OnVentilationOpened));
            public static Event OnPressMachineOpened = new Event(nameof(OnPressMachineOpened));

            public static Event OnCraneCollider = new Event(nameof(OnCraneCollider));
            
            public static Event OnDrawerTrigger = new Event(nameof(OnDrawerTrigger));
            public static Event OnDrawerExit = new Event(nameof(OnDrawerExit));

            public static Event OnAgentDetected = new Event(nameof(OnAgentDetected));
        }
    }
}