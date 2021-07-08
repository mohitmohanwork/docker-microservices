using Blaash.Gaming.Service.Game.Services.Api.Game.Models;
using Blaash.Gaming.Service.Game.test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZNxt.Net.Core.Helpers;

namespace Blaash.Gaming.Service.Game.test
{
    [TestClass]
    public class GameControllerUnitTest
    {
        [TestMethod]
        public void AddGame()
        {
            var request = new GameDbo()
            {
                name = "Game 1",
                description = "Game 1 desc",
                banner_image_url = "http://b",
                game_url = "https://b",
                game_type = GameType.social
            }.ToJObject();

            var gamectrl = ControllerHelper.GetGameController(request);
            var response = gamectrl.AddGame();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void UpdateGame()
        {
            var request = new GameDbo()
            {
                game_id = 1,
                name = "Game 11111",
                description = "Game 11111 desc",
                banner_image_url = "http://b1111",
                game_url = "https://b1111",
                game_type = GameType.social
            }.ToJObject();

            var gamectrl = ControllerHelper.GetGameController(request);
            var response = gamectrl.UpdateGame();

            request = new GameDbo()
            {
                game_id = 1,
                name = "Game 1",
                description = "Game 11111 desc",
                banner_image_url = "http://b1111",
                game_url = "https://b1111",
                game_type = GameType.social
            }.ToJObject();

             response = gamectrl.UpdateGame();
            Assert.AreEqual("1", response["code"].ToString());

        }

        [TestMethod]
        public void InsertBulk()
        {
            for (int i = 0; i < 100; i++)
            {
                var request = new GameDbo()
                {
                    name = $"Game {CommonUtility.RandomString(10)}",
                    description = $"Game {CommonUtility.RandomString(10)}",
                    banner_image_url = $"http://b{CommonUtility.RandomString(10)}",
                    game_url = $"https://b{CommonUtility.RandomString(10)}",
                    game_type = GameType.social
                }.ToJObject();

                var gamectrl = ControllerHelper.GetGameController(request);
                var response = gamectrl.AddGame();
                Assert.AreEqual("1", response["code"].ToString());

            }
           

        }
        [TestMethod]
        public void GetGame()
        {
           
            var gamectrl = ControllerHelper.GetGameController(null, new System.Collections.Generic.Dictionary<string, string>() {  ["game_id"] = "1"});
            var response = gamectrl.GetGameById();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetGameByFilterAll()
        {

            var gamectrl = ControllerHelper.GetGameController();
            var response = gamectrl.GetGameByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetGameByFilterName()
        {

            var gamectrl = ControllerHelper.GetGameController(null, new System.Collections.Generic.Dictionary<string, string>() { ["name"] = "Game 1" });
            var response = gamectrl.GetGameByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetGameByFilterPagesize()
        {

            var gamectrl = ControllerHelper.GetGameController(null, new System.Collections.Generic.Dictionary<string, string>() { ["currentpage"] = "2", ["pagesize"] = "20" });
            var response = gamectrl.GetGameByFilter();
            Assert.AreEqual("1", response["code"].ToString());

        }


        [TestMethod]
        public void AddTenantGame()
        {
            var request = new TenantGameDbo()
            {
               game_id = 1,
               tenant_id = 1,
               is_active = true
            }.ToJObject();

            var gamectrl = ControllerHelper.GetGameController(request);
            var response = gamectrl.AddTenantGame();
            Assert.AreEqual("1", response["code"].ToString());

        }
        [TestMethod]
        public void GetTenantGame()
        {

            var gamectrl = ControllerHelper.GetGameController(querystring:new System.Collections.Generic.Dictionary<string, string>() {["tenant_id"] = "1" });
            var response = gamectrl.GetTenantGame();
            Assert.AreEqual("1", response["code"].ToString());

        }
    }
}
