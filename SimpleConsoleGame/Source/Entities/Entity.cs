using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleGame.Source.Entities
{
    class Entity
    {
        public int posX, posY;
        public virtual char Character => '@';
        public virtual ConsoleColor Color => ConsoleColor.Magenta;
        public Map Map { get; set; }

        protected void Move(int offsetX, int offsetY)
        {
            if (!Map.IsWall(posX + offsetX, posY + offsetY))
            {
                posX += offsetX;
                posY += offsetY;

                UpdateCollision();
            }
        }

        protected Entity GetCollisionEntity()
        {
            return Map.entityList.FirstOrDefault(e => e != this && e.posX == posX && e.posY == posY);
        }

        protected void UpdateCollision()
        {
            var ent = GetCollisionEntity();

            if (ent != null)
            {
                OnCollide(ent);
                ent.OnCollide(this);
            }
        }

        public virtual void OnCollide(Entity other)
        {
            
        }

        public virtual void Update()
        {
            
        }

        public void Spawn()
        {
            Map = Map.Instance;
            Map.entitiesToAdd.Add(this);
        }

        public void Destroy()
        {
            Map.toRemove.Add(this);
        }
    }
}
