//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TimeContext {

    public TimeEntity timeLineStackEntity { get { return GetGroup(TimeMatcher.TimeLineStack).GetSingleEntity(); } }
    public TimeLineStackComponent timeLineStack { get { return timeLineStackEntity.timeLineStack; } }
    public bool hasTimeLineStack { get { return timeLineStackEntity != null; } }

    public TimeEntity SetTimeLineStack(Tools.TimeLineStackTool.TimeLineStack newValue) {
        if (hasTimeLineStack) {
            throw new Entitas.EntitasException("Could not set TimeLineStack!\n" + this + " already has an entity with TimeLineStackComponent!",
                "You should check if the context already has a timeLineStackEntity before setting it or use context.ReplaceTimeLineStack().");
        }
        var entity = CreateEntity();
        entity.AddTimeLineStack(newValue);
        return entity;
    }

    public void ReplaceTimeLineStack(Tools.TimeLineStackTool.TimeLineStack newValue) {
        var entity = timeLineStackEntity;
        if (entity == null) {
            entity = SetTimeLineStack(newValue);
        } else {
            entity.ReplaceTimeLineStack(newValue);
        }
    }

    public void RemoveTimeLineStack() {
        timeLineStackEntity.Destroy();
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

    public TimeLineStackComponent timeLineStack { get { return (TimeLineStackComponent)GetComponent(TimeComponentsLookup.TimeLineStack); } }
    public bool hasTimeLineStack { get { return HasComponent(TimeComponentsLookup.TimeLineStack); } }

    public void AddTimeLineStack(Tools.TimeLineStackTool.TimeLineStack newValue) {
        var index = TimeComponentsLookup.TimeLineStack;
        var component = (TimeLineStackComponent)CreateComponent(index, typeof(TimeLineStackComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTimeLineStack(Tools.TimeLineStackTool.TimeLineStack newValue) {
        var index = TimeComponentsLookup.TimeLineStack;
        var component = (TimeLineStackComponent)CreateComponent(index, typeof(TimeLineStackComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTimeLineStack() {
        RemoveComponent(TimeComponentsLookup.TimeLineStack);
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

    static Entitas.IMatcher<TimeEntity> _matcherTimeLineStack;

    public static Entitas.IMatcher<TimeEntity> TimeLineStack {
        get {
            if (_matcherTimeLineStack == null) {
                var matcher = (Entitas.Matcher<TimeEntity>)Entitas.Matcher<TimeEntity>.AllOf(TimeComponentsLookup.TimeLineStack);
                matcher.componentNames = TimeComponentsLookup.componentNames;
                _matcherTimeLineStack = matcher;
            }

            return _matcherTimeLineStack;
        }
    }
}
