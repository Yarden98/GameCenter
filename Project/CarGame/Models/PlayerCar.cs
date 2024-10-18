using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GameCenter.Project.CarGame.Models
{
    public class PlayerCar : GameObject
    {
        public bool IsLeftKeyPressed { get; set; }
        public bool IsRightKeyPressed { get; set; }
        public PlayerCar(int x, int y, int speed, Image carImage) : base(x, y, speed, carImage) { }

        public override void Move()
        {
            if (IsLeftKeyPressed && X > 0)
                X -= Speed;

            if (IsRightKeyPressed && X < Application.Current.MainWindow.Width - Representation.Width)
                X += Speed;

            Draw();
        }
    }
}
