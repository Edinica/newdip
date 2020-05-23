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

        public class P 
        {
            public string firstx { get; set; }
            public string firsty { get; set; }
            public string level { get; set; }
            public string id { get; set; }
        }
        public class Segment
        {
            public string firstx { get; set; }
            public string firsty { get; set; }
            public string secondx { get; set; }
            public string secondy { get; set; }
            public string level { get; set; }
            public string id { get; set; }
        }

        public class Rectangle
        {
            public string firstx { get; set; }
            public string firsty { get; set; }
            public string secondx { get; set; }
            public string secondy { get; set; }
            public string thirdx { get; set; }
            public string thirdy { get; set; }
            public string fourthx { get; set; }
            public string fourthy { get; set; }
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
        public Room Room([FromBody] P point)
        {
            //var pointslist = db.Points.ToList();
            //var res = Similar(Convert.ToInt32(po.firstx), Convert.ToInt32(po.firsty));
            // Building building = db.Buildings.Where(x => x.Id == k).Include(x => x.Floors).FirstOrDefault();//получаем нужно здание

            int id = Convert.ToInt32(point.id);
            int level = Etazh(point.level);
            EdgeM edge = new EdgeM();
            List<PointM> points = db.Floors.Where(x => x.Level == level && x.BuildingId == id).Include(x => x.Points).FirstOrDefault().Points.ToList();//этаж просмотр
            ///создание и добавление первой точки
            Room result = new Room();
            foreach(var element in points)
            { for (int i = -2; i < 3; i++)
                for (int j = -2; j < 3; j++)
                {
                        if (element.IsWaypoint  && element.X == Convert.ToInt32(point.firstx)+i && element.Y  == Convert.ToInt32(point.firsty)+j) //если нашли такую точку на этаже
                        {
                            var room = db.Rooms.Include(x => x.Points).ToList();
                            foreach (var vroom in room) //ищем комнату
                            {
                                for (int k = 0; k < vroom.Points.Count; k++) 
                                {
                                    if (vroom.Points[k].IsWaypoint && 
                                        vroom.Points[k].X == element.X && 
                                        vroom.Points[k].Y == element.Y) 
                                    {
                                    result = new Room(vroom.Name,vroom.Description,vroom.Timetable,vroom.Phone,vroom.Site);
                                    }
                                }
                            }
                            
                        }
                } 
            }
                    //Point point = new Point();
                    //point.X = Convert.ToInt32(point.firstx);
                    //point.Y = Convert.ToInt32(point.firsty);
                    //point.IsWaypoint = false;
                    //point.FloorId = floor1.FloorId;
                    //db.Points.Add(point);
                    //db.SaveChanges();
                    //edge.PointFromId = db.Points.ToList().Last().Id;//Id первой вершины
                    /////создание и добавление первой точки
                    //Point point2 = new Point();
                    //point2.X = Convert.ToInt32(point.secondx);
                    //point2.Y = Convert.ToInt32(point.secondy);
                    //point2.IsWaypoint = false;
                    //point2.FloorId = floor1.FloorId;
                    //db.Points.Add(point2);
                    //db.SaveChanges();
                    /////сохранение ребра
                    //edge.PointToId = db.Points.ToList().Last().Id;//Id первой вершины
                    //edge.Weight = Math.Sqrt(Math.Pow(Convert.ToDouble(point.secondx) - Convert.ToDouble(point.firstx), 2) +
                    //    Math.Pow(Convert.ToDouble(point.secondy) - Convert.ToDouble(point.firsty), 2)) / 75.9;
                    //db.Edges.Add(edge);
                    //db.SaveChanges();
                    //var list = db.Edges.ToList();
                    return result;
           
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void Line([FromBody] Segment po)
        {
            //var pointslist = db.Points.ToList();
            //var res = Similar(Convert.ToInt32(po.firstx), Convert.ToInt32(po.firsty));
            // Building building = db.Buildings.Where(x => x.Id == k).Include(x => x.Floors).FirstOrDefault();//получаем нужно здание
            if (po.secondx != null)
            {
                int id = Convert.ToInt32(po.id);
                int level = Etazh(po.level);
                EdgeM edge = new EdgeM();
                Floor floor1 = db.Floors.Where(x => x.Level == level && x.BuildingId == id).Include(x => x.Points).FirstOrDefault();//этаж просмотр
                ///создание и добавление первой точки
                PointM point = new PointM();
                point.X = Convert.ToInt32(po.firstx);
                point.Y = Convert.ToInt32(po.firsty);
                point.IsWaypoint = false;
                point.FloorId = floor1.FloorId;
                db.Points.Add(point);
                db.SaveChanges();
                edge.PointFromId = db.Points.ToList().Last().Id;//Id первой вершины
                ///создание и добавление первой точки
                PointM point2 = new PointM();
                point2.X = Convert.ToInt32(po.secondx);
                point2.Y = Convert.ToInt32(po.secondy);
                point2.IsWaypoint = false;
                point2.FloorId = floor1.FloorId;
                db.Points.Add(point2);
                db.SaveChanges();
                ///сохранение ребра
                edge.PointToId = db.Points.ToList().Last().Id;//Id первой вершины
                edge.Weight = Math.Sqrt(Math.Pow(Convert.ToDouble(po.secondx) - Convert.ToDouble(po.firstx), 2) +
                    Math.Pow(Convert.ToDouble(po.secondy) - Convert.ToDouble(po.firsty), 2)) / 75.9;
                db.Edges.Add(edge);
                db.SaveChanges();
                var list = db.Edges.ToList();
            }
            //Point newp = new Point();
            //newp.X = Convert.ToInt32(po.firstx);
            //newp.Y = Convert.ToInt32(po.firsty);
            //newp.PointId = pointslist.Count() + 1;
            ////var obj = po;
            //db.Points.Add(newp);
            //db.SaveChanges();
        }
        public void Operation(string x1, string y1,string x2,string y2, Floor floor) 
        {
            PointM point = new PointM();
            EdgeM edge = new EdgeM();
            bool isexist = false;
            int X1 = Convert.ToInt32(x1);
            int Y1 = Convert.ToInt32(y1);
            int X2 = Convert.ToInt32(x2);
            int Y2 = Convert.ToInt32(y2);
            if (db.Points.FirstOrDefault(x => x.X == X1&& x.Y == Y1&&x.FloorId==floor.FloorId) != null) isexist = true ;
            ///первая вершина
            if (!isexist)
            {
                point.X = X1;
                point.Y = Y1;
                point.IsWaypoint = false;
                point.FloorId = floor.FloorId;
                db.Points.Add(point);
                db.SaveChanges();
                edge.PointFromId = db.Points.ToList().Last().Id;
            }
            else 
            {
                point = db.Points.FirstOrDefault(x => x.X == X1 && x.Y == Y1 && x.FloorId == floor.FloorId);
                edge.PointFromId = point.Id;
            }
            isexist = false;
            if (db.Points.FirstOrDefault(x => x.X == X2 && x.Y == Y2 && x.FloorId == floor.FloorId) != null) isexist = true;
            ///2 вершина
            PointM point2 = new PointM();
            if (!isexist)
            {
                
                point2.X = Convert.ToInt32(x2);
                point2.Y = Convert.ToInt32(y2);
                point2.IsWaypoint = false;
                point2.FloorId = floor.FloorId;
                db.Points.Add(point2);
                db.SaveChanges();
                ///сохранение ребра
                edge.PointToId = db.Points.ToList().Last().Id;//Id первой вершины
            }
            else 
            {
                point2 = db.Points.FirstOrDefault(x => x.X == X2 && x.Y == Y2 && x.FloorId == floor.FloorId);
                edge.PointToId = point2.Id;
            }
            edge.Weight = Math.Sqrt(Math.Pow(Convert.ToDouble(x2) - Convert.ToDouble(x1), 2) +
                Math.Pow(Convert.ToDouble(y2) - Convert.ToDouble(y1), 2)) / 75.9;
            db.Edges.Add(edge);
            db.SaveChanges();

        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void AddRectangle([FromBody] Rectangle po)
        {
            //var pointslist = db.Points.ToList();
            //var res = Similar(Convert.ToInt32(po.firstx), Convert.ToInt32(po.firsty));
            // Building building = db.Buildings.Where(x => x.Id == k).Include(x => x.Floors).FirstOrDefault();//получаем нужно здание
            int id = Convert.ToInt32(po.id);
            int level = Etazh(po.level);
            //Edge edge = new Edge();
            Floor floor1 = db.Floors.Where(x => x.Level == level && x.BuildingId == id).Include(x => x.Points).Include(x=>x.Rooms).FirstOrDefault();//этаж просмотр
            Operation(po.firstx, po.firsty, po.secondx, po.secondy, floor1);
            Operation(po.secondx, po.secondy, po.thirdx, po.thirdy, floor1);
            Operation(po.thirdx, po.thirdy, po.fourthx, po.fourthy, floor1);
            Operation(po.fourthx, po.fourthy, po.firstx, po.firsty, floor1);
            Room room = new Room();

            int px1 = Convert.ToInt32(po.firstx);
            int py1 = Convert.ToInt32(po.firsty);
            int px2 = Convert.ToInt32(po.secondx);
            int py2 = Convert.ToInt32(po.secondy);
            int px3 = Convert.ToInt32(po.thirdx);
            int py3 = Convert.ToInt32(po.thirdy);
            int px4 = Convert.ToInt32(po.fourthx);
            int py4 = Convert.ToInt32(po.fourthy);
            db.Rooms.Add(room);
            db.SaveChanges();
            PointM point = db.Points.FirstOrDefault(x => x.X == px1 && x.Y == py1 && x.FloorId == floor1.FloorId);
            PointM point2 = db.Points.FirstOrDefault(x => x.X == px2 && x.Y == py2 && x.FloorId == floor1.FloorId);
            PointM point3 = db.Points.FirstOrDefault(x => x.X == px3 && x.Y == py3 && x.FloorId == floor1.FloorId);
            PointM point4 = db.Points.FirstOrDefault(x => x.X == px4 && x.Y == py4 && x.FloorId == floor1.FloorId);
            point.RoomId = db.Rooms.ToList().Last().RoomId;
            point2.RoomId = db.Rooms.ToList().Last().RoomId;
            point3.RoomId = db.Rooms.ToList().Last().RoomId;
            point4.RoomId = db.Rooms.ToList().Last().RoomId;
            //room.Points.Add(point);
            //room.Points.Add(point2);
            //room.Points.Add(point3);
            //room.Points.Add(point4);
            PointM middle = new PointM();
            middle.IsWaypoint = true;
            middle.X = (Convert.ToInt32(po.firstx) + Convert.ToInt32(po.thirdx)) / 2;
            middle.Y = (Convert.ToInt32(po.firsty) + Convert.ToInt32(po.thirdy)) / 2;
            middle.FloorId = floor1.FloorId;
            middle.RoomId = db.Rooms.ToList().Last().RoomId;
            db.Points.Add(middle);
            db.SaveChanges();


            ///создание и добавление первой точки
            //Point point = new Point();
            //point.X = Convert.ToInt32(po.firstx);
            //point.Y = Convert.ToInt32(po.firsty);
            //point.IsWaypoint = false;
            //point.FloorId = floor1.FloorId;
            //db.Points.Add(point);
            //db.SaveChanges();
            //edge.PointFromId = db.Points.ToList().Last().Id;//Id первой вершины
            /////создание и добавление первой точки
            //Point point2 = new Point();
            //point2.X = Convert.ToInt32(po.secondx);
            //point2.Y = Convert.ToInt32(po.secondy);
            //point2.IsWaypoint = false;
            //point2.FloorId = floor1.FloorId;
            //db.Points.Add(point2);
            //db.SaveChanges();
            /////сохранение ребра
            //edge.PointToId = db.Points.ToList().Last().Id;//Id первой вершины
            //edge.Weight = Math.Sqrt(Math.Pow(Convert.ToDouble(po.secondx) - Convert.ToDouble(po.firstx), 2) +
            //    Math.Pow(Convert.ToDouble(po.secondy) - Convert.ToDouble(po.firsty), 2)) / 10;
            //db.Edges.Add(edge);
            //db.SaveChanges();
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
