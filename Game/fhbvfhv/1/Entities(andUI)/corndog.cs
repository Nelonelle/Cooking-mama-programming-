using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CookingSimulator
{
    public class Corndog : BakingMethods 
    {
        
        private Texture2D  oilBottle, oilBoiling;

        private Texture2D hotdogRaw, hotdogCut, cheeseBlock, cheeseCut;
        private Texture2D stick, skewerStep1, skewerStep2, skewerFull;
        private Texture2D stick2, skewer2Step1, skewer2Step2, skewer2Full;
        private Texture2D cornmeal, bakingPowder;
        private Texture2D batterLiquid;

        private Texture2D batterWrap1, batterWrap2, batterWrap3; 
        private Texture2D corndogFrying, corndogCooked;
        private Texture2D ketchup, mustard;

        private MouseState previousMouse;
        private KeyboardState previousKey;
        private int finalScore = 0; 

        public Corndog()
        {
            Name = "Corn Dog";
            
            centerStage = new Vector2(400, 300);
            
            corner1 = new Vector2(50, 50);
            corner2 = new Vector2(50, 150);
            corner3 = new Vector2(50, 250);
            corner4 = new Vector2(50, 350);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState currentKey = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (currentStep)
            {
                case 0:
                    if (Cut(currentMouse, previousMouse)) currentStep++;
                    break;
                case 1: 
                    if (Cut(currentMouse, previousMouse)) currentStep++;
                    break;
                case 2: 
                    if (Cut(currentMouse, previousMouse)) currentStep++;
                    break;
                
                case 3: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 4: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 5: 
                    int boilResult = Boil(currentKey, previousKey, deltaTime);
                    if (boilResult == 1 || boilResult == 2) currentStep++; 
                    break;
                case 6: 
                    if (CheckMouseClick(currentMouse, previousMouse)) currentStep++;
                    break;
                
                case 7: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 8: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 9: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 10: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 11: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 12: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                
                case 13: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 14: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 15: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 16: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 17: 
                    if (Mix(currentMouse, previousMouse)) currentStep++;
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
                
                case 23: 
                    if (Wrap(currentMouse, previousMouse)) currentStep++;
                    break;
                case 24: 
                    if (Wrap(currentMouse, previousMouse)) currentStep++;
                    break;
                
                case 25: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 26: 
                    int heatResult = Boil(currentKey, previousKey, deltaTime);
                    if (heatResult == 1 || heatResult == 2) currentStep++; 
                    break;
                case 27: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 28: 
                    int fryResult = Frying(currentKey, previousKey, deltaTime);
                    if (fryResult == 1) 
                    {
                        finalScore = 1; 
                        currentStep++;
                    }
                    else if (fryResult == 2)
                    {
                        finalScore = 2; 
                        currentStep++;
                    }
                    break;
                
                case 29: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 30: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
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
            if ((currentStep >= 3 && currentStep <= 6) || (currentStep >= 25 && currentStep <= 28))
            {
                spriteBatch.Draw(bgStove, Vector2.Zero, Color.White);
            }
            else
            {
                spriteBatch.Draw(bgTable, Vector2.Zero, Color.White);
            }

            switch (currentStep)
            {
                case 0: 
                    spriteBatch.Draw(hotdogRaw, centerStage, Color.White);
                    spriteBatch.Draw(hintSwipeVert, centerStage, Color.White);
                    break;
                case 1: 
                    spriteBatch.Draw(hotdogCut, corner1, Color.White);
                    spriteBatch.Draw(hotdogRaw, centerStage, Color.White);
                    spriteBatch.Draw(hintSwipeVert, centerStage, Color.White);
                    break;
                case 2: 
                    spriteBatch.Draw(hotdogCut, corner1, Color.White);
                    spriteBatch.Draw(hotdogCut, corner2, Color.White);
                    spriteBatch.Draw(cheeseBlock, centerStage, Color.White);
                    spriteBatch.Draw(hintSwipeVert, centerStage, Color.White);
                    break;
                
                case 3: 
                    spriteBatch.Draw(pot, centerStage, Color.White); 
                    spriteBatch.Draw(water, corner1, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 4: 
                    spriteBatch.Draw(pot, centerStage, Color.White);
                    spriteBatch.Draw(water, centerStage, Color.White); 
                    spriteBatch.Draw(hotdogCut, corner1, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 5: 
                    spriteBatch.Draw(pot, centerStage, Color.White);
                    spriteBatch.Draw(water, centerStage, Color.White);
                    spriteBatch.Draw(hotdogCut, centerStage, Color.White); 
                    
                    Vector2 boilBar = new Vector2(centerStage.X, centerStage.Y - 50);
                    spriteBatch.Draw(uiBarBackground, boilBar, Color.White);
                    float boilNeedleX = boilBar.X + ((cookingProgress / 100f) * uiBarBackground.Width);
                    spriteBatch.Draw(uiMeterNeedle, new Vector2(boilNeedleX, boilBar.Y - 10), Color.White);
                    
                    spriteBatch.Draw(hintSpacebar, centerStage, Color.White);
                    break;
                case 6: 
                    spriteBatch.Draw(pot, centerStage, Color.White);
                    spriteBatch.Draw(water, centerStage, Color.White);
                    spriteBatch.Draw(hotdogCut, centerStage, Color.White);
                    spriteBatch.Draw(popupGood, centerStage, Color.White);
                    break;
                
                case 7: 
                    spriteBatch.Draw(hotdogCut, corner1, Color.White); 
                    spriteBatch.Draw(cheeseCut, corner2, Color.White); 
                    
                    spriteBatch.Draw(stick, centerStage, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 8: 
                    spriteBatch.Draw(hotdogCut, corner1, Color.White); 
                    spriteBatch.Draw(cheeseCut, corner2, Color.White);
                    
                    spriteBatch.Draw(skewerStep1, centerStage, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 9: 
                    spriteBatch.Draw(hotdogCut, corner1, Color.White); 
                    spriteBatch.Draw(cheeseCut, corner2, Color.White);
                    
                    spriteBatch.Draw(skewerStep2, centerStage, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                
                case 10: 
                    spriteBatch.Draw(hotdogCut, corner1, Color.White); 
                    spriteBatch.Draw(cheeseCut, corner2, Color.White);
                    spriteBatch.Draw(skewerFull, corner3, Color.White); 
                    
                    spriteBatch.Draw(stick2, centerStage, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 11: 
                    spriteBatch.Draw(hotdogCut, corner1, Color.White); 
                    spriteBatch.Draw(cheeseCut, corner2, Color.White);
                    spriteBatch.Draw(skewerFull, corner3, Color.White);
                    
                    spriteBatch.Draw(skewer2Step1, centerStage, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 12: 
                    spriteBatch.Draw(hotdogCut, corner1, Color.White); 
                    spriteBatch.Draw(cheeseCut, corner2, Color.White);
                    spriteBatch.Draw(skewerFull, corner3, Color.White);
                    
                    spriteBatch.Draw(skewer2Step2, centerStage, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                
                case 13: 
                    spriteBatch.Draw(skewerFull, corner3, Color.White); 
                    spriteBatch.Draw(skewer2Full, corner4, Color.White); 
                    
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(egg, corner1, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 14: 
                    spriteBatch.Draw(skewerFull, corner3, Color.White); 
                    spriteBatch.Draw(skewer2Full, corner4, Color.White);
                    
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(egg, centerStage, Color.White);
                    spriteBatch.Draw(milk, corner1, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 15: 
                    spriteBatch.Draw(skewerFull, corner3, Color.White); 
                    spriteBatch.Draw(skewer2Full, corner4, Color.White);
                    
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(egg, centerStage, Color.White);
                    spriteBatch.Draw(milk, centerStage, Color.White);
                    spriteBatch.Draw(sugar, corner1, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 16: 
                    spriteBatch.Draw(skewerFull, corner3, Color.White); 
                    spriteBatch.Draw(skewer2Full, corner4, Color.White);
                    
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(egg, centerStage, Color.White);
                    spriteBatch.Draw(milk, centerStage, Color.White);
                    spriteBatch.Draw(sugar, centerStage, Color.White);
                    spriteBatch.Draw(salt, corner1, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 17: 
                    spriteBatch.Draw(skewerFull, corner3, Color.White); 
                    spriteBatch.Draw(skewer2Full, corner4, Color.White);
                    
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(egg, centerStage, Color.White);
                    spriteBatch.Draw(milk, centerStage, Color.White);
                    spriteBatch.Draw(sugar, centerStage, Color.White);
                    spriteBatch.Draw(salt, centerStage, Color.White);
                    spriteBatch.Draw(hintSwipeHoriz, centerStage, Color.White);
                    break;
                case 18: 
                    spriteBatch.Draw(skewerFull, corner3, Color.White); 
                    spriteBatch.Draw(skewer2Full, corner4, Color.White);
                    
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(batterLiquid, centerStage, Color.White); 
                    spriteBatch.Draw(cornmeal, corner1, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 19: 
                    spriteBatch.Draw(skewerFull, corner3, Color.White); 
                    spriteBatch.Draw(skewer2Full, corner4, Color.White);
                    
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(batterLiquid, centerStage, Color.White);
                    spriteBatch.Draw(cornmeal, centerStage, Color.White);
                    spriteBatch.Draw(flour, corner1, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 20: 
                    spriteBatch.Draw(skewerFull, corner3, Color.White); 
                    spriteBatch.Draw(skewer2Full, corner4, Color.White);
                    
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(batterLiquid, centerStage, Color.White);
                    spriteBatch.Draw(cornmeal, centerStage, Color.White);
                    spriteBatch.Draw(flour, centerStage, Color.White);
                    spriteBatch.Draw(bakingPowder, corner1, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 21: 
                    spriteBatch.Draw(skewerFull, corner3, Color.White); 
                    spriteBatch.Draw(skewer2Full, corner4, Color.White);
                    
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(batterLiquid, centerStage, Color.White);
                    spriteBatch.Draw(cornmeal, centerStage, Color.White);
                    spriteBatch.Draw(flour, centerStage, Color.White);
                    spriteBatch.Draw(bakingPowder, centerStage, Color.White);
                    spriteBatch.Draw(hintSwipeHoriz, centerStage, Color.White);
                    break;
                
                case 23: 
                    spriteBatch.Draw(skewer2Full, corner4, Color.White); 
                    
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(batterLiquid, centerStage, Color.White);
                    spriteBatch.Draw(skewerFull, centerStage, Color.White); 
                    
                    if (currentSwipeDistance < 400f) 
                        spriteBatch.Draw(hintSwipeHoriz, centerStage, Color.White);
                    else if (currentSwipeDistance >= 400f && currentSwipeDistance < 800f) 
                        spriteBatch.Draw(batterWrap1, centerStage, Color.White); 
                    else if (currentSwipeDistance >= 800f) 
                        spriteBatch.Draw(batterWrap2, centerStage, Color.White); 
                    break;
                    
                case 24: 
                    spriteBatch.Draw(batterWrap3, corner3, Color.White); 
                    
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(batterLiquid, centerStage, Color.White);
                    spriteBatch.Draw(skewer2Full, centerStage, Color.White); 
                    
                    if (currentSwipeDistance < 400f) 
                        spriteBatch.Draw(hintSwipeHoriz, centerStage, Color.White);
                    else if (currentSwipeDistance >= 400f && currentSwipeDistance < 800f) 
                        spriteBatch.Draw(batterWrap1, centerStage, Color.White); 
                    else if (currentSwipeDistance >= 800f) 
                        spriteBatch.Draw(batterWrap2, centerStage, Color.White); 
                    break;
                
                case 25: 
                    spriteBatch.Draw(pan, centerStage, Color.White); 
                    spriteBatch.Draw(oilBottle, corner1, Color.White); 
                    spriteBatch.Draw(batterWrap3, corner3, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 26: 
                    spriteBatch.Draw(pan, centerStage, Color.White);
                    spriteBatch.Draw(oilBottle, centerStage, Color.White); 
                    spriteBatch.Draw(batterWrap3, corner3, Color.White);
                    
                    Vector2 heatBar = new Vector2(centerStage.X, centerStage.Y - 50);
                    spriteBatch.Draw(uiBarBackground, heatBar, Color.White);
                    float heatNeedleX = heatBar.X + ((cookingProgress / 100f) * uiBarBackground.Width);
                    spriteBatch.Draw(uiMeterNeedle, new Vector2(heatNeedleX, heatBar.Y - 10), Color.White);
                    
                    spriteBatch.Draw(hintSpacebar, centerStage, Color.White);
                    break;
                case 27: 
                    spriteBatch.Draw(oilBoiling, centerStage, Color.White); 
                    spriteBatch.Draw(batterWrap3, corner3, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 28: 
                    spriteBatch.Draw(oilBoiling, centerStage, Color.White);
                    spriteBatch.Draw(corndogFrying, centerStage, Color.White); 
                    
                    Vector2 fryBar = new Vector2(centerStage.X, centerStage.Y - 50);
                    spriteBatch.Draw(uiBarBackground, fryBar, Color.White);
                    float fryNeedleX = fryBar.X + ((cookingProgress / 100f) * uiBarBackground.Width);
                    spriteBatch.Draw(uiMeterNeedle, new Vector2(fryNeedleX, fryBar.Y - 10), Color.White);
                    
                    spriteBatch.Draw(hintSpacebar, centerStage, Color.White);
                    break;
                
                case 29: 
                    spriteBatch.Draw(corndogCooked, centerStage, Color.White); 
                    spriteBatch.Draw(ketchup, corner1, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 30: 
                    spriteBatch.Draw(corndogCooked, centerStage, Color.White);
                    spriteBatch.Draw(ketchup, centerStage, Color.White); 
                    spriteBatch.Draw(mustard, corner1, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 31: 
                    spriteBatch.Draw(corndogCooked, centerStage, Color.White);
                    spriteBatch.Draw(ketchup, centerStage, Color.White);
                    spriteBatch.Draw(mustard, centerStage, Color.White); 
                    
                    if (finalScore == 1)
                        spriteBatch.Draw(popupAmazing, centerStage, Color.White);
                    else
                        spriteBatch.Draw(popupMiss, centerStage, Color.White);
                    break;
            }
        }
    }
}