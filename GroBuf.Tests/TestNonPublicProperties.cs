using GroBuf.DataMembersExtracters;

using NUnit.Framework;

namespace GroBuf.Tests
{
    [TestFixture]
    public class TestNonPublicProperties
    {
        [SetUp]
        public void SetUp()
        {
            serializer = new Serializer(new PropertiesExtractor());
        }

        [Test]
        public void Test()
        {
            var source = new CWithnonPublics("abstract", 2378, 3434, 5656, 6754, 9075, 4376) {PropIgnoredByExtractor = 7231};
            var serialize = serializer.Serialize(source);
            var result = serializer.Deserialize<CWithnonPublics>(serialize);
            Assert.AreEqual("abstract", result.AbstractProp);
            Assert.AreEqual(2378, result.A);
            Assert.AreEqual(3434, result.GetB());
            Assert.AreEqual(5656, result.C);
            Assert.AreEqual(6754, result.D);
            Assert.AreEqual(9075, result.E);
            Assert.AreEqual(4376, result.F);
            Assert.AreEqual(0, result.GetPropIgnoredByExtractor());
        }

        private Serializer serializer;

        public abstract class CWithnonPublicsBase
        {
            public abstract string AbstractProp { get; }
        }

        public class CWithnonPublics : CWithnonPublicsBase
        {
            public CWithnonPublics()
            {
            }

            public CWithnonPublics(string abstractProp, int a, int b, int c, int d, int e, int f)
            {
                AbstractProp = abstractProp;
                A = a;
                B = b;
                C = c;
                this.D = d;
                this.E = e;
                this.F = f;
            }

            public override string AbstractProp { get; }

            public int GetB()
            {
                return B;
            }

            public int GetPropIgnoredByExtractor()
            {
                return g;
            }

            public int A { get; private set; }
            public int B { private get; set; }
            public int C { get; }
            public int D { get; }
            public int E { get; }
            public int F { get; }

            public int PropIgnoredByExtractor { set => g = value; }
            private int g;
        }
    }
}