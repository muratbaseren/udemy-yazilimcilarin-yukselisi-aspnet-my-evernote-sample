namespace MyEvernote.DataAccessLayer.MySql
{
    public class RepositoryBase
    {
        protected static object context;
        protected static object _lockSync = new object();

        protected RepositoryBase()
        {
            CreateContext();
        }

        private static void CreateContext()
        {
            if (context == null)
            {
                lock (_lockSync)
                {
                    if (context == null)
                    {
                        context = new object();
                    }
                }

            }
        }
    }
}
