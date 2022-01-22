using Merlin2d.Game;
using Merlin2d.Game.Actors;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public abstract class AbstractActor : IActor
    {
        private string name;
        private Animation animation;
        private IWorld world;
        private bool isRemoved;
        private bool isAffectedByPhysics = true;
        private int posX;
        private int posY;
        public AbstractActor(): this(string.Empty)
        {

        }
        public AbstractActor(string name)
        {
            this.name = name;
        }


        public Animation GetAnimation()
        {
            return this.animation;
        }
        public void SetAnimation(Animation animation)
        {
            this.animation = animation;
        }


        public int GetHeight()  
        {
            return animation.GetHeight();
        }
        public int GetWidth()
        {
            return animation.GetWidth();
        }


        public string GetName()
        {
            return this.name;
        }
        public void SetName(string name)
        {
            this.name = name;
        }


        public int GetX()
        {
            return this.posX;
        }
        public int GetY()
        {
            return this.posY;
        }
        public void SetPosition(int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
        }


        public bool IntersectsWithActor(IActor other)
        {
            int xDistance = this.GetX() - other.GetX();
            int yDistance = this.GetY() - other.GetY();
            int mainHeight = (yDistance < 0) ? this.GetHeight() : other.GetHeight();
            int mainWidth = (xDistance < 0) ? this.GetWidth() : other.GetWidth();

            if (Math.Abs(xDistance) <= mainWidth && Math.Abs(yDistance) <= mainHeight)
            {
                return true;
            }
            return false;
        }

        public void OnAddedToWorld(IWorld world)
        {
            this.world = world;
        }
        public void RemoveFromWorld()
        {
            isRemoved = true;
        }
        public bool RemovedFromWorld()
        {
            return isRemoved;
        }
        public IWorld GetWorld()
        {
            return world;
        }


        public bool IsAffectedByPhysics()
        {
            return isAffectedByPhysics;
        }

        public void SetPhysics(bool isPhysicsEnabled)
        {
            this.isAffectedByPhysics = isPhysicsEnabled;
        }

        public abstract void Update();
    }
}
