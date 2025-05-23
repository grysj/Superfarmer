using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Superfarmer.Game.Dice;
using Superfarmer.Game.Models;
using Superfarmer.Game.Player;

namespace Superfarmer.Game;



public class Game
{
    private Dice.Dice _blueDice = new BlueDice();
    private Dice.Dice _redDice = new RedDice();
    private List<Player.IPlayer> _players = new List<Player.IPlayer>();
    public Ranch.Ranch Ranch { get;  }
    private int _round = 1;

    public Game()
    {
        Ranch = new Ranch.Ranch();

    }



    public void InitializeGame()
    {
        int totalPlayerCount = 0;
        int humanPlayerCount = 0;
        while (true)
        {
            Console.Write("Enter total number of players (2-4): ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out totalPlayerCount) && totalPlayerCount >= 2 && totalPlayerCount <= 4)
                break;
            Console.WriteLine("Invalid input. Please enter a number between 2 and 4.");
        }

        while (true)
        {
            Console.Write($"Enter number of human players (0-{totalPlayerCount}): ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out humanPlayerCount) && humanPlayerCount >= 0 && humanPlayerCount <= totalPlayerCount)
                break;
            Console.WriteLine($"Invalid input. Please enter a number between 0 and {totalPlayerCount}.");
        }

        for (int i = 1; i <= humanPlayerCount; i++)
        {
            Console.Write($"Enter name for Player {i}: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty. Try again.");
                i--;
                continue;
            }
            var player = new Player.HumanPlayer(_redDice, _blueDice, this);
            player.Name = name;
            AddPlayer(player);
        }

        for (int i = 1; i <= totalPlayerCount - humanPlayerCount; i++)
        {
            var bot = new Player.RandomPlayer(_redDice, _blueDice, this);
            bot.Name = $"Bot{i}";
            AddPlayer(bot);
        }
    }



    public void AddPlayer(Player.IPlayer player)
    {
        _players.Add(player);
    }

    public List<ITrader> GetAllTraders(ITrader trader)
    {
        var traders = new List<ITrader>();
        traders.AddRange(_players);
        traders.Add(Ranch);
        return traders.Where(x => x != trader).ToList();
    }


    private void StartTurn()
    {
        List<IPlayer> playersWhoWon = new List<IPlayer>();
        foreach (var player in _players)
        {
            player.TakeTurn();
            var playerWon = player.IsWinner();
            if (playerWon)
            {
                playersWhoWon.Add(player);
            }
        }
        if (playersWhoWon.Count == 1)
        {
            Console.WriteLine("The winner is: ");
            foreach (var winner in playersWhoWon)
            {
                Console.WriteLine(winner.Name);
            }
            Console.WriteLine($"Congratulations! You have won the game!");
            return;
        }
        if (playersWhoWon.Count > 1)
        {
            Console.WriteLine("There are multiple winners: ");
            foreach (var winner in playersWhoWon)
            {
                Console.WriteLine(winner.Name);
            }
            Console.WriteLine($"Congratulations! You have won the game!");
            return;
        }

        foreach (var player in playersWhoWon)
        {
           _players.Remove(player);
        }

        _round++;
    }

    public void StartGame()
    {
        InitializeGame();
        while (true)
        {
            Console.WriteLine($"Round {_round}");
            StartTurn();
            if (_players.Count <= 1)
            {
                Console.WriteLine("Game Over! No more players left.");
                break;
            }
        }
    }



}



