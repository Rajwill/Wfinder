using NUnit.Framework;
using WordFinderConsole;

namespace WFinder.Test
{
    public class Test
    {
        private FinderHelper board;

        [SetUp]
        public void Setup()
        {
            string[] list = { "abcold", "fgwioi", "chilln", "pqnsdd", "uvdxya", };
            board = new FinderHelper(list);

        }


        [Test]
        public void Can_Find_One_Word()
        {
            var word = "cold";
            var retval = board.Find(new[] { word });
            Assert.That(1, Is.EqualTo(retval.Count()));
        }

        [Test]
        public void Can_Find_Multiple_Word()
        {
            var retval = board.Find(new[] { "cold", "wind", "snow", "chill" });
            Assert.That(3, Is.EqualTo(retval.Count()));
        }

        [Test]
        public void Can_not_Find_Multiple_Word_Empty()
        {
            var retval = board.Find(new[] { "", " " });
            Assert.That(0, Is.EqualTo(retval.Count()));
        }

        [Test]
        public void Can_Not_Find_Because_Does_Not_Exist()
        {
            var retval = board.Find(new[] { "fearless", "solario" });
            Assert.That(0, Is.EqualTo(retval.Count()));
        }

        [Test]
        public void Throw_ArgumentNullException_Wrong_Input()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _board = new FinderHelper(null!);
            });
        }

        [Test]
        public void Throw_ArgumentException_Wrong_Input()
        {
            var list = new List<string>();
            for (int i = 0; i < 65; i++)
            {
                list.Add($"{i}");
            }
            Assert.Throws<ArgumentException>(() =>
            {
                var _board = new FinderHelper(list);
            });
        }
    }
}
