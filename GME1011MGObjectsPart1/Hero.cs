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
    internal class Hero
    {
        //Hero attributes
        private string _name;
        private int _health;
        private Texture2D _currentSprite, _healthTexture;
        private SpriteFont _heroFont;
        private float _x, _y;
        private float _speed;
        private bool _isDead;

        //Constructor
        public Hero(string name, int health, Texture2D heroSprite, Texture2D healthSprite, SpriteFont heroFont, int x, int y)
        {
            _currentSprite = heroSprite;
            _healthTexture = healthSprite;
            _name = name;
            _health = health;
            _x = x;
            _y = y;
            _speed = 5;
            _isDead = false;
            _heroFont = heroFont;
         }


        //Accessors
        public int GetHealth() {  return _health; }
        public string GetName() { return _name; }
        public bool IsDead() { return _isDead; }

        //Mutators
        public void TakeDamage(int damage) { _health -= damage; }
        public void Heal(int healing) { _health += healing; }

        //Helper
        public int DealDamage()
        {
            Random rng = new Random();
            return rng.Next(1, 4);
        }

        //For debug purposes
        public override string ToString() { 
            if(!IsDead()) 
                return "Hero[" +  _name + ", " + _health + "]";
            else
                return "Hero[" + _name + ", has perished!]";
        }

        //For Collision checks
        public Rectangle GetBounds()
        {
            return new Rectangle((int)_x, (int)_y, _currentSprite.Width, _currentSprite.Height);
        }

        //All update logic for the hero
        public void Update(GameTime gameTime)
        {
            //If not dead, move.
            if (!IsDead())
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    _x -= _speed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    _x += _speed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    _y -= _speed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    _y += _speed;
                }
            }
            
            //We died.
            if(_health <= 0)
            {
                _isDead = true;
            }

        }

        //All hero draw logic.
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //main sprite
            spriteBatch.Draw(_currentSprite, new Vector2(_x, _y), Color.White);

            //for centering the health sprites
            float startX = _x + (_currentSprite.Width / 2) - ((_health * 30)/2);
            //draw all the health sprites
            for (int i = 0; i < _health; i++) 
            {
                spriteBatch.Draw(_healthTexture, new Vector2( startX + (i * 30), _y - 35), Color.White); 
            }

            //draw hero name
            spriteBatch.DrawString(_heroFont, _name, new Vector2(_x + 25, _y + _currentSprite.Height), Color.White);

            spriteBatch.End();
        }
    }
}
