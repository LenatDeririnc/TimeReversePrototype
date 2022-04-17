//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TimeContext {

    public TimeEntity timeChangerEntity { get { return GetGroup(TimeMatcher.TimeChanger).GetSingleEntity(); } }
    public TimeChangerComponent timeChanger { get { return timeChangerEntity.timeChanger; } }
    public bool hasTimeChanger { get { return timeChangerEntity != null; } }

    public TimeEntity SetTimeChanger(Common.IVelocity newValue) {
        if (hasTimeChanger) {
            throw new Entitas.EntitasException("Could not set TimeChanger!\n" + this + " already has an entity with TimeChangerComponent!",
                "You should check if the context already has a timeChangerEntity before setting it or use context.ReplaceTimeChanger().");
        }
        var entity = CreateEntity();
        entity.AddTimeChanger(newValue);
        return entity;
    }

    public void ReplaceTimeChanger(Common.IVelocity newValue) {
        var entity = timeChangerEntity;
        if (entity == null) {
            entity = SetTimeChanger(newValue);
        } else {
            entity.ReplaceTimeChanger(newValue);
        }
    }

    public void RemoveTimeChanger() {
        timeChangerEntity.Destroy();
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

    public TimeChangerComponent timeChanger { get { return (TimeChangerComponent)GetComponent(TimeComponentsLookup.TimeChanger); } }
    public bool hasTimeChanger { get { return HasComponent(TimeComponentsLookup.TimeChanger); } }

    public void AddTimeChanger(Common.IVelocity newValue) {
        var index = TimeComponentsLookup.TimeChanger;
        var component = (TimeChangerComponent)CreateComponent(index, typeof(TimeChangerComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTimeChanger(Common.IVelocity newValue) {
        var index = TimeComponentsLookup.TimeChanger;
        var component = (TimeChangerComponent)CreateComponent(index, typeof(TimeChangerComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTimeChanger() {
        RemoveComponent(TimeComponentsLookup.TimeChanger);
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

    static Entitas.IMatcher<TimeEntity> _matcherTimeChanger;

    public static Entitas.IMatcher<TimeEntity> TimeChanger {
        get {
            if (_matcherTimeChanger == null) {
                var matcher = (Entitas.Matcher<TimeEntity>)Entitas.Matcher<TimeEntity>.AllOf(TimeComponentsLookup.TimeChanger);
                matcher.componentNames = TimeComponentsLookup.componentNames;
                _matcherTimeChanger = matcher;
            }

            return _matcherTimeChanger;
        }
    }
}