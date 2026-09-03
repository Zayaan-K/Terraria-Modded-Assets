using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TestMod.Content.Projectiles;

namespace TestMod.Content.Items.Weapons
{
    public sealed class TestSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 2.5f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useTurn = true;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 73;
            Item.knockBack = 8f;
            Item.crit = 13;

            Item.value = Item.buyPrice(gold: 50);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;

            Item.shoot =
                ModContent.ProjectileType<TestSwordProjectile>();

            Item.shootSpeed = 12f;
        }

        public override void UseStyle(
            Player player,
            Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                int cursorDirection =
                    Main.MouseWorld.X >= player.Center.X ? 1 : -1;

                player.ChangeDir(cursorDirection);
            }
        }

        public override bool Shoot(
            Player player,
            EntitySource_ItemUse_WithAmmo source,
            Vector2 position,
            Vector2 velocity,
            int type,
            int damage,
            float knockback)
        {
            Vector2 fallbackDirection =
                new Vector2(player.direction, 0f);

            Vector2 aimDirection =
                velocity.SafeNormalize(fallbackDirection);

            float projectileSpeed = velocity.Length();
            float spread = MathHelper.ToRadians(30f);

            float stopDistance = Vector2.Distance(
                player.Center,
                Main.MouseWorld
            );

            for (int i = -1; i <= 1; i++)
            {
                Vector2 projectileVelocity =
                    aimDirection.RotatedBy(spread * i)
                    * projectileSpeed;

                Projectile.NewProjectile(
                    source,
                    player.Center,
                    projectileVelocity,
                    type,
                    damage,
                    knockback,
                    player.whoAmI,
                    ai0: 0f,
                    ai1: stopDistance,
                    ai2: projectileVelocity.ToRotation()
                );
            }

            return false;
        }
    }
}

