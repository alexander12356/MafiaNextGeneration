using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class InsiderCard : BaseCard
    {
        public float MafiaArrestDecrease;

        public override void StartEffect()
        {
            base.StartEffect();

            PersonManager.Instance.MafiaArrestChance -= MafiaArrestDecrease;
        }

        public override void StopEffect()
        {
            base.StopEffect();

            PersonManager.Instance.MafiaArrestChance += MafiaArrestDecrease;
        }
    }
}