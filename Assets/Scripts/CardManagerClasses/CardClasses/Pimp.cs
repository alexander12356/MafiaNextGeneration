using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class Pimp : BaseCard
    {
        public float DeathRate;
        public float MoneyIncreaseValue;

        public override void StartEffect()
        {
            base.StartEffect();

            PersonManager.Instance.DeadPersonTime -= DeathRate;
            MoneyManager.Instance.MoneyIncreaseValue += MoneyIncreaseValue;
        }

        public override void StopEffect()
        {
            base.StopEffect();

            PersonManager.Instance.DeadPersonTime += DeathRate;
            MoneyManager.Instance.MoneyIncreaseValue -= MoneyIncreaseValue;
        }
    }
}