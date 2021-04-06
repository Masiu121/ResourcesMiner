using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;

namespace ResourcesMiner
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        //Main game variables
        private Inventory _inventory;
        private decimal _money;

        //Heat variables
        private decimal _heatMax;
        private decimal _heatPercent;
        private decimal _heatStage;
        private int _timeToBurn;
        
        //Durability variables
        private decimal _health;
        private decimal _healthMax;
        private decimal _healthDecreasion;
        private bool _healthRegen;
        private decimal _baseHealthDecreasion = 0.2m;
        
        //Fuel variables
        private decimal _fuel;
        private decimal _fuelMax;
        private decimal _fuelRefill;
        private int _timeToRefill;
        private bool _fuelRefilling;
        private decimal _fuelConsumption;
        private decimal _baseFuelConsumption = 0.25m;

        //Movement variables
        private Vector2 _minerPos;
        private Vector2 _mapPos;
        private bool _canMove = true;
        private bool _moveRight;
        private bool _moveLeft;
        private bool _moveUp;
        private bool _moveDown;
        private int _movedBy;
        private int _movementSpeed;
        private int _baseMovementSpeed = 2;
        private int _miningMovementSpeed = 1;
        
        //Map variables
        private Map _map;
        private const int MapWidth = 384;
        private const int TileWidth = 64;
        private int _renderDistance = 12;

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
        private Texture2D _minedBlock;
        private Texture2D _border;
        
        //Miner tiers
        private int _drillTier;
        private int _chassisTier;
        private int _bodyTier;
        private int _coolerTier;
        
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
            _border = Content.Load<Texture2D>("Terrain/border");
            _minedBlock = Content.Load<Texture2D>("Terrain/mined_block");

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
            SetComponentTexture();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                
            }

            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                
            }

            if (Keyboard.GetState().IsKeyDown(Keys.H))
            {
                Debug.WriteLine("------------------------------------------");
                Debug.WriteLine("Move: WSAD");
                Debug.WriteLine("Properties: Q");
                Debug.WriteLine("Repair: R");
                Debug.WriteLine("Refuel: T");
                Debug.WriteLine("Sell all: Y");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Debug.WriteLine("------------------------------------------");
                Debug.WriteLine("Pos: X: " + _minerPos.X + ", Y: " + _minerPos.Y);
                Debug.WriteLine("Fuel: " + _fuel);
                Debug.WriteLine("Health: " + _health);
                Debug.WriteLine("Heat: " + _heatPercent);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                Debug.WriteLine("------------------------------------------");
                Debug.WriteLine("Coal: " + _inventory._inventory[5].Count);
                Debug.WriteLine("Copper: " + _inventory._inventory[7].Count);
                Debug.WriteLine("Iron: " + _inventory._inventory[8].Count);
                Debug.WriteLine("Apatite: " + _inventory._inventory[9].Count);
                Debug.WriteLine("Diamond: " + _inventory._inventory[11].Count);
                Debug.WriteLine("Emerald: " + _inventory._inventory[12].Count);
            }

            if (CanMove())
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Right) && _minerPos.X < MapWidth - 2)
                {
                    if (_canMove)
                    {
                        _canMove = false;
                        _moveRight = true;
                        _minerPos.X++;
                        CheckForType();
                        _fuel -= _fuelConsumption;
                        _health -= _healthDecreasion;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Left) && _minerPos.X > 1)
                {
                    if (_canMove)
                    {
                        _canMove = false;
                        _moveLeft = true;
                        _minerPos.X--;
                        CheckForType();
                        _fuel -= _fuelConsumption;
                        _health -= _healthDecreasion;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Up) && _minerPos.Y > 63)
                {
                    if (_canMove)
                    {
                        _canMove = false;
                        _moveUp = true;
                        _minerPos.Y--;
                        CheckForType();
                        _fuel -= _fuelConsumption;
                        _health -= _healthDecreasion;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down) && _minerPos.Y < MapWidth - 2)
                {
                    if (_canMove)
                    {
                        _canMove = false;
                        _moveDown = true;
                        _minerPos.Y++;
                        CheckForType();
                        _fuel -= _fuelConsumption;
                        _health -= _healthDecreasion;
                    }
                }
            }

            if (!_canMove)
            {
                if (_movedBy < 64)
                {
                    if (_moveRight)
                    {
                        _mapPos.X -= _movementSpeed;
                        _movedBy += _movementSpeed;
                    }

                    if (_moveLeft)
                    {
                        _mapPos.X += _movementSpeed;
                        _movedBy += _movementSpeed;
                    }

                    if (_moveDown)
                    {
                        _mapPos.Y -= _movementSpeed;
                        _movedBy += _movementSpeed;
                    }

                    if (_moveUp)
                    {
                        _mapPos.Y += _movementSpeed;
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

            if (_heatPercent >= 1)
            {
                if (_timeToBurn <= 0)
                {
                    _timeToBurn = 50;
                    _health--;
                }
                else
                {
                    _timeToBurn--;
                }
            }

            if (_fuelRefilling)
            {
                if (_fuel >= _fuelMax - _fuelRefill)
                {
                    _fuel = _fuelMax;
                    _fuelRefilling = false;
                }
                else
                {
                    if (_timeToRefill <= 0)
                    {
                        _fuel += _fuelRefill;
                        _timeToRefill = 20;
                    }
                    else
                    {
                        _timeToRefill++;
                    }
                }
            }

            if (_healthRegen)
            {
                _health = _healthMax;
                _healthRegen = false;
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
                    if (i > _minerPos.X-_renderDistance && i < _minerPos.X+_renderDistance && j > _minerPos.Y-_renderDistance && j < _minerPos.Y+_renderDistance)
                    {
                        if (_map.map[i, j].Texture != null)
                            _spriteBatch.Draw(_map.map[i, j].Texture,
                                new Rectangle(Convert.ToInt32(_mapPos.X + i * TileWidth),
                                    Convert.ToInt32(_mapPos.Y + j * TileWidth), TileWidth, TileWidth), Color.White);
                    }
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

        private void StartGame()
        {
            //Map
            _map = new Map(MapWidth, MapWidth);
            GenerateMap();
            _minerPos = new Vector2(MapWidth/2-1, 63);
            _mapPos.X = -MapWidth*TileWidth / 2 + _graphics.PreferredBackBufferWidth/2 + TileWidth/2;
            _mapPos.Y = -(_minerPos.Y-5)*TileWidth + 20;
            
            //Tiers
            _drillTier = 0;
            _chassisTier = 0;
            _bodyTier = 0;
            _coolerTier = 0;
            
            //Fuel
            _fuelMax = 100;
            _fuel = _fuelMax;
            _fuelRefill = 2;
            _timeToRefill = 50;
            _fuelRefilling = false;
            _fuelConsumption = _baseFuelConsumption;
            
            //Health
            _healthMax = 100;
            _health = _healthMax;
            _healthRegen = false;
            _healthDecreasion = _baseHealthDecreasion;

            //Miner
            _inventory = new Inventory();
            _movementSpeed = _baseMovementSpeed;
        }

        private void CheckForType()
        {
            GameTile tile = _map.map[Convert.ToInt32(_minerPos.X), Convert.ToInt32(_minerPos.Y)];
            tile.SetHardness();

            switch (tile.Type)
            {
                case 0:
                    _movementSpeed = _baseMovementSpeed;
                    break;
                case 1:
                    tile.Type = 13;
                    _movementSpeed = _miningMovementSpeed;
                    break;
                case 2:
                    tile.Type = 13;
                    _movementSpeed = _miningMovementSpeed;
                    break;
                case 3:
                    tile.Type = 13;
                    _movementSpeed = _miningMovementSpeed;
                    break;
                case 4:
                    tile.Type = 13;
                    _movementSpeed = _miningMovementSpeed;
                    break;
                case 5:
                    tile.Type = 13;
                    _inventory.Add(5);
                    _movementSpeed = _miningMovementSpeed;
                    break;
                case 6:
                    tile.Type = 13;
                    _inventory.Add(6);
                    _movementSpeed = _miningMovementSpeed;
                    break;
                case 7:
                    tile.Type = 13;
                    _inventory.Add(7);
                    _movementSpeed = _miningMovementSpeed;
                    break;
                case 8:
                    tile.Type = 13;
                    _inventory.Add(8);
                    _movementSpeed = _miningMovementSpeed;
                    break;
                case 9:
                    tile.Type = 13;
                    _inventory.Add(9);
                    _movementSpeed = _miningMovementSpeed;
                    break;
                case 10:
                    tile.Type = 13;
                    _inventory.Add(10);
                    _movementSpeed = _miningMovementSpeed;
                    break;
                case 11:
                    tile.Type = 13;
                    _inventory.Add(11);
                    _movementSpeed = _miningMovementSpeed;
                    break;
                case 12:
                    tile.Type = 13;
                    _inventory.Add(12);
                    _movementSpeed = _miningMovementSpeed;
                    break;
                case 13:
                    _movementSpeed = _baseMovementSpeed;
                    break;
            }
            
            SetComponentTexture();
            SetHeatPercent();
            SetFuelConsumption(tile);
        }

        private void SetFuelConsumption(GameTile tile)
        {
            switch (tile.Hardness)
            {
                case 0:
                    _fuelConsumption = _baseFuelConsumption;
                    _healthDecreasion = _baseHealthDecreasion;
                    break;
                case 1:
                    _fuelConsumption = 0.5m;
                    _healthDecreasion = 0.5m;
                    break;
                case 2:
                    _fuelConsumption = 1.0m;
                    break;
                case 3:
                    _fuelConsumption = 2.0m;
                    break;
                case 4:
                    _fuelConsumption = 2.0m;
                    break;
            }
        }

        private void SetHeatPercent()
        {
            _heatStage = Convert.ToDecimal(MapWidth) / 5.0m;
            _heatMax = Convert.ToDecimal(_coolerTier)+1 * Convert.ToDecimal(_heatStage);
            _heatPercent = Convert.ToDecimal(_minerPos.Y) / _heatMax;
        }

        private bool CanMove()
        {
            if (_fuel >= _fuelConsumption && _health >= _healthDecreasion)
                return true;
            return false;
        }

        private bool SpendMoney(decimal money)
        {
            if (_money >= money)
            {
                _money -= money;
                return true;
            }

            return false;
        }

        private void AddMapComponents()
        {
            //Air
            _map.AddComponent(new GameTile(0), 0, 63, 1);
            
            //Grass
            _map.AddComponent(new GameTile(1), 64, 64, 1);
            
            //Dirt
            _map.AddComponent(new GameTile(2), 65, 67, 1);
            
            //Stone
            _map.AddComponent(new GameTile(3), 68, 296, 1);
            _map.AddComponent(new GameTile(3), 67, 67, 0.5);
            
            //Deepslate
            _map.AddComponent(new GameTile(4), 297, 384, 1);
            _map.AddComponent(new GameTile(4), 296, 296, 0.7);
            _map.AddComponent(new GameTile(4), 295, 295, 0.3);
            
            //Coal
            _map.AddComponent(new GameTile(5), 66, 67, 0.1);
            _map.AddComponent(new GameTile(6), 68, 96, 0.1);
            
            //Copper
            _map.AddComponent(new GameTile(7), 84, 164, 0.08);
            
            //Iron
            _map.AddComponent(new GameTile(8), 104, 264, 0.07);
            
            //Apatite
            _map.AddComponent(new GameTile(9), 216, 296, 0.05);
            _map.AddComponent(new GameTile(10), 297, 316, 0.05);
            
            //Diamond
            _map.AddComponent(new GameTile(11), 304, 364, 0.02);
            
            //Emerald
            _map.AddComponent(new GameTile(12), 316, 384, 0.02);
        }

        private void SetComponentTexture()
        {
            GameTile tile;
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    tile = _map.map[i, j];
                    switch (tile.Type)
                    {
                        case 1:
                            tile.Texture = _grass;
                            break;
                        case 2:
                            tile.Texture = _dirt;
                            break;
                        case 3:
                            tile.Texture = _stone;
                            break;
                        case 4:
                            tile.Texture = _deepslate;
                            break;
                        case 5:
                            tile.Texture = _coalDirt;
                            break;
                        case 6:
                            tile.Texture = _coal;
                            break;
                        case 7:
                            tile.Texture = _copper;
                            break;
                        case 8:
                            tile.Texture = _iron;
                            break;
                        case 9:
                            tile.Texture = _apatite;
                            break;
                        case 10:
                            tile.Texture = _apatiteDeepslate;
                            break;
                        case 11:
                            tile.Texture = _diamond;
                            break;
                        case 12:
                            tile.Texture = _emerald;
                            break;
                        case 13:
                            tile.Texture = _minedBlock;
                            break;
                        case 14:
                            tile.Texture = _border;
                            break;
                    }
                }
            }
        }

        private void GenerateMap()
        {
            AddMapComponents();
            _map.GenerateMap();
        }
    }
}