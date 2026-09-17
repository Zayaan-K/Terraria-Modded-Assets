using Terraria;
using Terraria.ModLoader;
using TestMod.Content.Items.Weapons;
using TestMod.Content.Players;

namespace TestMod.Content.Projectiles
{
    public sealed class Gun1GlobalProjectile : GlobalProjectile
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
                itemSource.Item.ModItem is Gun1)
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
            owner.GetModPlayer<Gun1Player>().RegisterHit();
        }

        public override void OnKill(Projectile projectile, int timeLeft)
        {
            if (!firedFromGun1 || hitEnemy)
            {
                return;
            }

            Player owner = Main.player[projectile.owner];
            owner.GetModPlayer<Gun1Player>().RegisterMiss();
        }
    }
}