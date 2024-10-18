using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameCenter.Project.CarGame.Models
{
    public class Obstacle : GameObject
    {
        private readonly int _direction;

        public Obstacle(int x, int y, int speed, Image carImage) : base(x, y, speed, carImage)
        {
            Random rand = new();
            _direction = rand.Next(-1, 2);
        }

        public override void Move()
        {
            Y += Speed;
            X += _direction * Speed;
            Draw();
        }
    }
}
