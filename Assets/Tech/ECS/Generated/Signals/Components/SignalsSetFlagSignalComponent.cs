//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class SignalsEntity {

    public SetFlagSignal setFlagSignal { get { return (SetFlagSignal)GetComponent(SignalsComponentsLookup.SetFlagSignal); } }
    public bool hasSetFlagSignal { get { return HasComponent(SignalsComponentsLookup.SetFlagSignal); } }

    public void AddSetFlagSignal(bool newValue, System.Action<bool> newDelegate) {
        var index = SignalsComponentsLookup.SetFlagSignal;
        var component = (SetFlagSignal)CreateComponent(index, typeof(SetFlagSignal));
        component.Value = newValue;
        component.Delegate = newDelegate;
        AddComponent(index, component);
    }

    public void ReplaceSetFlagSignal(bool newValue, System.Action<bool> newDelegate) {
        var index = SignalsComponentsLookup.SetFlagSignal;
        var component = (SetFlagSignal)CreateComponent(index, typeof(SetFlagSignal));
        component.Value = newValue;
        component.Delegate = newDelegate;
        ReplaceComponent(index, component);
    }

    public void RemoveSetFlagSignal() {
        RemoveComponent(SignalsComponentsLookup.SetFlagSignal);
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

    static Entitas.IMatcher<SignalsEntity> _matcherSetFlagSignal;

    public static Entitas.IMatcher<SignalsEntity> SetFlagSignal {
        get {
            if (_matcherSetFlagSignal == null) {
                var matcher = (Entitas.Matcher<SignalsEntity>)Entitas.Matcher<SignalsEntity>.AllOf(SignalsComponentsLookup.SetFlagSignal);
                matcher.componentNames = SignalsComponentsLookup.componentNames;
                _matcherSetFlagSignal = matcher;
            }

            return _matcherSetFlagSignal;
        }
    }
}
