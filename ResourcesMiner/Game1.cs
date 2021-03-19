using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ResourcesMiner
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int TileWidth = 64;
        private Rectangle _tile;

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
            _tile = new Rectangle(Convert.ToInt32(_minerPos.X), Convert.ToInt32(_minerPos.Y), TileWidth, TileWidth);

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

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _minerPos.X -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _minerPos.X += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _minerPos.Y -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _minerPos.Y += 5;
            }

            _tile.X = Convert.ToInt32(_minerPos.X);
            _tile.Y = Convert.ToInt32(_minerPos.Y);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            
            //Drawing miner
            _spriteBatch.Draw(_drillBase, _tile, Color.White);
            _spriteBatch.Draw(_chassisBase, _tile, Color.White);
            _spriteBatch.Draw(_bodyBase, _tile, Color.White);
            
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