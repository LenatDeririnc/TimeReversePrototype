//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity gamepadEntity { get { return GetGroup(InputMatcher.Gamepad).GetSingleEntity(); } }

    public bool isGamepad {
        get { return gamepadEntity != null; }
        set {
            var entity = gamepadEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isGamepad = true;
                } else {
                    entity.Destroy();
                }
            }
        }
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
public partial class InputEntity {

    static readonly GamepadComponent gamepadComponent = new GamepadComponent();

    public bool isGamepad {
        get { return HasComponent(InputComponentsLookup.Gamepad); }
        set {
            if (value != isGamepad) {
                var index = InputComponentsLookup.Gamepad;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : gamepadComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<InputEntity> _matcherGamepad;

    public static Entitas.IMatcher<InputEntity> Gamepad {
        get {
            if (_matcherGamepad == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.Gamepad);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherGamepad = matcher;
            }

            return _matcherGamepad;
        }
    }
}
