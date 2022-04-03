//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public BasePlayerControllerHolderComponent basePlayerControllerHolder { get { return (BasePlayerControllerHolderComponent)GetComponent(InputComponentsLookup.BasePlayerControllerHolder); } }
    public bool hasBasePlayerControllerHolder { get { return HasComponent(InputComponentsLookup.BasePlayerControllerHolder); } }

    public void AddBasePlayerControllerHolder(ECM.Controllers.BasePlayerController newValue) {
        var index = InputComponentsLookup.BasePlayerControllerHolder;
        var component = (BasePlayerControllerHolderComponent)CreateComponent(index, typeof(BasePlayerControllerHolderComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBasePlayerControllerHolder(ECM.Controllers.BasePlayerController newValue) {
        var index = InputComponentsLookup.BasePlayerControllerHolder;
        var component = (BasePlayerControllerHolderComponent)CreateComponent(index, typeof(BasePlayerControllerHolderComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBasePlayerControllerHolder() {
        RemoveComponent(InputComponentsLookup.BasePlayerControllerHolder);
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

    static Entitas.IMatcher<InputEntity> _matcherBasePlayerControllerHolder;

    public static Entitas.IMatcher<InputEntity> BasePlayerControllerHolder {
        get {
            if (_matcherBasePlayerControllerHolder == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.BasePlayerControllerHolder);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherBasePlayerControllerHolder = matcher;
            }

            return _matcherBasePlayerControllerHolder;
        }
    }
}