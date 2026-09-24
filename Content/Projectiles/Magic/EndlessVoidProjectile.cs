using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace testmod.Content.Projectiles.Magic
{
    public class EndlessVoidProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {

            int targetIndex = (int)Projectile.ai[0] - 1;

            if (targetIndex < 0 ||
                targetIndex >= Main.maxNPCs ||
                !Main.npc[targetIndex].CanBeChasedBy(Projectile))
            {
                targetIndex = FindTarget();

                float storedTarget = targetIndex + 1f;
                if (Projectile.ai[0] != storedTarget)
                {
                    Projectile.ai[0] = storedTarget;
                    Projectile.netUpdate = true;
                }
            }

            if (targetIndex >= 0 && Projectile.velocity.LengthSquared() > 0f)
            {
                Vector2 toTarget = Main.npc[targetIndex].Center - Projectile.Center;
                if (toTarget.LengthSquared() > 0f)
                {
                    float angle = MathHelper.WrapAngle(
                        toTarget.ToRotation() - Projectile.velocity.ToRotation());


                    Projectile.velocity = Projectile.velocity.RotatedBy(angle * 0.225f);
                }
            }
            
            if (Projectile.velocity.LengthSquared() > 0f)
                Projectile.velocity += Vector2.Normalize(Projectile.velocity) * 0.0025f;

            Projectile.rotation += (Projectile.velocity.X + Projectile.velocity.Y) * 0.1f;
        }

        private int FindTarget()
        {
            int closestIndex = -1;
            float closestDistanceSquared = 500f * 500f;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (!npc.CanBeChasedBy(Projectile))
                    continue;

                float distanceSquared =
                    Vector2.DistanceSquared(Projectile.Center, npc.Center);

                if (distanceSquared < closestDistanceSquared &&
                    Collision.CanHit(Projectile.Center, 1, 1, npc.Center, 1, 1))
                {
                    closestDistanceSquared = distanceSquared;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }
    }
}