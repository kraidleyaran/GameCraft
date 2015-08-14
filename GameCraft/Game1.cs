#region Using Statements

using System;
using System.Collections.Generic;
using System.Threading;
using GameCraft.Archive;
using GameCraft.Designer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace GameCraft
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        GameGraphics gameGraphics;
	    private GameDesigner gameDesigner;
	    private GameObserver gameObserver;
	    private GameArchive gameArchive;
	    private GameData loadGameData;
        TimeSpan oneSecond = new TimeSpan(0, 0, 1);

		public Game1 ()
		{
            gameGraphics = new GameGraphics(this);
		    gameDesigner = GameDesigner.Instance;
		    gameArchive = GameArchive.Instance;
		    loadGameData = gameArchive.LoadData(@"G:\GameCraft\testData.gcd");
		    graphics = gameGraphics.DeviceManager;
			gameObserver = GameObserver.Instance;
            

            Content.RootDirectory = "Content";	            
			graphics.IsFullScreen = false;
		    graphics.PreferredBackBufferHeight = loadGameData.GameOptions.ViewSize.Height;
		    graphics.PreferredBackBufferWidth = loadGameData.GameOptions.ViewSize.Width;
            
		}
        public Game1(string[] args)
        {
            gameGraphics = new GameGraphics(this);
            gameDesigner = GameDesigner.Instance;
            gameArchive = GameArchive.Instance;
            try
            {
                loadGameData = gameArchive.LoadData(args[0]);
            }
            catch (Exception e)
            {
                throw new Exception("Cannot Load Game Data", e);
            }
            
            graphics = gameGraphics.DeviceManager;
            gameObserver = GameObserver.Instance;


            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
        }

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
            gameDesigner.CurrentState = loadGameData.StateData.TitleState;
		    gameDesigner.TitleState = loadGameData.StateData.TitleState;
		    gameObserver.RegisterManyGameObjects(loadGameData.ObserverData.GameObjects);

            foreach (KeyValuePair<string, State> pair in loadGameData.StateData.States)
		    {
		        gameDesigner.StateList.Add(pair.Key, pair.Value);
		    }
            IsFixedTimeStep = false;
            
			// TODO: Add your initialization logic here
			base.Initialize ();
				
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);
            gameGraphics.SetSpriteBatch(spriteBatch);
		    foreach (KeyValuePair<string, GraphicContent> pair in loadGameData.GraphicsData.GraphicContents)
		    {
		        gameGraphics.LoadContent(pair.Value);
		    }

		    //TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
		    
            // For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
			    Keyboard.GetState ().IsKeyDown (Keys.Escape)) {
				Exit ();
			}
			#endif
			// TODO: Add your update logic here
            
            if ((gameTime.TotalGameTime - gameTime.ElapsedGameTime) >= oneSecond)
		    {
                gameDesigner.Update(gameTime);
                gameGraphics.Update(gameTime);
		    }
            
            
		    
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
            
			graphics.GraphicsDevice.Clear (Color.DarkGray);
		    gameGraphics.SpriteBatch.Begin();
            gameGraphics.Draw();
            gameGraphics.SpriteBatch.End();
			//TODO: Add your drawing code here
            
			base.Draw (gameTime);
		}
	}
}

