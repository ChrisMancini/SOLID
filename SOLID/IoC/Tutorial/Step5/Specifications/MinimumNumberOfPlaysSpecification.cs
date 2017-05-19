using SOLID.IoC.Tutorial.Step5.SpecificationPattern;

namespace SOLID.IoC.Tutorial.Step5.Specifications
{
    public class MinimumNumberOfPlaysSpecification : CompositeSpecification<Game>
    {
        private readonly long _minimumNumberOfPlays;

        public MinimumNumberOfPlaysSpecification(long minimumNumberOfPlays)
        {
            _minimumNumberOfPlays = minimumNumberOfPlays;
        }

        public override bool IsSatisfiedBy(Game game)
        {
            return game.Plays >= _minimumNumberOfPlays;
        }
    }
}