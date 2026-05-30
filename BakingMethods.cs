using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace CookingSimulator
{
    public abstract class BakingMethods
    {
        public string Name { get; protected set; }
        public bool IsLevelComplete { get; protected set; } = false;
        public int FinalLevelScore { get; protected set; } = 0; 

        protected int currentStep = 0;
        protected int previousStepMemory = 0; 
        
        public float stepTimer { get; protected set; } = 10f; 
        protected int totalFailures = 0; 

        protected float currentSwipeDistance = 0f;
        protected float cookingProgress = 0f;
        protected bool progressIncreasing = true;

        protected Vector2 centerStage;
        protected Vector2 corner1;
        protected Vector2 corner2;
        protected Vector2 corner3;
        protected Vector2 corner4;

        protected Texture2D bgTable;
        protected Texture2D bgStove;
        protected Texture2D bgOven;

        protected Texture2D emptyBowl;
        protected Texture2D pot;
        protected Texture2D pan;

        protected Texture2D water;
        protected Texture2D sugar;
        protected Texture2D salt;
        protected Texture2D egg;
        protected Texture2D milk;
        protected Texture2D flour;

        protected Texture2D hintPressE;
        protected Texture2D hintSwipeHoriz;
        protected Texture2D hintSwipeVert;
        protected Texture2D hintClick;
        protected Texture2D hintSpacebar;
        protected Texture2D uiBarBackground;
        protected Texture2D uiMeterNeedle;
        protected Texture2D popupAmazing;
        protected Texture2D popupGood;
        protected Texture2D popupMiss;

        public virtual void LoadBaseContent(ContentManager Content)
        {
            bgTable = Content.Load<Texture2D>("bgTable");
            bgStove = Content.Load<Texture2D>("bgStove");
            bgOven = Content.Load<Texture2D>("bgOven");
            emptyBowl = Content.Load<Texture2D>("emptyBowl");
            pot = Content.Load<Texture2D>("pot");
            pan = Content.Load<Texture2D>("pan");
            water = Content.Load<Texture2D>("water");
            sugar = Content.Load<Texture2D>("sugar");
            salt = Content.Load<Texture2D>("salt");
            egg = Content.Load<Texture2D>("egg");
            milk = Content.Load<Texture2D>("milk");
            flour = Content.Load<Texture2D>("flour");
            hintPressE = Content.Load<Texture2D>("hintPressE");
            hintSwipeHoriz = Content.Load<Texture2D>("hintSwipeHoriz");
            hintSwipeVert = Content.Load<Texture2D>("hintSwipeVert");
            hintClick = Content.Load<Texture2D>("hintClick");
            hintSpacebar = Content.Load<Texture2D>("hintSpacebar");
            uiBarBackground = Content.Load<Texture2D>("uiBarBackground");
            uiMeterNeedle = Content.Load<Texture2D>("uiMeterNeedle");
            popupAmazing = Content.Load<Texture2D>("popupAmazing");
            popupGood = Content.Load<Texture2D>("popupGood");
            popupMiss = Content.Load<Texture2D>("popupMiss");
        }

        protected void GameTimer(float deltaTime)
        {
            if (currentStep != previousStepMemory)
            {
                stepTimer = 10f;
                cookingProgress = 0f;
                currentSwipeDistance = 0f;
                previousStepMemory = currentStep;
            }

            stepTimer -= deltaTime;

            if (stepTimer <= 0f)
            {
                totalFailures++;
                currentStep++; 
            }
        }

        protected int TimerMechanic(KeyboardState currentKey, KeyboardState previousKey, Keys requiredKey, float deltaTime)
        {
            float speed = 100f; 
            if (progressIncreasing)
            {
                cookingProgress += speed * deltaTime;
                if (cookingProgress >= 100f) progressIncreasing = false;
            }
            else
            {
                cookingProgress -= speed * deltaTime;
                if (cookingProgress <= 0f) progressIncreasing = true;
            }

            if (currentKey.IsKeyDown(requiredKey) && previousKey.IsKeyUp(requiredKey))
            {
                if (cookingProgress >= 40f && cookingProgress <= 60f) return 1; 
                else return 2; 
            }
            return 0;
        }

        protected void CalculateFinalGrade(int miniGameResult)
        {
            if (miniGameResult == 1 && totalFailures == 0) FinalLevelScore = 1;      
            else if (miniGameResult == 2 || totalFailures > 3) FinalLevelScore = 3;  
            else FinalLevelScore = 2;                                                
        }

        protected bool CheckIfKeyPressed(KeyboardState currentKey, Keys requiredKey)
        {
            if (currentKey.IsKeyDown(requiredKey))
            {
                cookingProgress = 100f;
                return true;
            }
            return false; 
        }

        protected bool CheckMouseMovement(MouseState currentMouse, MouseState previousMouse, bool isVertical, float requiredDistance)
        {
            if (currentMouse.LeftButton == ButtonState.Pressed)
            {
                if (isVertical) currentSwipeDistance += System.Math.Abs(currentMouse.Y - previousMouse.Y);
                else currentSwipeDistance += System.Math.Abs(currentMouse.X - previousMouse.X);

                cookingProgress = (currentSwipeDistance / requiredDistance) * 100f;

                if (currentSwipeDistance >= requiredDistance)
                {
                    currentSwipeDistance = 0f;
                    return true; 
                }
            }
            return false;
        }
        
        protected bool CheckMouseClick(MouseState currentMouse, MouseState previousMouse)
        {
            if (currentMouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released)
            {
                cookingProgress = 100f;
                return true;
            }
            return false;
        }

        protected bool AddIngredients(KeyboardState currentKey, MouseState currentMouse, MouseState previousMouse) 
        { return CheckIfKeyPressed(currentKey, Keys.E); }

        protected bool CrackEggs(KeyboardState currentKey) 
        { return CheckIfKeyPressed(currentKey, Keys.A); }

        protected bool Mix(MouseState currentMouse, MouseState previousMouse) 
        { return CheckMouseMovement(currentMouse, previousMouse, false, 1000f); }

        protected bool Knead(KeyboardState currentKey) 
        { return CheckIfKeyPressed(currentKey, Keys.E); }

        protected bool Wrap(MouseState currentMouse, MouseState previousMouse) 
        { return CheckMouseMovement(currentMouse, previousMouse, false, 1200f); }

        protected bool Cut(MouseState currentMouse, MouseState previousMouse) 
        { return CheckMouseMovement(currentMouse, previousMouse, true, 500f); }

        protected int Boil(KeyboardState currentKey, KeyboardState previousKey, float deltaTime) 
        { return TimerMechanic(currentKey, previousKey, Keys.Space, deltaTime); }

        protected bool Drain(MouseState currentMouse, MouseState previousMouse) 
        { return CheckMouseClick(currentMouse, previousMouse); }

        protected bool Rolling(MouseState currentMouse, MouseState previousMouse) 
        { return CheckMouseMovement(currentMouse, previousMouse, true, 1500f); }

        protected bool Pressing(KeyboardState currentKey) 
        { return CheckIfKeyPressed(currentKey, Keys.E); }

        protected int Frying(KeyboardState currentKey, KeyboardState previousKey, float deltaTime) 
        { return TimerMechanic(currentKey, previousKey, Keys.Space, deltaTime); }

        protected int Blending(KeyboardState currentKey, KeyboardState previousKey, float deltaTime) 
        { return TimerMechanic(currentKey, previousKey, Keys.Space, deltaTime); }

        protected bool Spreading(MouseState currentMouse, MouseState previousMouse) 
        { return CheckMouseMovement(currentMouse, previousMouse, false, 800f); }

        protected bool Grating(MouseState currentMouse, MouseState previousMouse) 
        { return CheckMouseMovement(currentMouse, previousMouse, true, 800f); }

        protected bool MakingHoles(MouseState currentMouse, MouseState previousMouse) 
        { return CheckMouseClick(currentMouse, previousMouse); }

        protected bool Stuffing(KeyboardState currentKey) 
        { return CheckIfKeyPressed(currentKey, Keys.E); }
    
        protected int Bake(KeyboardState currentKey, KeyboardState previousKey, float deltaTime) 
        { return TimerMechanic(currentKey, previousKey, Keys.Space, deltaTime); }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}