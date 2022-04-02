//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public BackMovementComponent backMovement { get { return (BackMovementComponent)GetComponent(InputComponentsLookup.BackMovement); } }
    public bool hasBackMovement { get { return HasComponent(InputComponentsLookup.BackMovement); } }

    public void AddBackMovement(UnityEngine.Vector3 newValue) {
        var index = InputComponentsLookup.BackMovement;
        var component = (BackMovementComponent)CreateComponent(index, typeof(BackMovementComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBackMovement(UnityEngine.Vector3 newValue) {
        var index = InputComponentsLookup.BackMovement;
        var component = (BackMovementComponent)CreateComponent(index, typeof(BackMovementComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBackMovement() {
        RemoveComponent(InputComponentsLookup.BackMovement);
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

    static Entitas.IMatcher<InputEntity> _matcherBackMovement;

    public static Entitas.IMatcher<InputEntity> BackMovement {
        get {
            if (_matcherBackMovement == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.BackMovement);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherBackMovement = matcher;
            }

            return _matcherBackMovement;
        }
    }
}