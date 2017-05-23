using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class DealersCard : BaseCard
    {
        public float MoneyIncreaseValue;
        public float MafiaDeathRate;

        public override void StartEffect()
        {
            base.StartEffect();

            PersonManager.Instance.MafiaDeathRate += MafiaDeathRate;
            MoneyManager.Instance.MoneyIncreaseValue += MoneyIncreaseValue;
        }

        public override void StopEffect()
        {
            base.StopEffect();

            PersonManager.Instance.MafiaDeathRate -= MafiaDeathRate;
            MoneyManager.Instance.MoneyIncreaseValue -= MoneyIncreaseValue;
        }
    }
}