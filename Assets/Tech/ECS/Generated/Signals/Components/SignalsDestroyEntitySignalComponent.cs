//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class SignalsEntity {

    public DestroyEntitySignalComponent destroyEntitySignal { get { return (DestroyEntitySignalComponent)GetComponent(SignalsComponentsLookup.DestroyEntitySignal); } }
    public bool hasDestroyEntitySignal { get { return HasComponent(SignalsComponentsLookup.DestroyEntitySignal); } }

    public void AddDestroyEntitySignal(GameEntity newEntityToDestroy) {
        var index = SignalsComponentsLookup.DestroyEntitySignal;
        var component = (DestroyEntitySignalComponent)CreateComponent(index, typeof(DestroyEntitySignalComponent));
        component.EntityToDestroy = newEntityToDestroy;
        AddComponent(index, component);
    }

    public void ReplaceDestroyEntitySignal(GameEntity newEntityToDestroy) {
        var index = SignalsComponentsLookup.DestroyEntitySignal;
        var component = (DestroyEntitySignalComponent)CreateComponent(index, typeof(DestroyEntitySignalComponent));
        component.EntityToDestroy = newEntityToDestroy;
        ReplaceComponent(index, component);
    }

    public void RemoveDestroyEntitySignal() {
        RemoveComponent(SignalsComponentsLookup.DestroyEntitySignal);
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
public sealed partial class SignalsMatcher {

    static Entitas.IMatcher<SignalsEntity> _matcherDestroyEntitySignal;

    public static Entitas.IMatcher<SignalsEntity> DestroyEntitySignal {
        get {
            if (_matcherDestroyEntitySignal == null) {
                var matcher = (Entitas.Matcher<SignalsEntity>)Entitas.Matcher<SignalsEntity>.AllOf(SignalsComponentsLookup.DestroyEntitySignal);
                matcher.componentNames = SignalsComponentsLookup.componentNames;
                _matcherDestroyEntitySignal = matcher;
            }

            return _matcherDestroyEntitySignal;
        }
    }
}