using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using testmod.Content.Projectiles.Magic;

namespace testmod.Content.Items.Weapons.Magic
{
    public class EndlessVoid : ModItem
    {
        public override void SetDefaults()
        {
            // Sprite & box
            Item.width = 40;
            Item.height = 54;
            Item.scale = 1f;
            Item.useTurn = true;

            // Use behavior
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item84;
            Item.autoReuse = true;
            Item.channel = false;

            // Combat
            Item.damage = 360;
            Item.crit = 19;
            Item.knockBack = 0.5f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.noMelee = true;
            Item.useTime = 15;
            Item.useAnimation = 15;

            // Inventory
            Item.maxStack = 1;
            Item.value = Item.sellPrice(gold: 44);
            Item.rare = ItemRarityID.Master;
            Item.consumable = false;

            Item.shoot = ModContent.ProjectileType<EndlessVoidProjectile>();
            Item.shootSpeed = 8f;
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
            int projectileCount = Main.rand.Next(4, 7); // 4, 5, or 6

            if (velocity.LengthSquared() == 0f)
                return false;

            Vector2 forward = Vector2.Normalize(velocity);
            Vector2 sideways = new Vector2(-forward.Y, forward.X);

            for (int i = 0; i < projectileCount; i++)
            {
                float angle = Main.rand.NextFloat(-0.35f, 0.35f);
                Vector2 shotVelocity = velocity.RotatedBy(angle);

                float forwardOffset = Main.rand.NextFloat(0f, 55f);
                float sidewaysOffset = Main.rand.NextFloat(-35f, 35f);

                Vector2 shotPosition =
                    position +
                    forward * forwardOffset +
                    sideways * sidewaysOffset;

                Projectile.NewProjectile(
                    source,
                    shotPosition,
                    shotVelocity,
                    type,
                    damage,
                    knockback,
                    player.whoAmI);
            }

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(
                Mod,
                "placeholder",
                "placeholding"));
        }
    }
}