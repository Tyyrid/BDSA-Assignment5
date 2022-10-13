namespace GildedRose.Tests;

public class ProgramTests
{
    [Fact]
    public void TestTheTruth()
    {
        true.Should().BeTrue();
    }

    /*[Fact]
    public void MainDoesSomethingAtAll(){
        var writer = new StringWriter();
        Console.SetOut(writer);

        Program.Main(Array.Empty<String>());

        var output = writer.ToString();
        writer.Close();

        output.Should().Contain("OMGHAI!");
        output.Should().Contain("day 30");
        output.Should().Contain("day 0");
    }*/

    [Fact]
    public void Item_check_values() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "Aged Brie", SellIn = 5, Quality = 24 }                        
                                          }
                                };
        
        Item item = new Item {
                Name = "Aged Brie",
                SellIn = 5,
                Quality = 24
                };

        app.Items[0].Name.Should().Be(item.Name);
        app.Items[0].SellIn.Should().Be(item.SellIn);
        app.Items[0].Quality.Should().Be(item.Quality);
    }

    [Fact]
    public void UpdateQuality_Does_SellIn_decrease_StdItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                        new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                        new Item { Name = "Aged Brie", SellIn = 8, Quality = 22 },
                        new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 25 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].SellIn.Should().Be(9);
        app.Items[1].SellIn.Should().Be(4);
        app.Items[2].SellIn.Should().Be(7);
        app.Items[3].SellIn.Should().Be(14);
    }

    [Fact]
    public void UpdateQuality_Does_Quality_degrade_StdItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                        new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].Quality.Should().Be(19);
        app.Items[1].Quality.Should().Be(6);
    }

    [Fact]
    public void UpdateQuality_After_SellDate_Quality_degrades_twice_as_fast_StdItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "+5 Dexterity Vest", SellIn = -2, Quality = 20 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].Quality.Should().Be(18);
    }

    [Fact]
    public void UpdateQuality_Quality_is_never_above_50_ALLItem_except_SulfurasItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 60 },
                        new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 60 },
                        new Item { Name = "Aged Brie", SellIn = 8, Quality = 60 },
                        new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 60 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].Quality.Should().BeLessThanOrEqualTo(50);
        app.Items[1].Quality.Should().BeLessThanOrEqualTo(50);
        app.Items[2].Quality.Should().BeLessThanOrEqualTo(50);
        app.Items[3].Quality.Should().BeLessThanOrEqualTo(50);
    }

    [Fact]
    public void UpdateQuality_Quality_is_never_negative_StdItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = -5 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].Quality.Should().Be(0);
    }



    /////  Aged Brie  /////
    [Fact]
    public void UpdateQuality_Quality_increses_per_day_BrieItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "Aged Brie", SellIn = 10, Quality = 22 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].Quality.Should().Be(23);
    }

    [Fact]
    public void UpdateQuality_Quality_increses_2_per_day_when_SellIn_has_passed_BrieItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "Aged Brie", SellIn = -2, Quality = 22 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].Quality.Should().Be(24);
    }

    [Fact]
    public void UpdateQuality_Quality_is_never_above_50_BrieItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "Aged Brie", SellIn = 10, Quality = 55 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].Quality.Should().Be(50);
    }


    /////  Sulfuras  /////
    [Fact]
    public void UpdateQuality_Quality_is_always_80_SulfurasItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 85 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].Quality.Should().Be(80);
    }

    [Fact]
    public void UpdateQuality_SellIn_is_always_as_assigned_SulfurasItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].SellIn.Should().Be(10);
    }

    /////  Backstage passes  /////
    [Fact]
    public void UpdateQuality_Quality_increases_by_1_where_SellIn_is_above_10_BackstagePassesItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 25 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].Quality.Should().Be(26);
    }

    [Fact]
    public void UpdateQuality_Quality_increases_by_2_where_SellIn_is_10_or_below_BackstagePassesItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 25 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].Quality.Should().Be(27);
    }

    [Fact]
    public void UpdateQuality_Quality_increases_by_3_where_SellIn_is_5_or_below_BackstagePassesItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 25 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].Quality.Should().Be(28);
    }

    [Fact]
    public void UpdateQuality_Quality_drops_to_0_when_SellIn_date_has_passed_BackstagePassesItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 25 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].Quality.Should().Be(0);
    }


    /////  Conjured  /////
    [Fact]
    public void UpdateQuality_Quality_degrades_twice_as_fast_as_StdItem_ConjuredItem() {
        var app = new Program() {
                    Items = new List<Item> {
                        new Item { Name = "Conjured Mana Cake", SellIn = 5, Quality = 25 }
                                          }
                                };

        app.UpdateQuality();

        app.Items[0].Quality.Should().Be(23);
    }

}