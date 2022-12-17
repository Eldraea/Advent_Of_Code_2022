namespace Day_17_Pyroclastic_Flow
{
    public class Cycle
    {
        public int[] LastSeen { get; set; }
        public int[] LastHeight { get; set; }
        public long CycleLength { get; set; }
        public long CycleHeight { get; set; }
        public long LastDiff {get; set;}   
        
        public Cycle(int size)
        {
            LastSeen = new int[size * 32];
            LastHeight = new int[size * 32];
            CycleLength = 0;
            CycleHeight = 0;
            LastDiff = 0;

        }
        public bool IsReady(int currentRound, long rounds, int position, int width, int height)
        {
            int code = (width << 5) + position;
            if (LastSeen[code] != 0)
            {
                long dt = currentRound - LastSeen[code];
                long dh = height - LastHeight[code];
                if (LastDiff == 0)
                    LastDiff = dt;
                if (LastDiff == dt)
                    CycleLength++;
                if (CycleLength > 10 && (currentRound % LastDiff) == ((rounds - 1) % LastDiff))
                {
                    long nrounds = (rounds - currentRound) / LastDiff;
                    CycleHeight = nrounds * dh;
                    return true;
                }
            }
            else 
                CycleLength = LastDiff = 0;
            LastSeen[code] = currentRound;
            LastHeight[code] = height;
            return false;
        }
    }
}
