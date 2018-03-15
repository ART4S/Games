using System.Drawing;

namespace AirForce
{
    public interface IGameState
    {
        void MovePlayer(Point2D movespeedModifer);
        void Restart();
        void Update();
        void PlayerFire();
    }
}