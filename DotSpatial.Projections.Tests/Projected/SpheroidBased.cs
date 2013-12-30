using System.IO;
using NUnit.Framework;
using DotSpatial.Projections;

namespace DotSpatial.Projections.Tests.Projected
{
    /// <summary>
    /// This class contains all the tests for the SpheroidBased category of Projected coordinate systems
    /// </summary>
    [TestFixture]
    public class SpheroidBased
    {
        /// <summary>
        /// Creates a new instance of the Africa Class
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            
        }

        [Test]
        public void Lambert2()
        {
            ProjectionInfo pStart = KnownCoordinateSystems.Projected.SpheroidBased.Lambert2;
            Tester.TestProjection(pStart);
        }


        [Test]
        [Ignore("Verify this test")]
        public void Lambert2Wide()
        {
            ProjectionInfo pStart = KnownCoordinateSystems.Projected.SpheroidBased.Lambert2Wide;
            Tester.TestProjection(pStart);
        }
    }
}