namespace MrNibbles1D
{
    internal static class NibblesExtensions
    {
        public static bool Is(this float[] action, int actionType)
        {
            return (int)action[0] == actionType;
        }
    }
}