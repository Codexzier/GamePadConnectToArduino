using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO.Ports;
using System.Threading.Tasks;

namespace GamePadConnectToArduino
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        private SerialPort _serialPort;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            // Verwendete COM Port muss ggf. angepasst werden.
            this._serialPort = new SerialPort("COM5", 115200, Parity.None, 8, StopBits.One);
            this._serialPort.Open();
        }

        protected override void UnloadContent()
        {
            this._serialPort.DiscardOutBuffer();
            this._serialPort.Close();
        }
        
        protected override void Update(GameTime gameTime)
        {
            GamePadState state = GamePad.GetState(PlayerIndex.One);

            byte stickValue = (byte)((state.ThumbSticks.Left.X + 1) * 90);
            this._serialPort.Write(new byte[] { stickValue }, 0, 1);
            
            Task.Delay(50).Wait();
        }
    }
}
