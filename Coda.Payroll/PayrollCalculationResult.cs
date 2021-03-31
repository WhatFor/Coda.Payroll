﻿// Copyright (c) Coda Technology Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using Coda.Payroll.Calculation.NationalInsurance;
using Coda.Payroll.Calculation.Paye;

namespace Coda.Payroll
{
    public class PayrollCalculationResult
    {
        public PayrollCalculationRequest Request { get; set; }

        /* Internal Calculation Results */
        public NationalInsuranceCalculation NationalInsurance { get; set; }
        public PayeCalculation Paye { get; set; }

        /* Helpers */
        public decimal PayeTax => Paye.ln;
        public decimal EmployeeNi => NationalInsurance.EmployeeNi;
        public decimal EmployerNi => NationalInsurance.EmployerNi;
    }
}