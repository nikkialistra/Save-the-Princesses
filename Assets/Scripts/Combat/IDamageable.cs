namespace Combat
{
    public interface IDamageable
    {
        void TakeDamage(int value);
        void TakeDamageContinuously(int value, float interval);
        void StopTakingDamage();
    }
}
