﻿using System;
using System.Collections.Generic;
using ConsoleRPG.Mobs;

namespace ConsoleRPG.GameComponents
{
    internal enum Events { Battle, Nothing, Items }

    internal class Event
    {
        public static void RandomEvent(Player player)
        {
            Console.Clear();
            switch ((Events)GameManager.rand.Next(3))
            {
                case Events.Battle:
                    Battle(player);
                    break;

                case Events.Nothing:
                    Items(player);
                    break;

                case Events.Items:
                    Nothing(player.name);
                    break;
            }
        }

        private static void Battle(Player player)
        {
            var enemy = GameManager.RandomEnemy(player);
            Console.WriteLine($"{player.name} found an enemy, {enemy.name} !");
            Console.ReadLine();
        Start:
            Console.Clear();
            Console.WriteLine($"{enemy.name}:\nLife: {enemy.life}\nLevel: {enemy.level}\nAttack damage: {enemy.AttackDamage}\nStrength: {enemy.Strength}\nResistence: {enemy.Resistence}\nSpeed: {enemy.Speed}\n\n" +
                $"{player.name}:\nLife: {player.life}\nLevel: {player.level}\nAttack damage: {player.AttackDamage}\nStrength: {player.Strength}\nResistence: {player.Resistence}\nSpeed: {player.Speed}\nMana: {player.mana}\n\n" +
                $"1. Light attack ({player.lightAttack.ToString().Replace("_", "")})  2. Heavy attack ({player.heavyAttack.ToString().Replace("_", "")})  3. Run away  4. Check inventory\n");
            switch (Console.ReadLine())
            {
                case "1":
                    player.LightAttack(player.lightAttack, enemy);
                    break;

                case "2":
                    if (player.mana - 10 <= 0)
                    {
                        Console.WriteLine($"{player.name} has no mana !");
                        Console.ReadLine();
                        goto Start;
                    }
                    else player.HeavyAttack(player.heavyAttack, enemy);
                    break;

                case "3":
                    if (player.TryRunAway(enemy.Speed))
                    {
                        Console.WriteLine($"{player.name} ran away !");
                        goto End;
                    }
                    else Console.WriteLine($"{player.name} failed to run away !");
                    break;

                case "4":
                    player.Inventory();
                    goto Start;

                default:
                    Console.WriteLine("Invalid value !");
                    Console.ReadLine();
                    goto Start;
            }

            Console.Clear();
            if (enemy.life <= 0)
            {
                GameManager.EnemyKilled(player, enemy);
                goto End;
            }
            else
            {
                enemy.AI(player);
                if (player.life <= 0)
                {
                    GameManager.GameOver(player);
                    goto End;
                }
                else goto Start;
            }

        End:;
        }

        private static void Items(Player player)
        {
            List<Item> itemsFound = Item.RandomList(1, 5);
            Console.WriteLine($"{player.name} found {itemsFound.Count} item(s) !\n");
            foreach (var item in itemsFound)
            {
                Console.WriteLine($"{item.name}\nPrice: {item.price}\nDescription: {item.description}\n");
                player.inventory.Add(item);
            }
            Console.ReadLine();
        }

        private static void Nothing(string playerName)
        {
            Console.WriteLine($"{playerName} found nothing !");
            Console.ReadLine();
        }
    }
}