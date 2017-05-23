using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class BountyHunterCard : BaseCard
    {
        public float Chance;

        public override void StartEffect()
        {
            base.StartEffect();

            PersonManager.Instance.ChangeBuffChance(BuffType.BountyHunter, Chance);
        }

        public override void StopEffect()
        {
            base.StopEffect();

            PersonManager.Instance.ChangeBuffChance(BuffType.BountyHunter, -Chance);
        }
    }
}