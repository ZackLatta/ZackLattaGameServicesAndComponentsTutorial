using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameServicesAndComponentsExercise;

public class StandaloneAchievementService : DrawableGameComponent, IAchievementService
{
    // a private dictionary for holding achievements
    Dictionary<string,uint> _achievement = new ();

    private struct Toast
    {
        public string Message{get; set;}

        public TimeSpan Lifetime {get; set;}

        public Vector2 Size {get; set;}
    }

    private List<Toast> _toasts = new();

    private SpriteBatch _spriteBatch;

    private SpriteFont _toastFont;

    public StandaloneAchievementService(Game game): base(game)
    {
        game.Components.Add(this);
        DrawOrder = int.MaxValue;
    }
    
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _toastFont = Game.Content.Load<SpriteFont>("Fonts/ToastFont");
    }

    public override void Update(GameTime gameTime)
    {
        for(int i = 0; i < _toasts.Count; i++)
        {
            var toast = _toasts[i];
            toast.Lifetime -= gameTime.ElapsedGameTime;
            _toasts[i] = toast;
            if(toast.Lifetime <= TimeSpan.Zero)
            {
                _toasts.RemoveAt(i);
                i--;
            }
        }
    }

    public override void Draw(GameTime gameTIme)
    {
        _spriteBatch.Begin();
        foreach(var toast in _toasts)
        {
            var position = new Vector2(GraphicsDevice.Viewport.Width - toast.Size.X - 50, 10 + (_toasts.IndexOf(toast) * 30));
            _spriteBatch.DrawString(_toastFont, toast.Message, position, Color.White);
        }
        _spriteBatch.End();
    }

    /// <summary>
    /// Adds and achievement to the achievement system
    /// </summary>
    /// <param name="name">name of the achievement</param>
    /// <param name="progress">the progress of the achievement (0-100)</param>
    public void UpdateAchievement(string name, uint progress)
    {
        string message = $"Achievement: '{name}' {progress}% complete!";
        _toasts.Add(new Toast()
        {
            Message = message,
            Lifetime = TimeSpan.FromSeconds(3),
            Size = _toastFont.MeasureString(message)
        });
        _achievement[name] = progress;
    }
}