using Merlin2d.Game;
using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;
using MerlinCor.Actors;
using MerlinCor.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Spells
{
    public class ProjectileSpell : AbstractActor, IMovable, ISpell
    {
        private ISpeedStrategy strategy;
        private IWizard wizard;
        private Commands.Move move;
        private IEnumerable<IAction<AbstractCharacter>> effects;
        private AbstractCharacter myenemy;

        public ProjectileSpell(IWizard wizard, IEnumerable<IAction<AbstractCharacter>> effects)
        {
            this.wizard = wizard;
            this.effects = effects;
            wizard.GetWorld().AddActor(this);
            this.SetPhysics(true);

        }

        public ISpell AddEffect(IAction<AbstractCharacter> effect)
        {
            myenemy.AddEffect(effect);
            return this;
        }

        public void AddEffects(IEnumerable<IAction<AbstractCharacter>> effects)
        {
            foreach (IAction<AbstractCharacter>  effect in effects)
            {
                AddEffect(effect);
            }
        }


        public void Cast()
        {
            if ((wizard).GetSide() == ActorOrientation.FacingRight)
            {
                move = new Commands.Move(this, 5, 1, 0);
                this.SetPosition(wizard.GetX() + 70, wizard.GetY() - 10);
            }
            else if((wizard).GetSide() == ActorOrientation.FacingLeft)
            {
                move = new Commands.Move(this, 5, -1, 0);
                this.SetPosition(wizard.GetX() - 70, wizard.GetY() - 10);
            }
            else
            {
                move = new Commands.Move(this, 3, 0, -1);
                this.SetPosition(wizard.GetX() + wizard.GetWidth() / 2, wizard.GetY() + wizard.GetHeight());
            }
        }

        public int GetCost()
        {
            return 0;
        }

        public double GetSpeed(double speed)
        {
            return this.strategy.GetSpeed(speed);
        }

        public void SetSpeedStrategy(ISpeedStrategy strategy)
        {
            this.strategy = strategy;
        }

        public override void Update()
        {
            foreach (var enemy in this.wizard.GetWorld().GetActors())
            {
                if (this.IntersectsWithActor(enemy))
                {
                    if (enemy.GetType() != typeof(ProjectileSpell) && enemy != wizard && enemy is AbstractCharacter)
                    {
                        myenemy = (AbstractCharacter)enemy;
                        this.wizard.GetWorld().RemoveActor(this);
                        AddEffects(effects);
                    }
                }
            }
            if (move != null) {
                move.Execute();
            }
        }
    }
}
