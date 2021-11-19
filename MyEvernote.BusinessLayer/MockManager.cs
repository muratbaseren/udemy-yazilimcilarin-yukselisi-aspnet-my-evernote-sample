using MyEvernote.DataAccessLayer.Mocks;

namespace MyEvernote.BusinessLayer
{
    public class MockManager
    {
        public static void Initialize()
        {
            MockDataSets.Reset();
            MockDataSets.Seed();
        }
    }
}
