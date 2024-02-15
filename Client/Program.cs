using SFML.Window;
using SFML.Graphics;
using System.Reflection;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            uint height = 200;
            uint width = height * 16 / 9;


            RenderWindow window = new RenderWindow(new VideoMode(width, height), "Multiplayer Game");
            
            window.Closed += (object sender, EventArgs e) =>
            {
                window.Close();
            };

            window.KeyPressed += (object sender, KeyEventArgs e) =>{ };
            window.KeyReleased += (object sender, KeyEventArgs e) => { };
            window.MouseButtonPressed += (object sender, MouseButtonEventArgs e) => { };
            window.MouseButtonReleased += (object sender, MouseButtonEventArgs e) => { };
            window.MouseWheelScrolled += (object sender, MouseWheelScrollEventArgs e) => { };
            window.MouseMoved += (object sender, MouseMoveEventArgs e) => { };

            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(Color.Red);
                window.Display();
            }
        }
    }
}
