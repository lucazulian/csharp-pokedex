using CsharpPokedex.Domain.Models;

namespace CsharpPokedex.Domain.UnitTests.Helpers
{
    public static class PokemonBasicInformationFixture
    {
        public static PokemonBasicInformation CreateDummy()
        {
            return TestFixture.Create<PokemonBasicInformation>();
        }

        public static PokemonBasicInformation CreateFullDummy()
        {
            return new PokemonBasicInformation
            {
                Name = "bulbasaur",
                Description =
                    "A strange seed was planted on its back at birth. The plant sprouts and grows with this POKéMON.",
                Habitat = "grassland",
                IsLegendary = false
            };
        }
    }
}