using NFluent;
using NUnit.Framework;
using ScoreAnalyser.Models;

namespace ScoreAnalyser.Tests
{
    [TestFixture]
    public class ModelTest
    {
        [Test]
        public void FillPropertiesWhenAddingItem()
        {
            var musicItems = new []{new MusicItem("/random/item/path", 1, 2)};
            var page1 = new ScorePage(0);
            var scoreBoard = new ScoreBoard("/random/pdf/path", new [] {page1}, 50);
            scoreBoard.ScorePages[0].AddMusicItem("/random/other/item/path", 2, 3);
            Check.That(scoreBoard.ScorePages[0].Scaling).Equals(1);
            Check.That(scoreBoard.ScorePages[0].MusicItems[0].Path).Equals("/random/item/path");
            Check.That(scoreBoard.ScorePages[0].PageNumber).Equals(0);
        }
    }
}