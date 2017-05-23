namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class RobberyCard : BaseCard
    {
        public float MoneyIncreaseValue;

        public override void StartEffect()
        {
            base.StartEffect();

            MoneyManager.Instance.MoneyIncreaseValue += MoneyIncreaseValue;
        }

        public override void StopEffect()
        {
            base.StopEffect();

            MoneyManager.Instance.MoneyIncreaseValue -= MoneyIncreaseValue;
        }
    }
}