using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using testmod.Content.Projectiles.Magic;


namespace testmod.Content.Items.Weapons.Magic
{
    public class Spellbook1 : ModItem
    {
        public override void SetDefaults()
        {
            
            // Sprite & box
            Item.width = 32;
            Item.height = 32;
            Item.scale = 1f;
            Item.useTurn = true;
            
            // Use behavior
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item9;
            Item.autoReuse = true;
            Item.channel = false;
            

            // combat
            Item.damage = 20;
            Item.crit = 4;
            Item.knockBack = 0f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 6;
            Item.noMelee = true;
            Item.noUseGraphic = false;
            Item.useTime = 30;
            Item.useAnimation = 30;
            
            
            // inventory
            Item.maxStack = 1;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Master;
            
            
            Item.consumable = false;
            
            Item.shoot = ModContent.ProjectileType<Spellbook1Projectile>();
            
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
            int projectileCount = Main.rand.Next(3, 6);
            Vector2 targetPosition = Main.MouseWorld;
            float speed = Main.rand.NextFloat(10f, 16f);

            for (int i = 0; i < projectileCount; i++)
            {
                Vector2 spawnPosition = new Vector2(
                    targetPosition.X + Main.rand.NextFloat(-200f, 200f),
                    targetPosition.Y - Main.rand.NextFloat(700f, 850f)
                );
                
                
                Vector2 direction = targetPosition - spawnPosition;
                direction.Normalize();
                
                Vector2 fallVelocity = direction * speed;

                int projectileIndex = Projectile.NewProjectile(
                    source,
                    spawnPosition,
                    fallVelocity,
                    type,
                    damage,
                    knockback,
                    player.whoAmI
                );
                
                float rotationOffset = MathHelper.ToRadians(
                    Main.rand.NextFloat(-10f, 10f)
                );

                Main.projectile[projectileIndex].rotation =
                    fallVelocity.ToRotation() + rotationOffset;
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
                "SpellbookTooltip",
                "the tooltip"

            ));
        }
        

            
        

        
        
    }
}

