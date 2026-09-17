using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testmod.Content.Players;

namespace testmod.Content.Items.Weapons
{
    public sealed class Gun1 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1f;

            Item.damage = 20;
            Item.crit = 4;
            Item.knockBack = 0.75f;
            Item.DamageType = DamageClass.Ranged;

            Item.noMelee = true;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTurn = false;

            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
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
            Gun1Player gunPlayer = player.GetModPlayer<Gun1Player>();
            return 1f + gunPlayer.HitStreak * 0.10f;
        }

        public override void ModifyShootStats(
            Player player,
            ref Vector2 position,
            ref Vector2 velocity,
            ref int type,
            ref int damage,
            ref float knockback)
        {
            if (type == ProjectileID.Bullet)
            {
                type = ProjectileID.BulletHighVelocity;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Revolver)
                .AddIngredient(ItemID.ChlorophyteBar, 18)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(
                Mod,
                "Gun1Tooltip",
                "Converts Musket Balls into High Velocity Bullets\n" +
                "Consecutive hits increase firing speed"
            ));
        }
    }
}