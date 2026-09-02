using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace testmod.Content.Items
{
    public class SpellBook1 : ModItem
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
            //Item.UseSound = SoundID.;
            Item.autoReuse = true;
            Item.channel = false;
            Item.useTurn = false;

            // combat
            Item.damage = 20;
            Item.crit = 4;
            Item.knockBack = 0f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 6;
            Item.noMelee = true;
            Item.noUseGraphic = false;
            
            // Projectile
            //Item.shoot = ProjectileID.;
            Item.shootSpeed = 10f;
            
            
            // inventory
            Item.maxStack = 1;
            Item.value = Item.sellPrice(gold: 48);
            Item.value = Item.buyPrice(gold: 48);
            Item.rare = ItemRarityID.Master;
            
            
            Item.consumable = false;
            
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
            return true;
        }
        
        

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
        
        public override bool CanUseItem(Player player)
        {
            return player.statMana >= Item.mana;
        }
            
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
        }
        
        
    }
}