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

        private const int MapWidth = 64;
        
        private GameTile[,] _map;
        
        //Movement variables
        private Vector2 _minerPos;
        private bool _canMove = true;
        private bool _moveRight;
        private bool _moveLeft;
        private bool _moveUp;
        private bool _moveDown;
        private int _movedBy;
        private int _movementSpeed = 2;
        
        //Terrain textures
        private Texture2D _grass;
        private Texture2D _dirt;
        private Texture2D _stone;
        private Texture2D _deepslate;
        private Texture2D _coal;
        private Texture2D _coalDirt;
        private Texture2D _copper;
        private Texture2D _iron;
        private Texture2D _apatite;
        private Texture2D _apatiteDeepslate;
        private Texture2D _diamond;
        private Texture2D _emerald;
        
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
        private Texture2D _drillTier2;
        private Texture2D _chassisTier2;
        private Texture2D _bodyTier2;
        private Texture2D _drillTier3;
        private Texture2D _chassisTier3;
        private Texture2D _bodyTier3;
        private Texture2D _drillTier4;
        private Texture2D _chassisTier4;
        private Texture2D _bodyTier4;
        private Texture2D _drillTier5;
        private Texture2D _chassisTier5;
        private Texture2D _bodyTier5;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.ApplyChanges();
            
            StartGame();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _grass = Content.Load<Texture2D>("Terrain/grass_block_side");
            _dirt = Content.Load<Texture2D>("Terrain/dirt");
            _stone = Content.Load<Texture2D>("Terrain/stone");
            _deepslate = Content.Load<Texture2D>("Terrain/deepslate");
            _coal = Content.Load<Texture2D>("Terrain/Ores/coal_ore");
            _coalDirt = Content.Load<Texture2D>("Terrain/Ores/coal_ore_dirt");
            _copper = Content.Load<Texture2D>("Terrain/Ores/copper_ore");
            _iron = Content.Load<Texture2D>("Terrain/Ores/iron_ore");
            _apatite = Content.Load<Texture2D>("Terrain/Ores/apatite_ore");
            _apatiteDeepslate = Content.Load<Texture2D>("Terrain/Ores/apatite_ore_deepslate");
            _diamond = Content.Load<Texture2D>("Terrain/Ores/diamond_ore");
            _emerald = Content.Load<Texture2D>("Terrain/Ores/emerald_ore");

            _drillTier1 = Content.Load<Texture2D>("Components/Tier 1/drillTier1");
            _chassisTier1 = Content.Load<Texture2D>("Components/Tier 1/chassisTier1");
            _bodyTier1 = Content.Load<Texture2D>("Components/Tier 1/bodyTier1");
            
            _drillTier2 = Content.Load<Texture2D>("Components/Tier 2/drillTier2");
            _chassisTier2 = Content.Load<Texture2D>("Components/Tier 2/chassisTier2");
            _bodyTier2 = Content.Load<Texture2D>("Components/Tier 2/bodyTier2");
            
            _drillTier3 = Content.Load<Texture2D>("Components/Tier 3/drillTier3");
            _chassisTier3 = Content.Load<Texture2D>("Components/Tier 3/chassisTier3");
            _bodyTier3 = Content.Load<Texture2D>("Components/Tier 3/bodyTier3");
            
            _drillTier4 = Content.Load<Texture2D>("Components/Tier 4/drillTier4");
            _chassisTier4 = Content.Load<Texture2D>("Components/Tier 4/chassisTier4");
            _bodyTier4 = Content.Load<Texture2D>("Components/Tier 4/bodyTier4");
            
            _drillTier5 = Content.Load<Texture2D>("Components/Tier 5/drillTier5");
            _chassisTier5 = Content.Load<Texture2D>("Components/Tier 5/chassisTier5");
            _bodyTier5 = Content.Load<Texture2D>("Components/Tier 5/bodyTier5");
            ApplyTextures();
            SetTileTexture();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (_canMove)
                {
                    _canMove = false;
                    _moveRight = true;
                }

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (_canMove)
                {
                    _canMove = false;
                    _moveLeft = true;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (_canMove)
                {
                    _canMove = false;
                    _moveUp = true;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (_canMove)
                {
                    _canMove = false;
                    _moveDown = true;
                }
            }

            if (!_canMove)
            {
                if (_movedBy < 64)
                {
                    if (_moveRight)
                    {
                        _minerPos.X -= _movementSpeed;
                        _movedBy += _movementSpeed;
                    }

                    if (_moveLeft)
                    {
                        _minerPos.X += _movementSpeed;
                        _movedBy += _movementSpeed;
                    }

                    if (_moveDown)
                    {
                        _minerPos.Y -= _movementSpeed;
                        _movedBy += _movementSpeed;
                    }

                    if (_moveUp)
                    {
                        _minerPos.Y += _movementSpeed;
                        _movedBy += _movementSpeed;
                    }
                }
                else
                {
                    _canMove = true;
                    _moveRight = false;
                    _moveLeft = false;
                    _moveUp = false;
                    _moveDown = false;
                    _movedBy = 0;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            //Drawing map
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    if(_map[i, j].Texture != null)
                        _spriteBatch.Draw(_map[i, j].Texture, new Rectangle(Convert.ToInt32(_minerPos.X+i*TileWidth), Convert.ToInt32(_minerPos.Y+j*TileWidth), TileWidth, TileWidth), Color.White);
                }
            }
            
            //Drawing miner
            _spriteBatch.Draw(_drillBase, new Rectangle(608, 340, TileWidth, TileWidth), Color.White);
            _spriteBatch.Draw(_chassisBase, new Rectangle(608, 340, TileWidth, TileWidth), Color.White);
            _spriteBatch.Draw(_bodyBase, new Rectangle(608, 340, TileWidth, TileWidth), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ApplyTextures()
        {
            //Drill
            switch (_drillTier)
            {
                case 0:
                    _drillBase = _drillTier1;
                    break;
                case 1:
                    _drillBase = _drillTier2;
                    break;
                case 2:
                    _drillBase = _drillTier3;
                    break;
                case 3:
                    _drillBase = _drillTier4;
                    break;
                case 4:
                    _drillBase = _drillTier5;
                    break;
            }
            
            //Chassis
            switch (_chassisTier)
            {
                case 0:
                    _chassisBase = _chassisTier1;
                    break;
                case 1:
                    _chassisBase = _chassisTier2;
                    break;
                case 2:
                    _chassisBase = _chassisTier3;
                    break;
                case 3:
                    _chassisBase = _chassisTier4;
                    break;
                case 4:
                    _chassisBase = _chassisTier5;
                    break;
            }
            
            //Body
            switch (_bodyTier)
            {
                case 0:
                    _bodyBase = _bodyTier1;
                    break;
                case 1:
                    _bodyBase = _bodyTier2;
                    break;
                case 2:
                    _bodyBase = _bodyTier3;
                    break;
                case 3:
                    _bodyBase = _bodyTier4;
                    break;
                case 4:
                    _bodyBase = _bodyTier5;
                    break;
            }
        }

        private void GenerateMap()
        {
            GameTile tile = null;
            Random rand = new Random();
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    double chance;
                    
                    //Base map generation
                    if (j == 0)
                        tile = new GameTile(0, new Location(i, j));
                    if (j == 1)
                        tile = new GameTile(1, new Location(i, j));
                    if (j > 1)
                        tile = new GameTile(2, new Location(i, j));
                    
                    if (j > 3)
                    {
                        tile = new GameTile(3, new Location(i, j));
                        if (j == 4)
                        {
                            chance = rand.NextDouble();
                            if (chance < 0.5)
                                tile = new GameTile(2, new Location(i, j));
                        }
                    }

                    if (j > 30)
                    {
                        tile = new GameTile(4, new Location(i, j));
                        if (j == 31)
                        {
                            chance = rand.NextDouble();
                            if (chance < 0.8)
                                tile = new GameTile(3, new Location(i, j));
                        }

                        if (j == 32)
                        {
                            chance = rand.NextDouble();
                            if (chance < 0.3)
                                tile = new GameTile(3, new Location(i, j));
                        }
                    }
                    
                    //Ores generation
                    if (j > 1 && j < 16)
                    {
                        chance = rand.NextDouble();
                        if (chance < 0.1)
                        {
                            if (j < 4)
                            {
                                tile.Type = 5;
                            }
                            else
                            {
                                tile.Type = 6;
                            }
                        }
                    }

                    if (j > 9 && j < 26)
                    {
                        chance = rand.NextDouble();
                        if (chance < 0.1)
                        {
                            tile.Type = 7;
                        }
                    }

                    if (j > 14 && j < 31)
                    {
                        chance = rand.NextDouble();
                        if (chance < 0.05)
                        {
                            tile.Type = 8;
                        }
                    }

                    if (j > 24 && j < 41)
                    {
                        chance = rand.NextDouble();
                        if (chance < 0.05)
                        {
                            if (j < 32)
                            {
                                tile.Type = 9;
                            }
                            else
                            {
                                tile.Type = 10;
                            }
                        }
                    }

                    if (j > 34 && j < 56)
                    {
                        chance = rand.NextDouble();
                        if (chance < 0.01)
                        {
                            tile.Type = 11;
                        }
                    }

                    if (j > 44 && j < 65)
                    {
                        chance = rand.NextDouble();
                        if (chance < 0.01)
                        {
                            tile.Type = 12;
                        }
                    }

                    _map[i, j] = tile;
                }
            }
        }

        private void SetTileTexture()
        {
            for(int i = 0; i < TileWidth; i++)
            {
                for (int j = 0; j < TileWidth; j++)
                {
                    switch (_map[i, j].Type)
                    {
                        case 0:
                            _map[i, j].Texture = null;
                            break;
                        case 1:
                            _map[i, j].Texture = _grass;
                            break;
                        case 2:
                            _map[i, j].Texture = _dirt;
                            break;
                        case 3:
                            _map[i, j].Texture = _stone;
                            break;
                        case 4:
                            _map[i, j].Texture = _deepslate;
                            break;
                        case 5:
                            _map[i, j].Texture = _coalDirt;
                            break;
                        case 6:
                            _map[i, j].Texture = _coal;
                            break;
                        case 7:
                            _map[i, j].Texture = _copper;
                            break;
                        case 8:
                            _map[i, j].Texture = _iron;
                            break;
                        case 9:
                            _map[i, j].Texture = _apatite;
                            break;
                        case 10:
                            _map[i, j].Texture = _apatiteDeepslate;
                            break;
                        case 11:
                            _map[i, j].Texture = _diamond;
                            break;
                        case 12:
                            _map[i, j].Texture = _emerald;
                            break;
                    }
                }
            }
        }

        private void StartGame()
        {
            _map = new GameTile[MapWidth, MapWidth];
            GenerateMap();
            _drillTier = 0;
            _chassisTier = 0;
            _bodyTier = 0;
            _minerPos.X = -MapWidth*TileWidth / 2 + _graphics.PreferredBackBufferWidth/2 + TileWidth/2;
            _minerPos.Y = 5*TileWidth + 20;
        }
    }
}