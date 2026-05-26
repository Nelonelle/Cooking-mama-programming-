using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CookingSimulator 
{
    public class UIManager
    {
        private Texture2D _pixel;
        private SpriteBatch _sb;
        
        public UIManager(GraphicsDevice gd, SpriteBatch sb)
        {
            _pixel = new Texture2D(gd, 1, 1);
            _pixel.SetData(new[] { Color.White });
            _sb = sb;
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
            DrawText("COOKING SIMULATOR", 450, 60, Color.Gold, true);
            DrawText("Choose a recipe:", 450, 110, Color.White, true);
            
            Color[] colors = { Color.DarkRed, Color.DarkGreen, Color.DarkOrange };
            for (int i = 0; i < 3; i++)
            {
                Color btnColor = i == hover ? colors[i] * 1.2f : colors[i];
                DrawRect(300, 160 + i * 100, 300, 60, btnColor);
                DrawText(names[i].ToUpper(), 450, 185 + i * 100, Color.White, true);
                DrawText($"{stages[i][0]} → {stages[i][1]} → {stages[i][2]}", 450, 210 + i * 100, Color.LightGray, true);
            }
        }
        
        public void DrawGameUI(string recipe, int stage, string[] stages, string[] needs,
                               float timer, float maxTime, int score, int combo, float comboTime,
                               string msg, float msgTime)
        {
            // Header
            DrawText($"RECIPE: {recipe}", 450, 15, Color.Gold, true);
            DrawText($"STAGE {stage+1}/3: {stages[stage]}", 20, 15, Color.Cyan);
            
            // Stage progress
            for (int i = 0; i < 3; i++)
            {
                Color c = i < stage ? Color.Green : (i == stage ? Color.Yellow : Color.Gray);
                DrawRect(20, 45 + i * 20, 150, 12, c);
                DrawText(stages[i], 180, 45 + i * 20, c);
            }
            
            // Timer
            float p = timer / maxTime;
            DrawRect(20, 120, (int)(200 * p), 15, timer < 3 ? Color.Red : Color.Lime);
            DrawText($"Time: {timer:F1}s", 20, 140, timer < 3 ? Color.Red : Color.White);
            
            // Score & Combo
            DrawText($"Score: {score}", 20, 170, Color.Gold);
            if (combo > 1)
            {
                DrawText($"COMBO x{combo}!", 20, 195, Color.Orange);
                DrawRect(20, 215, (int)((comboTime / 3f) * 100), 5, Color.Orange);
            }
            
            DrawText($"Need: {needs[stage]}", 20, 240, Color.Cyan);
            
            // Message
            if (msgTime > 0)
                DrawText(msg, 450, 300, Color.Yellow, true);
        }
        
        public void DrawStations(Rectangle cut, Rectangle cook, Rectangle mix)
        {
            // Cut
            DrawRect(cut.X, cut.Y, cut.Width, cut.Height, Color.Orange);
            DrawText("CUT", cut.X + 60, cut.Y + 50, Color.White, true);
            
            // Cook
            DrawRect(cook.X, cook.Y, cook.Width, cook.Height, Color.Red);
            DrawText("COOK", cook.X + 60, cook.Y + 50, Color.White, true);
            
            // Mix
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
                DrawText($"✓ {stages[i]}", 450, 250 + i * 30, Color.LightGreen, true);
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