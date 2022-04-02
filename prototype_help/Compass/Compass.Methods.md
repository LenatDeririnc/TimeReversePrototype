#methods

Если [[Player.MoveDirection.Value]].Speed = 0, то [[Compass]] находится в статическом положении.

Иначе

[[Compass.MoveDirrection]]

Если [[Compass.MoveDirrection]] направлен в сторону [[Compass.ForwardDirection]], то его указатель поворачивается в сторону, куда он движется. 

Если [[Compass.MoveDirrection]] попадает в зону между значениями [[Compass.RollbackZone]], то происходит откат времени до тех пор, пока [[InputDirrection.IsPressed]].