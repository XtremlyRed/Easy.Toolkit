
using Easy.Toolkit.Connect;

namespace Easy.Toolkit
{
    /// <summary>
    ///  Connect Builder
    /// </summary>
    public static class ConnectBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IConnectConfiguration Builder()
        {
            return new ConnectConfiguration();
        }
    }
}
