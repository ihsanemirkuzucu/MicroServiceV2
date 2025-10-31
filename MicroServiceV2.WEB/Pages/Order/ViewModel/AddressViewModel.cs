﻿#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace MicroServiceV2.WEB.Pages.Order.ViewModel;

public record AddressViewModel
{
    [Display(Name = "Address Line")] public string Street { get; set; } = null!;
    [Display(Name = "Address Line")] public string Line { get; set; } = null!;

    [Display(Name = "Province")] public string Province { get; set; } = null!;

    [Display(Name = "District")] public string District { get; set; } = null!;

    [Display(Name = "Zip Code")] public string ZipCode { get; set; } = null!;

    public static AddressViewModel Empty => new();
}