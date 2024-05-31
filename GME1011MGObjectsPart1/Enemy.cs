using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GME1011MGObjectsPart1
{
    internal class Enemy
    {

        //Enemy attributes
        private int _health;
        private Texture2D _currentSprite;
        private float _x, _y, _speed;
        private Random _rng;

        //Constructor - pay attention to random attributes that are set inside.
        public Enemy(int health, Texture2D currentSprite)
        {
            _health = health;
            _currentSprite = currentSprite;
            _rng = new Random();
            _x = 800 + _rng.Next(200, 5000);
            _y = _rng.Next(1, 301);
            _speed = _rng.Next(200, 500)/100f;
        }

        //need to know where the Enemy is
        public float GetX() { return _x; }


        //For collision checks
        public Rectangle GetBounds()
        {
            return new Rectangle((int)_x, (int)_y, _currentSprite.Width, _currentSprite.Height);
        }

        //This is all that the Enemy does - move to the left
        public void Update(GameTime gameTime)
        {
            _x -= _speed;
        }

        //Enemy draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_currentSprite, new Vector2(_x, _y), Color.White);
            spriteBatch.End();
        }

    }
}
