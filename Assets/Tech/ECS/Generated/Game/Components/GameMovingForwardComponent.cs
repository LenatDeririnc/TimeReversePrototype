//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MovingForwardComponent movingForward { get { return (MovingForwardComponent)GetComponent(GameComponentsLookup.MovingForward); } }
    public bool hasMovingForward { get { return HasComponent(GameComponentsLookup.MovingForward); } }

    public void AddMovingForward(float newSpeed) {
        var index = GameComponentsLookup.MovingForward;
        var component = (MovingForwardComponent)CreateComponent(index, typeof(MovingForwardComponent));
        component.Speed = newSpeed;
        AddComponent(index, component);
    }

    public void ReplaceMovingForward(float newSpeed) {
        var index = GameComponentsLookup.MovingForward;
        var component = (MovingForwardComponent)CreateComponent(index, typeof(MovingForwardComponent));
        component.Speed = newSpeed;
        ReplaceComponent(index, component);
    }

    public void RemoveMovingForward() {
        RemoveComponent(GameComponentsLookup.MovingForward);
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

    static Entitas.IMatcher<GameEntity> _matcherMovingForward;

    public static Entitas.IMatcher<GameEntity> MovingForward {
        get {
            if (_matcherMovingForward == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MovingForward);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMovingForward = matcher;
            }

            return _matcherMovingForward;
        }
    }
}
