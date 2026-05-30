using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace CookingSimulator
{
    public class Pizza : BakingMethods 
    {
        private Vector2 centerBowl;
        private Vector2 ovenLocation;
        
        private Texture2D yeast, yeastMixture;
        private Texture2D oliveOil, rawDoughBall;
        private Texture2D doughCut, doughInPan;
        private Texture2D tomatoPaste, mint, pizzaSauce;
        private Texture2D rolledDough, sauceSpread, cheese, pepperoni;
        private Texture2D wrap1, wrap2, wrap3;
        private Texture2D oven, pizzaCooked, pizzaPan;

        private MouseState previousMouse;
        private KeyboardState previousKey;
        private int finalScore = 0; 

        public Pizza()
        {
            Name = "Pizza";
            
            centerBowl = new Vector2(400, 300);
            ovenLocation = new Vector2(200, 100);
            
            corner1 = new Vector2(50, 50);
            corner2 = new Vector2(50, 150);
            corner3 = new Vector2(50, 250);
            corner4 = new Vector2(50, 350);
        }

 public void LoadContent(ContentManager Content)
        {
            yeast = Content.Load<Texture2D>("yeast");
            yeastMixture = Content.Load<Texture2D>("yeastMixture");
            oliveOil = Content.Load<Texture2D>("oliveOil");
            rawDoughBall = Content.Load<Texture2D>("rawDoughBall");
            doughCut = Content.Load<Texture2D>("doughCut");
            doughInPan = Content.Load<Texture2D>("doughInPan");
            tomatoPaste = Content.Load<Texture2D>("tomatoPaste");
            mint = Content.Load<Texture2D>("mint");
            pizzaSauce = Content.Load<Texture2D>("bowl_PizzaSauce");
            rolledDough = Content.Load<Texture2D>("rolledDough");
            sauceSpread = Content.Load<Texture2D>("bowl_PizzaSauce");
            cheese = Content.Load<Texture2D>("cheese");
            pepperoni = Content.Load<Texture2D>("pepperoni");
            wrap1 = Content.Load<Texture2D>("dough_Wrap1");
            wrap2 = Content.Load<Texture2D>("dough_Wrap2");
            wrap3 = Content.Load<Texture2D>("dough_WrapFull");
            oven = Content.Load<Texture2D>("oven_PizzaUncooked");
            pizzaCooked = Content.Load<Texture2D>("pizzaCooked");
            pizzaPan = Content.Load<Texture2D>("pizzaPan");
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState currentKey = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (currentStep)
            {
                case 0:
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 1:
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 2:
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 3:
                    if (Mix(currentMouse, previousMouse)) currentStep++;
                    break;
                case 4: 
                    if (CheckMouseClick(currentMouse, previousMouse)) currentStep++; 
                    break;
                case 5:
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 6:
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 7:
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 8:
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 9:
                    if (Mix(currentMouse, previousMouse)) currentStep++;
                    break;
                case 10:
                    if (Wrap(currentMouse, previousMouse)) currentStep++;
                    break;
                case 11: 
                    if (CheckMouseClick(currentMouse, previousMouse)) currentStep++;
                    break;
                case 12: 
                    if (CheckMouseClick(currentMouse, previousMouse)) currentStep++;
                    break;
                case 13: 
                    if (Cut(currentMouse, previousMouse)) currentStep++;
                    break;
                case 14: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 15: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 16: 
                    if (Pressing(currentKey)) currentStep++;
                    break;
                case 17: 
                    if (CheckMouseClick(currentMouse, previousMouse)) currentStep++;
                    break;
                case 18:  
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 19: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 20: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 21: 
                    if (Mix(currentMouse, previousMouse)) currentStep++;
                    break;
                case 22: 
                    if (CheckMouseClick(currentMouse, previousMouse)) currentStep++;
                    break;
                case 23: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 24: 
                    if (Rolling(currentMouse, previousMouse)) currentStep++;
                    break;
                case 25: 
                    if (CheckMouseClick(currentMouse, previousMouse)) currentStep++;
                    break;
                case 26: 
                    if (Spreading(currentMouse, previousMouse)) currentStep++;
                    break;
                case 27: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 28: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 29: 
                    if (CheckMouseClick(currentMouse, previousMouse)) currentStep++;
                    break;

                case 30: 
                    int bakeResult = Bake(currentKey, previousKey, deltaTime);
                    
                    if (bakeResult == 1) 
                    {
                        finalScore = 1; 
                        currentStep++;
                    }
                    else if (bakeResult == 2)
                    {
                        finalScore = 2; 
                        currentStep++;
                    }
                    break;
                
                case 31: 
                    if (CheckMouseClick(currentMouse, previousMouse)) 
                    {
                        FinalLevelScore = finalScore; 
                        IsLevelComplete = true; 
                        currentStep++; 
                    }
                    break;
            }

            previousMouse = currentMouse;
            previousKey = currentKey;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (currentStep)
            {
                case 0: 
                    spriteBatch.Draw(water, corner1, Color.White); 
                    spriteBatch.Draw(sugar, corner2, Color.White); 
                    spriteBatch.Draw(yeast, corner3, Color.White); 
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 1: 
                    spriteBatch.Draw(water, centerBowl, Color.White); 
                    spriteBatch.Draw(sugar, corner2, Color.White); 
                    spriteBatch.Draw(yeast, corner3, Color.White); 
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 2: 
                    spriteBatch.Draw(water, centerBowl, Color.White); 
                    spriteBatch.Draw(sugar, centerBowl, Color.White); 
                    spriteBatch.Draw(yeast, corner3, Color.White); 
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 3: 
                    spriteBatch.Draw(water, centerBowl, Color.White);
                    spriteBatch.Draw(sugar, centerBowl, Color.White);
                    spriteBatch.Draw(yeast, centerBowl, Color.White); 
                    spriteBatch.Draw(hintSwipeHoriz, centerBowl, Color.White);
                    break;
                case 4: 
                    spriteBatch.Draw(yeastMixture, centerBowl, Color.White);
                    spriteBatch.Draw(popupAmazing, centerBowl, Color.White);
                    break;
                case 5: 
                    spriteBatch.Draw(flour, corner1, Color.White);
                    spriteBatch.Draw(salt, corner2, Color.White);
                    spriteBatch.Draw(oliveOil, corner3, Color.White);
                    spriteBatch.Draw(yeastMixture, corner4, Color.White);
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 6: 
                    spriteBatch.Draw(flour, centerBowl, Color.White);
                    spriteBatch.Draw(salt, corner2, Color.White);
                    spriteBatch.Draw(oliveOil, corner3, Color.White);
                    spriteBatch.Draw(yeastMixture, corner4, Color.White);
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 7: 
                    spriteBatch.Draw(flour, centerBowl, Color.White);
                    spriteBatch.Draw(salt, centerBowl, Color.White);
                    spriteBatch.Draw(oliveOil, corner3, Color.White);
                    spriteBatch.Draw(yeastMixture, corner4, Color.White);
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 8: 
                    spriteBatch.Draw(flour, centerBowl, Color.White);
                    spriteBatch.Draw(salt, centerBowl, Color.White);
                    spriteBatch.Draw(oliveOil, centerBowl, Color.White);
                    spriteBatch.Draw(yeastMixture, corner4, Color.White);
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 9: 
                    spriteBatch.Draw(flour, centerBowl, Color.White);
                    spriteBatch.Draw(salt, centerBowl, Color.White);
                    spriteBatch.Draw(oliveOil, centerBowl, Color.White);
                    spriteBatch.Draw(yeastMixture, centerBowl, Color.White);
                    spriteBatch.Draw(hintSwipeHoriz, centerBowl, Color.White);
                    break;
                case 10: 
                    spriteBatch.Draw(rawDoughBall, centerBowl, Color.White);
                    if (currentSwipeDistance < 400f) 
                        spriteBatch.Draw(hintSwipeHoriz, centerBowl, Color.White);
                    else if (currentSwipeDistance >= 400f && currentSwipeDistance < 800f) 
                        spriteBatch.Draw(wrap1, centerBowl, Color.White);
                    else if (currentSwipeDistance >= 800f) 
                        spriteBatch.Draw(wrap2, centerBowl, Color.White);
                    break;
                case 11: 
                    spriteBatch.Draw(rawDoughBall, centerBowl, Color.White);
                    spriteBatch.Draw(wrap3, centerBowl, Color.White); 
                    spriteBatch.Draw(popupGood, centerBowl, Color.White);
                    break;
                case 12: 
                    spriteBatch.Draw(rawDoughBall, centerBowl, Color.White);
                    spriteBatch.Draw(wrap3, centerBowl, Color.White);
                    spriteBatch.Draw(hintClick, centerBowl, Color.White); 
                    break;
                case 13: 
                    spriteBatch.Draw(rawDoughBall, centerBowl, Color.White);
                    spriteBatch.Draw(hintSwipeVert, centerBowl, Color.White); 
                    break;
                case 14: 
                    spriteBatch.Draw(pizzaPan, centerBowl, Color.White);
                    spriteBatch.Draw(oliveOil, corner1, Color.White);
                    spriteBatch.Draw(doughCut, corner2, Color.White);
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 15: 
                    spriteBatch.Draw(pizzaPan, centerBowl, Color.White);
                    spriteBatch.Draw(oliveOil, centerBowl, Color.White); 
                    spriteBatch.Draw(doughCut, corner2, Color.White); 
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 16: 
                    spriteBatch.Draw(pizzaPan, centerBowl, Color.White);
                    spriteBatch.Draw(doughInPan, centerBowl, Color.White); 
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White); 
                    break;
                case 17: 
                    spriteBatch.Draw(pizzaPan, centerBowl, Color.White);
                    spriteBatch.Draw(doughInPan, centerBowl, Color.White);
                    spriteBatch.Draw(wrap3, centerBowl, Color.White); 
                    spriteBatch.Draw(popupGood, centerBowl, Color.White);
                    break;
                case 18: 
                    spriteBatch.Draw(tomatoPaste, corner1, Color.White);
                    spriteBatch.Draw(mint, corner2, Color.White);
                    spriteBatch.Draw(salt, corner3, Color.White);
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 19: 
                    spriteBatch.Draw(tomatoPaste, centerBowl, Color.White);
                    spriteBatch.Draw(mint, corner2, Color.White);
                    spriteBatch.Draw(salt, corner3, Color.White);
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 20: 
                    spriteBatch.Draw(tomatoPaste, centerBowl, Color.White);
                    spriteBatch.Draw(mint, centerBowl, Color.White);
                    spriteBatch.Draw(salt, corner3, Color.White);
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 21: 
                    spriteBatch.Draw(tomatoPaste, centerBowl, Color.White);
                    spriteBatch.Draw(mint, centerBowl, Color.White);
                    spriteBatch.Draw(salt, centerBowl, Color.White);
                    spriteBatch.Draw(hintSwipeHoriz, centerBowl, Color.White);
                    break;
                case 22: 
                    spriteBatch.Draw(pizzaSauce, centerBowl, Color.White);
                    spriteBatch.Draw(popupAmazing, centerBowl, Color.White);
                    break;
                case 23: 
                    spriteBatch.Draw(doughCut, corner1, Color.White);
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White); 
                    break;
                case 24: 
                    spriteBatch.Draw(doughCut, centerBowl, Color.White); 
                    spriteBatch.Draw(hintSwipeVert, centerBowl, Color.White);
                    break;
                case 25: 
                    spriteBatch.Draw(pizzaPan, centerBowl, Color.White);
                    spriteBatch.Draw(rolledDough, centerBowl, Color.White);
                    spriteBatch.Draw(popupGood, centerBowl, Color.White);
                    break;
                case 26: 
                    spriteBatch.Draw(pizzaPan, centerBowl, Color.White);
                    spriteBatch.Draw(rolledDough, centerBowl, Color.White); 
                    spriteBatch.Draw(cheese, corner1, Color.White); 
                    spriteBatch.Draw(pepperoni, corner2, Color.White);
                    spriteBatch.Draw(hintSwipeHoriz, centerBowl, Color.White);
                    break;
                case 27: 
                    spriteBatch.Draw(pizzaPan, centerBowl, Color.White);
                    spriteBatch.Draw(rolledDough, centerBowl, Color.White);
                    spriteBatch.Draw(sauceSpread, centerBowl, Color.White);
                    spriteBatch.Draw(cheese, corner1, Color.White); 
                    spriteBatch.Draw(pepperoni, corner2, Color.White);
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 28: 
                    spriteBatch.Draw(pizzaPan, centerBowl, Color.White);
                    spriteBatch.Draw(rolledDough, centerBowl, Color.White);
                    spriteBatch.Draw(sauceSpread, centerBowl, Color.White);
                    spriteBatch.Draw(cheese, centerBowl, Color.White); 
                    spriteBatch.Draw(pepperoni, corner2, Color.White); 
                    spriteBatch.Draw(hintPressE, centerBowl, Color.White);
                    break;
                case 29: 
                    spriteBatch.Draw(pizzaPan, centerBowl, Color.White);
                    spriteBatch.Draw(rolledDough, centerBowl, Color.White);
                    spriteBatch.Draw(sauceSpread, centerBowl, Color.White);
                    spriteBatch.Draw(cheese, centerBowl, Color.White);
                    spriteBatch.Draw(pepperoni, centerBowl, Color.White); 
                    spriteBatch.Draw(popupAmazing, centerBowl, Color.White);
                    break;

                case 30: 
                    spriteBatch.Draw(oven, ovenLocation, Color.White);
                    spriteBatch.Draw(pizzaPan, ovenLocation, Color.White);
                    spriteBatch.Draw(rolledDough, ovenLocation, Color.White); 
                    spriteBatch.Draw(sauceSpread, ovenLocation, Color.White);
                    spriteBatch.Draw(cheese, ovenLocation, Color.White);
                    spriteBatch.Draw(pepperoni, ovenLocation, Color.White);
                    
                    Vector2 barLocation = new Vector2(ovenLocation.X, ovenLocation.Y - 50);
                    spriteBatch.Draw(uiBarBackground, barLocation, Color.White);

                    float needleX = barLocation.X + ((cookingProgress / 100f) * uiBarBackground.Width);
                    Vector2 needleLocation = new Vector2(needleX, barLocation.Y - 10); 
                    spriteBatch.Draw(uiMeterNeedle, needleLocation, Color.White);

                    spriteBatch.Draw(hintSpacebar, centerBowl, Color.White);
                    break;
                
                case 31: 
                    spriteBatch.Draw(pizzaPan, centerBowl, Color.White);
                    spriteBatch.Draw(pizzaCooked, centerBowl, Color.White);
                    
                    if (finalScore == 1)
                        spriteBatch.Draw(popupAmazing, centerBowl, Color.White);
                    else
                        spriteBatch.Draw(popupMiss, centerBowl, Color.White);
                    break;
            }
        }
    }
}