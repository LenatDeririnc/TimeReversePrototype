//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TriggeredByComponent triggeredBy { get { return (TriggeredByComponent)GetComponent(GameComponentsLookup.TriggeredBy); } }
    public bool hasTriggeredBy { get { return HasComponent(GameComponentsLookup.TriggeredBy); } }

    public void AddTriggeredBy(GameEntity newEntity) {
        var index = GameComponentsLookup.TriggeredBy;
        var component = (TriggeredByComponent)CreateComponent(index, typeof(TriggeredByComponent));
        component.Entity = newEntity;
        AddComponent(index, component);
    }

    public void ReplaceTriggeredBy(GameEntity newEntity) {
        var index = GameComponentsLookup.TriggeredBy;
        var component = (TriggeredByComponent)CreateComponent(index, typeof(TriggeredByComponent));
        component.Entity = newEntity;
        ReplaceComponent(index, component);
    }

    public void RemoveTriggeredBy() {
        RemoveComponent(GameComponentsLookup.TriggeredBy);
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
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTriggeredBy;

    public static Entitas.IMatcher<GameEntity> TriggeredBy {
        get {
            if (_matcherTriggeredBy == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TriggeredBy);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTriggeredBy = matcher;
            }

            return _matcherTriggeredBy;
        }
    }
}