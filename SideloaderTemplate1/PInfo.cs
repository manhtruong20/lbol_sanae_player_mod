using HarmonyLib;

namespace SanaeStar
{
    public static class PInfo
    {
        public const string GUID = "SanaePlayable.sTaRGazer";

        public const string Name = "SanaePlayable";

        public const string version = "1.0.0";

        public static readonly Harmony harmony = new Harmony("SanaePlayable.sTaRGazer");
    }
}