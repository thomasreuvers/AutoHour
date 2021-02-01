using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHour.Pages
{
    public partial class TrajectPlannerPage
    {
        // Main Page Url
        private const string MainPageUrl = "https://nijmegen.trajectplanner.nl/";

        // Xpath
        public const string BpvMenuTab = "/html/body/table/tbody/tr[1]/td/div[3]/div/ul/li[4]/a";

        // Xpath
        public const string BpvAnchor = "/html/body/table/tbody/tr[1]/td/div[3]/div/ul/li[4]/ul/li[2]/a";

        // Xpath
        public const string FirstInternship = "/html/body/table/tbody/tr[2]/td[3]/div[2]/div[2]/table/tbody/tr[1]/td[3]";

        // Xpath
        public const string HoursTab = "/html/body/table/tbody/tr/td[2]/div[7]/div[1]/img";

        // Xpath
        public const string NewHoursBtn = "/html/body/table/tbody/tr/td[2]/div[7]/div[1]/div/img[3]";

        // Id
        public const string DateInput = "boek_datum";

        // Id
        public const string StartingTimeInput = "boek_starttijd";

        // Id
        public const string EndingTimeInput = "boek_eindtijd";

        // Id
        public const string DescriptionInput = "boek_omschrijving";

        // Xpath
        public const string SaveBtn = "/html/body/div[22]/div[11]/div/button";
    }
}
