using ECS.Systems.TimeManagement.Bullet;
using ECS.Systems.TimeManagement.Player;
using ECS.Systems.TimeManagement.Player.Input;
using ECS.Systems.TimeManagement.Rigidbody;

namespace ECS.Systems.Features
{
    public sealed class TimelineFeature : Feature
    {
        public TimelineFeature(Contexts contexts)
        {
            //PlayerTimeline
            Add(new PlayerTimeLineSystem(contexts));
            Add(new PlayerMovementTimeSpeedSystem(contexts));
            Add(new PlayerFireTimeSpeedSystem(contexts));
            Add(new UndoPlayerTimelineSystem(contexts));
            Add(new WritePlayerTimelineSystem(contexts));

            //BulletTimeline
            Add(new OnBulletInitReactiveSystem(contexts));
            Add(new UndoBulletTimeLineSystem(contexts));
            Add(new WriteBulletTimeLineSystem(contexts));
            
            //RigidbodyTimeline
            Add(new UndoRigidbodyImpulseTimelineSystem(contexts));
            Add(new WriteRigidbodyImpulseTimelineSystem(contexts));
        }
    }
}