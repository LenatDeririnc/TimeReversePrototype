//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SetFlagSignal setFlagSignal { get { return (SetFlagSignal)GetComponent(GameComponentsLookup.SetFlagSignal); } }
    public bool hasSetFlagSignal { get { return HasComponent(GameComponentsLookup.SetFlagSignal); } }

    public void AddSetFlagSignal(bool newValue, System.Action<bool> newDelegate) {
        var index = GameComponentsLookup.SetFlagSignal;
        var component = (SetFlagSignal)CreateComponent(index, typeof(SetFlagSignal));
        component.Value = newValue;
        component.Delegate = newDelegate;
        AddComponent(index, component);
    }

    public void ReplaceSetFlagSignal(bool newValue, System.Action<bool> newDelegate) {
        var index = GameComponentsLookup.SetFlagSignal;
        var component = (SetFlagSignal)CreateComponent(index, typeof(SetFlagSignal));
        component.Value = newValue;
        component.Delegate = newDelegate;
        ReplaceComponent(index, component);
    }

    public void RemoveSetFlagSignal() {
        RemoveComponent(GameComponentsLookup.SetFlagSignal);
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

    static Entitas.IMatcher<GameEntity> _matcherSetFlagSignal;

    public static Entitas.IMatcher<GameEntity> SetFlagSignal {
        get {
            if (_matcherSetFlagSignal == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.SetFlagSignal);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSetFlagSignal = matcher;
            }

            return _matcherSetFlagSignal;
        }
    }
}
