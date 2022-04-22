namespace ECS.Extensions
{
    public static class TimeExtensions
    {
        public static float ScaledTimeSpeed(this TimeContext context)
        {
            return context.globalTimeSpeed.Value * UnityEngine.Time.deltaTime;
        }
    }
}