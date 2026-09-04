using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TestMod.Content.Projectiles;

namespace TestMod.Content.Items.Weapons
{

        public sealed class gun1 : ModItem
        {
        
            public override void SetStaticDefaults() 
            {
            
                Item.width = 32;
                Item.height = 32;
                Item.scale = 1f;
                Item.useTurn = true;
                
                Item.damage = 20;
                Item.crit = 4;
                Item.knockBack = 0f;
                Item.DamageType = DamageClass.Magic;
                
                Item.noMelee = true;
                
                Item.useTime = 3;
                Item.useAnimation = 3;
                
                Item.maxStack = 1;
                Item.value = Item.sellPrice(gold: 4);
                Item.rare = ItemRarityID.Master;                                
                
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.UseSound = SoundID.Item11;
                Item.autoReuse = true;
                Item.channel = false;                
            
            
            }
            public override bool Shoot(
                Player player,
                EntitySource_ItemUse_WithAmmo source,
                Vector2 position,
                Vector2 velocity,
                int type,
                int damage,
                float knockback){
                
                
                }
                
                
                public override void addRecipes(){
                    CreateRecipe(){
                        .AddIngredient(ItemID.Revolver, 1)
                        .AddIngredient(ItemID.ChlorophyteBar, 18)
                        .AddTile(TileID.WorkBenches)
                        .Register();                    
                    }
                
                }
                
                public override void ModifyTooltips(List<TooltipLine> tooltips)
                {
                tooltips.Add(new TooltipLine(
                                            Mod,
                                            "gun1tooltip",
                                            "the tooltip"
                                            ));
        }     
        
        
        
        }
 
 
 }       