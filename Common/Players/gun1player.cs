using Terraria.ModLoader;

namespace TestMod.Content.Players
{
    public sealed class Gun1Player : ModPlayer
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