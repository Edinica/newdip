﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newdip.Models
{
	public class Building 
	{ 
		[Key]
		public int BuildingId { get; set; }

		public string Name { get; set; }
		public string Addrees { get; set; }
		public string Description { get; set; }
		public string Site { get; set; }
		public string TimeTable { get; set; }
		public List<Floor>Floors { get; set; }
        public Building()
        {
            Floors = new List<Floor>();
        }

        public Building(string name, string description = null,
                        string addrees = null, string timetable = null,
                        string site = null, int buildingid = 0) : this()
        {
            BuildingId = buildingid;

            Name = name;
            Description = description;

            Addrees = addrees;
            TimeTable = timetable;
            Site = site;
        }

    }
}