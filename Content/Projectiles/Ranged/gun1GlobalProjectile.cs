using Terraria;
using Terraria.ModLoader;
using testmod.Common.Players;
using testmod.Content.Items.Weapons.Ranged;

namespace testmod.Content.Projectiles.Ranged
{
    public sealed class gun1GlobalProjectile : GlobalProjectile
    {
        // A separate copy is required for every projectile.
        public override bool InstancePerEntity => true;

        private bool firedFromGun1;
        private bool hitEnemy;

        public override void OnSpawn(
            Projectile projectile,
            Terraria.DataStructures.IEntitySource source)
        {
            if (source is Terraria.DataStructures.EntitySource_ItemUse_WithAmmo itemSource &&
                itemSource.Item.ModItem is gun1)
            {
                firedFromGun1 = true;
            }
        }

        public override void OnHitNPC(
            Projectile projectile,
            NPC target,
            NPC.HitInfo hit,
            int damageDone)
        {
            if (!firedFromGun1 || hitEnemy)
            {
                return;
            }

            hitEnemy = true;

            Player owner = Main.player[projectile.owner];
            owner.GetModPlayer<gun1player>().RegisterHit();
        }

        public override void OnKill(Projectile projectile, int timeLeft)
        {
            if (!firedFromGun1 || hitEnemy)
            {
                return;
            }

            Player owner = Main.player[projectile.owner];
            owner.GetModPlayer<gun1player>().RegisterMiss();
        }
    }
}
