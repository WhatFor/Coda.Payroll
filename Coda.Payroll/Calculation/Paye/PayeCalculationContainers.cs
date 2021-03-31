﻿// Copyright (c) Coda Technology Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using Coda.Payroll.Models;

namespace Coda.Payroll.Calculation.Paye
{
    public class PayeCalculation
    {
        public TaxCode TaxCode { get; set; }
        public bool Week1 { get; set; }
        public PayPeriods Periods { get; set; }
        public decimal TaxToDate { get; set; }

        public int n;
        public decimal a1;
        public decimal na1;
        public decimal pn;
        public decimal Pn1;
        public decimal Pn;
        public decimal M;
        public decimal Ln;
        public decimal ln;
        public decimal Un;
        public decimal Tn;
    }

    public class PayeInternalBracket
    {
        public decimal R;
        public decimal B;
        public decimal C;
        public decimal c;
        public decimal V;
        public decimal v;
        public decimal K;
        public decimal k;
    }
}
