using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class ReketCard : BaseCard
    {
        public float PolicemanAppearChance;
        public float MoneyIncreaseValue;

        public override void StartEffect()
        {
            base.StartEffect();

            PersonManager.Instance.ChangeMutationChance(PersonType.Policeman, PolicemanAppearChance);
            MoneyManager.Instance.MoneyIncreaseValue += MoneyIncreaseValue;
        }

        public override void StopEffect()
        {
            base.StopEffect();

            PersonManager.Instance.ChangeMutationChance(PersonType.Policeman, -PolicemanAppearChance);
            MoneyManager.Instance.MoneyIncreaseValue -= MoneyIncreaseValue;
        }
    }
}