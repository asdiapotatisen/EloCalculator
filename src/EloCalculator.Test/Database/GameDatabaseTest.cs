﻿namespace EloCalculator.Test
{
    using System;
    using System.Collections.Generic;
    using EloCalculator;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="GameDatabase"/>.
    /// </summary>
    [TestClass]
    public class GameDatabaseTest
    {
        /// <summary>
        /// Tests <see cref="GameDatabase.Games"/>.
        /// </summary>
        [TestMethod]
        public void GameDatabaseGames()
        {
            Game game = new(new("Player 1"), new("Player 2"), Result.White, DateTime.Now, true);

            List<Game> games = new()
            {
                game,
            };

            CollectionAssert.AreEquivalent(games, GameDatabase.Games);

            PlayerDatabase.Players.Clear();
            GameDatabase.Games.Clear();
        }

        /// <summary>
        /// Tests <see cref="GameDatabase.Load(string)"/> and <see cref="GameDatabase.Export(string)"/>.
        /// </summary>
        [TestMethod]
        public void GameDatabaseLoadAndExport()
        {
            _ = new Game(new("Player 1"), new("Player 2"), Result.White, DateTime.Now, true);

            GameDatabase.Export("game.json");

            GameDatabase.Games.Clear();

            GameDatabase.Load("game.json");

            Game actual = GameDatabase.Games[0];

            Game expected = new(PlayerDatabase.Players[0], PlayerDatabase.Players[1], Result.White, DateTime.Now, true);

            Assert.AreEqual(0, actual.ID);
            Assert.AreEqual(expected.WhitePlayer, actual.WhitePlayer);
            Assert.AreEqual(expected.WhiteID, actual.WhiteID);
            Assert.AreEqual(expected.BlackPlayer, actual.BlackPlayer);
            Assert.AreEqual(expected.BlackID, actual.BlackID);
            Assert.AreEqual(expected.Result, actual.Result);
            Assert.AreEqual(expected.Rated, actual.Rated);

            PlayerDatabase.Players.Clear();
            GameDatabase.Games.Clear();
        }
    }
}