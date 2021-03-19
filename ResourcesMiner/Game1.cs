using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ResourcesMiner
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vector2 _minerPos;
        
        //Miner tiers
        private int _drillTier;
        private int _chassisTier;
        private int _bodyTier;
        
        //Base textures
        private Texture2D _drillBase;
        private Texture2D _chassisBase;
        private Texture2D _bodyBase;
        
        //Tiers textures
        private Texture2D _drillTier1;
        private Texture2D _chassisTier1;
        private Texture2D _bodyTier1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _drillTier1 = Content.Load<Texture2D>("drillTier1");
            _chassisTier1 = Content.Load<Texture2D>("chassisTier1");
            _bodyTier1 = Content.Load<Texture2D>("bodyTier1");
            applyTextures();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void applyTextures()
        {
            //Drill
            switch (_drillTier)
            {
                case 0:
                    _drillBase = _drillTier1;
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
            
            //Chassis
            switch (_chassisTier)
            {
                case 0:
                    _chassisBase = _chassisTier1;
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
            
            //Body
            switch (_bodyTier)
            {
                case 0:
                    _bodyBase = _bodyTier1;
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
    }
}