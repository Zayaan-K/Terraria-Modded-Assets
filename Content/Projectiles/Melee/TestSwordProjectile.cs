using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TestMod.Content.Projectiles
{
    public sealed class TestSwordProjectile : ModProjectile
    {
        public override string Texture =>
            "TestMod/Content/Items/Weapons/TestSword";

        // Values synchronized by Terraria.
        private ref float State => ref Projectile.ai[0];
        private ref float StopDistance => ref Projectile.ai[1];
        private ref float OriginalDirection => ref Projectile.ai[2];

        // Local counters used by this projectile.
        private ref float DistanceTravelled =>
            ref Projectile.localAI[0];

        private ref float StateTimer =>
            ref Projectile.localAI[1];

        private const float SpinDuration = 45f;
        private const float TargetSearchDuration = 30f;

        private const float LockedLaunchSpeed = 33f;
        private const float HomingRange = 600f;

        private const float TravellingState = 0f;
        private const float SpinningState = 1f;
        private const float TargetingState = 2f;
        private const float StraightFlightState = 3f;

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.scale = 1.5f;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;

            Projectile.penetrate = 1;
            Projectile.timeLeft = 360;

            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            StateTimer++;

            if (State == TravellingState)
            {
                TravelToStopDistance();
                return;
            }

            if (State == SpinningState)
            {
                SpinInPlace();
                return;
            }

            if (State == TargetingState)
            {
                SelectTargetAndLaunch();
                return;
            }

            // Straight-flight state: maintain the locked direction.
            FaceTravelDirection();
        }

        private void TravelToStopDistance()
        {
            if (DistanceTravelled >= StopDistance)
            {
                Projectile.velocity = Vector2.Zero;

                State = SpinningState;
                StateTimer = 0f;
                Projectile.netUpdate = true;
                return;
            }

            float remainingDistance =
                StopDistance - DistanceTravelled;

            float currentSpeed =
                Projectile.velocity.Length();

            // Shorten the final movement step so that the sword
            // does not travel past the desired stopping distance.
            if (currentSpeed > remainingDistance)
            {
                Projectile.velocity =
                    Projectile.velocity.SafeNormalize(
                        Vector2.UnitX
                    ) * remainingDistance;
            }

            DistanceTravelled += Projectile.velocity.Length();

            FaceTravelDirection();
        }

        private void SpinInPlace()
        {
            Projectile.velocity = Vector2.Zero;
            Projectile.rotation += 0.35f;

            if (StateTimer >= SpinDuration)
            {
                State = TargetingState;
                StateTimer = 0f;
                Projectile.netUpdate = true;
            }
        }

        private void SelectTargetAndLaunch()
        {
            NPC? target = FindClosestEnemy();

            if (target != null)
            {
                LockOnAndLaunch(target);
                return;
            }

            // Remain still briefly while waiting for an enemy.
            if (StateTimer <= TargetSearchDuration)
            {
                Projectile.velocity = Vector2.Zero;
                return;
            }

            // No enemy appeared, so resume the original path.
            Player owner = Main.player[Projectile.owner];

            Vector2 outwardDirection =
                (Projectile.Center - owner.Center).SafeNormalize(
                    OriginalDirection.ToRotationVector2()
                );

            Projectile.velocity =
                outwardDirection * LockedLaunchSpeed;
            State = StraightFlightState;
            StateTimer = 0f;
            Projectile.netUpdate = true;

            FaceTravelDirection();
        }

        private void LockOnAndLaunch(NPC target)
        {
            Vector2 fallbackDirection =
                OriginalDirection.ToRotationVector2();

            Vector2 lockedDirection =
                (target.Center - Projectile.Center)
                .SafeNormalize(fallbackDirection);

            // Calculate the target direction once.
            // It will not be updated as the enemy moves.
            Projectile.velocity =
                lockedDirection * LockedLaunchSpeed;

            State = StraightFlightState;
            StateTimer = 0f;
            Projectile.netUpdate = true;

            FaceTravelDirection();
        }

        private void FaceTravelDirection()
        {
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.rotation =
                    Projectile.velocity.ToRotation()
                    + MathHelper.PiOver4
                    + MathHelper.Pi;
            }
        }

        private NPC? FindClosestEnemy()
        {
            NPC? closestEnemy = null;
            float closestDistance = HomingRange;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (!npc.CanBeChasedBy(Projectile))
                {
                    continue;
                }

                float distance = Vector2.Distance(
                    Projectile.Center,
                    npc.Center
                );

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = npc;
                }
            }

            return closestEnemy;
        }
    }
}

