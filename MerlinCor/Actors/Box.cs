using Merlin2d.Game;
using Merlin2d.Game.Actors;
using MerlinCor.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public class Box: AbstractCharacter
    {
        private Animation box;
        private Animation boxPressed;
        private Jump<IActor> jump;
        private int counter;
        private Player myPlayer;
        public Box(string name, int x, int y)
        {
            box = new Animation("resources/springboardUp.png", 70, 70);
            boxPressed = new Animation("resources/springboardDown.png", 70, 70);
            this.SetName(name);
            this.SetPosition(x, y);
            this.SetAnimation(box);
            jump = new Jump<IActor>(62);
            counter = 0;
        }

        public override void Update()
        {
            foreach (var player in this.GetWorld().GetActors())
            {
                if(player is Player && this.IntersectsWithActor(player))
                {
                    jump = new Jump<IActor>(62);
                    counter++;
                    this.SetAnimation(boxPressed);
                    myPlayer = (Player)player;
                    jump.Execute(myPlayer);
                    
                }

            }
            if(counter!= 0)
            {
                jump.Execute(myPlayer);
                counter++;
                myPlayer.SetPosition(myPlayer.GetX(), myPlayer.GetY() + 2);
                if (counter == 61 || myPlayer.GetWorld().IntersectWithWall(myPlayer))
                {
                    counter = 0;
                    this.SetAnimation(box);
                }
                myPlayer.SetPosition(myPlayer.GetX(), myPlayer.GetY() - 2);
            }
        }
    }
}
