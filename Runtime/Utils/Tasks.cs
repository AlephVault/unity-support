using System.Threading.Tasks;

namespace AlephVault.Unity.Support
{
    namespace Utils
    {
        public static class Tasks
        {
            public static async Task Blink()
            {
                await Task.Yield();
                // Task.Delay(TimeSpan.FromMilliseconds(1));
            }
        }
    }
}