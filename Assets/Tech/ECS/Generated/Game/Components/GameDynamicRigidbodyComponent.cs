//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public DynamicRigidbodyComponent dynamicRigidbody { get { return (DynamicRigidbodyComponent)GetComponent(GameComponentsLookup.DynamicRigidbody); } }
    public bool hasDynamicRigidbody { get { return HasComponent(GameComponentsLookup.DynamicRigidbody); } }

    public void AddDynamicRigidbody(UnityEngine.Rigidbody newValue) {
        var index = GameComponentsLookup.DynamicRigidbody;
        var component = (DynamicRigidbodyComponent)CreateComponent(index, typeof(DynamicRigidbodyComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDynamicRigidbody(UnityEngine.Rigidbody newValue) {
        var index = GameComponentsLookup.DynamicRigidbody;
        var component = (DynamicRigidbodyComponent)CreateComponent(index, typeof(DynamicRigidbodyComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDynamicRigidbody() {
        RemoveComponent(GameComponentsLookup.DynamicRigidbody);
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

    static Entitas.IMatcher<GameEntity> _matcherDynamicRigidbody;

    public static Entitas.IMatcher<GameEntity> DynamicRigidbody {
        get {
            if (_matcherDynamicRigidbody == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.DynamicRigidbody);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDynamicRigidbody = matcher;
            }

            return _matcherDynamicRigidbody;
        }
    }
}