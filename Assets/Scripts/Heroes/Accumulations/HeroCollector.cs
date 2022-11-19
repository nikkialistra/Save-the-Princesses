using System;
using Surrounding.Collectables;

namespace Heroes.Accumulations
{
    public class HeroCollector
    {
        public HeroGold Gold { get; } = new();
        public HeroAmmo Ammo { get; } = new();

        public void Pickup(Collectable collectable)
        {
            var collected = collectable.TryPickup();

            if (collected == null) return;

            if (TryUpdateAccumulations(collected))
                collectable.MarkPickuped();
        }

        private bool TryUpdateAccumulations(Collected collected)
        {
            Func<int, bool> action = collected.Type switch {
                CollectableType.Coin => UpdateGold,
                CollectableType.Ammo => UpdateAmmo,
                _ => throw new ArgumentOutOfRangeException()
            };

            return action(collected.Quantity);
        }

        private bool UpdateGold(int quantity)
        {
            Gold.Add(quantity);

            return true;
        }

        private bool UpdateAmmo(int quantity)
        {
            return Ammo.TryIncrease(quantity);
        }
    }
}
