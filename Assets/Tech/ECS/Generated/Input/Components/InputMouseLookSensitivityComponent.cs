//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity mouseLookSensitivityEntity { get { return GetGroup(InputMatcher.MouseLookSensitivity).GetSingleEntity(); } }
    public MouseLookSensitivity mouseLookSensitivity { get { return mouseLookSensitivityEntity.mouseLookSensitivity; } }
    public bool hasMouseLookSensitivity { get { return mouseLookSensitivityEntity != null; } }

    public InputEntity SetMouseLookSensitivity(float newValue) {
        if (hasMouseLookSensitivity) {
            throw new Entitas.EntitasException("Could not set MouseLookSensitivity!\n" + this + " already has an entity with MouseLookSensitivity!",
                "You should check if the context already has a mouseLookSensitivityEntity before setting it or use context.ReplaceMouseLookSensitivity().");
        }
        var entity = CreateEntity();
        entity.AddMouseLookSensitivity(newValue);
        return entity;
    }

    public void ReplaceMouseLookSensitivity(float newValue) {
        var entity = mouseLookSensitivityEntity;
        if (entity == null) {
            entity = SetMouseLookSensitivity(newValue);
        } else {
            entity.ReplaceMouseLookSensitivity(newValue);
        }
    }

    public void RemoveMouseLookSensitivity() {
        mouseLookSensitivityEntity.Destroy();
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

    public MouseLookSensitivity mouseLookSensitivity { get { return (MouseLookSensitivity)GetComponent(InputComponentsLookup.MouseLookSensitivity); } }
    public bool hasMouseLookSensitivity { get { return HasComponent(InputComponentsLookup.MouseLookSensitivity); } }

    public void AddMouseLookSensitivity(float newValue) {
        var index = InputComponentsLookup.MouseLookSensitivity;
        var component = (MouseLookSensitivity)CreateComponent(index, typeof(MouseLookSensitivity));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMouseLookSensitivity(float newValue) {
        var index = InputComponentsLookup.MouseLookSensitivity;
        var component = (MouseLookSensitivity)CreateComponent(index, typeof(MouseLookSensitivity));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMouseLookSensitivity() {
        RemoveComponent(InputComponentsLookup.MouseLookSensitivity);
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

    static Entitas.IMatcher<InputEntity> _matcherMouseLookSensitivity;

    public static Entitas.IMatcher<InputEntity> MouseLookSensitivity {
        get {
            if (_matcherMouseLookSensitivity == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.MouseLookSensitivity);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherMouseLookSensitivity = matcher;
            }

            return _matcherMouseLookSensitivity;
        }
    }
}
