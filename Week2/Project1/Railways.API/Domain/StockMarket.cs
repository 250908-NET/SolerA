namespace Railways.Domain
{
    public static class StockMarket
    {
        public static readonly List<int> Track = new()
        {
            0, 10, 20, 30, 40, 50,
            60, 70, 80, 90, 100, 110, 120,
            135, 150, 165, 180, 200, 220, 245,
            270, 300, 330, 360, 400, 450, 500
        };

        // Finds the index in Track whose value is closest to the given price.
        public static int GetClosestIndex(int price)
        {
            int closestIndex = 0;
            int smallestDiff = Math.Abs(Track[0] - price);

            for (int i = 1; i < Track.Count; i++)
            {
                int diff = Math.Abs(Track[i] - price);
                if (diff < smallestDiff)
                {
                    smallestDiff = diff;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }

        public static int MoveUp(int currentIndex, int steps)
        {
            int newIndex = currentIndex + steps;
            if (newIndex >= Track.Count) newIndex = Track.Count - 1;
            return newIndex;
        }

        public static int MoveDown(int currentIndex, int steps)
        {
            int newIndex = currentIndex - steps;
            if (newIndex < 0) newIndex = 0;
            return newIndex;
        }
    }
}