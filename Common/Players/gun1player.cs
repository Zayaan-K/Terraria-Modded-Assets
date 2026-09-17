using Terraria.ModLoader;

namespace testmod.Common.Players
{
    public class gun1player : ModPlayer
    {
        public int ConsecutiveHits;
        public int HitResetTimer;

        public void RegisterHit()
        {
            ConsecutiveHits++;
            HitResetTimer = 60;
        }

        public void RegisterMiss()
        {
            ConsecutiveHits = 0;
            HitResetTimer = 0;
        }

        public override void PostUpdate()
        {
            if (HitResetTimer > 0)
                HitResetTimer--;
            else
                ConsecutiveHits = 0;
        }
    }
}