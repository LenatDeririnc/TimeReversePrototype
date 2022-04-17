namespace ECS.Extensions
{
    public static class TimeExtensions
    {
        public static float ScaledTimeSpeed(this TimeContext context)
        {
            return context.timeSpeed.Value * UnityEngine.Time.deltaTime;
        }
    }
}