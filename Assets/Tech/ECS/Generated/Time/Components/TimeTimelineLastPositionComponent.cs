//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TimeContext {

    public TimeEntity timelineLastPositionEntity { get { return GetGroup(TimeMatcher.TimelineLastPosition).GetSingleEntity(); } }
    public TimelineLastPositionComponent timelineLastPosition { get { return timelineLastPositionEntity.timelineLastPosition; } }
    public bool hasTimelineLastPosition { get { return timelineLastPositionEntity != null; } }

    public TimeEntity SetTimelineLastPosition(Common.TimelineData newValue) {
        if (hasTimelineLastPosition) {
            throw new Entitas.EntitasException("Could not set TimelineLastPosition!\n" + this + " already has an entity with TimelineLastPositionComponent!",
                "You should check if the context already has a timelineLastPositionEntity before setting it or use context.ReplaceTimelineLastPosition().");
        }
        var entity = CreateEntity();
        entity.AddTimelineLastPosition(newValue);
        return entity;
    }

    public void ReplaceTimelineLastPosition(Common.TimelineData newValue) {
        var entity = timelineLastPositionEntity;
        if (entity == null) {
            entity = SetTimelineLastPosition(newValue);
        } else {
            entity.ReplaceTimelineLastPosition(newValue);
        }
    }

    public void RemoveTimelineLastPosition() {
        timelineLastPositionEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TimeEntity {

    public TimelineLastPositionComponent timelineLastPosition { get { return (TimelineLastPositionComponent)GetComponent(TimeComponentsLookup.TimelineLastPosition); } }
    public bool hasTimelineLastPosition { get { return HasComponent(TimeComponentsLookup.TimelineLastPosition); } }

    public void AddTimelineLastPosition(Common.TimelineData newValue) {
        var index = TimeComponentsLookup.TimelineLastPosition;
        var component = (TimelineLastPositionComponent)CreateComponent(index, typeof(TimelineLastPositionComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTimelineLastPosition(Common.TimelineData newValue) {
        var index = TimeComponentsLookup.TimelineLastPosition;
        var component = (TimelineLastPositionComponent)CreateComponent(index, typeof(TimelineLastPositionComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTimelineLastPosition() {
        RemoveComponent(TimeComponentsLookup.TimelineLastPosition);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class TimeMatcher {

    static Entitas.IMatcher<TimeEntity> _matcherTimelineLastPosition;

    public static Entitas.IMatcher<TimeEntity> TimelineLastPosition {
        get {
            if (_matcherTimelineLastPosition == null) {
                var matcher = (Entitas.Matcher<TimeEntity>)Entitas.Matcher<TimeEntity>.AllOf(TimeComponentsLookup.TimelineLastPosition);
                matcher.componentNames = TimeComponentsLookup.componentNames;
                _matcherTimelineLastPosition = matcher;
            }

            return _matcherTimelineLastPosition;
        }
    }
}
