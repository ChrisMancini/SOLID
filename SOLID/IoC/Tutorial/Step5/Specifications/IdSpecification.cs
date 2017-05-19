using SOLID.IoC.Tutorial.Step5.SpecificationPattern;

namespace SOLID.IoC.Tutorial.Step5.Specifications
{
    public class IdSpecification : CompositeSpecification<Game>
    {
        private readonly long _id;

        public IdSpecification(long id)
        {
            _id = id;
        }

        public override bool IsSatisfiedBy(Game game)
        {
            return game.Id == _id;
        }
    }
}