using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blaash.Gaming.Service.Game.test.Helpers;
using ZNxt.Net.Core.Consts;

namespace Blaash.Gaming.Service.Game.test
{
    [TestClass]
    public class PingControllerUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var pingctrl = ControllerHelper.GetPingController();
            var response = pingctrl.Ping();
            Assert.AreEqual(CommonConst._1_SUCCESS.ToString(), response[CommonConst.CommonField.HTTP_RESPONE_CODE].ToString());

        }
    }
}
