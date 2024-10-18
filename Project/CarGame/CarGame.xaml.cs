using GameCenter.Project.CarGame.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;



namespace GameCenter.Project.CarGame
{
   
    public partial class CarGame : Window
    {
        private readonly PlayerCar _playerCar;
        private readonly List<Obstacle> _obstacles;
        private readonly Random _random;
        private readonly int _carSpeed = 15;
        private int _score = 0;
        private DispatcherTimer? _timer;
        private readonly string videoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Videos", "C:\\yarden\\c# project\\GameCenter\\GameCenter\\Images\\carGameVideo.mp4");
        private readonly string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "C:\\yarden\\c# project\\GameCenter\\GameCenter\\Images\\Boom.png");


        public CarGame() 
        {
            InitializeComponent();

            BackgroundVideo.Source = new Uri(videoPath, UriKind.Relative);

            _playerCar = new PlayerCar(200, 300, _carSpeed, playerCarImage);
            _obstacles = new List<Obstacle>();
            _random = new Random();

            Subscribe();

            score.Text = $"Score: {_score}";
        }

        private void Subscribe()
        {
            Activated += StartGame;
            Closing += OnWindowClosing;
            Closed += OnWindowClosed;
            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Play();
        }

        private void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            _timer?.Stop();
            BackgroundVideo.Stop();
        }

        private void StartGame(object? sender, EventArgs e)
        {
            _timer = new() { Interval = TimeSpan.FromMilliseconds(20) };
            _timer.Tick += GameLoop;
            _timer.Tick += VisibilityCheckTimer_Tick;
            _timer.Start();
        }

        private void OnWindowClosed(object? sender, EventArgs e)
        {
            StopGameTimer();
        }

        private void VisibilityCheckTimer_Tick(object? sender, EventArgs e)
        {
            CheckImageVisibility();
        }

        private void CheckImageVisibility()
        {
            List<Image> imagesToRemove = new List<Image>();

            foreach (UIElement imageElement in gameCanvas.Children)
            {
                if (imageElement is Image image && image != playerCarImage)
                {
                    Point imagePosition = image.TranslatePoint(new Point(0, 0), gameCanvas);

                    if (!IsImageVisible(imagePosition, image.Width, image.Height))
                    {
                        _score++;
                        score.Text = $"Score: {_score}";

                        imagesToRemove.Add(image);
                    }
                }
            }

            foreach (Image imageToRemove in imagesToRemove)
            {
                gameCanvas.Children.Remove(imageToRemove);
            }
        }

        private bool IsImageVisible(Point position, double width, double height)
        {
            double windowWidth = ActualWidth;
            double windowHeight = ActualHeight;

            return position.X + width > 0 && position.X < windowWidth &&
                   position.Y + height > 0 && position.Y < windowHeight;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    _playerCar.IsLeftKeyPressed = true;
                    break;
                case Key.Right:
                    _playerCar.IsRightKeyPressed = true;
                    break;
            }
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    _playerCar.IsLeftKeyPressed = false;
                    break;
                case Key.Right:
                    _playerCar.IsRightKeyPressed = false;
                    break;
            }
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            _playerCar.Move();

            GenerateObstacles();

            foreach (Obstacle obstacle in _obstacles)
            {
                obstacle.Move();

                double collisionBuffer = 5;
                if (IsCarHitAnObstacle(collisionBuffer, obstacle))
                {
                    BackgroundVideo.Stop();
                    EndGame();
                    break;
                }
            }
        }

        private void EndGame()
        {
            StopGameTimer();
            ShowScore();
            Close();
        }

        private bool IsCarHitAnObstacle(double collisionBuffer, Obstacle obstacle)
        {
            return _playerCar.Representation.Margin.Left + _playerCar.Representation.Width - collisionBuffer >= obstacle.Representation.Margin.Left + collisionBuffer
                                && _playerCar.Representation.Margin.Left + collisionBuffer <= obstacle.Representation.Margin.Left + obstacle.Representation.Width - collisionBuffer
                                && _playerCar.Representation.Margin.Top + collisionBuffer <= obstacle.Representation.Margin.Top + obstacle.Representation.Height - collisionBuffer
                                && _playerCar.Representation.Margin.Top + _playerCar.Representation.Height - collisionBuffer >= obstacle.Representation.Margin.Top + collisionBuffer;
        }

        private void GenerateObstacles()
        { 
            if (_random.Next(0, 50) == 1)
            {
                Image obstacleImage = new()
                {
                    Source = new BitmapImage(new Uri(imagePath)),
                    Width = 50,
                    Height = 50
                };

                Obstacle obstacle = new(_random.Next(0, (int)Width), 0, 2, obstacleImage);
                _obstacles.Add(obstacle);
                gameCanvas.Children.Add(obstacleImage);
            }
        }

        private void ShowScore()
        {
            string baseMsg = $"You got {_score} points,";
            string message = _score switch
            {
                < 10 => "try better next time!",
                <= 50 => "nice score!",
                _ => "you are the champ!!!"
            };

            MessageBox.Show($"{baseMsg} {message}", "Game Over!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void StopGameTimer()
        {
            _timer?.Stop();
        }

        private void BackgroundVideo_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundVideo.Position = TimeSpan.Zero;
            BackgroundVideo.Play();
        }
    }
}
