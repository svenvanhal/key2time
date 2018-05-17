using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Timetabling.Exceptions;
using Timetabling.Helper;

namespace Timetabling.Algorithms.Tests
{

    [TestFixture]
    public class FetOutputProcessorTest
    {

        readonly string fetPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, Util.GetAppSetting("FetBinaryLocation"));

        [Test]
        public void test()
        {
        }


    }
}
