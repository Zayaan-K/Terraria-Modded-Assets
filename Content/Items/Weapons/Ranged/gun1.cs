using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testmod.Common.Players;

namespace testmod.Content.Items.Weapons.Ranged
{
    public sealed class gun1 : ModItem
    {
        private const int BaseUseTime = 30;
        private const float MaximumSpeedMultiplier = 40f;
        private const int HitsToMaximumSpeed = 30;

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1f;

            Item.damage = 77;
            Item.crit = 24;
            Item.knockBack = 0.75f;
            Item.DamageType = DamageClass.Ranged;

            Item.noMelee = true;

            Item.useTime = BaseUseTime;
            Item.useAnimation = BaseUseTime;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTurn = false;

            Item.UseSound = SoundID.Item11;
            Item.channel = false;

            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Bullet;

            Item.maxStack = 1;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Master;
        }

        public override float UseSpeedMultiplier(Player player)
        {
            gun1player gunPlayer =
                player.GetModPlayer<gun1player>();

            float progress = MathHelper.Clamp(
                gunPlayer.HitStreak / (float)HitsToMaximumSpeed,
                0f,
                1f
            );
            
            float curvedProgress =
                System.MathF.Pow(progress, 1.5f);

            return MathHelper.Lerp(
                1f,
                MaximumSpeedMultiplier,
                curvedProgress
            );
        }

        public override void ModifyShootStats(
            Player player,
            ref Vector2 position,
            ref Vector2 velocity,
            ref int type,
            ref int damage,
            ref float knockback)
        {
            gun1player gunPlayer = player.GetModPlayer<gun1player>();

            float spreadDegrees = gunPlayer.HitStreak * 0.45f;
            float spreadRadians = MathHelper.ToRadians(spreadDegrees);

            velocity = velocity.RotatedBy(
                Main.rand.NextFloat(-spreadRadians, spreadRadians)
            );
            
            if (type == ProjectileID.Bullet)
            {
                type = ProjectileID.BulletHighVelocity;
            }
            
            if (type == ProjectileID.ChlorophyteBullet)
            {
                damage = (int)(damage * 0.60f);
            }
        }

        public override bool CanConsumeAmmo(
            Item ammo,
            Player player)
        {
            // 25% chance to preserve ammunition.
            return !Main.rand.NextBool(4);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Revolver)
                .AddIngredient(ItemID.ChlorophyteBar, 18)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void ModifyTooltips(
            List<TooltipLine> tooltips)
        {
            gun1player gunPlayer =
                Main.LocalPlayer.GetModPlayer<gun1player>();

            tooltips.Add(new TooltipLine(
                Mod,
                "Gun1Tooltip",
                "25% chance to not consume ammo\n" +
                "Converts Musket Balls into High Velocity Bullets\n" +
                "Consecutive hits increase firing speed\n" +
                "Reduced damage with chlorophyte bullets" 
            ));
        }
    }
}