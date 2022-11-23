using System;
using Surrounding.Interactables.Types.Accumulations;

namespace Heroes.Accumulations
{
    public class HeroAccumulations
    {
        public HeroGold Gold { get; } = new();
        public HeroAmmo Ammo { get; } = new();

        public void Pickup(Accumulation accumulation)
        {
            var collected = accumulation.TryPickup();

            if (collected == null) return;

            if (TryUpdateAccumulations(collected))
                accumulation.MarkPickuped();
        }

        private bool TryUpdateAccumulations(Accumulated accumulated)
        {
            Func<int, bool> action = accumulated.Type switch {
                AccumulationType.Coin => UpdateGold,
                AccumulationType.Ammo => UpdateAmmo,
                _ => throw new ArgumentOutOfRangeException()
            };

            return action(accumulated.Quantity);
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
