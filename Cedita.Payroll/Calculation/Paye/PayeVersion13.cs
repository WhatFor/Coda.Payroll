﻿// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using System;
using Cedita.Payroll.Configuration;

namespace Cedita.Payroll.Calculation.Paye
{
    [CalculationEngineTaxYear(TaxYear = 2015)]
    [CalculationEngineTaxYear(TaxYear = 2016)]
    [CalculationEngineTaxYear(TaxYear = 2017)]
    [CalculationEngineTaxYear(TaxYear = 2018)]
    public class PayeVersion13 : PayeVersion12
    {
        public PayeVersion13(TaxYearConfigurationData taxYearConfigurationData) : base(taxYearConfigurationData)
        {
        }

        protected override void Calculateln()
        {
            CalculationContainer.ln = TaxMath.Truncate(CalculationContainer.Ln, 2);

            if (CalculationContainer.n > 1)
                CalculationContainer.ln -= CalculationContainer.TaxToDate;

            // In V13+ we always apply the regulatory limit
            CalculationContainer.ln = Math.Min(CalculationContainer.ln, TaxMath.Truncate(CalculationContainer.pn * (CalculationContainer.M / 100), 2));
        }
    }
}