using Terraria.ModLoader;

namespace testmod.Common.Players
{
    public sealed class gun1player : ModPlayer
    {
        public const int MaxHitStreak = 5;

        public int HitStreak { get; private set; }

        public void RegisterHit()
        {
            if (HitStreak < MaxHitStreak)
            {
                HitStreak++;
            }
        }

        public void RegisterMiss()
        {
            HitStreak = 0;
        }

        public override void Initialize()
        {
            HitStreak = 0;
        }

        public override void UpdateDead()
        {
            HitStreak = 0;
        }
    }
}
