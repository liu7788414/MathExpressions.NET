﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;

namespace MathFunctions.Tests
{
	[TestFixture]
	public class MathFuncTests
	{
		MathExprParser Parser;

		[SetUp]
		public void InitParser()
		{
			Parser = new MathExprParser();

			var rools = new StringBuilder();

			var derivatives = new StringBuilder();

			derivatives.AppendLine("(f(x) / g(x))' = (f(x)' * g(x) + f(x) * g(x)') / g(x)^2;");
			derivatives.AppendLine("(f(x) ^ g(x))' = f(x) ^ g(x) * (f(x)' * g(x) / f(x) + g(x)' * ln(f(x)));");

			derivatives.AppendLine("neg(f(x))' = neg(f(x)');");

			derivatives.AppendLine("sin(f(x))' = cos(f(x)) * f(x)';											   ");
			derivatives.AppendLine("cos(f(x))' = -sin(f(x)) * f(x)';											   ");
			derivatives.AppendLine("tan(f(x))' = f(x)' / cos(f(x)) ^ 2;									   ");
			derivatives.AppendLine("cot(f(x))' = -f(x)' / sin(f(x)) ^ 2;									   ");

			derivatives.AppendLine("arcsin(f(x))' = f(x)' / sqrt(1 - f(x) ^ 2);							   ");
			derivatives.AppendLine("arccos(f(x))' = -f(x)' / sqrt(1 - f(x) ^ 2);							   ");
			derivatives.AppendLine("arctan(f(x))' = f(x)' / (1 + f(x) ^ 2);								   ");
			derivatives.AppendLine("arccot(f(x))' = -f(x)' / (1 + f(x) ^ 2);								   ");

			derivatives.AppendLine("sinh(f(x))' = f(x)' * cosh(x);											   ");
			derivatives.AppendLine("cosh(f(x))' = f(x)' * sinh(x);											   ");

			derivatives.AppendLine("arcsinh(f(x))' = f(x)' / sqrt(f(x) ^ 2 + 1);							   ");
			derivatives.AppendLine("arcosh(f(x))' = f(x)' / sqrt(f(x) ^ 2 - 1);							   ");

			derivatives.AppendLine("ln(f(x))' = f(x)' / f(x);												   ");
			derivatives.AppendLine("log(f(x), g(x))' = (ln(f(x)) * g(x)' / g(x) - f(x)' * ln(g(x)) / f(x)) / ln(f(x)) ^ 2;");

			Helper.InitDerivatives(derivatives.ToString());
		}

		[Test]
		public void IsValueTest()
		{
			Assert.IsTrue(new MathFunc("100").IsValue);
		}

		[Test]
		public void IsNotValueTest()
		{
			Assert.IsFalse(new MathFunc("a").IsValue);
		}

		[Test]
		public void IsValueTest2()
		{
			Assert.IsTrue(new MathFunc("3 + f(5 + 7 ^ 0.342345 - sqrt(2)) * 3 * 1").IsValue);
		}

		[Test]
		public void IsNotValueTest2()
		{
			Assert.IsFalse(new MathFunc("3 + f(5 + 7 ^ 0.342345 - sqrt(2) + x) * 3 * 1").IsValue);
		}

		[Test]
		public void SortTest()
		{
			var f = new MathFunc("sqrt(2) + x^2 + 1");
			Assert.IsTrue(f.ToString() == "x^2 + sqrt(2) + 1");
		}

		[Test]
		public void ToStringTest()
		{
			var f = new MathFunc("sin(f(x) + g(x))");
			Assert.IsTrue(f.ToString() == "sin(f(x) + g(x))");
		}

		[Test]
		public void ToStringTest2()
		{
			var f = new MathFunc("x1 + x2 + x3 * x4 * (x5 + x6 + x7) * x8");
			Assert.IsTrue(f.ToString() == "(x5 + x6 + x7) * x3 * x4 * x8 + x1 + x2");
		}

		[Test]
		public void SimpleAdditionDerivativeTest()
		{
			var f = new MathFunc("x + 5 + 2*x");
			var derivative = f.GetDerivative();
			Assert.IsTrue(derivative == "3");
		}

		[Test]
		public void SimpleAdditionDerivativeTest2()
		{
			var f = new MathFunc("x + sin(x) + ln(x)");
			var derivative = f.GetDerivative();
			Assert.IsTrue(derivative == "x ^ -1 + cos(x) + 1");
		}

		[Test]
		public void OneDivOneDerivativeTest()
		{
			var f = new MathFunc("1 / x");
			var derivative = f.GetDerivative();
			Assert.IsTrue(derivative == "-x ^ -2");
		}

		[Test]
		public void DerivativeDerivativeTest()
		{
			var f = new MathFunc("diff(x ^ 3)");
			var derivative = f.GetDerivative();
			Assert.IsTrue(derivative == "x * 6");
		}

		[Test]
		public void XinPowerXDerivativeTest()
		{
			var f = new MathFunc("x ^ x");
			var derivative = f.GetDerivative();
			Assert.IsTrue(derivative == "x ^ x * (ln(x) + 1)");
		}

		[Test]
		public void UnknownFuncDerivativeTest()
		{
			var f = new MathFunc("f(x)");
			var derivative = f.GetDerivative();
			Assert.IsTrue(derivative == "diff(f(x))");
		}

		/*[Test]
		public void DerivativeTest()
		{
			var f = new MathFunction("(2 * x ^ 2 - 1) / (2 * x ^ 2 + 1)");
			var derivative = f.GetDerivative();
			Assert.IsTrue(derivative == "8 * x / (2 * x ^ 2 + 1) ^ 2");
		}*/
	}
}