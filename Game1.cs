using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CookingSimulator
{
    public enum GameState
    {
        TitleScreen,
        MamaIntro,
        LevelSelect,
        CookingPizza,
        CookingCorndog,
        CookingPie,
        RecipeResults,
        FinalGrades
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private UIManager _uiManager;
        private Pizza _pizzaLevel;
        private Corndog _corndogLevel;
        private Pie _pieLevel;
        private GameState _currentState = GameState.TitleScreen;
        
        private bool _pizzaDone = false;
        private bool _corndogDone = false;
        private bool _pieDone = false;

        private int _pizzaScore = 0;
        private int _corndogScore = 0;
        private int _pieScore = 0;
        
        private Texture2D _titleVector;
        private Texture2D textBoxBackground; 
        private Texture2D mamaexcited, mamaHappy, mamaupset, mamaregular, mamatalking, mamaexplain;
        private SpriteFont _mainFont;
        
        private int _dialogueLine = 0; 
        private MouseState _previousMouse;
        private KeyboardState _previousKey;

        private float _bounceOffset;
        
        private string[] _mamaScript = new string[]
        {
            "Sweetie!",
            "I'll need your help cooking today.",
            "We'll be baking pizza, corndogs and some pie today!",
            "So... which one would you want to help mama with first? :D"
        };

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 650;
        }

        protected override void Initialize()
        {
            _pizzaLevel = new Pizza();
            _corndogLevel = new Corndog();
            _pieLevel = new Pie();
            base.Initialize();
        }

      protected override void LoadContent()
{
    _spriteBatch = new SpriteBatch(GraphicsDevice);
    _uiManager = new UIManager(GraphicsDevice, _spriteBatch); 
    
    _mainFont = Content.Load<SpriteFont>("MainFont");
    _uiManager.Font = _mainFont;

    
    textBoxBackground = Content.Load<Texture2D>("TextBox");
    mamaHappy = Content.Load<Texture2D>("MamaHappy");
    mamaexcited = Content.Load<Texture2D>("MamaExcited");
    mamaupset = Content.Load<Texture2D>("MamaUpset");
    mamaregular = Content.Load<Texture2D>("MamaRegular");
    mamatalking = Content.Load<Texture2D>("MamaTalking");
    mamaexplain = Content.Load<Texture2D>("MamaExplain");
    
    _titleVector = Content.Load<Texture2D>("TitleLogo");
    
    _pizzaLevel.LoadContent(Content);
    _corndogLevel.LoadContent(Content);
    _pieLevel.LoadContent(Content);
    
    _pizzaLevel.LoadBaseContent(Content);
    _corndogLevel.LoadBaseContent(Content);
    _pieLevel.LoadBaseContent(Content);
}

        protected override void Update(GameTime gameTime)
        {
            MouseState currentMouse = Mouse.GetState();
            KeyboardState currentKey = Keyboard.GetState();

            bool isClicked = currentMouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released;

            _bounceOffset = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 5f) * 10f;

            switch (_currentState)
            {
                case GameState.TitleScreen:
                    if (isClicked) 
                    {
                        _currentState = GameState.MamaIntro;
                        _dialogueLine = 0; 
                    }
                    break;

                case GameState.MamaIntro:
                    if (isClicked)
                    {
                        _dialogueLine++; 
                        
                        if (_dialogueLine > 3) 
                        {
                            _currentState = GameState.LevelSelect;
                        }
                    }
                    break;

                case GameState.LevelSelect:
                    if (currentKey.IsKeyDown(Keys.P) && !_pizzaDone) 
                    {
                        _currentState = GameState.CookingPizza;
                    }
                    else if (currentKey.IsKeyDown(Keys.C) && !_corndogDone)
                    {
                        _currentState = GameState.CookingCorndog;
                    }
                    else if (currentKey.IsKeyDown(Keys.A) && !_pieDone)
                    {
                        _currentState = GameState.CookingPie;
                    }
                    else if (_pizzaDone && _corndogDone && _pieDone)
                    {
                        _currentState = GameState.FinalGrades;
                    }
                    break;

                case GameState.CookingPizza:
                    _pizzaLevel.Update(gameTime); 

                    if (_pizzaLevel.IsLevelComplete) 
                    {
                        _pizzaDone = true;
                        _pizzaScore = _pizzaLevel.FinalLevelScore;
                        _currentState = GameState.RecipeResults;
                    }
                    break;

                case GameState.CookingCorndog:
                    _corndogLevel.Update(gameTime); 

                    if (_corndogLevel.IsLevelComplete) 
                    {
                        _corndogDone = true;
                        _corndogScore = _corndogLevel.FinalLevelScore;
                        _currentState = GameState.RecipeResults;
                    }
                    break;

                case GameState.CookingPie:
                    _pieLevel.Update(gameTime); 

                    if (_pieLevel.IsLevelComplete) 
                    {
                        _pieDone = true;
                        _pieScore = _pieLevel.FinalLevelScore;
                        _currentState = GameState.RecipeResults;
                    }
                    break;

                case GameState.RecipeResults:
                    if (isClicked)
                    {
                        _currentState = GameState.LevelSelect; 
                    }
                    break;

                case GameState.FinalGrades:
                    if (isClicked)
                    {
                    }
                    break;
            }

            _previousMouse = currentMouse;
            _previousKey = currentKey;
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); 

            _spriteBatch.Begin();

            switch (_currentState)
            {
                case GameState.TitleScreen:
                    if (_titleVector != null)
                    {
                        Vector2 logoPosition = new Vector2(200, 100 + _bounceOffset);
                        _spriteBatch.Draw(_titleVector, logoPosition, Color.White);
                    }
                    break;

                case GameState.MamaIntro:
                    Texture2D currentPortrait = mamatalking; 
                    
                    if (_dialogueLine == 1) currentPortrait = mamaexplain;
                    else if (_dialogueLine == 2) currentPortrait = mamaexcited;

                    _uiManager.DrawDialogue(textBoxBackground, currentPortrait, "Mama", _mamaScript[_dialogueLine]);
                    break;

                case GameState.LevelSelect:
                    _uiManager.DrawDialogue(textBoxBackground, mamaregular, "Mama", "Press P for Pizza, C for Corndog, A for Apple Pie!");
                    
                    string[] names = { "Pizza", "Corndog", "Apple Pie" };
                    string[][] stages = { 
                        new string[] { "Prep", "Mix", "Bake" },
                        new string[] { "Chop", "Batter", "Fry" },
                        new string[] { "Filling", "Crust", "Bake" }
                    };
                    _uiManager.DrawMenu(names, stages, -1);
                    break;

                case GameState.CookingPizza:
                    _pizzaLevel.Draw(_spriteBatch);
                    break;

                case GameState.CookingCorndog:
                    _corndogLevel.Draw(_spriteBatch);
                    break;

                case GameState.CookingPie:
                    _pieLevel.Draw(_spriteBatch);
                    break;

                case GameState.RecipeResults:
                    _uiManager.DrawDialogue(textBoxBackground, mamaHappy, "Mama", "Great job! Click to go back to the menu.");
                    break;

                case GameState.FinalGrades:
                    _uiManager.DrawDialogue(textBoxBackground, mamaexcited, "Mama", "You finished all the recipes! Service is over!");
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    public class UIManager
    {
        private Texture2D _pixel;
        private SpriteBatch _sb;
        private SpriteFont _font;

        public SpriteFont Font { set { _font = value; } }

        public UIManager(GraphicsDevice gd, SpriteBatch sb)
        {
            _pixel = new Texture2D(gd, 1, 1);
            _pixel.SetData(new[] { Color.White });
            _sb = sb;
        }

        public void DrawDialogue(Texture2D boxTexture, Texture2D portrait, string name, string text)
        {
            Vector2 boxPos = new Vector2(50, 450);
            
            if (boxTexture != null)
                _sb.Draw(boxTexture, boxPos, Color.White);

            if (portrait != null)
            {
                _sb.Draw(portrait, new Vector2(boxPos.X + 20, boxPos.Y + 20), Color.White);
            }

            if (_font != null)
            {
                _sb.DrawString(_font, name, new Vector2(boxPos.X + 150, boxPos.Y + 20), Color.Black);
                _sb.DrawString(_font, text, new Vector2(boxPos.X + 150, boxPos.Y + 60), Color.White);
            }
            else 
            {
                DrawText(name, boxPos.X + 150, boxPos.Y + 20, Color.Black);
                DrawText(text, boxPos.X + 150, boxPos.Y + 60, Color.White);
            }
        }

        public void DrawRect(float x, float y, float w, float h, Color c)
        {
            _sb.Draw(_pixel, new Rectangle((int)x, (int)y, (int)w, (int)h), c);
        }
        
        public void DrawText(string t, float x, float y, Color c, bool center = false)
        {
            if (center) x -= t.Length * 4;
            for (int i = 0; i < t.Length; i++)
                _sb.Draw(_pixel, new Rectangle((int)x + i * 8, (int)y, 6, 12), c);
        }
        
        public void DrawMenu(string[] names, string[][] stages, int hover)
        {
            DrawRect(0, 0, 900, 650, new Color(0, 0, 0, 200));
            
            if (_font != null)
            {
                _sb.DrawString(_font, "COOKING SIMULATOR", new Vector2(350, 60), Color.Gold);
                _sb.DrawString(_font, "Choose a recipe:", new Vector2(350, 110), Color.White);
            }
            else
            {
                DrawText("COOKING SIMULATOR", 450, 60, Color.Gold, true);
                DrawText("Choose a recipe:", 450, 110, Color.White, true);
            }
            
            Color[] colors = { Color.DarkRed, Color.DarkGreen, Color.DarkOrange };
            for (int i = 0; i < 3; i++)
            {
                Color btnColor = i == hover ? colors[i] * 1.2f : colors[i];
                DrawRect(300, 160 + i * 100, 300, 60, btnColor);
                
                if (_font != null)
                {
                    _sb.DrawString(_font, names[i].ToUpper(), new Vector2(400, 185 + i * 100), Color.White);
                    _sb.DrawString(_font, $"{stages[i][0]} -> {stages[i][1]} -> {stages[i][2]}", new Vector2(350, 210 + i * 100), Color.LightGray);
                }
                else
                {
                    DrawText(names[i].ToUpper(), 450, 185 + i * 100, Color.White, true);
                    DrawText($"{stages[i][0]} -> {stages[i][1]} -> {stages[i][2]}", 450, 210 + i * 100, Color.LightGray, true);
                }
            }
        }
        
        public void DrawGameUI(string recipe, int stage, string[] stages, string[] needs,
                               float timer, float maxTime, int score, int combo, float comboTime,
                               string msg, float msgTime)
        {
            DrawText($"RECIPE: {recipe}", 450, 15, Color.Gold, true);
            DrawText($"STAGE {stage+1}/3: {stages[stage]}", 20, 15, Color.Cyan);
            
            for (int i = 0; i < 3; i++)
            {
                Color c = i < stage ? Color.Green : (i == stage ? Color.Yellow : Color.Gray);
                DrawRect(20, 45 + i * 20, 150, 12, c);
                DrawText(stages[i], 180, 45 + i * 20, c);
            }
            
            float p = timer / maxTime;
            DrawRect(20, 120, (int)(200 * p), 15, timer < 3 ? Color.Red : Color.Lime);
            DrawText($"Time: {timer:F1}s", 20, 140, timer < 3 ? Color.Red : Color.White);
            
            DrawText($"Score: {score}", 20, 170, Color.Gold);
            if (combo > 1)
            {
                DrawText($"COMBO x{combo}!", 20, 195, Color.Orange);
                DrawRect(20, 215, (int)((comboTime / 3f) * 100), 5, Color.Orange);
            }
            
            DrawText($"Need: {needs[stage]}", 20, 240, Color.Cyan);
            
            if (msgTime > 0)
                DrawText(msg, 450, 300, Color.Yellow, true);
        }
        
        public void DrawStations(Rectangle cut, Rectangle cook, Rectangle mix)
        {
            DrawRect(cut.X, cut.Y, cut.Width, cut.Height, Color.Orange);
            DrawText("CUT", cut.X + 60, cut.Y + 50, Color.White, true);
            
            DrawRect(cook.X, cook.Y, cook.Width, cook.Height, Color.Red);
            DrawText("COOK", cook.X + 60, cook.Y + 50, Color.White, true);
            
            DrawRect(mix.X, mix.Y, mix.Width, mix.Height, Color.Purple);
            DrawText("MIX", mix.X + 60, mix.Y + 50, Color.White, true);
        }
        
        public void DrawPause()
        {
            DrawRect(0, 0, 900, 650, new Color(0, 0, 0, 200));
            DrawText("PAUSED", 450, 250, Color.Gold, true);
            DrawText("ESC - Resume | M - Menu", 450, 320, Color.White, true);
        }
        
        public void DrawComplete(string name, string[] stages, int score)
        {
            DrawRect(0, 0, 900, 650, new Color(0, 0, 0, 200));
            DrawText($"You mastered {name}!", 450, 180, Color.Gold, true);
            for (int i = 0; i < 3; i++)
                DrawText($"  {stages[i]}", 450, 250 + i * 30, Color.LightGreen, true);
            DrawText($"Score: {score}", 450, 370, Color.Yellow, true);
            DrawText("Click to continue", 450, 500, Color.White, true);
        }
        
        public void DrawGameOver(int score)
        {
            DrawRect(0, 0, 900, 650, new Color(0, 0, 0, 200));
            DrawText("GAME OVER!", 450, 200, Color.Red, true);
            DrawText($"Score: {score}", 450, 280, Color.White, true);
            DrawText("Press M for Menu | R to Restart", 450, 380, Color.Yellow, true);
        }
        
        public void DrawTooltip(string text, Vector2 mouse)
        {
            DrawRect(mouse.X + 15, mouse.Y - 20, text.Length * 8 + 10, 25, new Color(0, 0, 0, 200));
            DrawText(text, mouse.X + 20, mouse.Y - 15, Color.Yellow);
        }
        
        public void DrawCursor(Vector2 mouse)
        {
            DrawRect(mouse.X, mouse.Y, 10, 10, Color.White);
        }
    }
}