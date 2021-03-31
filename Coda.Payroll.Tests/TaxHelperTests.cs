﻿// Copyright (c) Coda Technology Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coda.Payroll.Tests
{
    [TestClass]
    public class TaxHelperTests
    {
        [TestCategory("Helpers"), TestMethod]
        public void TaxDateDerivation()
        {
            // Tax Week 53 occured slightly differently in 2014
            Assert.AreEqual(52, new DateTime(2014, 04, 04).GetTaxPeriod().Week);
            Assert.AreEqual(53, new DateTime(2014, 04, 05).GetTaxPeriod().Week);
            Assert.AreEqual(12, new DateTime(2014, 04, 05).GetTaxPeriod().Month);

            Assert.AreEqual(52, new DateTime(2016, 04, 03).GetTaxPeriod().Week);
            Assert.AreEqual(53, new DateTime(2016, 04, 04).GetTaxPeriod().Week);
            Assert.AreEqual(53, new DateTime(2016, 04, 05).GetTaxPeriod().Week);
            Assert.AreEqual(1, new DateTime(2016, 04, 06).GetTaxPeriod().Week);
            Assert.AreEqual(1, new DateTime(2016, 04, 07).GetTaxPeriod().Week);
            
            Assert.AreEqual(12, new DateTime(2016, 04, 05).GetTaxPeriod().Month);
            Assert.AreEqual(1, new DateTime(2016, 04, 06).GetTaxPeriod().Month);

            Assert.AreEqual(2015, new DateTime(2016, 04, 05).GetTaxPeriod().Year);
            Assert.AreEqual(2016, new DateTime(2016, 04, 06).GetTaxPeriod().Year);
            Assert.AreEqual(2016, new DateTime(2017, 01, 01).GetTaxPeriod().Year);
            Assert.AreEqual(2016, new DateTime(2017, 04, 05).GetTaxPeriod().Year);
            Assert.AreEqual(2017, new DateTime(2017, 04, 06).GetTaxPeriod().Year);


            Assert.AreEqual(1, new DateTime(2017, 04, 06).GetTaxPeriod().Fortnight);
            Assert.AreEqual(1, new DateTime(2017, 04, 07).GetTaxPeriod().Fortnight);
            Assert.AreEqual(1, new DateTime(2017, 04, 13).GetTaxPeriod().Fortnight);
            Assert.AreEqual(1, new DateTime(2017, 04, 14).GetTaxPeriod().Fortnight);
            Assert.AreEqual(1, new DateTime(2017, 04, 19).GetTaxPeriod().Fortnight);
            Assert.AreEqual(2, new DateTime(2017, 04, 20).GetTaxPeriod().Fortnight);
            Assert.AreEqual(2, new DateTime(2017, 04, 24).GetTaxPeriod().Fortnight);
            Assert.AreEqual(27, new DateTime(2018, 04, 05).GetTaxPeriod().Fortnight);
            
            Assert.AreEqual(1, new DateTime(2017, 04, 06).GetTaxPeriod().FourWeek);
            Assert.AreEqual(1, new DateTime(2017, 04, 07).GetTaxPeriod().FourWeek);
            Assert.AreEqual(1, new DateTime(2017, 04, 20).GetTaxPeriod().FourWeek);
            Assert.AreEqual(1, new DateTime(2017, 04, 24).GetTaxPeriod().FourWeek);
            Assert.AreEqual(1, new DateTime(2017, 05, 02).GetTaxPeriod().FourWeek);
            Assert.AreEqual(1, new DateTime(2017, 05, 03).GetTaxPeriod().FourWeek);
            Assert.AreEqual(2, new DateTime(2017, 05, 04).GetTaxPeriod().FourWeek);
            Assert.AreEqual(2, new DateTime(2017, 05, 05).GetTaxPeriod().FourWeek);
            Assert.AreEqual(14, new DateTime(2018, 04, 05).GetTaxPeriod().FourWeek);
        }

        [TestCategory("Helpers"), TestMethod]
        public void TaxPeriodGeneration()
        {
            var taxDates = Models.TaxPeriod.GetPeriodsForTaxYear(2016);
            var firstDate = taxDates[0];
            var lastDate = taxDates[364];
            Assert.AreEqual(1, firstDate.Week);
            Assert.AreEqual(53, lastDate.Week);
            
            Assert.AreEqual(1, firstDate.Fortnight);
            Assert.AreEqual(27, lastDate.Fortnight);

            Assert.AreEqual(1, firstDate.FourWeek);
            Assert.AreEqual(14, lastDate.FourWeek);

            Assert.AreEqual(1, firstDate.Month);
            Assert.AreEqual(12, lastDate.Month);

            Assert.AreEqual(2016, firstDate.Year);
            Assert.AreEqual(2016, lastDate.Year);
        }

        [TestCategory("Helpers"), TestMethod]
        public void NumberTruncationTest()
        {
            Assert.AreEqual(9999.99999m, TaxMath.Truncate(9999.999999999m, 5));
            Assert.AreEqual(9999.9999m, TaxMath.Truncate(9999.999999999m, 4));
            Assert.AreEqual(9999.999m, TaxMath.Truncate(9999.999999999m, 3));
            Assert.AreEqual(9999.99m, TaxMath.Truncate(9999.999999999m, 2));
            Assert.AreEqual(9999.9m, TaxMath.Truncate(9999.999999999m, 1));
            Assert.AreEqual(9999m, TaxMath.Truncate(9999.999999999m, 0));
            Assert.AreEqual(9990m, TaxMath.Truncate(9999.999999999m, -1));

            Assert.AreEqual(-9999.99999m, TaxMath.Truncate(-9999.999999999m, 5));
            Assert.AreEqual(-9999.9999m, TaxMath.Truncate(-9999.999999999m, 4));
            Assert.AreEqual(-9999.999m, TaxMath.Truncate(-9999.999999999m, 3));
            Assert.AreEqual(-9999.99m, TaxMath.Truncate(-9999.999999999m, 2));
            Assert.AreEqual(-9999.9m, TaxMath.Truncate(-9999.999999999m, 1));
            Assert.AreEqual(-9999m, TaxMath.Truncate(-9999.999999999m, 0));
            Assert.AreEqual(-9990m, TaxMath.Truncate(-9999.999999999m, -1));
        }

        [TestCategory("Helpers"), TestMethod]
        public void BankersRoundingTest()
        {
            Assert.AreEqual(1m, TaxMath.BankersRound(0.99999m));
            Assert.AreEqual(1.96m, TaxMath.BankersRound(1.956m));
            Assert.AreEqual(2.96m, TaxMath.BankersRound(2.9555555m));
            Assert.AreEqual(2.47m, TaxMath.BankersRound(2.4719m));
            Assert.AreEqual(978.55m, TaxMath.BankersRound(978.54823m));
            Assert.AreEqual(8956.54m, TaxMath.BankersRound(8956.54168m));
            Assert.AreEqual(654.17m, TaxMath.BankersRound(654.168749m));
            Assert.AreEqual(236514.47m, TaxMath.BankersRound(236514.46984m));
            Assert.AreEqual(784.47m, TaxMath.BankersRound(784.4687m));
        }

        [TestCategory("Helpers"), TestMethod]
        public void HmrcRoundingTest()
        {
            Assert.AreEqual(1m, TaxMath.HmrcRound(0.99999m));
            Assert.AreEqual(1.96m, TaxMath.HmrcRound(1.956m));
            Assert.AreEqual(2.95m, TaxMath.HmrcRound(2.9555555m));
            Assert.AreEqual(2.47m, TaxMath.HmrcRound(2.4719m));
            Assert.AreEqual(978.55m, TaxMath.HmrcRound(978.54823m));
            Assert.AreEqual(8956.54m, TaxMath.HmrcRound(8956.54168m));
            Assert.AreEqual(654.17m, TaxMath.HmrcRound(654.168749m));
            Assert.AreEqual(236514.47m, TaxMath.HmrcRound(236514.46984m));
            Assert.AreEqual(784.47m, TaxMath.HmrcRound(784.4687m));
        }

        [TestCategory("Helpers"), TestMethod]
        public void UpRoundingTest()
        {
            Assert.AreEqual(1m, TaxMath.UpRound(0.99999m, 2));
            Assert.AreEqual(1.96m, TaxMath.UpRound(1.956m, 2));
            Assert.AreEqual(2.96m, TaxMath.UpRound(2.9555555m, 2));
            Assert.AreEqual(2.48m, TaxMath.UpRound(2.4719m, 2));
            Assert.AreEqual(978.55m, TaxMath.UpRound(978.54823m, 2));
            Assert.AreEqual(8956.55m, TaxMath.UpRound(8956.54168m, 2));
            Assert.AreEqual(654.17m, TaxMath.UpRound(654.168749m, 2));
            Assert.AreEqual(236514.47m, TaxMath.UpRound(236514.46984m, 2));
            Assert.AreEqual(784.47m, TaxMath.UpRound(784.4687m, 2));
        }
    }
}
