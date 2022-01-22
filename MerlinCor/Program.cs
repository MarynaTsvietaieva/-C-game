using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Enums;
using MerlinCor.Actors;
using MerlinCor.Commands;
using MerlinCor.Factories;
using MerlinCor.Item;
using MerlinCor.Spells;
using MerlinCor.Spells.Effects;
using OneOf.Types;
using System;
using System.Collections.Generic;

namespace MerlinCor
{
    class Program
    {
        static void Main(string[] args)
        {
            GameContainer container = new GameContainer("My game", 1900, 950, true);
            container.AddWorld("resources/maps/level1.tmx");
            container.GetWorld(0).SetPhysics(new Gravity());
            container.SetCameraFollowStyle(CameraFollowStyle.CenteredInsideMapPreferBottom);
            container.GetWorld(0).SetFactory(new ActorFactory());
            Player actor = null;
            container.GetWorld(0).AddInitAction(world =>
            {
                actor = (Player)world.GetActors().Find(x => x.GetName() == "Pink");
                world.CenterOn(actor);
                actor.SetPhysics(true);

                List<IActor> enemy = world.GetActors().FindAll(x => x.GetName() == "spider");
                foreach (Spider e in enemy)
                {
                    world.CenterOn(e);
                    e.SetPhysics(true);
                    e.AddPlayer(actor);
                }

                Door winterDoor = (Door)world.GetActors().Find(x => x.GetName() == "WinterDoor");
                world.CenterOn(winterDoor);
                winterDoor.SetPhysics(false);

                Key key = (Key)world.GetActors().Find(x => x.GetName() == "Key");
                world.CenterOn(key);
                key.SetPhysics(false);
                key.AddDoor(winterDoor);

                GravityOff gravityOff = (GravityOff)world.GetActors().Find(x => x.GetName() == "GravityOff");
                world.CenterOn(gravityOff);
                gravityOff.SetPhysics(true);

                SummonMagic summonMagic = (SummonMagic)world.GetActors().Find(x => x.GetName() == "SummonMagic");
                world.CenterOn(summonMagic);
                summonMagic.SetPhysics(false);

                Spikes spike = (Spikes)world.GetActors().Find(x => x.GetName() == "Spikes");
                world.CenterOn(spike);
                spike.SetPhysics(true);

            });
            container.GetWorld(0).SetEndCondition(world =>
            {
                if (world.GetActors().Find(x => x.GetName() == "Pink").GetX() > 1750 &&
                    world.GetActors().Find(x => x.GetName() == "Pink").GetY() < 560 &&
                    world.GetActors().Find(x => x.GetName() == "Pink").GetY() > 470)
                {
                    return MapStatus.Finished;
                }
                if (((Player)world.GetActors().Find(x => x.GetName() == "Pink")).GetState() is DyingState || (
                world.GetActors().Find(x => x.GetName() == "Pink").GetY() > 810 && 
                world.GetActors().Find(x => x.GetName() == "Pink").GetX() > 630 &&
                world.GetActors().Find(x => x.GetName() == "Pink").GetX() < 980))
                {
                    return MapStatus.Failed;
                }
                return MapStatus.Unfinished;
            });

            container.AddWorld("resources/maps/level2.tmx");
            container.GetWorld(1).SetFactory(new ActorFactory());
            container.GetWorld(1).SetPhysics(new Gravity());
            container.GetWorld(1).AddInitAction(world =>
            {
                actor = (Player)world.GetActors().Find(x => x.GetName() == "Pink");
                world.CenterOn(actor);
                actor.SetPhysics(true);

                Bridge bridge1 = (Bridge)world.GetActors().Find(x => x.GetName() == "Bridge1");
                world.CenterOn(bridge1);
                bridge1.SetPhysics(false);
               
                Switch doorSwitch = (Switch)world.GetActors().Find(x => x.GetName() == "Switch");
                world.CenterOn(doorSwitch);
                doorSwitch.SetPhysics(false);

                Door door = (Door)world.GetActors().Find(x => x.GetName() == "Door");
                world.CenterOn(door);
                door.SetPhysics(false);
                doorSwitch.Subscribe(door);

                PressurePlate plate = (PressurePlate)world.GetActors().Find(x => x.GetName() == "RedButton");
                world.CenterOn(plate);
                plate.SetPhysics(true);
                plate.Subscribe(bridge1);

                HealingPotion healingPotion2 = (HealingPotion)world.GetActors().Find(x => x.GetName() == "HealingPotion2");
                world.CenterOn(healingPotion2);
                healingPotion2.SetPhysics(true);

                Box box = (Box)world.GetActors().Find(x => x.GetName() == "Box");
                world.CenterOn(box);
                box.SetPhysics(false);

                Spinner spinner = (Spinner)world.GetActors().Find(x => x.GetName() == "Spinner");
                world.CenterOn(spinner);
                spinner.SetPhysics(true);

                Autogun gunT = (Autogun)world.GetActors().Find(x => x.GetName() == "AutogunT");
                world.CenterOn(gunT);
                gunT.SetPhysics(false);

                Autogun gunL = (Autogun)world.GetActors().Find(x => x.GetName() == "AutogunL");
                world.CenterOn(gunL);
                gunL.SetPhysics(true);

                Autogun gun = (Autogun)world.GetActors().Find(x => x.GetName() == "AutogunR");
                world.CenterOn(gun);
                gun.SetPhysics(true);
            });
            container.GetWorld(1).SetEndCondition(world =>
            {
                if (world.GetActors().Find(x => x.GetName() == "Pink").GetX() > 1750 && world.GetActors().Find(x => x.GetName() == "Pink").GetY() < 140)
                {
                    return MapStatus.Finished;
                }
                if (((Player)world.GetActors().Find(x => x.GetName() == "Pink")).GetState() is DyingState || (
                    world.GetActors().Find(x => x.GetName() == "Pink").GetY() > 810))
                {
                    return MapStatus.Failed;
                }
                return MapStatus.Unfinished;
            });
            Message winMessage = new Message("Congratulations, you won", 850, 470,25, Color.White, MessageDuration.Short);
            Message lossMessage = new Message("Game over", 900, 470, 25, Color.White, MessageDuration.Short);
            container.SetEndGameMessage(winMessage, MapStatus.Finished);
            container.SetEndGameMessage(lossMessage, MapStatus.Failed);
            container.Run();
        }
    }
}