namespace CH.DVDCentral.BL.Test
{
    [TestClass]
    public class utRating
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, RatingManager.Load().Count);
        }
    }
}