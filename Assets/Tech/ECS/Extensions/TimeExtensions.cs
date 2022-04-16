namespace ECS.Extensions
{
    public static class TimeExtensions
    {
        public static float ScaledTimeSpeed(this TimeContext context)
        {
            return context.timeSpeed.Value * UnityEngine.Time.deltaTime;
        }

        public static float TickRateMod(this TimeContext context)
        {
            return context.time.Value % context.tickRate.Value;
        }

        public static float TickRateDivideRatio(this TimeContext context)
        {
            return context.TickRateMod() / context.tickRate.Value;
        }
    }
}