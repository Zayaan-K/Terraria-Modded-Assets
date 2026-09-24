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
            

            // combat
            Item.damage = 360;
            Item.crit = 19;
            Item.knockBack = 0.5f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.noMelee = true;
            Item.useTime = 15;
            Item.useAnimation = 15;
            
            
            // inventory
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
            int projectileCount = Main.rand.Next(5,8);
            const float spread = 0.18f; 

            for (int i = 0; i < projectileCount; i++)
            {
                float angle = (i - (projectileCount - 1) / 2f) * spread;
                Vector2 shotVelocity = velocity.RotatedBy(angle);

                Projectile.NewProjectile(
                    source,
                    position,
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
                "placeholding"

            ));
        }
        

            
        

        
        
    }
}

