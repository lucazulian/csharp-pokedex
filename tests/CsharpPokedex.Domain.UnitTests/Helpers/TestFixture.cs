using AutoFixture;

namespace CsharpPokedex.Domain.UnitTests.Helpers
{
    public static class TestFixture
    {
        private static readonly Fixture Fixture = new();
        
        public static T Create<T>()
        {
            return Fixture.Create<T>();
        }
    }
}