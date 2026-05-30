using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace CookingSimulator
{
    public class Pie : BakingMethods 
    {
        private Texture2D piePan;

        private Texture2D butter, butterMelted, rouxMix;
        private Texture2D pieSauce;

        private Texture2D appleRaw, appleSliced, cinnamon, appleCinnamonMix, appleFilling;

        private Texture2D doughBall, rolledDough, crustInPan, pieWithFilling;

        private Texture2D doughStrips, pieCrissCross, eggWashBrush, pieWashed;

        private Texture2D pieCooked;

        private MouseState previousMouse;
        private KeyboardState previousKey;
        private int finalScore = 0; 

        public Pie()
        {
            Name = "Apple Pie";
            
            centerStage = new Vector2(400, 300);
            
            corner1 = new Vector2(50, 50);
            corner2 = new Vector2(50, 150);
            corner3 = new Vector2(50, 250);
            corner4 = new Vector2(50, 350);
        }

    public void LoadContent(ContentManager Content)
        {
            piePan = Content.Load<Texture2D>("piePan");
            butter = Content.Load<Texture2D>("butter");
            butterMelted = Content.Load<Texture2D>("pan_ButterMelted");
            rouxMix = Content.Load<Texture2D>("rouxMix");
            pieSauce = Content.Load<Texture2D>("pieSauce");
            appleRaw = Content.Load<Texture2D>("appleRaw");
            appleSliced = Content.Load<Texture2D>("appleSliced");
            cinnamon = Content.Load<Texture2D>("cinnamon");
            appleCinnamonMix = Content.Load<Texture2D>("bowl_AppleCinnamonSauce");
            appleFilling = Content.Load<Texture2D>("appleFilling");
            doughBall = Content.Load<Texture2D>("doughBall");
            rolledDough = Content.Load<Texture2D>("rolledDough");
            crustInPan = Content.Load<Texture2D>("crustInPan");
            pieWithFilling = Content.Load<Texture2D>("pieWithFilling");
            doughStrips = Content.Load<Texture2D>("doughStrips");
            pieCrissCross = Content.Load<Texture2D>("pieCrissCross2");
            eggWashBrush = Content.Load<Texture2D>("eggWashBrush");
            pieWashed = Content.Load<Texture2D>("pieWashed");
            pieCooked = Content.Load<Texture2D>("pieCooked");
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
                    if (CheckMouseClick(currentMouse, previousMouse)) currentStep++;
                    break;
                case 2: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 3: 
                    if (Mix(currentMouse, previousMouse)) currentStep++;
                    break;

                case 4: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 5: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 6: 
                    if (Mix(currentMouse, previousMouse)) currentStep++;
                    break;

                case 7: 
                    if (Cut(currentMouse, previousMouse)) currentStep++;
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
                    if (Mix(currentMouse, previousMouse)) currentStep++;
                    break;
                case 12: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 13: 
                    if (Mix(currentMouse, previousMouse)) currentStep++;
                    break;

                case 14: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 15: 
                    if (Rolling(currentMouse, previousMouse)) currentStep++;
                    break;
                case 16: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 17: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 18: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;

                case 19: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 20: 
                    if (Rolling(currentMouse, previousMouse)) currentStep++;
                    break;
                case 21: 
                    if (Cut(currentMouse, previousMouse)) currentStep++;
                    break;
                case 22: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 23: 
                    if (Spreading(currentMouse, previousMouse)) currentStep++;
                    break;

                case 24: 
                    if (AddIngredients(currentKey, currentMouse, previousMouse)) currentStep++;
                    break;
                case 25: 
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
                case 26: 
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
            if (currentStep <= 6) 
            {
                spriteBatch.Draw(bgStove, Vector2.Zero, Color.White);
            }
            else if (currentStep >= 24) 
            {
                spriteBatch.Draw(bgOven, Vector2.Zero, Color.White);
            }
            else 
            {
                spriteBatch.Draw(bgTable, Vector2.Zero, Color.White);
            }

            switch (currentStep)
            {
                case 0: 
                    spriteBatch.Draw(pan, centerStage, Color.White); 
                    spriteBatch.Draw(butter, corner1, Color.White); 
                    spriteBatch.Draw(flour, corner2, Color.White);  
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 1: 
                    spriteBatch.Draw(pan, centerStage, Color.White);
                    spriteBatch.Draw(butter, centerStage, Color.White); 
                    spriteBatch.Draw(flour, corner2, Color.White);
                    spriteBatch.Draw(hintClick, centerStage, Color.White); 
                    break;
                case 2: 
                    spriteBatch.Draw(pan, centerStage, Color.White);
                    spriteBatch.Draw(butterMelted, centerStage, Color.White); 
                    spriteBatch.Draw(flour, corner2, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 3: 
                    spriteBatch.Draw(pan, centerStage, Color.White);
                    spriteBatch.Draw(butterMelted, centerStage, Color.White);
                    spriteBatch.Draw(flour, centerStage, Color.White); 
                    spriteBatch.Draw(hintSwipeHoriz, centerStage, Color.White);
                    break;

                case 4: 
                    spriteBatch.Draw(pan, centerStage, Color.White);
                    spriteBatch.Draw(rouxMix, centerStage, Color.White); 
                    spriteBatch.Draw(water, corner1, Color.White); 
                    spriteBatch.Draw(sugar, corner2, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 5: 
                    spriteBatch.Draw(pan, centerStage, Color.White);
                    spriteBatch.Draw(rouxMix, centerStage, Color.White);
                    spriteBatch.Draw(water, centerStage, Color.White); 
                    spriteBatch.Draw(sugar, corner2, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 6: 
                    spriteBatch.Draw(pan, centerStage, Color.White);
                    spriteBatch.Draw(rouxMix, centerStage, Color.White);
                    spriteBatch.Draw(water, centerStage, Color.White);
                    spriteBatch.Draw(sugar, centerStage, Color.White); 
                    spriteBatch.Draw(hintSwipeHoriz, centerStage, Color.White);
                    break;

                case 7: 
                    spriteBatch.Draw(appleRaw, centerStage, Color.White);
                    spriteBatch.Draw(pieSauce, corner4, Color.White); 
                    spriteBatch.Draw(hintSwipeVert, centerStage, Color.White);
                    break;

                case 8: 
                    spriteBatch.Draw(appleSliced, corner1, Color.White); 
                    spriteBatch.Draw(cinnamon, corner2, Color.White);
                    spriteBatch.Draw(pieSauce, corner4, Color.White); 
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 9: 
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(appleSliced, corner1, Color.White);
                    spriteBatch.Draw(cinnamon, corner2, Color.White);
                    spriteBatch.Draw(pieSauce, corner4, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 10: 
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(appleSliced, centerStage, Color.White); 
                    spriteBatch.Draw(cinnamon, corner2, Color.White);
                    spriteBatch.Draw(pieSauce, corner4, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 11: 
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(appleSliced, centerStage, Color.White);
                    spriteBatch.Draw(cinnamon, centerStage, Color.White); 
                    spriteBatch.Draw(pieSauce, corner4, Color.White); 
                    spriteBatch.Draw(hintSwipeHoriz, centerStage, Color.White);
                    break;
                case 12: 
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(appleCinnamonMix, centerStage, Color.White); 
                    spriteBatch.Draw(pieSauce, corner4, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 13: 
                    spriteBatch.Draw(emptyBowl, centerStage, Color.White);
                    spriteBatch.Draw(appleCinnamonMix, centerStage, Color.White);
                    spriteBatch.Draw(pieSauce, centerStage, Color.White); 
                    spriteBatch.Draw(hintSwipeHoriz, centerStage, Color.White);
                    break;

                case 14: 
                    spriteBatch.Draw(appleFilling, corner4, Color.White); 
                    spriteBatch.Draw(doughBall, centerStage, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 15: 
                    spriteBatch.Draw(appleFilling, corner4, Color.White);
                    spriteBatch.Draw(doughBall, centerStage, Color.White);
                    spriteBatch.Draw(hintSwipeVert, centerStage, Color.White);
                    break;
                case 16: 
                    spriteBatch.Draw(appleFilling, corner4, Color.White);
                    spriteBatch.Draw(rolledDough, corner1, Color.White); 
                    spriteBatch.Draw(piePan, centerStage, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 17: 
                    spriteBatch.Draw(appleFilling, corner4, Color.White);
                    spriteBatch.Draw(piePan, centerStage, Color.White);
                    spriteBatch.Draw(rolledDough, corner1, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 18: 
                    spriteBatch.Draw(appleFilling, corner4, Color.White);
                    spriteBatch.Draw(crustInPan, centerStage, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;

                case 19: 
                    spriteBatch.Draw(pieWithFilling, corner4, Color.White); 
                    spriteBatch.Draw(doughBall, centerStage, Color.White);
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 20: 
                    spriteBatch.Draw(pieWithFilling, corner4, Color.White);
                    spriteBatch.Draw(doughBall, centerStage, Color.White);
                    spriteBatch.Draw(hintSwipeVert, centerStage, Color.White);
                    break;
                case 21: 
                    spriteBatch.Draw(pieWithFilling, corner4, Color.White);
                    spriteBatch.Draw(rolledDough, centerStage, Color.White);
                    spriteBatch.Draw(hintSwipeVert, centerStage, Color.White);
                    break;
                case 22: 
                    spriteBatch.Draw(pieWithFilling, centerStage, Color.White); 
                    spriteBatch.Draw(doughStrips, corner1, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 23: 
                    spriteBatch.Draw(pieCrissCross, centerStage, Color.White); 
                    spriteBatch.Draw(eggWashBrush, corner1, Color.White); 
                    spriteBatch.Draw(hintSwipeHoriz, centerStage, Color.White); 
                    break;

                case 24: 
                    spriteBatch.Draw(pieWashed, corner1, Color.White); 
                    spriteBatch.Draw(hintPressE, centerStage, Color.White);
                    break;
                case 25: 
                    spriteBatch.Draw(pieWashed, centerStage, Color.White); 
                    
                    Vector2 bakeBar = new Vector2(centerStage.X, centerStage.Y - 50);
                    spriteBatch.Draw(uiBarBackground, bakeBar, Color.White);
                    float bakeNeedleX = bakeBar.X + ((cookingProgress / 100f) * uiBarBackground.Width);
                    spriteBatch.Draw(uiMeterNeedle, new Vector2(bakeNeedleX, bakeBar.Y - 10), Color.White);
                    
                    spriteBatch.Draw(hintSpacebar, centerStage, Color.White);
                    break;
                case 26: 
                    spriteBatch.Draw(pieCooked, centerStage, Color.White); 
                    
                    if (finalScore == 1)
                        spriteBatch.Draw(popupAmazing, centerStage, Color.White);
                    else
                        spriteBatch.Draw(popupMiss, centerStage, Color.White);
                    break;
            }
        }
    }
}