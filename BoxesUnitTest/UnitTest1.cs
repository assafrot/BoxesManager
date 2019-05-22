using BoxesManger.DataStructers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace BoxesUnitTest
{
    [TestClass]
    public class UnitTest1
    {

        BoxesManager test = new BoxesManager(new DebugNotifier());
        public UnitTest1()
        {
            test.UpdateSupply(1, 3, 6);
            test.UpdateSupply(1, 2.5, 13);
            test.UpdateSupply(1.6, 2, 10);
            test.UpdateSupply(1, 7, 12);
        }


        [TestMethod]
        public void LoadBoxestest()
        {
            test.UpdateSupply(3, 4, 20);
            test.UpdateSupply(3, 5, 21);
            test.UpdateSupply(4, 5, 20);
            //test.GetBoxesSupply(3, 5, 21);
            //test.GetBoxesSupply(4, 5, 20);
            //test.GetBoxesSupply(3, 1, 20);
            Assert.IsNotNull(test);

        }

        [TestMethod]
        public void GetAmount()
        {
            test.UpdateSupply(3, 4, 20);
            test.UpdateSupply(3, 4, 10);
            test.UpdateSupply(3, 5, 21);
            test.UpdateSupply(4, 5, 20);
            test.UpdateSupply(3, 1, 20);
            test.GetBoxAmount(3, 4, out int x);
            Assert.IsTrue(x == 30);
        }

        [TestMethod]
        public void GetGiftBox()
        {
            test.UpdateSupply(3, 4, 5);
            test.UpdateSupply(3, 5, 21);
            test.UpdateSupply(4, 7, 20);
            test.UpdateSupply(3, 1, 20);
            test.GetGiftBox(2, 2, out double x, out double y);
            test.GetBoxAmount(3, 4, out int amt);
            Assert.IsTrue(x == 3 && y == 4 && amt == 4);
            test.GetGiftBox(4, 8, out x, out y);
            Assert.IsTrue(x == 0 && y == 0);
            test.GetGiftBox(3, 6, out x, out y);
            Assert.IsTrue(x == 4 && y == 7);
        }

        [TestMethod]
        public void GetLastGiftBox()
        {
            test.UpdateSupply(3, 4, 1);
            test.UpdateSupply(3, 5, 1);
            test.UpdateSupply(4, 5, 1);
            test.UpdateSupply(3, 1, 1);
            test.GetGiftBox(3, 4, out double x, out double y);
            test.GetBoxAmount(3, 4, out int tmp);
            Assert.IsTrue(tmp == 0);
            test.GetGiftBox(3, 5, out x, out y);
            test.GetBoxAmount(3, 5, out tmp);
            Assert.IsTrue(tmp == 0);
            test.GetGiftBox(4, 5, out x, out y);
            test.GetBoxAmount(4, 5, out tmp);
            Assert.IsTrue(tmp == 0);
        }

        [TestMethod]
        public void GetRidOfBoxes()
        {
            test.UpdateSupply(3, 4, 20);
            test.UpdateSupply(3, 5, 21);
            Thread.Sleep(10000);
            test.UpdateSupply(3, 6, 21);
            test.GetRidOfBoxes();
            test.GetBoxAmount(3, 4, out int x);
            test.GetBoxAmount(3, 5, out int y);
            test.GetBoxAmount(3, 6, out int z);
            Assert.IsTrue(x == 0 && y == 0 && z == 21);
        }

    }
}
