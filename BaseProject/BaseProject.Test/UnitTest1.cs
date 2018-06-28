using BaseProject.Data;
using FluentAssertions;
using System;
using Xunit;

namespace BaseProject.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            "apple".Should().StartWith("a").And.EndWith("e").And.HaveLength(5);
        }
    }
}
