//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public FireInputComponent fireInput { get { return (FireInputComponent)GetComponent(InputComponentsLookup.FireInput); } }
    public bool hasFireInput { get { return HasComponent(InputComponentsLookup.FireInput); } }

    public void AddFireInput(float newValue) {
        var index = InputComponentsLookup.FireInput;
        var component = (FireInputComponent)CreateComponent(index, typeof(FireInputComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceFireInput(float newValue) {
        var index = InputComponentsLookup.FireInput;
        var component = (FireInputComponent)CreateComponent(index, typeof(FireInputComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveFireInput() {
        RemoveComponent(InputComponentsLookup.FireInput);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherFireInput;

    public static Entitas.IMatcher<InputEntity> FireInput {
        get {
            if (_matcherFireInput == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.FireInput);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherFireInput = matcher;
            }

            return _matcherFireInput;
        }
    }
}