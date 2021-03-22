﻿using Microsoft.Xna.Framework;
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
        private bool _canMove = true;
        private int _canMoveTill;

        private Vector2 _minerPos;
        private const int MapWidth = 64;
        
        /* 0 - void
         * 1 - grass
         * 2 - dirt
         * 3 - coal ore
         * 4 - copper ore
         * 5 - iron ore
         * 6 - apatite ore
         * 7 - diamond ore
         * 8 - emerald ore
         */
        private int[,] _map;
        
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
                    _minerPos.X -= TileWidth;
                    _canMove = false;
                }

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (_canMove)
                {
                    _minerPos.X += TileWidth;
                    _canMove = false;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (_canMove)
                {
                    if (_minerPos.Y < 5 * TileWidth + 20)
                    {
                        _minerPos.Y += TileWidth;
                        _canMove = false;
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (_canMove)
                {
                    
                    _minerPos.Y -= TileWidth;
                    _canMove = false;
                }
            }

            if (!_canMove)
            {
                if (_canMoveTill < 20)
                    _canMoveTill++;
                else
                {
                    _canMove = true;
                    _canMoveTill = 0;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            //Drawing map
            Texture2D texture = _dirt;
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    if (_map[i, j] == 1)
                        texture = _grass;
                    if (_map[i, j] == 2)
                        texture = _dirt;
                    if (_map[i, j] == 3)
                        texture = _stone;
                    if (_map[i, j] == 4)
                        texture = _deepslate;
                    if (_map[i, j] == 5)
                        texture = _coalDirt;
                    if (_map[i, j] == 6)
                        texture = _coal;
                    if (_map[i, j] == 7)
                        texture = _copper;
                    if (_map[i, j] == 8)
                        texture = _iron;
                    if (_map[i, j] == 9)
                        texture = _apatite;
                    if (_map[i, j] == 10)
                        texture = _diamond;
                    if (_map[i, j] == 11)
                        texture = _emerald;
                    if (_map[i, j] != 0)
                        _spriteBatch.Draw(texture, new Rectangle(Convert.ToInt32(_minerPos.X+i*TileWidth), Convert.ToInt32(_minerPos.Y+j*TileWidth), TileWidth, TileWidth), Color.White);
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
            Random rand = new Random();
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    if (j != 0)
                    {
                        double chance;
                        if(j == 1)
                            _map[i, j] = 1;
                        if (j > 1)
                        {
                            _map[i, j] = 2;
                        }
                        
                        if (j > 3)
                            _map[i, j] = 3;
                        
                        if (j == 4)
                        {
                            chance = rand.NextDouble();
                            if(chance < 0.4)
                                _map[i, j] = 2;
                        }
                        
                        if (j > 30)
                            _map[i, j] = 4;
                        if (j == 30)
                        {
                            chance = rand.NextDouble();
                            if(chance < 0.8)
                                _map[i, j] = 4;
                        }
                        if (j == 29)
                        {
                            chance = rand.NextDouble();
                            if(chance < 0.2)
                                _map[i, j] = 4;
                        }
                        
                        if (j >= 2 && j <= 15)
                        {
                            chance = rand.NextDouble();
                            if (chance < 0.1)
                            {
                                if (j > 4)
                                    _map[i, j] = 6;
                                else
                                    _map[i, j] = 5;
                            }
                        }

                        if (j >= 10 && j <= 20)
                        {
                            chance = rand.NextDouble();
                            if (chance < 0.1)
                            {
                                _map[i, j] = 7;
                            }
                        }
                        
                        if (j >= 15 && j <= 30)
                        {
                            chance = rand.NextDouble();
                            if (chance < 0.1)
                            {
                                _map[i, j] = 8;
                            }
                        }
                        
                        if (j >= 25 && j <= 40)
                        {
                            chance = rand.NextDouble();
                            if (chance < 0.05)
                            {
                                _map[i, j] = 9;
                            }
                        }
                        
                        if (j >= 35 && j <= 50)
                        {
                            chance = rand.NextDouble();
                            if (chance < 0.01)
                            {
                                _map[i, j] = 10;
                            }
                        }
                        
                        if (j >= 40 && j <= 64)
                        {
                            chance = rand.NextDouble();
                            if (chance < 0.01)
                            {
                                _map[i, j] = 11;
                            }
                        }
                    }
                }
            }
        }

        private void StartGame()
        {
            _map = new int[MapWidth, MapWidth];
            GenerateMap();
            _drillTier = 0;
            _chassisTier = 0;
            _bodyTier = 0;
            _minerPos.X = -MapWidth*TileWidth / 2 + _graphics.PreferredBackBufferWidth/2 + TileWidth/2;
            _minerPos.Y = 5*TileWidth + 20;
        }
    }
}