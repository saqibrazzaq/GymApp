﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using api.Dtos.PlanCategory;
using api.Entities;

namespace api.Dtos.Plan
{
    public class PlanRes
    {
        public int PlanId { get; set; }
        public int? AccountId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PlanCategoryId { get; set; }
        public PlanCategoryRes? planCategory { get; set; }
        public string? PlanTypeId { get; set; }
        public PlanType? PlanType { get; set; }
        public int Duration { get; set; }
        public int? TimeUnitId { get; set; }
        public TimeUnit? TimeUnit { get; set; }
        public int SetupFee { get; set; }
        public int Price { get; set; }
    }
}
