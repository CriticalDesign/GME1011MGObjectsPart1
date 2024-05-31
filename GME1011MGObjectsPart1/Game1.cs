using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GME1011MGObjectsPart1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //These are the attributes we set up
        private Hero _myHero;
        private int _heroNumber;
        private List<Hero> _myHeroes;
        private List<Enemy> _myEnemies;
        private bool _birthingHero; //silly boolean

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            //initialize lists and hero number
            _myHeroes = new List<Hero>();
            _myEnemies = new List<Enemy>();
            _heroNumber = 1;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //create initial hero and add them to the list - the list will only
            //have one hero, but the list makes it easy to keep track of whether or not
            //the hero is still in the game.
            _myHero = new Hero("CrocHero #" + _heroNumber, 5, Content.Load<Texture2D>("hero"), Content.Load<Texture2D>("health"), Content.Load<SpriteFont>("GameFont"), 300, 200);
            _myHeroes.Add(_myHero);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //If we have less than 3 enemies, add up to 3 enemies
            while (_myEnemies.Count <= 3)
            {
                _myEnemies.Add(new Enemy(1, Content.Load<Texture2D>("skellie")));
            }

            //for each enemy in the game...
            for(int i = 0; i < _myEnemies.Count; i++)
            {
                //...update the enemy (move)
                _myEnemies[i].Update(gameTime);

                //if the enemy leaves the screen by more than 100 pixels
                if (_myEnemies[i].GetX() < -100)
                {
                    //..terminate the enemy!
                    _myEnemies.RemoveAt(i);
                }
            }

            //Check for collisions with the hero - for every enemy...
            for(int i = 0; i < _myEnemies.Count; i++)
            {
                //..if the hero is in the game AND they are overlapping with the enemy
                if (_myHeroes.Count > 0 && _myEnemies[i].GetBounds().Intersects(_myHeroes[0].GetBounds()))
                {
                    //hero takes damage
                    _myHeroes[0].TakeDamage(1);
                    //enemy is removed
                    _myEnemies.RemoveAt(i);
                }
            }

            //for all heroes in the game (doesn't run if there are 0 heroes)
            for (int i = 0; i < _myHeroes.Count; i++)
            {
                //run hero update
                _myHeroes[i].Update(gameTime);

                //if the hero is dead
                if (_myHeroes[i].GetHealth() <= 0)
                {
                    //take them out of the list
                    _myHeroes.RemoveAt(i);
                }
            }

            //This is a silly way to create a new hero, if we want one to come back into our game
            if (Keyboard.GetState().IsKeyDown(Keys.Tab) && !_birthingHero && _myHeroes.Count < 1)
            {
                _heroNumber++;
                _myHeroes.Add(new Hero("CrocHero #" + _heroNumber, 5, Content.Load<Texture2D>("hero"), Content.Load<Texture2D>("health"), Content.Load<SpriteFont>("GameFont"), 300, 200));
                _birthingHero = true;
            }
            if (!Keyboard.GetState().IsKeyDown(Keys.Tab))
            {
                _birthingHero = false;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            //for each hero in the list, draw them
            foreach (Hero hero in _myHeroes)
                hero.Draw(_spriteBatch);

            //for each enemy in the list, draw them
            foreach (Enemy enemy in _myEnemies)
                enemy.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
