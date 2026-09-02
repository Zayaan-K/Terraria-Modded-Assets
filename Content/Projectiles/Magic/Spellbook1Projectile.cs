using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace testmod.Content.Projectiles.Magic
{
    public class Spellbook1Projectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;

            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
        }

        public override void OnHitNPC(
            NPC target,
            NPC.HitInfo hit,
            int damageDone)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.PinkCrystalShard 
                );
            }

            SoundEngine.PlaySound(
                SoundID.Item27,
                Projectile.Center
            );
        }
    }
}