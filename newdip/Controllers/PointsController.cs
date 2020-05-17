using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

using newdip.Models;

namespace newdip.Controllers
{
    public class PointsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public class Segment
        {
            public string firstx { get; set; }
            public string firsty { get; set; }
            public string secondx { get; set; }
            public string secondy { get; set; }
            public string level { get; set; }
            public string id { get; set; }
        }
        public PointM Similar(int x, int y)
        {
            var points = db.Points.ToList();
            foreach (var element in points)
            {
                for (int i = -2; i < 2; i++)
                    for (int j = -2; j < 2; j++)
                    {
                        if (element.X + i == x && element.Y + j == y) return element;
                    }
            }

            return null;
        }
        public int Etazh(string income)
        {
            return Convert.ToInt32(income.Substring(0, income.Length - 5));
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void Line([FromBody] Segment po)
        {
            //var pointslist = db.Points.ToList();
            //var res = Similar(Convert.ToInt32(po.firstx), Convert.ToInt32(po.firsty));
           // Building building = db.Buildings.Where(x => x.Id == k).Include(x => x.Floors).FirstOrDefault();//получаем нужно здание
            int id = Convert.ToInt32(po.id);
            int level = Etazh(po.level);
            EdgeM edge = new EdgeM();
            Floor floor1 = db.Floors.Where(x => x.Level == level&&x.BuildingId==id).Include(x => x.Points).FirstOrDefault();//этаж просмотр
            ///создание и добавление первой точки
            PointM point = new PointM();
            point.X = Convert.ToInt32(po.firstx);
            point.Y = Convert.ToInt32(po.firsty);
            point.IsWaypoint = false;
            point.FloorId = floor1.Id;
            db.Points.Add(point);
            db.SaveChanges();
            edge.PointFromId = db.Points.ToList().Last().Id;//Id первой вершины
            ///создание и добавление первой точки
            PointM point2 = new PointM();
            point2.X = Convert.ToInt32(po.secondx);
            point2.Y = Convert.ToInt32(po.secondy);
            point2.IsWaypoint = false;
            point2.FloorId = floor1.Id;
            db.Points.Add(point2);
            db.SaveChanges();
            ///сохранение ребра
            edge.PointToId = db.Points.ToList().Last().Id;//Id первой вершины
            edge.Weight = Math.Sqrt(Math.Pow(Convert.ToDouble(po.secondx) - Convert.ToDouble(po.firstx), 2) +
                Math.Pow(Convert.ToDouble(po.secondy) - Convert.ToDouble(po.firsty), 2))/10;
            db.Edges.Add(edge);
            db.SaveChanges();
            var list = db.Edges.ToList();

            //Point newp = new Point();
            //newp.X = Convert.ToInt32(po.firstx);
            //newp.Y = Convert.ToInt32(po.firsty);
            //newp.PointId = pointslist.Count() + 1;
            ////var obj = po;
            //db.Points.Add(newp);
            //db.SaveChanges();
        }
    }
}
