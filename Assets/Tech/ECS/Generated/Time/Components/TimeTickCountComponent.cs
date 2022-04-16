//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TimeContext {

    public TimeEntity tickCountEntity { get { return GetGroup(TimeMatcher.TickCount).GetSingleEntity(); } }
    public TickCountComponent tickCount { get { return tickCountEntity.tickCount; } }
    public bool hasTickCount { get { return tickCountEntity != null; } }

    public TimeEntity SetTickCount(int newValue) {
        if (hasTickCount) {
            throw new Entitas.EntitasException("Could not set TickCount!\n" + this + " already has an entity with TickCountComponent!",
                "You should check if the context already has a tickCountEntity before setting it or use context.ReplaceTickCount().");
        }
        var entity = CreateEntity();
        entity.AddTickCount(newValue);
        return entity;
    }

    public void ReplaceTickCount(int newValue) {
        var entity = tickCountEntity;
        if (entity == null) {
            entity = SetTickCount(newValue);
        } else {
            entity.ReplaceTickCount(newValue);
        }
    }

    public void RemoveTickCount() {
        tickCountEntity.Destroy();
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

    public TickCountComponent tickCount { get { return (TickCountComponent)GetComponent(TimeComponentsLookup.TickCount); } }
    public bool hasTickCount { get { return HasComponent(TimeComponentsLookup.TickCount); } }

    public void AddTickCount(int newValue) {
        var index = TimeComponentsLookup.TickCount;
        var component = (TickCountComponent)CreateComponent(index, typeof(TickCountComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTickCount(int newValue) {
        var index = TimeComponentsLookup.TickCount;
        var component = (TickCountComponent)CreateComponent(index, typeof(TickCountComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTickCount() {
        RemoveComponent(TimeComponentsLookup.TickCount);
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

    static Entitas.IMatcher<TimeEntity> _matcherTickCount;

    public static Entitas.IMatcher<TimeEntity> TickCount {
        get {
            if (_matcherTickCount == null) {
                var matcher = (Entitas.Matcher<TimeEntity>)Entitas.Matcher<TimeEntity>.AllOf(TimeComponentsLookup.TickCount);
                matcher.componentNames = TimeComponentsLookup.componentNames;
                _matcherTickCount = matcher;
            }

            return _matcherTickCount;
        }
    }
}
