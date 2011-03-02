﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace ChainingAssertion
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void IsTest()
        {
            // "Is" extend on all object and has three overloads.

            // This same as Assert.AreEqual(25, Math.Pow(5, 2))
            Math.Pow(5, 2).Is(25);

            // lambda predicate assertion.
            // This same as Assert.IsTrue("foobar".StartsWith("foo") && "foobar".EndWith("bar"))
            "foobar".Is(s => s.StartsWith("foo") && s.EndsWith("bar"));

            // has collection assert
            // This same as CollectionAssert.AreEqual(Enumerable.Range(1,5), new[]{1, 2, 3, 4, 5})
            Enumerable.Range(1, 5).Is(1, 2, 3, 4, 5);
        }

        [Test]
        public void CollectionTest()
        {
            // if you want to use CollectionAssert Methods then use Linq to Objects and Is
            new[] { 1, 3, 7, 8 }.Contains(8).Is(true);
            new[] { 1, 3, 7, 8 }.Count(i => i % 2 != 0).Is(3);
            new[] { 1, 3, 7, 8 }.Any().Is(true);
            new[] { 1, 3, 7, 8 }.All(i => i < 5).Is(false);

            // IsOrdered
            var array = new[] { 1, 5, 10, 100 };
            array.Is(array.OrderBy(x => x));
        }

        [Test]
        public void OthersTest()
        {
            // Null Assertions
            Object obj = null;
            obj.IsNull();             // Assert.IsNull(obj)
            new Object().IsNotNull(); // Assert.IsNotNull(obj)

            // Not Assertion
            "foobar".IsNot("fooooooo"); // Assert.AreNotEqual
            new[] { "a", "z", "x" }.IsNot("a", "x", "z"); /// CollectionAssert.AreNotEqual

            // ReferenceEqual Assertion
            var tuple = Tuple.Create("foo");
            tuple.IsSameReferenceAs(tuple); // Assert.AreSame
            tuple.IsNotSameReferenceAs(Tuple.Create("foo")); // Assert.AreNotSame

            // Type Assertion
            "foobar".IsInstanceOf<string>(); // Assert.IsInstanceOfType
            (999).IsNotInstanceOf<double>(); // Assert.IsNotInstanceOfType
        }

        [Test]
        [TestCase(1, 2, 3)]
        [TestCase(10, 20, 30)]
        [TestCase(100, 200, 300)]
        public void TestCaseTest(int x, int y, int z)
        {
            (x + y).Is(z);
            (x + y + z).Is(i => i < 1000);
        }

        [Test]
        [TestCaseSource("toaruSource")]
        public void TestTestCaseSource(int x, int y, string z)
        {
            string.Concat(x, y).Is(z);
        }

        public static object[] toaruSource = new[]
        {
            new object[] {1, 1, "11"},
            new object[] {5, 3, "53"},
            new object[] {9, 4, "94"}
        };

        [Test]
        public void TestTestTest()
        {
            // TODO:

            var lower = new[] { "a", "b", "c" };
            var upper = new[] { "A", "B", "C" };

            // Comparer CollectionAssert, use IComparer<T> or Comparison delegate
            // value is equality then return 0 else other number
            lower.Is(upper, StringComparer.InvariantCultureIgnoreCase);
            lower.Is(upper, (x, y) => x.ToUpper() == y.ToUpper() ? 0 : -1);
            lower.Is(upper, (x, y) => string.Compare(x, y, true));


            // or you can use Linq to Objects - SequenceEqual
            lower.SequenceEqual(upper, StringComparer.InvariantCultureIgnoreCase).Is(true);


        }

        // TODO:Write Throws Test
    }
}