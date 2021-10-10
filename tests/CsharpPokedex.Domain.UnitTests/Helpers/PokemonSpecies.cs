using CsharpPokedex.Domain.Models;

namespace CsharpPokedex.Domain.UnitTests.Helpers
{
    public static class PokemonSpeciesFixture
    {
        public static PokemonSpecies CreateDummy()
        {
            return TestFixture.Create<PokemonSpecies>();
        }

        public static PokemonSpecies CreateFullDummy()
        {
            return new PokemonSpecies
            {
                Name = "bulbasaur",
                FlavorTextEntries = new[]
                {
                    new PokemonFlavorTextEntry
                    {
                        FlavorText =
                            "A strange seed was\nplanted on its\nback at birth.\fThe plant sprouts\nand grows with\nthis POKéMON.",
                        Language = new PokemonFlavorTextEntryLanguage
                        {
                            Name = "en"
                        }
                    }
                },
                Habitat = new PokemonHabitat
                {
                    Name = "grassland"
                },
                IsLegendary = false
            };
        }
    }
}