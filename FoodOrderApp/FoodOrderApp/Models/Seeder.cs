﻿using FoodOrderApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Models
{
    public class Seeder
    {
        private sqlContext db;

        public Seeder(sqlContext db)
        {
            this.db = db;
            seedMenuData();
        }

        public void seedMenuData()
        {
            if (db.MenuItems.Count() != 0)
            {
                return;
            }

            MenuItems[] seedMenu = new MenuItems[]
            {
                new MenuItems {DishName = "Pancakes",
                    ImageUrl="https://cdn.pixabay.com/photo/2017/05/07/08/56/pancakes-2291908_1280.jpg",
                    Price=12,
                    RestaurantId = 2,
                    MenuSection = "Breakfast"
                },
                new MenuItems {DishName = "Spagetti",
                    ImageUrl="https://cdn.pixabay.com/photo/2017/11/08/22/18/spaghetti-2931846_1280.jpg",
                    Price=12,
                    RestaurantId=3,
                    MenuSection = "Lunch / Dinner"
                },

                new MenuItems {DishName = "Salmon Teriyaki",
                    ImageUrl="https://cdn.pixabay.com/photo/2015/04/08/13/13/food-712665_1280.jpg",
                    Price=12,
                    RestaurantId=1,
                    MenuSection= "Lunch / Dinner"
                },

                new MenuItems {DishName = "Hamburger",
                    ImageUrl="https://cdn.pixabay.com/photo/2014/10/19/20/59/hamburger-494706_1280.jpg",
                    Price=12,
                    RestaurantId=3,
                    MenuSection="Lunch / Dinner"
                },

                new MenuItems {DishName = "Cappuccino",
                    ImageUrl="https://cdn.pixabay.com/photo/2018/01/31/09/57/coffee-3120750_1280.jpg",
                    Price=12,
                    RestaurantId=1,
                    MenuSection= "Drinks"
                },

                new MenuItems {DishName = "Crepes",
                    ImageUrl="https://pixabay.com/photos/pancakes-pancake-crepe-s%C3%BCsspeise-2020863/",
                    Price=12,
                    RestaurantId=2,
                    MenuSection = "Breakfast"
                },
                new MenuItems {DishName = "Bread",
                    ImageUrl="https://cdn.pixabay.com/photo/2014/07/22/09/59/bread-399286_1280.jpg",
                    Price=12,
                    RestaurantId=2,
                    MenuSection= "Appetizers"
                },

                new MenuItems {DishName = "churros",
                    ImageUrl="https://cdn.pixabay.com/photo/2017/03/30/15/47/churros-2188871_1280.jpg",
                    Price=15,
                    RestaurantId=2,
                    MenuSection = "Appetizers"
                },

                new MenuItems {DishName = "Coffee",
                    ImageUrl="https://cdn.pixabay.com/photo/2015/06/24/01/15/morning-819362_1280.jpg",
                    Price=2,
                    RestaurantId=2,
                    MenuSection= "Drinks"
                },

            };

            db.MenuItems.AddRange(seedMenu);
            db.SaveChanges();

            //if(db.RestaurantInfo.Count() != 0)
            //{
            //    return;
            //}

            //RestaurantInfo AuntBettys = new RestaurantInfo
            //{
            //    Address = "1234 Main st. Snoqualmie, WA",
            //    Cuisine= "Mixed",
            //    PhoneNumber="4254284444",
            //    RestaurantName="Aunt Betty's Bed and Breakfast"
            //};

            //db.RestaurantInfo.Add(AuntBettys);
            //db.SaveChanges();

        }
    }
}
