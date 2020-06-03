namespace newdip.Migrations
{
    using newdip.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<newdip.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        ApplicationDbContext db = new ApplicationDbContext();
        public void LoadSampleBuildings()
        {
            db.Buildings.Add(new Building("KSU", buildingid: 1,
                                          site: "https://kursksu.ru/", timetable: "пн-сб 8:00–21:30",
                                          addrees: "ул. Радищева, 33, Курск",
                                          description: "Курский государственный университет"));
            db.Buildings.Add(new Building("MGU", buildingid: 2, description: "asdasd"));
            db.Buildings.Add(new Building("GPU", buildingid: 3, description: "s21adsadasdsadasdsadasdsadasdsadasdsadasdsadasdasd"));
            db.SaveChanges();
        }

        public void LoadSampleFloors()
        {
            db.Floors.Add(new Floor(1, floorid: 1, buildingid: 1));
            db.Floors.Add(new Floor(2, floorid: 2, buildingid: 1));
            db.Floors.Add(new Floor(3, floorid: 3, buildingid: 1));
            db.Floors.Add(new Floor(4, floorid: 4, buildingid: 1));
            db.SaveChanges();
            // db.Floors.Add(new Floor(-1, floorid: 5, buildingid: 1));
        }

        public void LoadSampleRooms()
        {
            db.Rooms.Add(new Room("213",
                             "The American black bear is a medium-sized bear native to North America. It is the continent's smallest and most widely distributed bear species. American black bears are omnivores, with their diets varying greatly depending on season and location. They typically live in largely forested areas, but do leave forests in search of food. Sometimes they become attracted to human communities because of the immediate availability of food. The American black bear is the world's most common bear species.",
                             "Время работы:вторник	  10:00–22:00" +
                                            "             среда       10:00–22:00" +
                                            "             четверг     10:00–22:00" +
                                            "             пятница     10:00–22:00" +
                                            "             суббота     10:00–22:00" +
                                            "             воскресенье 10:00–22:00" +
                                            "             понедельник 10:00–22:00",
                             "8(906)6944309",
                             "xn--80afqpaigicolm.xn--p1ai/csharp/csharp-otkryt-url-v-browser/",
                             floorid: 4));

            db.Rooms.Add(new Room("200",
                              "The Asian black bear, also known as the moon bear and the white-chested bear, is a medium-sized bear species native to Asia and largely adapted to arboreal life. It lives in the Himalayas, in the northern parts of the Indian subcontinent, Korea, northeastern China, the Russian Far East, the Honshū and Shikoku islands of Japan, and Taiwan. It is classified as vulnerable by the International Union for Conservation of Nature (IUCN), mostly because of deforestation and hunting for its body parts.",
                              "Время работы:вторник	  10:00–22:00" +
                                            "             среда       10:00–22:00" +
                                            "             четверг     10:00–22:00" +
                                            "             пятница     10:00–22:00" +
                                            "             суббота     10:00–22:00" +
                                            "             воскресенье 10:00–22:00" +
                                            "             понедельник 10:00–22:00",
                             "8(906)6944309",
                             "http://xn--80afqpaigicolm.xn--p1ai/csharp/csharp-otkryt-url-v-browser/",
                              floorid: 4));

            db.Rooms.Add(new Room("202",
                             "The brown bear is a bear that is found across much of northern Eurasia and North America. In North America the population of brown bears are often called grizzly bears. It is one of the largest living terrestrial members of the order Carnivora, rivaled in size only by its closest relative, the polar bear, which is much less variable in size and slightly larger on average. The brown bear's principal range includes parts of Russia, Central Asia, China, Canada, the United States, Scandinavia and the Carpathian region, especially Romania, Anatolia and the Caucasus. The brown bear is recognized as a national and state animal in several European countries.",
                              floorid: 4));


            db.Rooms.Add(new Room("201",
                             "The brown bear is a bear that is found across much of northern Eurasia and North America. In North America the population of brown bears are often called grizzly bears. It is one of the largest living terrestrial members of the order Carnivora, rivaled in size only by its closest relative, the polar bear, which is much less variable in size and slightly larger on average. The brown bear's principal range includes parts of Russia, Central Asia, China, Canada, the United States, Scandinavia and the Carpathian region, especially Romania, Anatolia and the Caucasus. The brown bear is recognized as a national and state animal in several European countries.",
                              floorid: 4));


            db.Rooms.Add(new Room("199",
                             "The giant panda, also known as panda bear or simply panda, is a bear native to south central China. It is easily recognized by the large, distinctive black patches around its eyes, over the ears, and across its round body. The name giant panda is sometimes used to distinguish it from the unrelated red panda. Though it belongs to the order Carnivora, the giant panda's diet is over 99% bamboo. Giant pandas in the wild will occasionally eat other grasses, wild tubers, or even meat in the form of birds, rodents, or carrion. In captivity, they may receive honey, eggs, fish, yams, shrub leaves, oranges, or bananas along with specially prepared food.",
                              floorid: 4));

            db.Rooms.Add(new Room("203",
                             "A grizzly–polar bear hybrid is a rare ursid hybrid that has occurred both in captivity and in the wild. In 2006, the occurrence of this hybrid in nature was confirmed by testing the DNA of a unique-looking bear that had been shot near Sachs Harbour, Northwest Territories on Banks Island in the Canadian Arctic. The number of confirmed hybrids has since risen to eight, all of them descending from the same female polar bear.",
                             phone: "8(906)6944309",
                             floorid: 4));

            db.Rooms.Add(new Room("204",
                             "The sloth bear is an insectivorous bear species native to the Indian subcontinent. It is listed as Vulnerable on the IUCN Red List, mainly because of habitat loss and degradation. It has also been called labiated bear because of its long lower lip and palate used for sucking insects. Compared to brown and black bears, the sloth bear is lankier, has a long, shaggy fur and a mane around the face, and long, sickle-shaped claws. It evolved from the ancestral brown bear during the Pleistocene and through convergent evolution shares features found in insect-eating mammals.",
                             floorid: 4));


            db.Rooms.Add(new Room("205",
                             "The sun bear is a bear species occurring in tropical forest habitats of Southeast Asia. It is listed as Vulnerable on the IUCN Red List. The global population is thought to have declined by more than 30% over the past three bear generations. Suitable habitat has been dramatically reduced due to the large-scale deforestation that has occurred throughout Southeast Asia over the past three decades. The sun bear is also known as the honey bear, which refers to its voracious appetite for honeycombs and honey.",
                             phone: "8(906)6944309",
                             floorid: 4));

            db.Rooms.Add(new Room("206",
                             "The polar bear is a hypercarnivorous bear whose native range lies largely within the Arctic Circle, encompassing the Arctic Ocean, its surrounding seas and surrounding land masses. It is a large bear, approximately the same size as the omnivorous Kodiak bear. A boar (adult male) weighs around 350–700 kg (772–1,543 lb), while a sow (adult female) is about half that size. Although it is the sister species of the brown bear, it has evolved to occupy a narrower ecological niche, with many body characteristics adapted for cold temperatures, for moving across snow, ice and open water, and for hunting seals, which make up most of its diet. Although most polar bears are born on land, they spend most of their time on the sea ice. Their scientific name means maritime bear and derives from this fact. Polar bears hunt their preferred food of seals from the edge of sea ice, often living off fat reserves when no sea ice is present. Because of their dependence on the sea ice, polar bears are classified as marine mammals.",
                             site: "http://xn--80afqpaigicolm.xn--p1ai/csharp/csharp-otkryt-url-v-browser/",
                             floorid: 4));

            db.Rooms.Add(new Room("207",
                             "The spectacled bear, also known as the Andean bear or Andean short-faced bear and locally as jukumari (Aymara), ukumari (Quechua) or ukuku, is the last remaining short-faced bear. Its closest relatives are the extinct Florida spectacled bear, and the giant short-faced bears of the Middle to Late Pleistocene age. Spectacled bears are the only surviving species of bear native to South America, and the only surviving member of the subfamily Tremarctinae. The species is classified as Vulnerable by the IUCN because of habitat loss.",
                             phone: "8(906)6944309",
                             floorid: 4));

            db.Rooms.Add(new Room("208",
                             "The cave bear was a species of bear that lived in Europe and Asia during the Pleistocene and became extinct about 24,000 years ago during the Last Glacial Maximum. Both the word cave and the scientific name spelaeus are used because fossils of this species were mostly found in caves. This reflects the views of experts that cave bears may have spent more time in caves than the brown bear, which uses caves only for hibernation.",
                             floorid: 4));

            db.Rooms.Add(new Room("209",
                             "The short-faced bears is an extinct bear genus that inhabited North America during the Pleistocene epoch from about 1.8 Mya until 11,000 years ago. It was the most common early North American bear and was most abundant in California. There are two recognized species: Arctodus pristinus and Arctodus simus, with the latter considered to be one of the largest known terrestrial mammalian carnivores that has ever existed. It has been hypothesized that their extinction coincides with the Younger Dryas period of global cooling commencing around 10,900 BC.",
                             phone: "8(906)6944309",
                             floorid: 3));

            db.Rooms.Add(new Room("409",
                             description: "The California grizzly bear is an extinct subspecies of the grizzly bear, the very large North American brown bear. Grizzly could have meant grizzled (that is, with golden and grey tips of the hair) or fear-inspiring. Nonetheless, after careful study, naturalist George Ord formally classified it in 1815 – not for its hair, but for its character – as Ursus horribilis (terrifying bear). Genetically, North American grizzlies are closely related; in size and coloring, the California grizzly bear was much like the grizzly bear of the southern coast of Alaska. In California, it was particularly admired for its beauty, size and strength. The grizzly became a symbol of the Bear Flag Republic, a moniker that was attached to the short-lived attempt by a group of American settlers to break away from Mexico in 1846. Later, this rebel flag became the basis for the state flag of California, and then California was known as the Bear State.",
                             phone: "8(906)6944309",
                             floorid: 3));

            db.Rooms.Add(new Room("309",
                             description: "The California grizzly bear is an extinct subspecies of the grizzly bear, the very large North American brown bear. Grizzly could have meant grizzled (that is, with golden and grey tips of the hair) or fear-inspiring. Nonetheless, after careful study, naturalist George Ord formally classified it in 1815 – not for its hair, but for its character – as Ursus horribilis (terrifying bear). Genetically, North American grizzlies are closely related; in size and coloring, the California grizzly bear was much like the grizzly bear of the southern coast of Alaska. In California, it was particularly admired for its beauty, size and strength. The grizzly became a symbol of the Bear Flag Republic, a moniker that was attached to the short-lived attempt by a group of American settlers to break away from Mexico in 1846. Later, this rebel flag became the basis for the state flag of California, and then California was known as the Bear State.",
                             phone: "21232938923",
                             site: "http://xn--80afqpaigicolm.xn--p1ai/csharp/csharp-otkryt-url-v-browser/",
                             timetable: "Время работы: \n" +
                                        "вторник	 10:00–22:00 \n" +
                                        "среда       10:00–22:00 \n" +
                                        "четверг     10:00–22:00 \n" +
                                        "пятница     10:00–22:00 \n" +
                                        "суббота     10:00–22:00 \n" +
                                        "воскресенье 10:00–22:00 \n" +
                                        "понедельник 10:00–22:00 \n",
                             floorid: 3));

            db.Rooms.Add(new Room("219",
                             description: "The California grizzly bear is an extinct subspecies of the grizzly bear, the very large North American brown bear. Grizzly could have meant grizzled (that is, with golden and grey tips of the hair) or fear-inspiring. Nonetheless, after careful study, naturalist George Ord formally classified it in 1815 – not for its hair, but for its character – as Ursus horribilis (terrifying bear). Genetically, North American grizzlies are closely related; in size and coloring, the California grizzly bear was much like the grizzly bear of the southern coast of Alaska. In California, it was particularly admired for its beauty, size and strength. The grizzly became a symbol of the Bear Flag Republic, a moniker that was attached to the short-lived attempt by a group of American settlers to break away from Mexico in 1846. Later, this rebel flag became the basis for the state flag of California, and then California was known as the Bear State.",
                             site: "okasdjasdk",
                             floorid: 3));
            db.SaveChanges();
        }

        public void LoadSampleWorkers()
        {
            db.Workers.Add(new Worker
            {
                FirstName = "Celivans",
                SecondName = "irina",
                LastName = "vasileva",

                Details = "The American black bear is a medium-sized bear native to North America. It is the continent's smallest and most widely distributed bear species. American black bears are omnivores, with their diets varying greatly depending on season and location. They typically live in largely forested areas, but do leave forests in search of food. Sometimes they become attracted to human communities because of the immediate availability of food. The American black bear is the world's most common bear species.",
                Status = "Teacher",

                Site = "http://xn--80afqpaigicolm.xn--p1ai/csharp/csharp-otkryt-url-v-browser/",
                Phone = "8(906)6944309",
                Email = "seliv@mail.ru",
                RoomId = 3,
            });

            db.Workers.Add(new Worker
            {
                FirstName = "Uraeva",
                SecondName = "Elena",

                Details = "The American black bear is a medium-sized bear native to North America. It is the continent's smallest and most widely distributed bear species. American black bears are omnivores, with their diets varying greatly depending on season and location. They typically live in largely forested areas, but do leave forests in search of food. Sometimes they become attracted to human communities because of the immediate availability of food. The American black bear is the world's most common bear species.",
                Status = "Teacher",

                Site = "http://xn--80afqpaigicolm.xn--p1ai/csharp/csharp-otkryt-url-v-browser/",
                Phone = "8(906)6944309",
                Email = "seliv@mail.ru",
                RoomId = 1,
            });

            db.Workers.Add(new Worker
            {
                FirstName = "Makarov",
                SecondName = "Kirya",

                Details = "The American black bear is a medium-sized bear native to North America. It is the continent's smallest and most widely distributed bear species. American black bears are omnivores, with their diets varying greatly depending on season and location. They typically live in largely forested areas, but do leave forests in search of food. Sometimes they become attracted to human communities because of the immediate availability of food. The American black bear is the world's most common bear species.",
                Status = "Teacher",

                Site = "http://xn--80afqpaigicolm.xn--p1ai/csharp/csharp-otkryt-url-v-browser/",
                Phone = "8(906)6944309",
                Email = "seliv@mail.ru",
                RoomId = 2,
            });
            db.SaveChanges();
        }

        public void LoadSamplePoints()
        {
            #region 4thFloor
            db.Points.Add(new PointM(0, 0, floorId: 2, pointId: 1));
            db.Points.Add(new PointM(0, 500, floorId: 2, pointId: 2));
            db.Points.Add(new PointM(250, 0, floorId: 2, pointId: 3));
            db.Points.Add(new PointM(250, 210, floorId: 2, pointId: 4));
            db.Points.Add(new PointM(250, 290, floorId: 2, pointId: 5));
            db.Points.Add(new PointM(250, 500, floorId: 2, pointId: 6));
            db.Points.Add(new PointM(400, 0, floorId: 2, pointId: 7));
            db.Points.Add(new PointM(400, 210, floorId: 2, pointId: 8));
            db.Points.Add(new PointM(400, 290, floorId: 2, pointId: 9));
            db.Points.Add(new PointM(400, 500, floorId: 2, pointId: 10));
            db.Points.Add(new PointM(820, 0, floorId: 2, pointId: 11));
            db.Points.Add(new PointM(820, 210, floorId: 2, pointId: 12));
            db.Points.Add(new PointM(820, 290, floorId: 2, pointId: 13));
            db.Points.Add(new PointM(820, 500, floorId: 2, pointId: 14));
            db.Points.Add(new PointM(1270, 0, floorId: 2, pointId: 15));
            db.Points.Add(new PointM(1270, 210, floorId: 2, pointId: 16));
            db.Points.Add(new PointM(1270, 290, floorId: 2, pointId: 17));
            db.Points.Add(new PointM(1270, 500, floorId: 2, pointId: 18));
            db.Points.Add(new PointM(1270, -730, floorId: 2, pointId: 19));
            db.Points.Add(new PointM(1270, -900, floorId: 2, pointId: 20));
            db.Points.Add(new PointM(1350, 0, floorId: 2, pointId: 21));
            db.Points.Add(new PointM(1350, 210, floorId: 2, pointId: 22));
            db.Points.Add(new PointM(1520, 290, floorId: 2, pointId: 23));
            db.Points.Add(new PointM(1520, 500, floorId: 2, pointId: 24));
            db.Points.Add(new PointM(1350, -200, floorId: 2, pointId: 25));
            db.Points.Add(new PointM(1350, -400, floorId: 2, pointId: 26));
            db.Points.Add(new PointM(1350, -730, floorId: 2, pointId: 27));
            db.Points.Add(new PointM(1700, 0, floorId: 2, pointId: 28));
            db.Points.Add(new PointM(1700, 210, floorId: 2, pointId: 29));
            db.Points.Add(new PointM(1700, 290, floorId: 2, pointId: 30));
            db.Points.Add(new PointM(1700, 500, floorId: 2, pointId: 31));
            db.Points.Add(new PointM(1700, -200, floorId: 2, pointId: 32));
            db.Points.Add(new PointM(1700, -400, floorId: 2, pointId: 33));
            db.Points.Add(new PointM(1700, -730, floorId: 2, pointId: 34));
            db.Points.Add(new PointM(1700, -900, floorId: 2, pointId: 35));
            db.Points.Add(new PointM(1900, 0, floorId: 2, pointId: 36));
            db.Points.Add(new PointM(1900, 500, floorId: 2, pointId: 37));

            #endregion
            //db.SaveChanges();

            #region 3thFloor
            db.Points.Add(new PointM(1, 1, floorId: 1, pointId: 38));
            db.Points.Add(new PointM(0, 500, floorId: 1, pointId: 39));
            db.Points.Add(new PointM(250, 0, floorId: 1, pointId: 40));
            db.Points.Add(new PointM(250, 210, floorId: 1, pointId: 41));
            db.Points.Add(new PointM(250, 290, floorId: 1, pointId: 42));
            db.Points.Add(new PointM(250, 500, floorId: 1, pointId: 43));
            db.Points.Add(new PointM(400, 0, floorId: 1, pointId: 44));
            db.Points.Add(new PointM(400, 210, floorId: 1, pointId: 45));
            db.Points.Add(new PointM(400, 290, floorId: 1, pointId: 46));
            db.Points.Add(new PointM(400, 500, floorId: 1, pointId: 47));
            db.Points.Add(new PointM(820, 0, floorId: 1, pointId: 48));
            db.Points.Add(new PointM(820, 210, floorId: 1, pointId: 49));
            db.Points.Add(new PointM(820, 290, floorId: 1, pointId: 50));
            db.Points.Add(new PointM(820, 500, floorId: 1, pointId: 51));
            db.Points.Add(new PointM(1270, 0, floorId: 1, pointId: 52));
            db.Points.Add(new PointM(1270, 210, floorId: 1, pointId: 53));
            db.Points.Add(new PointM(1270, 290, floorId: 1, pointId: 54));
            db.Points.Add(new PointM(1270, 500, floorId: 1, pointId: 55));
            db.Points.Add(new PointM(1350, 0, floorId: 1, pointId: 56));
            db.Points.Add(new PointM(1350, 210, floorId: 1, pointId: 57));
            db.Points.Add(new PointM(1520, 290, floorId: 1, pointId: 58));
            db.Points.Add(new PointM(1520, 500, floorId: 1, pointId: 59));
            db.Points.Add(new PointM(1700, 0, floorId: 1, pointId: 60));
            db.Points.Add(new PointM(1700, 210, floorId: 1, pointId: 61));
            db.Points.Add(new PointM(1700, 290, floorId: 1, pointId: 62));
            db.Points.Add(new PointM(1700, 500, floorId: 1, pointId: 63));
            db.Points.Add(new PointM(1900, 0, floorId: 1, pointId: 64));
            db.Points.Add(new PointM(1900, 500, floorId: 1, pointId: 65));
            #endregion
            db.SaveChanges();
        }

        public void LoadSampleEdges()
        {
            #region 4thFloor
            db.Edges.Add(new EdgeM(0, pointFirId: 1, pointSecId: 3, edgeId: 1));
            db.Edges.Add(new EdgeM(0, pointFirId: 1, pointSecId: 2, edgeId: 2));
            db.Edges.Add(new EdgeM(0, pointFirId: 2, pointSecId: 6, edgeId: 3));
            db.Edges.Add(new EdgeM(0, pointFirId: 3, pointSecId: 4, edgeId: 4));
            db.Edges.Add(new EdgeM(0, pointFirId: 4, pointSecId: 5, edgeId: 5));
            db.Edges.Add(new EdgeM(0, pointFirId: 5, pointSecId: 6, edgeId: 6));
            db.Edges.Add(new EdgeM(0, pointFirId: 6, pointSecId: 10, edgeId: 7));
            db.Edges.Add(new EdgeM(0, pointFirId: 10, pointSecId: 9, edgeId: 8));
            db.Edges.Add(new EdgeM(0, pointFirId: 9, pointSecId: 5, edgeId: 9));
            db.Edges.Add(new EdgeM(0, pointFirId: 4, pointSecId: 8, edgeId: 10));
            db.Edges.Add(new EdgeM(0, pointFirId: 8, pointSecId: 7, edgeId: 11));
            db.Edges.Add(new EdgeM(0, pointFirId: 7, pointSecId: 3, edgeId: 12));
            db.Edges.Add(new EdgeM(0, pointFirId: 7, pointSecId: 11, edgeId: 13));
            db.Edges.Add(new EdgeM(0, pointFirId: 11, pointSecId: 12, edgeId: 14));
            db.Edges.Add(new EdgeM(0, pointFirId: 12, pointSecId: 8, edgeId: 15));
            db.Edges.Add(new EdgeM(0, pointFirId: 11, pointSecId: 15, edgeId: 16));
            db.Edges.Add(new EdgeM(0, pointFirId: 15, pointSecId: 16, edgeId: 17));
            db.Edges.Add(new EdgeM(0, pointFirId: 16, pointSecId: 12, edgeId: 18));
            db.Edges.Add(new EdgeM(0, pointFirId: 16, pointSecId: 19, edgeId: 19));
            db.Edges.Add(new EdgeM(0, pointFirId: 19, pointSecId: 20, edgeId: 20));
            db.Edges.Add(new EdgeM(0, pointFirId: 20, pointSecId: 35, edgeId: 21));
            db.Edges.Add(new EdgeM(0, pointFirId: 35, pointSecId: 34, edgeId: 22));
            db.Edges.Add(new EdgeM(0, pointFirId: 34, pointSecId: 27, edgeId: 23));
            db.Edges.Add(new EdgeM(0, pointFirId: 27, pointSecId: 19, edgeId: 24));
            db.Edges.Add(new EdgeM(0, pointFirId: 34, pointSecId: 33, edgeId: 25));
            db.Edges.Add(new EdgeM(0, pointFirId: 33, pointSecId: 32, edgeId: 26));
            db.Edges.Add(new EdgeM(0, pointFirId: 32, pointSecId: 28, edgeId: 27));
            db.Edges.Add(new EdgeM(0, pointFirId: 28, pointSecId: 36, edgeId: 28));
            db.Edges.Add(new EdgeM(0, pointFirId: 37, pointSecId: 36, edgeId: 29));
            db.Edges.Add(new EdgeM(0, pointFirId: 37, pointSecId: 31, edgeId: 30));
            db.Edges.Add(new EdgeM(0, pointFirId: 31, pointSecId: 24, edgeId: 31));
            db.Edges.Add(new EdgeM(0, pointFirId: 24, pointSecId: 18, edgeId: 32));
            db.Edges.Add(new EdgeM(0, pointFirId: 18, pointSecId: 14, edgeId: 33));
            db.Edges.Add(new EdgeM(0, pointFirId: 14, pointSecId: 10, edgeId: 34));
            db.Edges.Add(new EdgeM(0, pointFirId: 9, pointSecId: 13, edgeId: 35));
            db.Edges.Add(new EdgeM(0, pointFirId: 13, pointSecId: 17, edgeId: 36));
            db.Edges.Add(new EdgeM(0, pointFirId: 17, pointSecId: 23, edgeId: 37));
            db.Edges.Add(new EdgeM(0, pointFirId: 23, pointSecId: 30, edgeId: 38));
            db.Edges.Add(new EdgeM(0, pointFirId: 13, pointSecId: 14, edgeId: 39));
            db.Edges.Add(new EdgeM(0, pointFirId: 17, pointSecId: 18, edgeId: 40));
            db.Edges.Add(new EdgeM(0, pointFirId: 23, pointSecId: 24, edgeId: 41));
            db.Edges.Add(new EdgeM(0, pointFirId: 31, pointSecId: 30, edgeId: 42));
            db.Edges.Add(new EdgeM(0, pointFirId: 30, pointSecId: 29, edgeId: 43));
            db.Edges.Add(new EdgeM(0, pointFirId: 29, pointSecId: 28, edgeId: 44));
            db.Edges.Add(new EdgeM(0, pointFirId: 22, pointSecId: 29, edgeId: 45));
            db.Edges.Add(new EdgeM(0, pointFirId: 21, pointSecId: 28, edgeId: 46));
            db.Edges.Add(new EdgeM(0, pointFirId: 25, pointSecId: 32, edgeId: 47));
            db.Edges.Add(new EdgeM(0, pointFirId: 26, pointSecId: 33, edgeId: 48));
            db.Edges.Add(new EdgeM(0, pointFirId: 27, pointSecId: 26, edgeId: 49));
            db.Edges.Add(new EdgeM(0, pointFirId: 26, pointSecId: 25, edgeId: 50));
            db.Edges.Add(new EdgeM(0, pointFirId: 25, pointSecId: 21, edgeId: 51));
            db.Edges.Add(new EdgeM(0, pointFirId: 21, pointSecId: 22, edgeId: 52));
            #endregion
            //db.SaveChanges();

            #region 3thFloor
            db.Edges.Add(new EdgeM(0, pointFirId: 38, pointSecId: 40, edgeId: 53));
            db.Edges.Add(new EdgeM(0, pointFirId: 38, pointSecId: 39, edgeId: 54));
            db.Edges.Add(new EdgeM(0, pointFirId: 39, pointSecId: 43, edgeId: 56));
            db.Edges.Add(new EdgeM(0, pointFirId: 40, pointSecId: 41, edgeId: 57));
            db.Edges.Add(new EdgeM(0, pointFirId: 41, pointSecId: 42, edgeId: 58));
            db.Edges.Add(new EdgeM(0, pointFirId: 42, pointSecId: 43, edgeId: 59));
            db.Edges.Add(new EdgeM(0, pointFirId: 43, pointSecId: 47, edgeId: 60));
            db.Edges.Add(new EdgeM(0, pointFirId: 47, pointSecId: 46, edgeId: 61));
            db.Edges.Add(new EdgeM(0, pointFirId: 46, pointSecId: 42, edgeId: 62));
            db.Edges.Add(new EdgeM(0, pointFirId: 41, pointSecId: 45, edgeId: 63));
            db.Edges.Add(new EdgeM(0, pointFirId: 45, pointSecId: 44, edgeId: 64));
            db.Edges.Add(new EdgeM(0, pointFirId: 44, pointSecId: 40, edgeId: 65));
            db.Edges.Add(new EdgeM(0, pointFirId: 44, pointSecId: 38, edgeId: 66));
            db.Edges.Add(new EdgeM(0, pointFirId: 38, pointSecId: 64, edgeId: 67));
            db.Edges.Add(new EdgeM(0, pointFirId: 64, pointSecId: 65, edgeId: 68));
            db.Edges.Add(new EdgeM(0, pointFirId: 65, pointSecId: 39, edgeId: 69));
            db.Edges.Add(new EdgeM(0, pointFirId: 48, pointSecId: 49, edgeId: 70));
            db.Edges.Add(new EdgeM(0, pointFirId: 50, pointSecId: 51, edgeId: 71));
            db.Edges.Add(new EdgeM(0, pointFirId: 52, pointSecId: 53, edgeId: 72));
            db.Edges.Add(new EdgeM(0, pointFirId: 54, pointSecId: 55, edgeId: 73));
            db.Edges.Add(new EdgeM(0, pointFirId: 60, pointSecId: 63, edgeId: 74));
            db.Edges.Add(new EdgeM(0, pointFirId: 53, pointSecId: 57, edgeId: 75));
            db.Edges.Add(new EdgeM(0, pointFirId: 58, pointSecId: 59, edgeId: 76));
            db.Edges.Add(new EdgeM(0, pointFirId: 41, pointSecId: 53, edgeId: 77));//
            db.Edges.Add(new EdgeM(0, pointFirId: 42, pointSecId: 58, edgeId: 78));
            db.Edges.Add(new EdgeM(0, pointFirId: 62, pointSecId: 58, edgeId: 79));
            db.Edges.Add(new EdgeM(0, pointFirId: 57, pointSecId: 61, edgeId: 80));

            #endregion
            db.SaveChanges();
        }

        public void LoadSampleWays()
        {
            #region 4thFloor
            // 4 этаж точки комнат
            db.Points.Add(new PointM(125, 250, pointId: 66, isWaypoint: true, floorId: 2, roomId: 1));
            db.Points.Add(new PointM(600, 120, pointId: 67, isWaypoint: true, floorId: 2, roomId: 2));
            db.Points.Add(new PointM(600, 400, pointId: 68, isWaypoint: true, floorId: 2, roomId: 3));
            db.Points.Add(new PointM(1000, 120, pointId: 69, isWaypoint: true, floorId: 2, roomId: 4));
            db.Points.Add(new PointM(1000, 400, pointId: 70, isWaypoint: true, floorId: 2, roomId: 5));
            db.Points.Add(new PointM(1500, -800, pointId: 71, isWaypoint: true, floorId: 2, roomId: 6));
            db.Points.Add(new PointM(1520, -500, pointId: 72, isWaypoint: true, floorId: 2, roomId: 7));
            db.Points.Add(new PointM(1520, -300, pointId: 73, isWaypoint: true, floorId: 2, roomId: 8));
            db.Points.Add(new PointM(1520, -100, pointId: 74, isWaypoint: true, floorId: 2, roomId: 9));
            db.Points.Add(new PointM(1800, 300, pointId: 75, isWaypoint: true, floorId: 2, roomId: 10));
            db.Points.Add(new PointM(1350, 400, pointId: 76, isWaypoint: true, floorId: 2, roomId: 11));
            // 4 этаж точки связи                         
            db.Points.Add(new PointM(600, 250, pointId: 77, isWaypoint: true, floorId: 2));
            db.Points.Add(new PointM(900, 250, pointId: 78, isWaypoint: true, floorId: 2));
            db.Points.Add(new PointM(1300, 250, pointId: 79, isWaypoint: true, floorId: 2));//////
            db.Points.Add(new PointM(1600, 250, pointId: 80, isWaypoint: true, floorId: 2));//////
            db.Points.Add(new PointM(1300, 120, pointId: 81, isWaypoint: true, floorId: 2));
            db.Points.Add(new PointM(1300, -300, pointId: 82, isWaypoint: true, floorId: 2));
            db.Points.Add(new PointM(1300, -500, pointId: 83, isWaypoint: true, floorId: 2));
            db.Points.Add(new PointM(1520, 100, pointId: 84, isWaypoint: true, floorId: 2));

            //db.SaveChanges();
            #endregion

            #region 3thFloor
            // 3 этаж токи комнат
            db.Points.Add(new PointM(125, 250, pointId: 85, isWaypoint: true, floorId: 1, roomId: 12));
            db.Points.Add(new PointM(600, 120, pointId: 86, isWaypoint: true, floorId: 1, roomId: 13));
            db.Points.Add(new PointM(600, 400, pointId: 87, isWaypoint: true, floorId: 1, roomId: 14));
            db.Points.Add(new PointM(1000, 120, pointId: 88, isWaypoint: true, floorId: 1, roomId: 15));
            // 3 этаж точки связи             
            db.Points.Add(new PointM(600, 250, pointId: 89, isWaypoint: true, floorId: 1));
            db.Points.Add(new PointM(900, 250, pointId: 90, isWaypoint: true, floorId: 1));
            db.Points.Add(new PointM(1300, 250, pointId: 91, isWaypoint: true, floorId: 1));
            db.Points.Add(new PointM(1600, 250, pointId: 92, isWaypoint: true, floorId: 1));
            db.Points.Add(new PointM(1300, 120, pointId: 93, isWaypoint: true, floorId: 1));
            db.Points.Add(new PointM(1520, 100, pointId: 94, isWaypoint: true, floorId: 1));
            //db.SaveChanges();
            #endregion

            #region 4thFloorEdge
            // 4 этаж связи комнат
            db.Edges.Add(new EdgeM(10, pointFirId: 66, pointSecId: 77, edgeId: 81));
            db.Edges.Add(new EdgeM(10, pointFirId: 77, pointSecId: 67, edgeId: 82));
            db.Edges.Add(new EdgeM(10, pointFirId: 77, pointSecId: 68, edgeId: 83));
            db.Edges.Add(new EdgeM(10, pointFirId: 77, pointSecId: 78, edgeId: 84));
            db.Edges.Add(new EdgeM(10, pointFirId: 78, pointSecId: 69, edgeId: 85));
            db.Edges.Add(new EdgeM(10, pointFirId: 78, pointSecId: 70, edgeId: 86));
            db.Edges.Add(new EdgeM(10, pointFirId: 78, pointSecId: 79, edgeId: 87));
            db.Edges.Add(new EdgeM(10, pointFirId: 79, pointSecId: 76, edgeId: 88));
            db.Edges.Add(new EdgeM(10, pointFirId: 79, pointSecId: 80, edgeId: 89));
            db.Edges.Add(new EdgeM(10, pointFirId: 80, pointSecId: 75, edgeId: 90));
            db.Edges.Add(new EdgeM(10, pointFirId: 79, pointSecId: 81, edgeId: 91));
            db.Edges.Add(new EdgeM(10, pointFirId: 81, pointSecId: 84, edgeId: 92));
            db.Edges.Add(new EdgeM(10, pointFirId: 81, pointSecId: 82, edgeId: 93));
            db.Edges.Add(new EdgeM(10, pointFirId: 82, pointSecId: 73, edgeId: 94));
            db.Edges.Add(new EdgeM(10, pointFirId: 82, pointSecId: 74, edgeId: 95));
            db.Edges.Add(new EdgeM(10, pointFirId: 82, pointSecId: 83, edgeId: 96));
            db.Edges.Add(new EdgeM(10, pointFirId: 83, pointSecId: 72, edgeId: 97));
            db.Edges.Add(new EdgeM(10, pointFirId: 83, pointSecId: 71, edgeId: 98));

            //db.SaveChanges();
            #endregion

            #region 3thFloorEdge
            // 3 этаж связи комнат 
            db.Edges.Add(new EdgeM(10, pointFirId: 85, pointSecId: 89, edgeId: 99));
            db.Edges.Add(new EdgeM(10, pointFirId: 89, pointSecId: 86, edgeId: 100));
            db.Edges.Add(new EdgeM(10, pointFirId: 89, pointSecId: 87, edgeId: 101));
            db.Edges.Add(new EdgeM(10, pointFirId: 89, pointSecId: 90, edgeId: 102));
            db.Edges.Add(new EdgeM(10, pointFirId: 90, pointSecId: 88, edgeId: 103));
            db.Edges.Add(new EdgeM(10, pointFirId: 90, pointSecId: 91, edgeId: 104));
            db.Edges.Add(new EdgeM(10, pointFirId: 91, pointSecId: 93, edgeId: 105));
            db.Edges.Add(new EdgeM(10, pointFirId: 93, pointSecId: 94, edgeId: 106));
            db.Edges.Add(new EdgeM(10, pointFirId: 91, pointSecId: 92, edgeId: 107));

            db.Edges.Add(new EdgeM(10, pointFirId: 94, pointSecId: 84, edgeId: 108)); // elevator end
            db.Edges.Add(new EdgeM(10, pointFirId: 57, pointSecId: 56, edgeId: 109));

            //db.SaveChanges();
            #endregion

            // cвязь этажей
            db.SaveChanges();
        }

        public void LoadSampleNotes()
        {
            db.Notes.Add(new Note("I'm open note", "KGU", "213", true, roomid: 1));
            db.Notes.Add(new Note("I'm open okey", "KGU", "213", true, roomid: 1));
            db.Notes.Add(new Note("I'm open yesi", "KGU", "200", true, roomid: 2));
            db.Notes.Add(new Note("I'm open noby", "KGU", "200", true, roomid: 2));
            db.Notes.Add(new Note("I'm open puko", "KGU", "202", true, roomid: 3));
            db.SaveChanges();
        }
        protected override void Seed(newdip.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            LoadSampleBuildings();
            LoadSampleFloors();
            LoadSamplePoints();
            LoadSampleEdges();
            LoadSampleRooms();
            LoadSampleWays();
            LoadSampleWorkers();
            LoadSampleNotes();
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
