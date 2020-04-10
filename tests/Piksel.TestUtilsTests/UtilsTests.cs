using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static Piksel.Testing.Utils;

namespace Piksel.TestUtils.Tests
{
    public class UtilsTests
    {
        [Test]
        public void Repeat_Executes_Correct_Times()
        {
            var i = 0;
            Repeat(10, _ => i += 1);

            Assert.AreEqual(10, i);
        }

        [Test]
        public void Repeat_Supplies_Index()
        {
            var i = 0;
            Repeat(10, ix =>
            {
                Assert.AreEqual(i, ix);
                ix += 1;
            });
        }
    }
}
