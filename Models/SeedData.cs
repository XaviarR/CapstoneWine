using Microsoft.EntityFrameworkCore;
using CapstoneWine.Data;

namespace CapstoneWine.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Wines.Any())
                {
                    return; // DB has been seeded
                }
                context.Wines.AddRange(
                    new WinesModel
                    {
                        WineName = "Chateau Margaux",
                        Image = "https://www.google.com/url?sa=i&url=https%3A%2F%2Funsplash.com%2Fs%2Fphotos%2Fwine-bottle&psig=AOvVaw088Z7qjImznJ4mOiY1vUrf&ust=1682981223851000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCJjA8OTX0v4CFQAAAAAdAAAAABAE",
                        Price = 50,
                        Blurb = "A classic Bordeaux blend of Cabernet Sauvignon, Merlot, Cabernet Franc, and Petit Verdot. With its velvety texture, complex aromas of blackcurrant, tobacco, and leather, and long, elegant finish, this wine is a true masterpiece.",
                        Quantity = 75,
                        Type = "Red wine",
                        Category = "Cabernet Sauvignon blend"
                    },
                    new WinesModel
                    {
                        WineName = "Opus One",
                        Image = "",
                        Price = 35,
                        Blurb = "A collaboration between Robert Mondavi and Baron Philippe de Rothschild, this Bordeaux-style blend is a true gem. It boasts a deep, rich color, aromas of blackberry, cassis, and chocolate, and a full-bodied, velvety texture that lingers on the palate.",
                        Quantity = 750,
                        Type = "Red wine",
                        Category = " Cabernet Sauvignon blend"
                    },
                    new WinesModel
                    {
                        WineName = "Cloudy Bay Sauvignon Blanc",
                        Image = "",
                        Price = 30,
                        Blurb = "With its vibrant aromas of grapefruit, lime, and passionfruit, this Sauvignon Blanc from New Zealand's Marlborough region is a refreshing and zesty wine that pairs perfectly with seafood or salads.",
                        Quantity = 750,
                        Type = "White wine",
                        Category = " Sauvignon Blanc"

                    },
                    new WinesModel
                    {
                        WineName = "Domaine de la Romanee-Conti Grand Cru",
                        Image = "",
                        Price = 50,
                        Blurb = "Considered by many to be the world's greatest wine, this Pinot Noir from Burgundy's Romanee-Conti vineyard is a rare and precious gem. With its exquisite aromas of cherry, raspberry, and forest floor, and its silky, nuanced texture, it is a wine that will be cherished for years to come",
                        Quantity = 12,
                        Type = "Red wine",
                        Category = "Pinot Noir"

                    },
                    new WinesModel
                    {
                        WineName = "Penfolds Grange",
                        Image = "",
                        Price = 85,
                        Blurb = "A flagship Shiraz blend from Australia's most iconic wine producer, this wine is rich, powerful, and complex, with aromas of blackberry, plum, and vanilla, and a long, silky finish. A true collector's item.",
                        Quantity = 43,
                        Type = "Red wine",
                        Category = "Shiraz blend"

                    },
                    new WinesModel
                    {
                        WineName = "Bollinger Special Cuvée Brut Champagne",
                        Image = "",
                        Price = 70,
                        Blurb = "One of the world's most famous Champagnes, this blend of Pinot Noir, Chardonnay, and Pinot Meunier is rich and complex, with notes of brioche, apple, and pear. It is perfect for celebrating special occasions or just enjoying with friends.",
                        Quantity = 75,
                        Type = "Sparkling wine",
                        Category = "Champagne"

                    },
                    new WinesModel
                    {
                        WineName = "Catena Zapata Nicolas Malbec",
                        Image = "",
                        Price = 120,
                        Blurb = "From one of Argentina's most prestigious wineries, this Malbec is a rich, full-bodied wine with aromas of blackberry, plum, and chocolate, and a long, elegant finish. It pairs perfectly with grilled meats or hearty stews.",
                        Quantity = 23,
                        Type = "Red wine",
                        Category = "Merlot"

                    },
                    new WinesModel
                    {
                        WineName = "Château d'Yquem",
                        Image = "",
                        Price = 120,
                        Blurb = "A renowned sweet wine from the Sauternes region of Bordeaux, France. With its honeyed flavors, notes of apricot and pineapple, and a luscious, silky texture, this wine is a perfect pairing for dessert or foie gras.",
                        Quantity = 90,
                        Type = "White wine",
                        Category = "Sweet wine"

                    },
                    new WinesModel
                    {
                        WineName = "Krug Grand Cuvée Brut Champagne",
                        Image = "",
                        Price = 250,
                        Blurb = "A luxurious Champagne from the prestigious Krug house, made from a blend of over 120 wines. With its complex flavors of apple, honey, and nuts, and a creamy, elegant texture, this Champagne is a true indulgence.",
                        Quantity = 24,
                        Type = "Sparkling wine",
                        Category = "Champagne"

                    },
                    new WinesModel
                    {
                        WineName = "Graham's 20-Year-Old Tawny Port",
                        Image = "",
                        Price = 60,
                        Blurb = "A beautifully aged Tawny Port from one of the most renowned Port houses in Portugal. With its rich, nutty flavors, notes of caramel and dried fruit, and a long, smooth finish, this wine is perfect for sipping after dinner or paired with cheese and nuts.",
                        Quantity = 33,
                        Type = "Fortified wine",
                        Category = "Port"

                    }


                    // new WinesModel
                    // {
                    //     WineName = "Château Margaux 2015",
                    //     Image = "https://www.db.wine/images/brands/Chateau-Margaux.png",
                    //     Price = 699.99M,
                    //     Blurb = "A stunning, captivating wine, the 2015 Margaux is magnificent. The flavors are deep and boldly sketched in this sumptuous, spectacularly ripe Margaux. Time in the glass brings out a compelling interplay of floral aromatics and succulent red-fleshed fruits. The 2015 is supremely finessed and nuanced, but it also has quite a bit of supporting structure. Most importantly, though, Margaux is flat-out gorgeous in 2015. This is a super-classic Margaux from the estate that appears to be firing on all cylinders. Don’t miss it! (Antonio Galloni)",
                    //     Quantity = 12,
                    //     Type = "Red",
                    //     Category = "Bordeaux Blend"

                    // },

                    // new WinesModel
                    // {
                    //     WineName = "Domaine de la Romanée-Conti La Tâche 2016",
                    //     Image = "https://www.db.wine/images/brands/DRC.png",
                    //     Price = 4999.99M,
                    //     Blurb = "The 2016 La Tâche Grand Cru was picked on September 24–25 at 31hL/ha (the highest of the five crus). It has an utterly sublime bouquet of blackberry, briar, crushed limestone, a dash of cracked black pepper and a little oyster shell. This is extremely complex and displays exquisite focus. The palate is medium-bodied with fine-grained tannin matched with a killer line of acidity that imparts so much freshness and tension. There is a sense of harmony and completeness here that I find beguiling, while the persistence is easily one of the longest that I have encountered in over 20 years visiting Burgundy. Stunning. (Neal Martin)",
                    //     Quantity = 6,
                    //     Type = "Red",
                    //     Category = "Pinot Noir"

                    // },

                    // new WinesModel
                    // {
                    //     WineName = "Krug Brut Rosé NV",
                    //     Image = "https://www.db.wine/images/brands/Krug.png",
                    //     Price = 299.99M,
                    //     Blurb = "A lovely rosé in an almost vinous style, with mouthwatering acidity and a fine, lacy mousse carrying appealing flavors of ripe raspberry, white cherry fruit, star anise, mandarin orange peel and honeysuckle. Lightly chalky on the lasting finish. Enjoy with food. Drink now through 2030. (Alison Napjus)",
                    //     Quantity = 24,
                    //     Type = "Sparkling",
                    //     Category = "Champagne Blend"

                    // }
                    );
                context.Subscriptions.AddRange(
                    new SubscriptionsModel
                    {
                        SubName = "Vin&Done",
                        Type = "Customized",
                        Frequency = 3,
                        NumOfBottles = 6,
                        RewardPoints = 60,
                    },
                    new SubscriptionsModel
                    {
                        SubName = "Everyday Delights",
                        Type = "Mixed",
                        Frequency = 12,
                        NumOfBottles = 12,
                        RewardPoints = 120,
                    },
                    new SubscriptionsModel
                    {
                        SubName = "Sauvignon Blanc Collection",
                        Type = "White",
                        Frequency = 6,
                        NumOfBottles = 6,
                        RewardPoints = 120,
                    },
                    new SubscriptionsModel
                    {
                        SubName = "Connoisseur Collection",
                        Type = "Red",
                        Frequency = 12,
                        NumOfBottles = 6,
                        RewardPoints = 180,
                    },
                    new SubscriptionsModel
                    {
                        SubName = "Star Wine Club",
                        Type = "Mixed",
                        Frequency = 3,
                        NumOfBottles = 12,
                        RewardPoints = 120,
                    },
                    new SubscriptionsModel
                    {
                        SubName = "Winc",
                        Type = "Customizable",
                        Frequency = 6,
                        NumOfBottles = 6,
                        RewardPoints = 30,
                    },
                    new SubscriptionsModel
                    {
                        SubName = "Wine Access",
                        Type = "Curated",
                        Frequency = 12,
                        NumOfBottles = 12,
                        RewardPoints = 60,
                    },
                    new SubscriptionsModel
                    {
                        SubName = "Star Wine Club",
                        Type = "Mixed",
                        Frequency = 3,
                        NumOfBottles = 12,
                        RewardPoints = 120,
                    },
                    new SubscriptionsModel
                    {
                        SubName = "Cheap and Cheerful",
                        Type = "Sparkling",
                        Frequency = 3,
                        NumOfBottles = 3,
                        RewardPoints = 30,
                    },
                    new SubscriptionsModel
                    {
                        SubName = "Cellar Masters Selection",
                        Type = "Customizable",
                        Frequency = 6,
                        NumOfBottles = 12,
                        RewardPoints = 120,
                    }
                    );
                context.Orders.AddRange(
                        new OrdersModel
                        {
                            OrderDate = DateTime.Now,
                            UserID = "0f1e1c11-6b01-48fc-b440-57a5b9e65fbb",
                            WineID = 1,
                            SubID = 1,
                            Quantity = 10,
                            DeliveryAdd = "01 bar rd",
                            DeliveryCharge = 9.99m,
                            TotalCost = 129.98m,
                            OrderStatus = "Pending",
                            RewardPoints = 12998
                        },
                        new OrdersModel
                        {
                            OrderDate = DateTime.Now,
                            UserID = "4f761d70-f797-4e90-95d7-cb0e6c39b4d8",
                            WineID = 2,
                            SubID = 3,
                            Quantity = 15,
                            DeliveryAdd = "04 bar rd",
                            DeliveryCharge = 15.99m,
                            TotalCost = 265.98m,
                            OrderStatus = "Delivering",
                            RewardPoints = 26598
                        },
                        new OrdersModel
                        {
                            OrderDate = DateTime.Now,
                            UserID = "b495ec40-e4c6-43da-a1de-00e22428ada0",
                            WineID = 4,
                            SubID = 3,
                            Quantity = 7,
                            DeliveryAdd = "07 bar rd",
                            DeliveryCharge = 29.99m,
                            TotalCost = 169.98m,
                            OrderStatus = "Pending",
                            RewardPoints = 16998
                        },
                        new OrdersModel
                        {
                            OrderDate = DateTime.Now,
                            UserID = "9d804031-7b76-47af-8266-4d4c28e614a6",
                            WineID = 1,
                            SubID = 2,
                            Quantity = 8,
                            DeliveryAdd = "20 bar rd",
                            DeliveryCharge = 50.99m,
                            TotalCost = 149.98m,
                            OrderStatus = "Pending",
                            RewardPoints = 14998
                        },
                        new OrdersModel
                        {
                            OrderDate = DateTime.Now,
                            UserID = "3b4d2e82-fdb8-4fc0-9a2b-98000c45b49c",
                            WineID = 4,
                            SubID = 1,
                            Quantity = 5,
                            DeliveryAdd = "06 bar rd",
                            DeliveryCharge = 25.49m,
                            TotalCost = 317.48m,
                            OrderStatus = "Delivering",
                            RewardPoints = 31748
                        },
                        new OrdersModel
                        {
                            OrderDate = DateTime.Now,
                            UserID = "b495ec40-e4c6-43da-a1de-00e22428ada0",
                            WineID = 2,
                            SubID = 1,
                            Quantity = 11,
                            DeliveryAdd = "08 bar rd",
                            DeliveryCharge = 10.49m,
                            TotalCost = 49.48m,
                            OrderStatus = "Delivering",
                            RewardPoints = 4948
                        },
                        new OrdersModel
                        {
                            OrderDate = DateTime.Now,
                            UserID = "2394f4f1-c073-49ab-881a-9ab1d19eb493",
                            WineID = 3,
                            SubID = 2,
                            Quantity = 6,
                            DeliveryAdd = "13 bar rd",
                            DeliveryCharge = 11.08m,
                            TotalCost = 99.30m,
                            OrderStatus = "Delivering",
                            RewardPoints = 9930
                        },
                        new OrdersModel
                        {
                            OrderDate = DateTime.Now,
                            UserID = "2394f4f1-c073-49ab-881a-9ab1d19eb493",
                            WineID = 1,
                            SubID = 3,
                            Quantity = 14,
                            DeliveryAdd = "09 bar rd",
                            DeliveryCharge = 17.69m,
                            TotalCost = 120.50m,
                            OrderStatus = "Delivering",
                            RewardPoints = 12050
                        },
                        new OrdersModel
                        {
                            OrderDate = DateTime.Now,
                            UserID = "5767c35c-a81d-4398-a014-16db4a6eecbe",
                            WineID = 4,
                            SubID = 3,
                            Quantity = 14,
                            DeliveryAdd = "15 bar rd",
                            DeliveryCharge = 17.69m,
                            TotalCost = 120.50m,
                            OrderStatus = "Delivering",
                            RewardPoints = 12050
                        },
                        new OrdersModel
                        {
                            OrderDate = DateTime.Now,
                            UserID = "2394f4f1-c073-49ab-881a-9ab1d19eb493",
                            WineID = 4,
                            SubID = 2,
                            Quantity = 14,
                            DeliveryAdd = "04 bar rd",
                            DeliveryCharge = 17.69m,
                            TotalCost = 120.50m,
                            OrderStatus = "Delivering",
                            RewardPoints = 12050
                        },
                        new OrdersModel
                        {
                            OrderDate = DateTime.Now,
                            UserID = "9d804031-7b76-47af-8266-4d4c28e614a6",
                            WineID = 2,
                            SubID = 2,
                            Quantity = 3,
                            DeliveryAdd = "20 bar rd",
                            DeliveryCharge = 21.38m,
                            TotalCost = 133.76m,
                            OrderStatus = "Pending",
                            RewardPoints = 13376
                        }
                        ); ;
                context.SaveChanges();
            }
        }
    }
}
