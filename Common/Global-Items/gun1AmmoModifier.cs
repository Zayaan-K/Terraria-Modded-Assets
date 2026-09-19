using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using testmod.Content.Items.Weapons.Ranged;

namespace testmod.Common.GlobalItems
{
    public sealed class gun1AmmoModifier : GlobalItem
    {
        private const float ChlorophyteDamageMultiplier = 0.65f;

        public override void PickAmmo(
            Item weapon,
            Item ammo,
            Player player,
            ref int type,
            ref float speed,
            ref StatModifier damage,
            ref float knockback)
        {
            bool isGun1 = weapon.type == ModContent.ItemType<gun1>();

            bool isChlorophyteBullet = ammo.type == ItemID.ChlorophyteBullet;

            if (isGun1 && isChlorophyteBullet)
            {
                damage *= ChlorophyteDamageMultiplier;
            }
        }
    }
}
