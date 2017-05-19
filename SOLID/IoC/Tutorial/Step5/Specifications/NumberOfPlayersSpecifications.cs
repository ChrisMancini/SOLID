using SOLID.IoC.Tutorial.Step5.SpecificationPattern;

namespace SOLID.IoC.Tutorial.Step5.Specifications
{
    public class NumberOfPlayersSpecifications : CompositeSpecification<Game>
    {
        private readonly int _numberOfPlayers;

        public NumberOfPlayersSpecifications(int numberOfPlayers)
        {
            _numberOfPlayers = numberOfPlayers;
        }

        public override bool IsSatisfiedBy(Game game)
        {
            return game.MinNumberOfPlayers <= _numberOfPlayers && game.MaxNumberOfPlayers >= _numberOfPlayers;
        }
    }
}