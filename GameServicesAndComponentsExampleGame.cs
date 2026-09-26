using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameServicesAndComponentsExercise;

public class GameServicesAndComponentsExampleGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private uint _spaceKeyPressCount = 0;
    private KeyboardState _previousKeyboardState;
    private IAchievementService _achievementService;

    public GameServicesAndComponentsExampleGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        
        _achievementService = new StandaloneAchievementService(this);
        Services.AddService<IAchievementService>(_achievementService);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _achievementService.UpdateAchievement("Game was loaded!", 100);

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var currentKeyboardState = Keyboard.GetState();

        if(_spaceKeyPressCount < 100 && currentKeyboardState.IsKeyDown(Keys.Space) && _previousKeyboardState.IsKeyUp(Keys.Space))
        {
            _spaceKeyPressCount++;

            // TODO: Advance achievement
            _achievementService.UpdateAchievement("Press space 100 times!", _spaceKeyPressCount);
        }

        _previousKeyboardState = currentKeyboardState;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
