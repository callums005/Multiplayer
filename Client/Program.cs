using SFML.Window;
using SFML.Graphics;
using static SFML.Window.Keyboard;
using static SFML.Window.Mouse;
using SFML.System;
using System.Net.Sockets;
using System.Net;

namespace Client
{
    internal class Input
    {
        public static bool[] Keys = new bool[101];
        public static bool[] MouseButtons = new bool[5];
        public static Tuple<int, int> MousePosition = new Tuple<int, int>(0, 0);
        public static float ScrollDelta = 0.0f;

        public static bool IsKeyDown(Key keyCode)
        {
            return Keys[(int)keyCode];
        }

        public static bool IsMouseDown(Button button)
        {
            return MouseButtons[(int)button];
        }

        public static Tuple<int, int> GetMousePos()
        {
            return MousePosition;
        }

        public static float GetScrollPos()
        {
            return ScrollDelta;
        }
    }

    internal class Window
    {
        public Window(uint height = 400, string title = "Multiplayer Game")
        {
            Height = height;
            Width = Height * 16 / 9;
            Title = title;

            _Window = new RenderWindow(new VideoMode(Width, Height), Title);
            _Window.SetFramerateLimit(60);

            _Window.Closed += (object sender, EventArgs e) =>
            {
                _Window.Close();
            };

            _Window.KeyPressed += (object sender, KeyEventArgs e) => { Input.Keys[(int)e.Code] = true; };
            _Window.KeyReleased += (object sender, KeyEventArgs e) => { Input.Keys[(int)e.Code] = false; };
            _Window.MouseButtonPressed += (object sender, MouseButtonEventArgs e) => { Input.MouseButtons[(int)e.Button] = true; };
            _Window.MouseButtonReleased += (object sender, MouseButtonEventArgs e) => { Input.MouseButtons[(int)e.Button] = false; };
            _Window.MouseWheelScrolled += (object sender, MouseWheelScrollEventArgs e) => { Input.ScrollDelta = e.Delta; };
            _Window.MouseMoved += (object sender, MouseMoveEventArgs e) => { Input.MousePosition = new Tuple<int, int>(e.X, e.Y); };
        }

        public RenderWindow? _Window { get; } = null;
        public uint Height { get; set; }
        public string Title { get; }
        public uint Width { get; }
        public bool IsOpen 
        { 
            get { return _Window.IsOpen; }
        }
    }

    internal class Entity
    {
        public Entity(string name)
        {
            name = Name;
        }

        public string Name { get; set; }
        
        public Vector2f Position { get; set; }
        public Vector2f Size { get; set; }
        public Vector2f Velocity { get; set; }
        public int Rotation { get; set; }
        public Color Colour { get; set; }
        public Texture Tex { get; set; }
        public bool ColourOverTexture { get; set; } = true;
        public Shape RenderObject { get; set; }

    }

    internal class SocketClient
    { 
        public SocketClient()
        {
            Start();            
        }

        async void Start()
        {
            IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync("localhost");
            IPAddress ipAddr = ipHostInfo.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 5000);

            clientSocket = new Socket(
                    ipEndPoint.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp
                );

            await clientSocket.ConnectAsync(ipEndPoint);
        }

        async void OnUpdate()
        {

        }

        void End()
        {
            clientSocket.Shutdown(SocketShutdown.Both);
        }

        Socket clientSocket;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Window window = new Window();

            Entity test = new Entity("Test")
            {
                Position = new Vector2f(30, 30),
                Velocity = new Vector2f(2, 2),
                Size = new Vector2f(40, 40),
                Colour = Color.Green,
            };
            
            test.RenderObject = new RectangleShape()
            {
                Position = test.Position,
                Size = test.Size,
                FillColor = test.Colour,
                Rotation = test.Rotation
            };


            while (window.IsOpen)
            {
                test.Position += test.Velocity;
                test.RenderObject.Position = test.Position;

                window._Window.DispatchEvents();

                window._Window.Clear(Color.Red);

                window._Window.Draw(test.RenderObject);

                window._Window.Display();
            }
        }
        
    }
}
