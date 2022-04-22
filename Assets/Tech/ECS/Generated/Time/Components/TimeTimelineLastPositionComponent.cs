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

    public bool isTimelineLastPosition {
        get { return timelineLastPositionEntity != null; }
        set {
            var entity = timelineLastPositionEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isTimelineLastPosition = true;
                } else {
                    entity.Destroy();
                }
            }
        }
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

    static readonly TimelineLastPositionComponent timelineLastPositionComponent = new TimelineLastPositionComponent();

    public bool isTimelineLastPosition {
        get { return HasComponent(TimeComponentsLookup.TimelineLastPosition); }
        set {
            if (value != isTimelineLastPosition) {
                var index = TimeComponentsLookup.TimelineLastPosition;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : timelineLastPositionComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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
