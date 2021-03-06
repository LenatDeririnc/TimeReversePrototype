//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class SignalsEntity {

    public TriggerEntitySignalComponent triggerEntitySignal { get { return (TriggerEntitySignalComponent)GetComponent(SignalsComponentsLookup.TriggerEntitySignal); } }
    public bool hasTriggerEntitySignal { get { return HasComponent(SignalsComponentsLookup.TriggerEntitySignal); } }

    public void AddTriggerEntitySignal(GameEntity newSender, GameEntity newGetter) {
        var index = SignalsComponentsLookup.TriggerEntitySignal;
        var component = (TriggerEntitySignalComponent)CreateComponent(index, typeof(TriggerEntitySignalComponent));
        component.Sender = newSender;
        component.Getter = newGetter;
        AddComponent(index, component);
    }

    public void ReplaceTriggerEntitySignal(GameEntity newSender, GameEntity newGetter) {
        var index = SignalsComponentsLookup.TriggerEntitySignal;
        var component = (TriggerEntitySignalComponent)CreateComponent(index, typeof(TriggerEntitySignalComponent));
        component.Sender = newSender;
        component.Getter = newGetter;
        ReplaceComponent(index, component);
    }

    public void RemoveTriggerEntitySignal() {
        RemoveComponent(SignalsComponentsLookup.TriggerEntitySignal);
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

    static Entitas.IMatcher<SignalsEntity> _matcherTriggerEntitySignal;

    public static Entitas.IMatcher<SignalsEntity> TriggerEntitySignal {
        get {
            if (_matcherTriggerEntitySignal == null) {
                var matcher = (Entitas.Matcher<SignalsEntity>)Entitas.Matcher<SignalsEntity>.AllOf(SignalsComponentsLookup.TriggerEntitySignal);
                matcher.componentNames = SignalsComponentsLookup.componentNames;
                _matcherTriggerEntitySignal = matcher;
            }

            return _matcherTriggerEntitySignal;
        }
    }
}
