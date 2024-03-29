﻿using ExordiumGames.MVC.Data.DbModels;

namespace ExordiumGames.MVC.Dto
{
    public class CategoryResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Priority { get; set; }
        public List<CreateItemResponseModel> Items { get; set; } = new List<CreateItemResponseModel>();
    }
}
