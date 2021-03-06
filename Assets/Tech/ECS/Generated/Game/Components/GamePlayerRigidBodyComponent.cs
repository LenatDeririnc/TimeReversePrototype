//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PlayerRigidBodyComponent playerRigidBody { get { return (PlayerRigidBodyComponent)GetComponent(GameComponentsLookup.PlayerRigidBody); } }
    public bool hasPlayerRigidBody { get { return HasComponent(GameComponentsLookup.PlayerRigidBody); } }

    public void AddPlayerRigidBody(UnityEngine.Rigidbody newValue) {
        var index = GameComponentsLookup.PlayerRigidBody;
        var component = (PlayerRigidBodyComponent)CreateComponent(index, typeof(PlayerRigidBodyComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePlayerRigidBody(UnityEngine.Rigidbody newValue) {
        var index = GameComponentsLookup.PlayerRigidBody;
        var component = (PlayerRigidBodyComponent)CreateComponent(index, typeof(PlayerRigidBodyComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePlayerRigidBody() {
        RemoveComponent(GameComponentsLookup.PlayerRigidBody);
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

    static Entitas.IMatcher<GameEntity> _matcherPlayerRigidBody;

    public static Entitas.IMatcher<GameEntity> PlayerRigidBody {
        get {
            if (_matcherPlayerRigidBody == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PlayerRigidBody);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlayerRigidBody = matcher;
            }

            return _matcherPlayerRigidBody;
        }
    }
}
